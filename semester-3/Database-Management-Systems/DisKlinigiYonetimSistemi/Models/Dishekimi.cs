using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Dishekimi
    {
        public Dishekimi()
        {
            Randevu = new HashSet<Randevu>();
            Randevutedavi = new HashSet<Randevutedavi>();
            Recete = new HashSet<Recete>();
        }

        public int Personelid { get; set; }
        public string Uzmanlikalani { get; set; }

        public virtual Personel Personel { get; set; }
        public virtual ICollection<Randevu> Randevu { get; set; }
        public virtual ICollection<Randevutedavi> Randevutedavi { get; set; }
        public virtual ICollection<Recete> Recete { get; set; }
    }
}
