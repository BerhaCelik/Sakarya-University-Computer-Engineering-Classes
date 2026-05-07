using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Stokhareket
    {
        public int Hareketid { get; set; }
        public int Stokid { get; set; }
        public string Harekettipi { get; set; }
        public int Miktar { get; set; }
        public DateTime Tarih { get; set; }

        public virtual Stok Stok { get; set; }
    }
}
