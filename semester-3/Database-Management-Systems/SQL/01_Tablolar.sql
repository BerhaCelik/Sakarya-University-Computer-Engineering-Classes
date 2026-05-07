DROP TABLE IF EXISTS ReceteIlac CASCADE;
DROP TABLE IF EXISTS RandevuTedavi CASCADE;
DROP TABLE IF EXISTS CalismaSaati CASCADE;
DROP TABLE IF EXISTS StokHareket CASCADE;
DROP TABLE IF EXISTS Odeme CASCADE;
DROP TABLE IF EXISTS Recete CASCADE;
DROP TABLE IF EXISTS Fatura CASCADE;
DROP TABLE IF EXISTS DisKayit CASCADE;
DROP TABLE IF EXISTS Randevu CASCADE;
DROP TABLE IF EXISTS Dis CASCADE;
DROP TABLE IF EXISTS Ilac CASCADE;
DROP TABLE IF EXISTS TedaviTuru CASCADE;
DROP TABLE IF EXISTS Stok CASCADE;
DROP TABLE IF EXISTS Asistan CASCADE;
DROP TABLE IF EXISTS Resepsiyonist CASCADE;
DROP TABLE IF EXISTS DisHekimi CASCADE;
DROP TABLE IF EXISTS Personel CASCADE;
DROP TABLE IF EXISTS Hasta CASCADE;

-- Parent Tablo: Personel
CREATE TABLE Personel (
    PersonelID SERIAL PRIMARY KEY,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    TCKN CHAR(11) UNIQUE NOT NULL,
    Telefon VARCHAR(15) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    IseBaslamaTarihi DATE NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT chk_tckn_uzunluk CHECK (LENGTH(TCKN) = 11),
    CONSTRAINT chk_email_format CHECK (Email LIKE '%@%')
);

-- Child Tablo 1: DisHekimi
CREATE TABLE DisHekimi (
    PersonelID INTEGER PRIMARY KEY,
    UzmanlikAlani VARCHAR(50),
    CONSTRAINT fk_dishekimi_personel FOREIGN KEY (PersonelID) 
        REFERENCES Personel(PersonelID) ON DELETE CASCADE
);

-- Child Tablo 2: Asistan (eski adı Hemsire)
CREATE TABLE Asistan (
    PersonelID INTEGER PRIMARY KEY,
    CONSTRAINT fk_asistan_personel FOREIGN KEY (PersonelID) 
        REFERENCES Personel(PersonelID) ON DELETE CASCADE
);

-- Child Tablo 3: Resepsiyonist
CREATE TABLE Resepsiyonist (
    PersonelID INTEGER PRIMARY KEY,
    VardiyaTuru VARCHAR(20) CHECK (VardiyaTuru IN ('Sabah', 'Oglen', 'Gece')),
    CONSTRAINT fk_resepsiyonist_personel FOREIGN KEY (PersonelID) 
        REFERENCES Personel(PersonelID) ON DELETE CASCADE
);

-- Hasta Tablosu
CREATE TABLE Hasta (
    HastaID SERIAL PRIMARY KEY,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    TCKN CHAR(11) UNIQUE NOT NULL,
    DogumTarihi DATE NOT NULL,
    Telefon VARCHAR(15) NOT NULL,
    Email VARCHAR(100),
    Adres TEXT,
    KayitTarihi TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT chk_hasta_tckn CHECK (LENGTH(TCKN) = 11),
    CONSTRAINT chk_dogum_tarihi CHECK (DogumTarihi < CURRENT_DATE)
);

-- Tedavi Türü Tablosu
CREATE TABLE TedaviTuru (
    TedaviTuruID SERIAL PRIMARY KEY,
    TedaviKodu VARCHAR(20) UNIQUE NOT NULL,
    TedaviAdi VARCHAR(100) NOT NULL,
    BirimFiyat DECIMAL(10,2) NOT NULL,
    CONSTRAINT chk_birim_fiyat CHECK (BirimFiyat > 0)
);

