-- 1. DİŞ REFERANS VERİLERİ (32 Diş)
INSERT INTO Dis (DisNo, DisAdi) VALUES
-- Üst Çene Sağ
(11, 'Üst Sağ 1. Kesici'),
(12, 'Üst Sağ 2. Kesici'),
(13, 'Üst Sağ Köpek Dişi'),
(14, 'Üst Sağ 1. Azı'),
(15, 'Üst Sağ 2. Azı'),
(16, 'Üst Sağ 1. Büyük Azı'),
(17, 'Üst Sağ 2. Büyük Azı'),
(18, 'Üst Sağ 3. Büyük Azı (Yirmilik)'),
-- Üst Çene Sol
(21, 'Üst Sol 1. Kesici'),
(22, 'Üst Sol 2. Kesici'),
(23, 'Üst Sol Köpek Dişi'),
(24, 'Üst Sol 1. Azı'),
(25, 'Üst Sol 2. Azı'),
(26, 'Üst Sol 1. Büyük Azı'),
(27, 'Üst Sol 2. Büyük Azı'),
(28, 'Üst Sol 3. Büyük Azı (Yirmilik)'),
-- Alt Çene Sol
(31, 'Alt Sol 1. Kesici'),
(32, 'Alt Sol 2. Kesici'),
(33, 'Alt Sol Köpek Dişi'),
(34, 'Alt Sol 1. Azı'),
(35, 'Alt Sol 2. Azı'),
(36, 'Alt Sol 1. Büyük Azı'),
(37, 'Alt Sol 2. Büyük Azı'),
(38, 'Alt Sol 3. Büyük Azı (Yirmilik)'),
-- Alt Çene Sağ
(41, 'Alt Sağ 1. Kesici'),
(42, 'Alt Sağ 2. Kesici'),
(43, 'Alt Sağ Köpek Dişi'),
(44, 'Alt Sağ 1. Azı'),
(45, 'Alt Sağ 2. Azı'),
(46, 'Alt Sağ 1. Büyük Azı'),
(47, 'Alt Sağ 2. Büyük Azı'),
(48, 'Alt Sağ 3. Büyük Azı (Yirmilik)');

-- 2. PERSONEL VERİLERİ (Kalıtım Hiyerarşisi)
-- Ana Personel Tablosu
INSERT INTO Personel (Ad, Soyad, TCKN, Telefon, Email, IseBaslamaTarihi) VALUES
('Ahmet', 'Yılmaz', '12345678901', '0532-111-1111', 'ahmet.yilmaz@disklinigi.com', '2020-01-15'),
('Ayşe', 'Kaya', '12345678902', '0532-222-2222', 'ayse.kaya@disklinigi.com', '2021-03-10'),
('Mehmet', 'Demir', '12345678903', '0532-333-3333', 'mehmet.demir@disklinigi.com', '2019-06-20'),
('Fatma', 'Şahin', '12345678904', '0532-444-4444', 'fatma.sahin@disklinigi.com', '2022-01-05'),
('Zeynep', 'Çelik', '12345678905', '0532-555-5555', 'zeynep.celik@disklinigi.com', '2021-09-12'),
('Ali', 'Öztürk', '12345678906', '0532-666-6666', 'ali.ozturk@disklinigi.com', '2022-08-01'),
('Elif', 'Aydın', '12345678907', '0532-777-7777', 'elif.aydin@disklinigi.com', '2023-02-14');

-- Diş Hekimleri
INSERT INTO DisHekimi (PersonelID, UzmanlikAlani) VALUES
(1, 'Endodonti'),
(2, 'Ortodonti'),
(3, 'Periodontoloji');

-- Asistanlar
INSERT INTO Asistan (PersonelID) VALUES
(4),
(5);

-- Resepsiyonistler
INSERT INTO Resepsiyonist (PersonelID, VardiyaTuru) VALUES
(6, 'Sabah'),
(7, 'Oglen');

-- 3. ÇALIŞMA SAATLERİ
-- Dr. Ahmet Yılmaz'ın çalışma saatleri
INSERT INTO CalismaSaati (PersonelID, Gun, BaslangicSaati, BitisSaati) VALUES
(1, 'Pazartesi', '09:00', '17:00'),
(1, 'Sali', '09:00', '17:00'),
(1, 'Carsamba', '09:00', '17:00'),
(1, 'Persembe', '09:00', '17:00'),
(1, 'Cuma', '09:00', '14:00');

