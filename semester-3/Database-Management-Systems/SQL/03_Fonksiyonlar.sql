-- FONKSİYON 1: Hasta Randevu Geçmişi Getir
-- Kullanım: SELECT * FROM fn_hasta_randevu_gecmisi(1);

DROP FUNCTION fn_hasta_randevu_gecmisi(integer);
CREATE FUNCTION fn_hasta_randevu_gecmisi(p_hasta_id INTEGER)
RETURNS TABLE (
    RandevuID INTEGER,
    RandevuTarihi DATE,
    RandevuSaati TIME,
    DoktorAdSoyad TEXT,
    TedaviAdi VARCHAR(100),
    DisAdi VARCHAR(50),
    ToplamTutar DECIMAL(10,2),
    OdemeDurumu VARCHAR(20),
    Durum VARCHAR(20)
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        r.RandevuID,
        r.RandevuTarihi,
        r.RandevuSaati,
        CONCAT(p.Ad, ' ', p.Soyad) as DoktorAdSoyad,
        tt.TedaviAdi,
        d.DisAdi,
        COALESCE(f.ToplamTutar, 0) as ToplamTutar,
        COALESCE(f.OdemeDurumu, 'Yok') as OdemeDurumu,
        r.Durum
    FROM 
        Randevu r
        INNER JOIN DisHekimi dh ON r.DisHekimiID = dh.PersonelID
        INNER JOIN Personel p ON dh.PersonelID = p.PersonelID
        LEFT JOIN RandevuTedavi rt ON r.RandevuID = rt.RandevuID
        LEFT JOIN TedaviTuru tt ON rt.TedaviTuruID = tt.TedaviTuruID
        LEFT JOIN Dis d ON rt.DisNo = d.DisNo
        LEFT JOIN Fatura f ON r.RandevuID = f.RandevuID
    WHERE 
        r.HastaID = p_hasta_id
    ORDER BY 
        r.RandevuTarihi DESC, r.RandevuSaati DESC;
END;
$$ LANGUAGE plpgsql;

COMMENT ON FUNCTION fn_hasta_randevu_gecmisi IS 
'Belirtilen hastanın tüm randevu geçmişini, tedavileri ve fatura bilgilerini getirir';

-- FONKSİYON 2: Belirli Tarih Aralığındaki Gelir Hesaplama
-- Kullanım: SELECT * FROM fn_tarih_araliginda_gelir('2024-01-01', '2024-12-31');
CREATE OR REPLACE FUNCTION fn_tarih_araliginda_gelir(
    p_baslangic_tarihi DATE,
    p_bitis_tarihi DATE
)
RETURNS TABLE (
    ToplamFaturaTutari DECIMAL(12,2),
    ToplamOdenenTutar DECIMAL(12,2),
    BekleyenTutar DECIMAL(12,2),
    FaturaSayisi INTEGER,
    OdenenFaturaSayisi INTEGER
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        COALESCE(SUM(f.ToplamTutar), 0)::DECIMAL(12,2) as ToplamFaturaTutari,
        COALESCE(SUM(o.OdenenTutar), 0)::DECIMAL(12,2) as ToplamOdenenTutar,
        COALESCE(SUM(f.ToplamTutar) - SUM(o.OdenenTutar), 0)::DECIMAL(12,2) as BekleyenTutar,
        COUNT(DISTINCT f.FaturaID)::INTEGER as FaturaSayisi,
        COUNT(DISTINCT CASE WHEN f.OdemeDurumu = 'Odendi' THEN f.FaturaID END)::INTEGER as OdenenFaturaSayisi
    FROM 
        Fatura f
        LEFT JOIN Odeme o ON f.FaturaID = o.FaturaID
    WHERE 
        f.FaturaTarihi BETWEEN p_baslangic_tarihi AND p_bitis_tarihi;
END;
$$ LANGUAGE plpgsql;

COMMENT ON FUNCTION fn_tarih_araliginda_gelir IS 
'Belirtilen tarih aralığındaki toplam gelir, ödeme ve bekleyen tutar bilgilerini hesaplar';

-- FONKSİYON 3: Doktor Randevu Uygunluk Kontrolü
-- Kullanım: SELECT fn_doktor_uygunluk_kontrol(1, '2024-12-20', '10:00');
-- Sonuç: true (uygun) / false (uygun değil)
CREATE OR REPLACE FUNCTION fn_doktor_uygunluk_kontrol(
    p_dishekimi_id INTEGER,
    p_randevu_tarihi DATE,
    p_randevu_saati TIME
)
RETURNS BOOLEAN AS $$
DECLARE
    v_mevcutmu INTEGER;
    v_gun VARCHAR(15);
    v_calisiyormu INTEGER;
BEGIN
    -- Tarihin gününü bul (Türkçe)
    v_gun := CASE EXTRACT(DOW FROM p_randevu_tarihi)
        WHEN 0 THEN 'Pazar'
        WHEN 1 THEN 'Pazartesi'
        WHEN 2 THEN 'Sali'
        WHEN 3 THEN 'Carsamba'
        WHEN 4 THEN 'Persembe'
        WHEN 5 THEN 'Cuma'
        WHEN 6 THEN 'Cumartesi'
    END;
    
    -- 1. Doktor o gün çalışıyor mu?
    SELECT COUNT(*) INTO v_calisiyormu
    FROM CalismaSaati
    WHERE PersonelID = p_dishekimi_id
      AND Gun = v_gun
      AND p_randevu_saati BETWEEN BaslangicSaati AND BitisSaati;
    
    IF v_calisiyormu = 0 THEN
        RETURN FALSE; -- Doktor o saatte çalışmıyor
    END IF;
    
    -- 2. O saatte başka randevusu var mı?
    SELECT COUNT(*) INTO v_mevcutmu
    FROM Randevu
    WHERE DisHekimiID = p_dishekimi_id
      AND RandevuTarihi = p_randevu_tarihi
      AND RandevuSaati = p_randevu_saati
      AND Durum != 'Iptal';
    
    IF v_mevcutmu > 0 THEN
        RETURN FALSE; -- Aynı saatte randevu var
    END IF;
    
    RETURN TRUE; -- Uygun
END;
$$ LANGUAGE plpgsql;

COMMENT ON FUNCTION fn_doktor_uygunluk_kontrol IS 
'Doktorun belirtilen tarih ve saatte randevu verebilir durumda olup olmadığını kontrol eder';

-- FONKSİYON 4: Kritik Stok Seviyesindeki Malzemeler
-- Kullanım: SELECT * FROM fn_kritik_stok_listesi();
CREATE OR REPLACE FUNCTION fn_kritik_stok_listesi()
RETURNS TABLE (
    StokID INTEGER,
    MalzemeAdi VARCHAR(100),
    StokKodu VARCHAR(50),
    MevcutMiktar INTEGER,
    MinimumStok INTEGER,
    EksikMiktar INTEGER,
    Durum VARCHAR(20),
    Birim VARCHAR(20)
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        s.StokID,
        s.MalzemeAdi,
        s.StokKodu,
        s.Miktar as MevcutMiktar,
        s.MinimumStok,
        CASE 
            WHEN s.Miktar < s.MinimumStok THEN s.MinimumStok - s.Miktar
            ELSE 0
        END as EksikMiktar,
        CASE 
            WHEN s.Miktar = 0 THEN 'Tukendi'
            WHEN s.Miktar < s.MinimumStok THEN 'Kritik'
            WHEN s.Miktar <= (s.MinimumStok * 1.2) THEN 'Uyari'
            ELSE 'Normal'
        END::VARCHAR(20) as Durum,
        s.Birim
    FROM 
        Stok s
    WHERE 
        s.Miktar <= (s.MinimumStok * 1.2) -- %20 tolerans
    ORDER BY 
        CASE 
            WHEN s.Miktar = 0 THEN 1
            WHEN s.Miktar < s.MinimumStok THEN 2
            ELSE 3
        END,
        s.Miktar ASC;
END;
$$ LANGUAGE plpgsql;

COMMENT ON FUNCTION fn_kritik_stok_listesi IS 
'Minimum stok seviyesinin altına düşen veya kritik seviyedeki malzemeleri listeler';

COMMIT;
