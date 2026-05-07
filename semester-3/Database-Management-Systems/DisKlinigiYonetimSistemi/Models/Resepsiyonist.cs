using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Resepsiyonist
    {
        public int Personelid { get; set; }
        public string Vardiyaturu { get; set; }

        public virtual Personel Personel { get; set; }
    }
}
