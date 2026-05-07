using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Receteilac
    {
        public int Receteilacid { get; set; }
        public int Receteid { get; set; }
        public int Ilacid { get; set; }

        public virtual Ilac Ilac { get; set; }
        public virtual Recete Recete { get; set; }
    }
}
