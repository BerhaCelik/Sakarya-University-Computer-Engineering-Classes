/**
*
* @author Berha Çelik B241210099
* @since 17/04/2026
* <p>
** İlçelere bağlı olan ve içerisinde kişileri liste halinde tutan sınıftır.
* Nüfus yönetimi, mahalle içerisindeki kişi listesinin boyutu üzerinden yapılır.
* </p>
*/
package odev1;

import java.util.ArrayList;
import java.util.List;

public class Mahalle {
    private String ad;
    private List<Kisi> kisiler; // Mahallede yaşayan bireylerin listesidir.

    public Mahalle(String ad) {
        this.ad = ad;
        this.kisiler = new ArrayList<>();
    }
    
 // Mahalleye yeni bir birey eklemek için kullanılır.
    public void kisiEkle(Kisi kisi) {
        this.kisiler.add(kisi);
    }

    public List<Kisi> getKisiler() { return kisiler; }
    public String getAd() { return ad; }
 // Mahalle nüfusunu dinamik olarak liste boyutundan döndürür.
    public int getNufus() { return kisiler.size(); } 
}