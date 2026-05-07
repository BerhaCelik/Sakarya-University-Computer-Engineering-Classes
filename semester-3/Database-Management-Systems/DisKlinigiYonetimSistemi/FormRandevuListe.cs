using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormRandevuListe : Form
    {
        public FormRandevuListe()
        {
            InitializeComponent();
        }

        private void FormRandevuListe_Load(object sender, EventArgs e)
        {
            cmbDurum.SelectedIndex = 0; // Tümü
            RandevulariYukle();
        }

        private void RandevulariYukle()
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var randevular = (from r in context.Randevu
                                      join h in context.Hasta on r.Hastaid equals h.Hastaid
                                      join dh in context.Dishekimi on r.Dishekimiid equals dh.Personelid
                                      join p in context.Personel on dh.Personelid equals p.Personelid
                                      orderby r.Randevutarihi descending, r.Randevusaati descending
                                      select new
                                      {
                                          RandevuID = r.Randevuid,
                                          Hasta = h.Ad + " " + h.Soyad,
                                          Doktor = "Dr. " + p.Ad + " " + p.Soyad,
                                          Tarih = r.Randevutarihi,
                                          Saat = r.Randevusaati,
                                          Durum = r.Durum
                                      }).ToList();

                    dgvRandevular.DataSource = randevular;

                    dgvRandevular.Columns["RandevuID"].HeaderText = "ID";
                    dgvRandevular.Columns["RandevuID"].Width = 50;
                    dgvRandevular.Columns["Hasta"].HeaderText = "Hasta";
                    dgvRandevular.Columns["Hasta"].Width = 180;
                    dgvRandevular.Columns["Doktor"].HeaderText = "Doktor";
                    dgvRandevular.Columns["Doktor"].Width = 180;
                    dgvRandevular.Columns["Tarih"].HeaderText = "Tarih";
                    dgvRandevular.Columns["Saat"].HeaderText = "Saat";
                    dgvRandevular.Columns["Durum"].HeaderText = "Durum";

                    // Renklendirme
                    foreach (DataGridViewRow row in dgvRandevular.Rows)
                    {
                        string durum = row.Cells["Durum"].Value?.ToString();
                        if (durum == "Beklemede")
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        else if (durum == "Tamamlandi")
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (durum == "Iptal")
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var query = from r in context.Randevu
                                join h in context.Hasta on r.Hastaid equals h.Hastaid
                                join dh in context.Dishekimi on r.Dishekimiid equals dh.Personelid
                                join p in context.Personel on dh.Personelid equals p.Personelid
                                select new
                                {
                                    RandevuID = r.Randevuid,
                                    Hasta = h.Ad + " " + h.Soyad,
                                    Doktor = "Dr. " + p.Ad + " " + p.Soyad,
                                    Tarih = r.Randevutarihi,
                                    Saat = r.Randevusaati,
                                    Durum = r.Durum
                                };

                    if (cmbDurum.SelectedItem.ToString() != "Tümü")
                    {
                        string durum = cmbDurum.SelectedItem.ToString();
                        query = query.Where(r => r.Durum == durum);
                    }

                    var randevular = query.OrderByDescending(r => r.Tarih).ToList();
                    dgvRandevular.DataSource = randevular;

                    // Renklendirme
                    foreach (DataGridViewRow row in dgvRandevular.Rows)
                    {
                        string durum = row.Cells["Durum"].Value?.ToString();
                        if (durum == "Beklemede")
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        else if (durum == "Tamamlandi")
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (durum == "Iptal")
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnYeniRandevu_Click(object sender, EventArgs e)
        {
            FormRandevuOlustur form = new FormRandevuOlustur();
            if (form.ShowDialog() == DialogResult.OK)
            {
                RandevulariYukle();
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            cmbDurum.SelectedIndex = 0;
            RandevulariYukle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
