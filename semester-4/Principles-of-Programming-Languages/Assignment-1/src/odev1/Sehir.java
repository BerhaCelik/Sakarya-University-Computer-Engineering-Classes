/**
*
* @author Berha Çelik B241210099
* @since 17/04/2026
* <p>
* * Simülasyonun ana idari birimidir. İlçeleri yönetir ve nüfus artış
* oranını kendi nüfusunun basamak değerlerine göre hesaplar.
* </p>
*/
package odev1;

import java.util.ArrayList;
import java.util.List;

public class Sehir {
    private String ad;
    private List<Ilce> ilceler;  // Şehre bağlı ilçelerin listesidir.

    public Sehir(String ad) {
        this.ad = ad;
        this.ilceler = new ArrayList<>();
    }

    public void ilceEkle(Ilce i) {
        ilceler.add(i);
    }
 // Şehir nüfusunu hiyerarşik olarak tüm alt birimlerden toplar.
    public int getNufus() {
        int toplam = 0;
        if (ilceler == null) return 0;
        for (Ilce i : ilceler) {
            for (Mahalle m : i.getMahalleler()) {
                toplam += m.getKisiler().size(); // Direkt kişi listesini sayar.
            }
        }
        return toplam;
    }

    // Nüfus artış oranı hesaplar. (Birler + Onlar basamağı)
    public int getArtisOrani() {
        int nufus = getNufus();
        int onlar = (nufus / 10) % 10;
        int birler = nufus % 10;
        return onlar + birler;
    }

    public List<Ilce> getIlceler() { return ilceler; }
    public String getAd() { return ad; }
}