-- İlaç Tablosu
CREATE TABLE Ilac (
    IlacID SERIAL PRIMARY KEY,
    IlacAdi VARCHAR(100) UNIQUE NOT NULL
);

-- Stok Tablosu
CREATE TABLE Stok (
    StokID SERIAL PRIMARY KEY,
    MalzemeAdi VARCHAR(100) NOT NULL,
    StokKodu VARCHAR(50) UNIQUE NOT NULL,
    Miktar INTEGER NOT NULL DEFAULT 0,
    MinimumStok INTEGER NOT NULL DEFAULT 0,
    Birim VARCHAR(20) NOT NULL,
    CONSTRAINT chk_miktar CHECK (Miktar >= 0),
    CONSTRAINT chk_minimum_stok CHECK (MinimumStok >= 0)
);

-- Diş Referans Tablosu (32 diş için sabit veri)
CREATE TABLE Dis (
    DisNo INTEGER PRIMARY KEY,
    DisAdi VARCHAR(50) NOT NULL,
    CHECK (
        (DisNo BETWEEN 11 AND 18) OR
        (DisNo BETWEEN 21 AND 28) OR
        (DisNo BETWEEN 31 AND 38) OR
        (DisNo BETWEEN 41 AND 48)
    )
);


-- Randevu Tablosu
CREATE TABLE Randevu (
    RandevuID SERIAL PRIMARY KEY,
    HastaID INTEGER NOT NULL,
    DisHekimiID INTEGER NOT NULL,
    RandevuTarihi DATE NOT NULL,
    RandevuSaati TIME NOT NULL,
    Durum VARCHAR(20) NOT NULL DEFAULT 'Beklemede',
    CONSTRAINT fk_randevu_hasta FOREIGN KEY (HastaID) 
        REFERENCES Hasta(HastaID) ON DELETE CASCADE,
    CONSTRAINT fk_randevu_dishekimi FOREIGN KEY (DisHekimiID) 
        REFERENCES DisHekimi(PersonelID) ON DELETE RESTRICT,
    CONSTRAINT chk_durum CHECK (Durum IN ('Beklemede', 'Tamamlandi', 'Iptal')),
    CONSTRAINT uq_randevu_slot UNIQUE (DisHekimiID, RandevuTarihi, RandevuSaati)
);

-- Diş Kayıt Tablosu
CREATE TABLE DisKayit (
    DisKayitID SERIAL PRIMARY KEY,
    HastaID INTEGER NOT NULL,
    DisNo INTEGER NOT NULL,
    DisDurumu VARCHAR(30) NOT NULL DEFAULT 'Saglikli',
    SonKontrolTarihi DATE,
    CONSTRAINT fk_diskayit_hasta FOREIGN KEY (HastaID) 
        REFERENCES Hasta(HastaID) ON DELETE CASCADE,
    CONSTRAINT fk_diskayit_dis FOREIGN KEY (DisNo) 
        REFERENCES Dis(DisNo) ON DELETE RESTRICT,
    CONSTRAINT chk_dis_durumu CHECK (DisDurumu IN 
        ('Saglikli', 'Curuk', 'Dolgulu', 'Cekilmis', 'Eksik', 'Tedavi_Gereken')),
    CONSTRAINT uq_hasta_dis UNIQUE (HastaID, DisNo)
);

-- Fatura Tablosu
CREATE TABLE Fatura (
    FaturaID SERIAL PRIMARY KEY,
    RandevuID INTEGER UNIQUE NOT NULL,
    FaturaTarihi DATE NOT NULL DEFAULT CURRENT_DATE,
    ToplamTutar DECIMAL(10,2) NOT NULL DEFAULT 0,
    OdemeDurumu VARCHAR(20) NOT NULL DEFAULT 'Odenmedi',
    CONSTRAINT fk_fatura_randevu FOREIGN KEY (RandevuID) 
        REFERENCES Randevu(RandevuID) ON DELETE CASCADE,
    CONSTRAINT chk_toplam_tutar CHECK (ToplamTutar >= 0),
    CONSTRAINT chk_odeme_durumu CHECK (OdemeDurumu IN 
        ('Odenmedi', 'KismiOdendi', 'Odendi'))
);

