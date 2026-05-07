using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Tedavituru
    {
        public Tedavituru()
        {
            Randevutedavi = new HashSet<Randevutedavi>();
        }

        public int Tedavituruid { get; set; }
        public string Tedavikodu { get; set; }
        public string Tedaviadi { get; set; }
        public decimal Birimfiyat { get; set; }

        public virtual ICollection<Randevutedavi> Randevutedavi { get; set; }
    }
}
