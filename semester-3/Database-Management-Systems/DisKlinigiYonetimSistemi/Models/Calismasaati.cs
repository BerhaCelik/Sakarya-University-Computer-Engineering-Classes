using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Calismasaati
    {
        public int Calismasaatiid { get; set; }
        public int Personelid { get; set; }
        public string Gun { get; set; }
        public TimeSpan Baslangicsaati { get; set; }
        public TimeSpan Bitissaati { get; set; }

        public virtual Personel Personel { get; set; }
    }
}
