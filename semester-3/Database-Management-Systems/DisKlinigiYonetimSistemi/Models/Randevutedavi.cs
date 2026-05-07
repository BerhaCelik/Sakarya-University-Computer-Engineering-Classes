using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Randevutedavi
    {
        public int Randevutedaviid { get; set; }
        public int Randevuid { get; set; }
        public int Tedavituruid { get; set; }
        public int? Disno { get; set; }
        public int Uygulayandishekimiid { get; set; }
        public TimeSpan? Baslangicsaati { get; set; }
        public TimeSpan? Bitissaati { get; set; }

        public virtual Dis DisnoNavigation { get; set; }
        public virtual Randevu Randevu { get; set; }
        public virtual Tedavituru Tedavituru { get; set; }
        public virtual Dishekimi Uygulayandishekimi { get; set; }
    }
}
