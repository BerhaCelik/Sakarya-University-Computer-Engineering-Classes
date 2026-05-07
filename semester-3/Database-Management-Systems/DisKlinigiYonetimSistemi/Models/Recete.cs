using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Recete
    {
        public Recete()
        {
            Receteilac = new HashSet<Receteilac>();
        }

        public int Receteid { get; set; }
        public int Randevuid { get; set; }
        public int Dishekimiid { get; set; }
        public DateTime Recetetarihi { get; set; }

        public virtual Dishekimi Dishekimi { get; set; }
        public virtual Randevu Randevu { get; set; }
        public virtual ICollection<Receteilac> Receteilac { get; set; }
    }
}
