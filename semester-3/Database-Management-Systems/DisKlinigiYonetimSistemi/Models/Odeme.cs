using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Odeme
    {
        public int Odemeid { get; set; }
        public int Faturaid { get; set; }
        public DateTime Odemetarihi { get; set; }
        public decimal Odenentutar { get; set; }

        public virtual Fatura Fatura { get; set; }
    }
}