-- Dr. Ayşe Kaya'nın çalışma saatleri
INSERT INTO CalismaSaati (PersonelID, Gun, BaslangicSaati, BitisSaati) VALUES
(2, 'Pazartesi', '13:00', '19:00'),
(2, 'Sali', '13:00', '19:00'),
(2, 'Carsamba', '13:00', '19:00'),
(2, 'Persembe', '13:00', '19:00'),
(2, 'Cuma', '13:00', '17:00');

-- 4. TEDAVİ TÜRLERİ
INSERT INTO TedaviTuru (TedaviKodu, TedaviAdi, BirimFiyat) VALUES
('T001', 'Dolgu (Kompozit)', 800.00),
('T002', 'Dolgu (Amalgam)', 600.00),
('T003', 'Kanal Tedavisi', 2500.00),
('T004', 'Diş Çekimi', 400.00),
('T005', 'Yirmilik Diş Çekimi', 800.00),
('T006', 'Diş Taşı Temizliği', 500.00),
('T007', 'Beyazlatma', 1500.00),
('T008', 'Kron (Porselen)', 3500.00),
('T009', 'Köprü (3 Üye)', 9000.00),
('T010', 'İmplant', 8000.00),
('T011', 'Tel Tedavisi (Aylık)', 1200.00),
('T012', 'Protez (Tam)', 5000.00),
('T013', 'Protez (Kısmi)', 3000.00),
('T014', 'Apse Drenajı', 600.00),
('T015', 'Florür Uygulaması', 300.00);

-- 5. İLAÇLAR
INSERT INTO Ilac (IlacAdi) VALUES
('Augmentin 1000 mg'),
('Amoksisilin 500 mg'),
('Arveles 25 mg'),
('Majezik 275 mg'),
('Voltaren 50 mg'),
('Nurofen 400 mg'),
('Apranax 550 mg'),
('Klorheksidin Gargara'),
('Metronidazol 500 mg'),
('Difflam Sprey'),
('Corsodyl Gargara'),
('Paradontax Diş Macunu'),
('Sensodyne Diş Macunu'),
('Tantum Verde Gargara'),
('Vermidon Suspensiyon');

-- 6. STOK MALZEMELERİ
INSERT INTO Stok (MalzemeAdi, StokKodu, Miktar, MinimumStok, Birim) VALUES
('Eldiven (Latex)', 'ELD001', 500, 100, 'Adet'),
('Maske (3 Katlı)', 'MSK001', 1000, 200, 'Adet'),
('Önlük (Steril)', 'ONL001', 50, 10, 'Adet'),
('Enjektör (2.5cc)', 'ENJ001', 200, 50, 'Adet'),
('İğne (21G)', 'IGN001', 300, 75, 'Adet'),
('Anestezi (Artikain)', 'ANE001', 150, 30, 'Ampul'),
('Pamuk Tampon', 'PAM001', 500, 100, 'Paket'),
('Gaz Bez (Steril)', 'GAZ001', 300, 75, 'Paket'),
('Diş Fırçası', 'FIR001', 200, 50, 'Adet'),
('Diş İpi', 'IPI001', 150, 40, 'Adet'),
('Kompozit Dolgu (A2)', 'KMP001', 25, 5, 'Şırınga'),
('Kompozit Dolgu (A3)', 'KMP002', 20, 5, 'Şırınga'),
('Etching Jel', 'ETJ001', 30, 10, 'Şişe'),
('Bonding Ajan', 'BON001', 25, 8, 'Şişe'),
('Kök Kanal Dolgu Materyali', 'KKD001', 15, 5, 'Paket');

-- 7. STOK HAREKETLERİ (İlk Giriş)
INSERT INTO StokHareket (StokID, HareketTipi, Miktar, Tarih) VALUES
(1, 'Giris', 500, '2024-01-01 09:00:00'),
(2, 'Giris', 1000, '2024-01-01 09:15:00'),
(3, 'Giris', 50, '2024-01-01 09:30:00'),
(4, 'Giris', 200, '2024-01-02 10:00:00'),
(5, 'Giris', 300, '2024-01-02 10:15:00'),
(6, 'Giris', 150, '2024-01-02 10:30:00'),
(7, 'Giris', 500, '2024-01-03 09:00:00'),
(8, 'Giris', 300, '2024-01-03 09:15:00');

