/**
*
* @author Berha Çelik B241210099
* @since 17/04/2026
* <p>
** Simülasyon içerisindeki en küçük birim olan bireyleri temsil eden sınıftır.
* Her kişinin kendine has bir ID'si, ismi ve yaşı bulunmaktadır.
* </p>
*/
package odev1;

//Tüm sistemde çakışmayan benzersiz ID üretimini sağlayan static sayaçtır.
public class Kisi {
    private static int idCounter = 1; // Tüm sistemde tekil ID için
    private int id;
    private String isimSoyisim;
    private int yas;

    public Kisi(String isimSoyisim, int yas) {
        this.id = idCounter++;  // Her yeni nesne oluşturulduğunda ID bir artırılır.
        this.isimSoyisim = isimSoyisim;
        this.yas = yas;
    }

    // Getter ve Setter metotları
    public int getId() { return id; }
    public String getIsimSoyisim() { return isimSoyisim; }
    public int getYas() { return yas; }
    public void setYas(int yas) { this.yas = yas; } // Her tur sonunda yaşın artırılması için kullanılan metot
}