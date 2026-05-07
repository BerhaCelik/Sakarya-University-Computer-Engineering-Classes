/**
*
* @author Berha Çelik B241210099
* @since 17/04/2026
* <p>
* Programın akışı burada kontrol edilir. Kullanıcıdan tur sayısını alır, sayıları doğrular ve Oyun nesnesini hayata geçirir.
* </p>
*/
package odev1;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
    	Scanner scanner = new Scanner(System.in);
        Oyun oyun = new Oyun();

        // Tur Sayısını Alır.
        System.out.print("Oyunun çalışacağı tur sayısını giriniz: ");
        int toplamTur = scanner.nextInt();
        scanner.nextLine(); // Boşluk temizlenir.

        //Şehir Sayılarını Alır ve Doğrular.
        String giris;
        while (true) {
            System.out.println("İki basamaklı sayıları aralarında boşluk bırakarak giriniz:");
            giris = scanner.nextLine();
            
            if (girisFormatiniKontrolEt(giris)) {
                break;
            } else {
                System.out.println("UYARI: Sadece iki basamaklı sayılar girmelisiniz!");
            }
        }

        // Oyun Kurulumu ve Turlar oluşturulur.
        oyun.kurulum(giris);

        for (int tur = 0; tur <= toplamTur; tur++) {
            Console.clear();
            System.out.println("--- Tur: " + tur + " ---");
            oyun.ekraniYazdir();
            
            if (tur < toplamTur) {
                try {
                    Thread.sleep(1000); // İzlenebilirlik için 1 saniye beklenir.
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                oyun.turAtla();
            }
        }
        Console.clear();
        System.out.println("--- OYUN BİTTİ: SON DURUM ---");
        oyun.ekraniYazdir(); // Son durum nüfuslarını tekrar basar
        
        // Satır ve Sütun Sorgulaması yapılır. 
        System.out.println("Oyun bitti. Görüntülemek istediğiniz şehrin konumunu girin.");
        System.out.print("Satır (0'dan başlar): ");
        int satir = scanner.nextInt();
        System.out.print("Sütun (0'dan başlar): ");
        int sutun = scanner.nextInt();

        oyun.detayliSehirYazdir(satir, sutun);

        System.out.println("\nKapatmak için bir tuşa basın...");
        scanner.next();
        scanner.close();
    }

    // Girdi formatı doğrulaması (Sadece 2 basamaklı sayılar mı?) yapılır.
    private static boolean girisFormatiniKontrolEt(String giris) {
        String[] parcalar = giris.split("\\s+");
        for (String p : parcalar) {
            if (p.length() != 2 || !Character.isDigit(p.charAt(0)) || !Character.isDigit(p.charAt(1))) {
                return false;
            }
        }
        return true;
    }
}
