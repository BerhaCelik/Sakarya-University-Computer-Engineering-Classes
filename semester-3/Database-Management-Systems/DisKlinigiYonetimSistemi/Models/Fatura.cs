using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DisKlinigiYonetimSistemi.Models
{
    public partial class Fatura
    {
        public Fatura()
        {
            Odeme = new HashSet<Odeme>();
        }

        public int Faturaid { get; set; }
        public int Randevuid { get; set; }
        public DateTime Faturatarihi { get; set; }
        public decimal Toplamtutar { get; set; }
        public string Odemedurumu { get; set; }

        public virtual Randevu Randevu { get; set; }
        public virtual ICollection<Odeme> Odeme { get; set; }
    }
}
