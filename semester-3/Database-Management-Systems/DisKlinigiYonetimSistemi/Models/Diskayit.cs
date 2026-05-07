using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Diskayit
    {
        public int Diskayitid { get; set; }
        public int Hastaid { get; set; }
        public int Disno { get; set; }
        public string Disdurumu { get; set; }
        public DateTime? Sonkontroltarihi { get; set; }

        public virtual Dis DisnoNavigation { get; set; }
        public virtual Hasta Hasta { get; set; }
    }
}
