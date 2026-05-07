Şehir Simülasyonu - Programlama Dillerinin Prensipleri Ödev 1

Bu proje, Sakarya Üniversitesi Bilgisayar Mühendisliği Bölümü Programlama Dillerinin Prensipleri dersi kapsamında geliştirilmiştir. Proje, hiyerarşik bir yapıda (Şehir-İlçe-Mahalle-Kişi) bir toplumsal birimin nüfus değişimlerini, büyüme dinamiklerini ve şehir bölünmesi mekanizmalarını simüle eden bir Java konsol uygulamasıdır.

🚀 Özellikler
Dinamik Veri Hiyerarşisi: Kullanıcıdan alınan 2 basamaklı sayılarla Şehir, İlçe ve Mahalle yapıları otomatik olarak kurulur.
Akıllı Veri Doğrulama: 99 Kuralı: Mahalle sayısı 0 olan veya ilçe sayısına tam bölünmeyen girdiler, hiyerarşiye uygun hale getirilir. 
77 Kuralı: Onlar basamağı sabit kalarak tam bölünen birler basamağı taranır.

Nüfus Artış Algoritması: Her turun sonunda şehir nüfusları, basamak değerlerinin toplamına göre artış gösterir.

Şehir Bölünmesi: Nüfusu 1000 eşiğini aşan şehirler, ilçelerini bölerek yeni şehirler oluşturur.

Hiyerarşik Sorgulama: Simülasyon sonunda matris üzerinden satır/sütun seçilerek şehir sakinlerinin detaylı dökümü (ID, İsim, Yaş) görüntülenebilir.


🛠️ Kullanılan Teknolojiler
Dil: Java (JRE JavaSE-25)
Kütüphaneler: Java Faker: Gerçekçi isim ve adres verileri üretimi için.
SnakeYAML: YAML veri işleme için.
Apache Commons Lang 3: Metin ve nesne işlemleri için.


📁 Proje Yapısı
Assignment-1/
├── src/            # Java kaynak kodları (.java)
├── lib/            # Gerekli harici kütüphaneler (.jar)
├── dist/           # Çalıştırılabilir dosya (odev.jar)
├── report/          # Ödev teknik raporu
├── .project        # Eclipse proje dosyası
└── .classpath      # Eclipse bağımlılık yolu dosyası


⚙️ Kurulum ve Çalıştırma

1. Kütüphanelerin Tanıtılması (Önemli)
Proje içindeki .classpath dosyası kütüphaneleri yerel bir yolda arayabilir. Projeyi IDE'nizde (Eclipse/IntelliJ) açtığınızda hata almamak için:
1) lib klasöründeki 3 adet .jar dosyasını seçin.
2) Sağ tıklayarak Build Path > Add to Build Path (Eclipse için) seçeneğini uygulayın.

2. Konsoldan Çalıştırma
Projeyi derlenmiş haliyle doğrudan çalıştırmak için terminale şu komutu yazın:
java -jar dist/odev.jar

📝 Notlar
-Simülasyonun ilerleyen turlarındaki (Tur 3+) yavaşlama, üstel nüfus artışı nedeniyle milyonlarca nesnenin dinamik olarak üretilmesinden kaynaklanmaktadır.

-Kişilerin başlangıç yaşları 0-50 arasında rastgele atanmaktadır.

Geliştiren: BERHA ÇELİK
Ders: Programlama Dillerinin Prensipleri
Dönem: 2025-2026 Bahar