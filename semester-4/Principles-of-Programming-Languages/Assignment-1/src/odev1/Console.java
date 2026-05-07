/**
*
* @author Berha Çelik B241210099
* @since 17/04/2026
* <p>
* Konsol temizleme işlemlerini yöneten yardımcı sınıftır.
* </p>
*/
package odev1;

import java.io.IOException;


public class Console {

    public static void clear() {
        try {
            // İşletim sistemi Windows ise "cls", değilse "clear" komutunu çalıştırır
            if (System.getProperty("os.name").contains("Windows")) {
                new ProcessBuilder("cmd", "/c", "cls").inheritIO().start().waitFor();
            } else {
                System.out.print("\033[H\033[2J");
                System.out.flush();
            }
        } catch (IOException | InterruptedException ex) {
            // Hata durumunda konsol temizlenemezse program devam eder
        }
    }
}