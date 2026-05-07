/**
*
* @author Berha Çelik B241210099
* @since 17/04/2026
* <p>
** Şehirlere bağlı olan ve bünyesinde birden fazla mahalleyi barındıran sınıftır.
* İlçe nüfusu, kendisine bağlı mahallelerin nüfusları toplamıdır.
* </p>
*/
package odev1;

import java.util.ArrayList;
import java.util.List;

public class Ilce {
    private String ad;
    private List<Mahalle> mahalleler; // İlçeye bağlı mahallelerin listesidir.

    public Ilce(String ad) {
        this.ad = ad;
        this.mahalleler = new ArrayList<>();
    }

    public void mahalleEkle(Mahalle m) {
        mahalleler.add(m);
    }

    public List<Mahalle> getMahalleler() { return mahalleler; }
    public String getAd() { return ad; }
    
 // İlçeye bağlı tüm mahallelerin nüfusunu döngü ile toplar.
    public int getNufus() {
        int toplam = 0;
        for (Mahalle m : mahalleler) {
            toplam += m.getNufus();
        }
        return toplam;
    }
}