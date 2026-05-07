using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Hasta
    {
        public Hasta()
        {
            Diskayit = new HashSet<Diskayit>();
            Randevu = new HashSet<Randevu>();
        }

        public int Hastaid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Tckn { get; set; }
        public DateTime Dogumtarihi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public DateTime Kayittarihi { get; set; }

        public virtual ICollection<Diskayit> Diskayit { get; set; }
        public virtual ICollection<Randevu> Randevu { get; set; }
    }
}