-- Reçete Tablosu
CREATE TABLE Recete (
    ReceteID SERIAL PRIMARY KEY,
    RandevuID INTEGER NOT NULL,
    DisHekimiID INTEGER NOT NULL,
    ReceteTarihi DATE NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT fk_recete_randevu FOREIGN KEY (RandevuID) 
        REFERENCES Randevu(RandevuID) ON DELETE CASCADE,
    CONSTRAINT fk_recete_dishekimi FOREIGN KEY (DisHekimiID) 
        REFERENCES DisHekimi(PersonelID) ON DELETE RESTRICT
);

-- Ödeme Tablosu
CREATE TABLE Odeme (
    OdemeID SERIAL PRIMARY KEY,
    FaturaID INTEGER NOT NULL,
    OdemeTarihi TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    OdenenTutar DECIMAL(10,2) NOT NULL,
    CONSTRAINT fk_odeme_fatura FOREIGN KEY (FaturaID) 
        REFERENCES Fatura(FaturaID) ON DELETE CASCADE,
    CONSTRAINT chk_odenen_tutar CHECK (OdenenTutar > 0)
);

-- Stok Hareket Tablosu
CREATE TABLE StokHareket (
    HareketID SERIAL PRIMARY KEY,
    StokID INTEGER NOT NULL,
    HareketTipi VARCHAR(20) NOT NULL,
    Miktar INTEGER NOT NULL,
    Tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_stokhareket_stok FOREIGN KEY (StokID) 
        REFERENCES Stok(StokID) ON DELETE CASCADE,
    CONSTRAINT chk_hareket_tipi CHECK (HareketTipi IN ('Giris', 'Cikis', 'Sayim')),
    CONSTRAINT chk_hareket_miktar CHECK (Miktar > 0)
);

-- Çalışma Saati Tablosu
CREATE TABLE CalismaSaati (
    CalismaSaatiID SERIAL PRIMARY KEY,
    PersonelID INTEGER NOT NULL,
    Gun VARCHAR(15) NOT NULL,
    BaslangicSaati TIME NOT NULL,
    BitisSaati TIME NOT NULL,
    CONSTRAINT fk_calismasaati_personel FOREIGN KEY (PersonelID) 
        REFERENCES Personel(PersonelID) ON DELETE CASCADE,
    CONSTRAINT chk_gun CHECK (Gun IN 
        ('Pazartesi', 'Sali', 'Carsamba', 'Persembe', 'Cuma', 'Cumartesi', 'Pazar')),
    CONSTRAINT chk_saat CHECK (BitisSaati > BaslangicSaati),
    CONSTRAINT uq_personel_gun UNIQUE (PersonelID, Gun)
);

-- Randevu-Tedavi Ara Tablosu
CREATE TABLE RandevuTedavi (
    RandevuTedaviID SERIAL PRIMARY KEY,
    RandevuID INTEGER NOT NULL,
    TedaviTuruID INTEGER NOT NULL,
    DisNo INTEGER,
    UygulayanDisHekimiID INTEGER NOT NULL,
    BaslangicSaati TIME,
    BitisSaati TIME,
    CONSTRAINT fk_randevutedavi_randevu FOREIGN KEY (RandevuID) 
        REFERENCES Randevu(RandevuID) ON DELETE CASCADE,
    CONSTRAINT fk_randevutedavi_tedavituru FOREIGN KEY (TedaviTuruID) 
        REFERENCES TedaviTuru(TedaviTuruID) ON DELETE RESTRICT,
    CONSTRAINT fk_randevutedavi_dis FOREIGN KEY (DisNo) 
        REFERENCES Dis(DisNo) ON DELETE RESTRICT,
    CONSTRAINT fk_randevutedavi_dishekimi FOREIGN KEY (UygulayanDisHekimiID) 
        REFERENCES DisHekimi(PersonelID) ON DELETE RESTRICT,
    CONSTRAINT chk_tedavi_saat CHECK (BitisSaati IS NULL OR BaslangicSaati IS NULL 
        OR BitisSaati > BaslangicSaati)
);

