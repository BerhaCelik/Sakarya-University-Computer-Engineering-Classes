using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Stok
    {
        public Stok()
        {
            Stokhareket = new HashSet<Stokhareket>();
        }

        public int Stokid { get; set; }
        public string Malzemeadi { get; set; }
        public string Stokkodu { get; set; }
        public int Miktar { get; set; }
        public int Minimumstok { get; set; }
        public string Birim { get; set; }

        public virtual ICollection<Stokhareket> Stokhareket { get; set; }
    }
}
