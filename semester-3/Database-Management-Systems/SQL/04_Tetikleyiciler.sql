-- TETİKLEYİCİ 1: Randevu Tamamlandığında Otomatik Fatura Oluştur

CREATE OR REPLACE FUNCTION trg_randevu_tamamlandi_fatura_olustur()
RETURNS TRIGGER AS $$
DECLARE
    v_fatura_var INTEGER;
    v_toplam_tutar DECIMAL(10,2);
BEGIN
    IF NEW.Durum = 'Tamamlandi' AND OLD.Durum != 'Tamamlandi' THEN
        SELECT COUNT(*) INTO v_fatura_var
        FROM Fatura WHERE RandevuID = NEW.RandevuID;
        
        IF v_fatura_var = 0 THEN
            SELECT COALESCE(SUM(tt.BirimFiyat), 0) INTO v_toplam_tutar
            FROM RandevuTedavi rt
            INNER JOIN TedaviTuru tt ON rt.TedaviTuruID = tt.TedaviTuruID
            WHERE rt.RandevuID = NEW.RandevuID;
            
            INSERT INTO Fatura (RandevuID, FaturaTarihi, ToplamTutar, OdemeDurumu)
            VALUES (NEW.RandevuID, CURRENT_DATE, v_toplam_tutar, 'Odenmedi');
            
            RAISE NOTICE 'Randevu ID: % için fatura otomatik oluşturuldu. Tutar: %', 
                         NEW.RandevuID, v_toplam_tutar;
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_fatura_olustur ON Randevu;
CREATE TRIGGER trg_fatura_olustur
    AFTER UPDATE ON Randevu
    FOR EACH ROW
    EXECUTE FUNCTION trg_randevu_tamamlandi_fatura_olustur();

	-- TETİKLEYİCİ 2: Stok Çıkışında Miktar Güncelleme

	CREATE OR REPLACE FUNCTION trg_stok_hareket_miktar_guncelle()
RETURNS TRIGGER AS $$
DECLARE
    v_mevcut_miktar INTEGER;
BEGIN
    IF NEW.HareketTipi = 'Giris' THEN
        UPDATE Stok SET Miktar = Miktar + NEW.Miktar WHERE StokID = NEW.StokID;
        RAISE NOTICE 'Stok ID: % için % adet giriş yapıldı', NEW.StokID, NEW.Miktar;
        
    ELSIF NEW.HareketTipi = 'Cikis' THEN
        SELECT Miktar INTO v_mevcut_miktar FROM Stok WHERE StokID = NEW.StokID;
        
        IF v_mevcut_miktar < NEW.Miktar THEN
            RAISE EXCEPTION 'Yetersiz stok! Mevcut: %, İstenen: %', 
                           v_mevcut_miktar, NEW.Miktar;
        END IF;
        
        UPDATE Stok SET Miktar = Miktar - NEW.Miktar WHERE StokID = NEW.StokID;
        RAISE NOTICE 'Stok ID: % için % adet çıkış yapıldı', NEW.StokID, NEW.Miktar;
        
    ELSIF NEW.HareketTipi = 'Sayim' THEN
        UPDATE Stok SET Miktar = NEW.Miktar WHERE StokID = NEW.StokID;
        RAISE NOTICE 'Stok ID: % için sayım yapıldı. Yeni miktar: %', NEW.StokID, NEW.Miktar;
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_stok_guncelle ON StokHareket;
CREATE TRIGGER trg_stok_guncelle
    AFTER INSERT ON StokHareket
    FOR EACH ROW
    EXECUTE FUNCTION trg_stok_hareket_miktar_guncelle();

	-- TETİKLEYİCİ 3: Randevu Çakışma Kontrolü

	CREATE OR REPLACE FUNCTION trg_randevu_cakisma_kontrolu()
RETURNS TRIGGER AS $$
DECLARE
    v_cakisma_var INTEGER;
BEGIN
    IF NEW.Durum = 'Iptal' THEN
        RETURN NEW;
    END IF;
    
    SELECT COUNT(*) INTO v_cakisma_var
    FROM Randevu
    WHERE DisHekimiID = NEW.DisHekimiID
      AND RandevuTarihi = NEW.RandevuTarihi
      AND RandevuSaati = NEW.RandevuSaati
      AND Durum != 'Iptal'
      AND RandevuID != COALESCE(NEW.RandevuID, 0);
    
    IF v_cakisma_var > 0 THEN
        RAISE EXCEPTION 'Randevu çakışması! Doktor ID: % için % tarihinde % saatinde zaten randevu var.',
                       NEW.DisHekimiID, NEW.RandevuTarihi, NEW.RandevuSaati;
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_randevu_kontrol ON Randevu;
CREATE TRIGGER trg_randevu_kontrol
    BEFORE INSERT OR UPDATE ON Randevu
    FOR EACH ROW
    EXECUTE FUNCTION trg_randevu_cakisma_kontrolu();

	-- TETİKLEYİCİ 4: Fatura Toplam Tutar Otomatik Güncelleme

	CREATE OR REPLACE FUNCTION trg_fatura_tutar_guncelle()
RETURNS TRIGGER AS $$
DECLARE
    v_randevu_id INTEGER;
    v_yeni_toplam DECIMAL(10,2);
    v_fatura_var INTEGER;
BEGIN
    IF TG_OP = 'DELETE' THEN
        v_randevu_id := OLD.RandevuID;
    ELSE
        v_randevu_id := NEW.RandevuID;
    END IF;
    
    SELECT COUNT(*) INTO v_fatura_var FROM Fatura WHERE RandevuID = v_randevu_id;
    
    IF v_fatura_var > 0 THEN
        SELECT COALESCE(SUM(tt.BirimFiyat), 0) INTO v_yeni_toplam
        FROM RandevuTedavi rt
        INNER JOIN TedaviTuru tt ON rt.TedaviTuruID = tt.TedaviTuruID
        WHERE rt.RandevuID = v_randevu_id;
        
        UPDATE Fatura SET ToplamTutar = v_yeni_toplam WHERE RandevuID = v_randevu_id;
        
        RAISE NOTICE 'Randevu ID: % için fatura tutarı güncellendi. Yeni tutar: %',
                     v_randevu_id, v_yeni_toplam;
    END IF;
    
    IF TG_OP = 'DELETE' THEN
        RETURN OLD;
    ELSE
        RETURN NEW;
    END IF;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS trg_fatura_tutar_guncelle_insert ON RandevuTedavi;
CREATE TRIGGER trg_fatura_tutar_guncelle_insert
    AFTER INSERT OR DELETE ON RandevuTedavi
    FOR EACH ROW
    EXECUTE FUNCTION trg_fatura_tutar_guncelle();

COMMIT;

-- Tetikleyici listesi
SELECT trigger_name, event_object_table
FROM information_schema.triggers
WHERE trigger_schema = 'public'
ORDER BY event_object_table;