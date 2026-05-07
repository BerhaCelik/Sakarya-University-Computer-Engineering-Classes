using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Ilac
    {
        public Ilac()
        {
            Receteilac = new HashSet<Receteilac>();
        }

        public int Ilacid { get; set; }
        public string Ilacadi { get; set; }

        public virtual ICollection<Receteilac> Receteilac { get; set; }
    }
}