-- 8. HASTALAR
INSERT INTO Hasta (Ad, Soyad, TCKN, DogumTarihi, Telefon, Email, Adres, KayitTarihi) VALUES
('Mustafa', 'Yıldız', '11111111111', '1990-05-15', '0533-111-1111', 'mustafa.yildiz@email.com', 'Ankara, Çankaya', '2024-01-05 10:00:00'),
('Emine', 'Arslan', '22222222222', '1985-08-22', '0533-222-2222', 'emine.arslan@email.com', 'Ankara, Keçiören', '2024-01-06 11:30:00'),
('Hasan', 'Koç', '33333333333', '1995-03-10', '0533-333-3333', 'hasan.koc@email.com', 'Ankara, Mamak', '2024-01-10 09:15:00'),
('Ayşegül', 'Şen', '44444444444', '2000-12-05', '0533-444-4444', 'aysegul.sen@email.com', 'Ankara, Yenimahalle', '2024-01-12 14:00:00'),
('Murat', 'Acar', '55555555555', '1988-07-18', '0533-555-5555', 'murat.acar@email.com', 'Ankara, Etimesgut', '2024-01-15 10:45:00'),
('Selin', 'Güler', '66666666666', '1992-11-30', '0533-666-6666', 'selin.guler@email.com', 'Ankara, Sincan', '2024-01-18 16:20:00'),
('Can', 'Yurt', '77777777777', '1998-04-25', '0533-777-7777', 'can.yurt@email.com', 'Ankara, Gölbaşı', '2024-01-20 09:00:00'),
('Deniz', 'Kara', '88888888888', '1993-09-14', '0533-888-8888', 'deniz.kara@email.com', 'Ankara, Pursaklar', '2024-01-22 11:00:00'),
('Ece', 'Özkan', '99999999999', '1997-06-08', '0533-999-9999', 'ece.ozkan@email.com', 'Ankara, Altındağ', '2024-01-25 13:30:00'),
('Burak', 'Tekin', '10101010101', '1991-02-20', '0534-111-1111', 'burak.tekin@email.com', 'Ankara, Polatlı', '2024-01-28 15:00:00');

-- 9. RANDEVULAR
INSERT INTO Randevu (HastaID, DisHekimiID, RandevuTarihi, RandevuSaati, Durum) VALUES
-- Geçmiş randevular (Tamamlandı)
(1, 1, '2024-01-10', '10:00', 'Tamamlandi'),
(2, 2, '2024-01-15', '14:00', 'Tamamlandi'),
(3, 1, '2024-01-20', '11:00', 'Tamamlandi'),
(4, 3, '2024-01-25', '15:00', 'Tamamlandi'),
(5, 2, '2024-02-01', '16:00', 'Tamamlandi'),
-- Yaklaşan randevular (Beklemede)
(6, 1, '2024-12-15', '09:30', 'Beklemede'),
(7, 2, '2024-12-16', '13:30', 'Beklemede'),
(8, 3, '2024-12-17', '10:00', 'Beklemede'),
(9, 1, '2024-12-18', '14:00', 'Beklemede'),
(10, 2, '2024-12-19', '15:30', 'Beklemede'),
-- İptal edilmiş randevu
(5, 1, '2024-02-10', '10:00', 'Iptal');

-- 10. DİŞ KAYITLARI (Bazı hastalar için örnek)
-- Mustafa Yıldız'ın diş kayıtları
INSERT INTO DisKayit (HastaID, DisNo, DisDurumu, SonKontrolTarihi) VALUES
(1, 11, 'Saglikli', '2024-01-10'),
(1, 12, 'Saglikli', '2024-01-10'),
(1, 16, 'Dolgulu', '2024-01-10'),
(1, 26, 'Curuk', '2024-01-10'),
(1, 36, 'Saglikli', '2024-01-10');

-- Emine Arslan'ın diş kayıtları
INSERT INTO DisKayit (HastaID, DisNo, DisDurumu, SonKontrolTarihi) VALUES
(2, 21, 'Saglikli', '2024-01-15'),
(2, 22, 'Dolgulu', '2024-01-15'),
(2, 38, 'Cekilmis', '2024-01-15');

-- 11. YAPILAN TEDAVİLER
-- Randevu 1: Mustafa Yıldız - Dolgu
INSERT INTO RandevuTedavi (RandevuID, TedaviTuruID, DisNo, UygulayanDisHekimiID, BaslangicSaati, BitisSaati) VALUES
(1, 1, 26, 1, '10:00', '10:45');

-- Randevu 2: Emine Arslan - Diş Taşı Temizliği
INSERT INTO RandevuTedavi (RandevuID, TedaviTuruID, DisNo, UygulayanDisHekimiID, BaslangicSaati, BitisSaati) VALUES
(2, 6, NULL, 2, '14:00', '14:30');

