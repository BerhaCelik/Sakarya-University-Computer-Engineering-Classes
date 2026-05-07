/**
*
* @author Berha Çelik B241210099
* @since 17/04/2026
* <p>
* * Simülasyonun mantıksal motorudur. Kurulum, tur yönetimi, nüfus artışı
* ve şehir bölünmesi gibi karmaşık algoritmaları yönetir.
* </p>
*/
package odev1;

import com.github.javafaker.Faker;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class Oyun {
    private List<Sehir> sehirler;
    private Faker faker;
    private Random random;

    public Oyun() {
        this.sehirler = new ArrayList<>();
        this.faker = new Faker();
        this.random = new Random();
    }

 // Kullanıcıdan alınan sayıları işleyerek ilk dünyayı kuran metotdur.
    public void kurulum(String girisSatiri) {
        String[] sayilar = girisSatiri.split(" ");
        
        for (String s : sayilar) {
            int sayi = Integer.parseInt(s);
            int ilceSayisi = sayi / 10;
            int mahalleSayisi = sayi % 10;
            int nufus = sayi;

         // Mahalle düzeltme: 0 ise veya bölünmüyorsa düzeltilir
            if (mahalleSayisi % ilceSayisi != 0 || mahalleSayisi == 0) {
                int bulunanMahalle = -1;
                // Birler basamağını 1'den 9'a kadar tarar. (onlar basamağı sabit kalsın)
                for (int i = 1; i <= 9; i++) {
                    if (i % ilceSayisi == 0) {
                        bulunanMahalle = i;
                        if (i >= mahalleSayisi) break; 
                    }
                }
                
                // Eğer yukarıda uygun sayı bulunamadıysa mecburi yukarı yuvarlanır.
                if (bulunanMahalle != -1) {
                    mahalleSayisi = bulunanMahalle;
                } else {
                    while (mahalleSayisi % ilceSayisi != 0) mahalleSayisi++;
                }
            }

            // Artık yeni sayı (onlar ve yeni birler) nufus olarak güncellenir.
            nufus = (ilceSayisi * 10) + mahalleSayisi;

            int ilceBasinaMahalle = mahalleSayisi / ilceSayisi;

         // Nüfus düzeltme: Mahalle sayısına tam bölünmelidir.
            if (nufus % mahalleSayisi != 0) {
                while (nufus % mahalleSayisi != 0) {
                    nufus++;
                }
            }
            int mahalleBasinaKisi = nufus / mahalleSayisi;

            //  Nesne Oluşturma
            Sehir yeniSehir = new Sehir(faker.address().city());
            
            for (int i = 0; i < ilceSayisi; i++) {
                Ilce yeniIlce = new Ilce(faker.address().state());
                for (int j = 0; j < ilceBasinaMahalle; j++) {
                    Mahalle yeniMahalle = new Mahalle(faker.address().streetName());
                    for (int k = 0; k < mahalleBasinaKisi; k++) {
                        yeniMahalle.kisiEkle(new Kisi(faker.name().fullName(), random.nextInt(51)));
                    }
                    yeniIlce.mahalleEkle(yeniMahalle);
                }
                yeniSehir.ilceEkle(yeniIlce);
            }
            sehirler.add(yeniSehir);
        }
    }
 // Her turda nüfusu artıran ve bölünmeleri kontrol eden ana döngüdür.
    public void turAtla() {
    	// Nüfus artış ve yaşlanma işlemleri burada yapılır.
        // Ardından bölünme kontrolü çağrılır.
        for (Sehir sehir : sehirler) {
            int oran = sehir.getArtisOrani();
            
            for (Ilce ilce : sehir.getIlceler()) {
                for (Mahalle mahalle : ilce.getMahalleler()) {
                    int mevcutMahalleNufusu = mahalle.getNufus();
                    int eklenecekKisiSayisi;

                    if (oran > 0) {
                        eklenecekKisiSayisi = (mevcutMahalleNufusu * oran) - mevcutMahalleNufusu;
                    } else {
                        // Oran 0 ise kural: Her mahallede nüfus 1 kişi artar.
                        eklenecekKisiSayisi = 1; 
                    }
                 // KURAL: Eğer oran 1 veya 0 ise nüfus 1 kişi artar.
                 // Çünkü oran 1 olduğunda (mevcut * 1) nüfusu artırmaz.
                    if (oran > 1) {
                        eklenecekKisiSayisi = (mevcutMahalleNufusu * oran) - mevcutMahalleNufusu;
                    } else {
                        eklenecekKisiSayisi = 1; 
                    }
                    // Yeni kişileri oluşturur ve ekler.
                    for (int i = 0; i < eklenecekKisiSayisi; i++) {
                        mahalle.kisiEkle(new Kisi(faker.name().fullName(), random.nextInt(51)));
                    }

                    // Yaşlandırma: Mahalledeki herkesin yaşını 1 artırır.
                    for (Kisi kisi : mahalle.getKisiler()) {
                        kisi.setYas(kisi.getYas() + 1);
                    }
                }
            }
        }
        
        // Bölünme Kontrolü (Tur sonunda bir kez yapılır).
        sehirleriBol();
    }
    
    private void sehirleriBol() {
        List<Sehir> yeniOlusanSehirler = new ArrayList<>();

        for (Sehir sehir : sehirler) {
            if (sehir.getNufus() >= 1000) {
                Sehir yeniSehir = new Sehir(faker.address().city());
                List<Ilce> eskiSehirIlceler = sehir.getIlceler();
                int ilceSayisi = eskiSehirIlceler.size();

                if (ilceSayisi > 1) {
                    // Kural: İlçe sayısı çiftse tam böler, tekse bir eksiğini yeni şehre verir.
                    int tasinacakIlceSayisi = ilceSayisi / 2;
                    for (int i = 0; i < tasinacakIlceSayisi; i++) {
                        // Listenin sonundan alıp yeni şehre taşır.
                        yeniSehir.ilceEkle(eskiSehirIlceler.remove(eskiSehirIlceler.size() - 1));
                    }
                } 
                else if (ilceSayisi == 1) {
                    // İstisna 1: 1 İlçe varsa mahalleler bölünür.
                    Ilce mevcutIlce = eskiSehirIlceler.get(0);
                    List<Mahalle> mahalleler = mevcutIlce.getMahalleler();
                    
                    if (mahalleler.size() > 1) {
                        Ilce yeniIlce = new Ilce(faker.address().state());
                        int tasinacakMahalleSayisi = mahalleler.size() / 2;
                        for (int i = 0; i < tasinacakMahalleSayisi; i++) {
                            yeniIlce.mahalleEkle(mahalleler.remove(mahalleler.size() - 1));
                        }
                        yeniSehir.ilceEkle(yeniIlce);
                    } 
                    else if (mahalleler.size() == 1) {
                        // İstisna 2: 1 İlçe 1 Mahalle varsa kişiler bölünür.
                        Mahalle mevcutMahalle = mahalleler.get(0);
                        List<Kisi> kisiler = mevcutMahalle.getKisiler();
                        
                        Ilce yeniIlce = new Ilce(faker.address().state());
                        Mahalle yeniMahalle = new Mahalle(faker.address().streetName());
                        
                        int tasinacakKisiSayisi = kisiler.size() / 2;
                        for (int i = 0; i < tasinacakKisiSayisi; i++) {
                            yeniMahalle.kisiEkle(kisiler.remove(kisiler.size() - 1));
                        }
                        yeniIlce.mahalleEkle(yeniMahalle);
                        yeniSehir.ilceEkle(yeniIlce);
                    }
                }
                yeniOlusanSehirler.add(yeniSehir);
            }
        }
        // Yeni oluşan şehirler ana listeye eklenir.
        sehirler.addAll(yeniOlusanSehirler);
    }
    
    public void ekraniYazdir() {
        for (int i = 0; i < sehirler.size(); i++) {
            System.out.print("[" + sehirler.get(i).getNufus() + "]");
            if ((i + 1) % 5 == 0) {
                System.out.println();
            } else if (i != sehirler.size() - 1) {
                System.out.print("-");
            }
        }
        System.out.println("\n");
    }
    
    public void detayliSehirYazdir(int satir, int sutun) {
        int indeks = (satir * 5) + sutun; // Her satırda 5 şehir listelenir.
        
        if (indeks < 0 || indeks >= sehirler.size()) {
            System.out.println("Hatalı konum girdiniz!");
            return;
        }

        Sehir hedef = sehirler.get(indeks);
        System.out.println("\nŞehir: " + hedef.getAd() + " - Nüfus: " + hedef.getNufus());

        for (Ilce ilce : hedef.getIlceler()) {
            System.out.println("  İlçe: " + ilce.getAd() + " - Nüfus: " + ilce.getNufus());
            for (Mahalle mahalle : ilce.getMahalleler()) {
                System.out.println("    Mahalle: " + mahalle.getAd() + " - Nüfus: " + mahalle.getNufus());
                System.out.println("    Kişiler:");
                for (Kisi kisi : mahalle.getKisiler()) {
                    System.out.println("      " + kisi.getId() + " - " + kisi.getIsimSoyisim() + " - " + kisi.getYas());
                }
            }
        }
    }
}