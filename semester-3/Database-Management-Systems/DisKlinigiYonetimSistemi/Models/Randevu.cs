using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Randevu
    {
        public Randevu()
        {
            Randevutedavi = new HashSet<Randevutedavi>();
            Recete = new HashSet<Recete>();
        }

        public int Randevuid { get; set; }
        public int Hastaid { get; set; }
        public int Dishekimiid { get; set; }
        public DateTime Randevutarihi { get; set; }
        public TimeSpan Randevusaati { get; set; }
        public string Durum { get; set; }

        public virtual Dishekimi Dishekimi { get; set; }
        public virtual Hasta Hasta { get; set; }
        public virtual Fatura Fatura { get; set; }
        public virtual ICollection<Randevutedavi> Randevutedavi { get; set; }
        public virtual ICollection<Recete> Recete { get; set; }
    }
}
