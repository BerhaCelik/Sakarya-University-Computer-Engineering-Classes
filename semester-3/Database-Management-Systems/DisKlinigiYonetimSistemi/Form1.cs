using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormAnaMenu : Form
    {
        public FormAnaMenu()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuHastaKayit_Click(object sender, EventArgs e)
        {
            FormHastaKayit form = new FormHastaKayit();
            form.ShowDialog();
        }

        private void menuHastaListesi_Click(object sender, EventArgs e)
        {
            FormHastaListe form = new FormHastaListe();
            form.ShowDialog();
        }

        private void menuYeniRandevu_Click(object sender, EventArgs e)
        {
            FormRandevuOlustur form = new FormRandevuOlustur();
            form.ShowDialog();
        }

        private void menuRandevuListesi_Click(object sender, EventArgs e)
        {
            FormRandevuListe form = new FormRandevuListe();
            form.ShowDialog();
        }

        private void menuFaturaListesi_Click(object sender, EventArgs e)
        {
            FormFaturaListe form = new FormFaturaListe();
            form.ShowDialog();
        }

        private void stokYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormStokListe form = new FormStokListe();
            form.ShowDialog();
        }

        private void menuHastaRaporu_Click(object sender, EventArgs e)
        {
            FormHastaRaporu form = new FormHastaRaporu();
            form.ShowDialog();
        }

        private void aylıkRaporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGelirRaporu form = new FormGelirRaporu();
            form.ShowDialog();
        }

        private void menuKritikStok_Click(object sender, EventArgs e)
        {
            FormKritikStok form = new FormKritikStok();
            form.ShowDialog();
        }
    }
}
