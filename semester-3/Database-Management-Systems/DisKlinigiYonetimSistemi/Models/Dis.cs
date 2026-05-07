using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Dis
    {
        public Dis()
        {
            Diskayit = new HashSet<Diskayit>();
            Randevutedavi = new HashSet<Randevutedavi>();
        }

        public int Disno { get; set; }
        public string Disadi { get; set; }

        public virtual ICollection<Diskayit> Diskayit { get; set; }
        public virtual ICollection<Randevutedavi> Randevutedavi { get; set; }
    }
}