-- Reçete-İlaç Ara Tablosu
CREATE TABLE ReceteIlac (
    ReceteIlacID SERIAL PRIMARY KEY,
    ReceteID INTEGER NOT NULL,
    IlacID INTEGER NOT NULL,
    CONSTRAINT fk_receteilac_recete FOREIGN KEY (ReceteID) 
        REFERENCES Recete(ReceteID) ON DELETE CASCADE,
    CONSTRAINT fk_receteilac_ilac FOREIGN KEY (IlacID) 
        REFERENCES Ilac(IlacID) ON DELETE RESTRICT,
    CONSTRAINT uq_recete_ilac UNIQUE (ReceteID, IlacID)
);

CREATE INDEX idx_hasta_tckn ON Hasta(TCKN);
CREATE INDEX idx_hasta_adsoyad ON Hasta(Ad, Soyad);
CREATE INDEX idx_personel_tckn ON Personel(TCKN);
CREATE INDEX idx_randevu_tarih ON Randevu(RandevuTarihi, RandevuSaati);
CREATE INDEX idx_randevu_hasta ON Randevu(HastaID);
CREATE INDEX idx_randevu_doktor ON Randevu(DisHekimiID);
CREATE INDEX idx_randevu_durum ON Randevu(Durum);
CREATE INDEX idx_fatura_durum ON Fatura(OdemeDurumu);
CREATE INDEX idx_diskayit_hasta ON DisKayit(HastaID);
CREATE INDEX idx_stok_miktar ON Stok(Miktar) WHERE Miktar < MinimumStok;

COMMENT ON TABLE Personel IS 'Ana personel tablosu - Kalıtım parent';
COMMENT ON TABLE DisHekimi IS 'Diş hekimi tablosu - Personel child';
COMMENT ON TABLE Asistan IS 'Asistan tablosu - Personel child';
COMMENT ON TABLE Resepsiyonist IS 'Resepsiyonist tablosu - Personel child';
COMMENT ON TABLE Hasta IS 'Hasta bilgileri - Güçlü varlık';
COMMENT ON TABLE TedaviTuru IS 'Tedavi türleri ve fiyatları - Güçlü varlık';
COMMENT ON TABLE Ilac IS 'İlaç listesi - Güçlü varlık';
COMMENT ON TABLE Stok IS 'Malzeme stok takibi - Güçlü varlık';
COMMENT ON TABLE Dis IS 'Diş referans tablosu (32 diş) - Güçlü varlık';
COMMENT ON TABLE Randevu IS 'Randevu kayıtları - Zayıf varlık';
COMMENT ON TABLE DisKayit IS 'Hasta diş durumu kayıtları - Zayıf varlık';
COMMENT ON TABLE Fatura IS 'Fatura kayıtları - Zayıf varlık';
COMMENT ON TABLE Recete IS 'Reçete kayıtları - Zayıf varlık';
COMMENT ON TABLE Odeme IS 'Ödeme kayıtları - Zayıf varlık';
COMMENT ON TABLE StokHareket IS 'Stok giriş/çıkış hareketleri - Zayıf varlık';
COMMENT ON TABLE CalismaSaati IS 'Personel çalışma saatleri - Zayıf varlık';
COMMENT ON TABLE RandevuTedavi IS 'Randevu-Tedavi ara tablosu - N:M ilişki';
COMMENT ON TABLE ReceteIlac IS 'Reçete-İlaç ara tablosu - N:M ilişki';

COMMIT;

SELECT table_name, table_type 
FROM information_schema.tables 
WHERE table_schema = 'public' 
ORDER BY table_name;

SELECT COUNT(*) as "Toplam Tablo Sayisi" 
FROM information_schema.tables 
WHERE table_schema = 'public' 
AND table_type = 'BASE TABLE';