using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Linq;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormHastaListe : Form
    {
        public FormHastaListe()
        {
            InitializeComponent();
        }

        private void FormHastaListe_Load(object sender, EventArgs e)
        {
            HastalarıYukle();
        }

        private void HastalarıYukle()
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var hastalar = context.Hasta
                        .Select(h => new
                        {
                            h.Hastaid,
                            h.Ad,
                            h.Soyad,
                            h.Tckn,
                            h.Telefon,
                            h.Email
                        })
                        .OrderBy(h => h.Ad)
                        .ToList();

                    dgvHastalar.DataSource = hastalar;

                    dgvHastalar.Columns["Hastaid"].HeaderText = "ID";
                    dgvHastalar.Columns["Hastaid"].Width = 50;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArama.Text))
            {
                MessageBox.Show("Arama için değer girin!");
                return;
            }

            using (var context = new DisKlinigiContext())
            {
                string aramaMetni = txtArama.Text.ToLower();

                var hastalar = context.Hasta
                    .Where(h => h.Ad.ToLower().Contains(aramaMetni) ||
                               h.Soyad.ToLower().Contains(aramaMetni) ||
                               h.Tckn.Contains(aramaMetni))
                    .Select(h => new { h.Hastaid, h.Ad, h.Soyad, h.Tckn, h.Telefon })
                    .ToList();

                dgvHastalar.DataSource = hastalar;
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            txtArama.Clear();
            HastalarıYukle();
        }

        private void btnYeniHasta_Click(object sender, EventArgs e)
        {
            FormHastaKayit form = new FormHastaKayit();
            form.ShowDialog();
            HastalarıYukle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvHastalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hasta seçin!");
                return;
            }

            int hastaId = Convert.ToInt32(dgvHastalar.SelectedRows[0].Cells["Hastaid"].Value);
            FormHastaGuncelle form = new FormHastaGuncelle(hastaId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                HastalarıYukle();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvHastalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hasta seçin!");
                return;
            }

            int hastaId = Convert.ToInt32(dgvHastalar.SelectedRows[0].Cells["Hastaid"].Value);

            var result = MessageBox.Show("Silmek istediğinizden emin misiniz?",
                "Onay", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                using (var context = new DisKlinigiContext())
                {
                    var hasta = context.Hasta.Find(hastaId);
                    context.Hasta.Remove(hasta);
                    context.SaveChanges();
                    MessageBox.Show("Silindi!");
                    HastalarıYukle();
                }
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}