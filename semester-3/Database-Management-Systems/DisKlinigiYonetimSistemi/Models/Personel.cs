using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Personel
    {
        public Personel()
        {
            Calismasaati = new HashSet<Calismasaati>();
        }

        public int Personelid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Tckn { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public DateTime Isebaslamatarihi { get; set; }

        public virtual Asistan Asistan { get; set; }
        public virtual Dishekimi Dishekimi { get; set; }
        public virtual Resepsiyonist Resepsiyonist { get; set; }
        public virtual ICollection<Calismasaati> Calismasaati { get; set; }
    }
}