-- Randevu 3: Hasan Koç - Diş Çekimi
INSERT INTO RandevuTedavi (RandevuID, TedaviTuruID, DisNo, UygulayanDisHekimiID, BaslangicSaati, BitisSaati) VALUES
(3, 4, 38, 1, '11:00', '11:30');

-- Randevu 4: Ayşegül Şen - Kanal Tedavisi
INSERT INTO RandevuTedavi (RandevuID, TedaviTuruID, DisNo, UygulayanDisHekimiID, BaslangicSaati, BitisSaati) VALUES
(4, 3, 16, 3, '15:00', '16:30');

-- Randevu 5: Murat Acar - Dolgu + Diş Taşı Temizliği
INSERT INTO RandevuTedavi (RandevuID, TedaviTuruID, DisNo, UygulayanDisHekimiID, BaslangicSaati, BitisSaati) VALUES
(5, 1, 12, 2, '16:00', '16:40'),
(5, 6, NULL, 2, '16:45', '17:15');

-- 12. FATURALAR (Tamamlanan randevular için)
INSERT INTO Fatura (RandevuID, FaturaTarihi, ToplamTutar, OdemeDurumu) VALUES
(1, '2024-01-10', 800.00, 'Odendi'),
(2, '2024-01-15', 500.00, 'Odendi'),
(3, '2024-01-20', 400.00, 'Odendi'),
(4, '2024-01-25', 2500.00, 'KismiOdendi'),
(5, '2024-02-01', 1300.00, 'Odenmedi');


-- 13. ÖDEMELER
INSERT INTO Odeme (FaturaID, OdemeTarihi, OdenenTutar) VALUES
-- Fatura 1: Tam ödendi
(1, '2024-01-10 10:50:00', 800.00),
-- Fatura 2: Tam ödendi
(2, '2024-01-15 14:35:00', 500.00),
-- Fatura 3: Tam ödendi
(3, '2024-01-20 11:35:00', 400.00),
-- Fatura 4: Kısmi ödeme (2 taksit)
(4, '2024-01-25 16:35:00', 1000.00),
(4, '2024-02-10 14:00:00', 1000.00);
-- Fatura 5: Henüz ödeme yok

-- 14. REÇETELER
-- Randevu 3 için reçete (Diş çekimi sonrası)
INSERT INTO Recete (RandevuID, DisHekimiID, ReceteTarihi) VALUES
(3, 1, '2024-01-20');

-- Randevu 4 için reçete (Kanal tedavisi sonrası)
INSERT INTO Recete (RandevuID, DisHekimiID, ReceteTarihi) VALUES
(4, 3, '2024-01-25');

-- 15. REÇETE İLAÇLARI
-- Reçete 1 ilaçları (Diş çekimi sonrası)
INSERT INTO ReceteIlac (ReceteID, IlacID) VALUES
(1, 1),  -- Augmentin 1000 mg
(1, 3),  -- Arveles 25 mg
(1, 8);  -- Klorheksidin Gargara

-- Reçete 2 ilaçları (Kanal tedavisi sonrası)
INSERT INTO ReceteIlac (ReceteID, IlacID) VALUES
(2, 2),  -- Amoksisilin 500 mg
(2, 4),  -- Majezik 275 mg
(2, 14); -- Tantum Verde Gargara

COMMIT;

SELECT 
    'Personel' as Tablo, COUNT(*) as Kayit_Sayisi FROM Personel
UNION ALL SELECT 'DisHekimi', COUNT(*) FROM DisHekimi
UNION ALL SELECT 'Asistan', COUNT(*) FROM Asistan
UNION ALL SELECT 'Resepsiyonist', COUNT(*) FROM Resepsiyonist
UNION ALL SELECT 'Hasta', COUNT(*) FROM Hasta
UNION ALL SELECT 'TedaviTuru', COUNT(*) FROM TedaviTuru
UNION ALL SELECT 'Ilac', COUNT(*) FROM Ilac
UNION ALL SELECT 'Stok', COUNT(*) FROM Stok
UNION ALL SELECT 'Dis', COUNT(*) FROM Dis
UNION ALL SELECT 'Randevu', COUNT(*) FROM Randevu
UNION ALL SELECT 'DisKayit', COUNT(*) FROM DisKayit
UNION ALL SELECT 'Fatura', COUNT(*) FROM Fatura
UNION ALL SELECT 'Odeme', COUNT(*) FROM Odeme
UNION ALL SELECT 'RandevuTedavi', COUNT(*) FROM RandevuTedavi
UNION ALL SELECT 'ReceteIlac', COUNT(*) FROM ReceteIlac;

