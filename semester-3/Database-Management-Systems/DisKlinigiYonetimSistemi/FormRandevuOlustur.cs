using System;
using System.Linq;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormRandevuOlustur : Form
    {
        public FormRandevuOlustur()
        {
            InitializeComponent();
        }

        private void FormRandevuOlustur_Load(object sender, EventArgs e)
        {
            HastaListesiniYukle();
            DoktorListesiniYukle();
            SaatListesiniOlustur();
            dtpRandevuTarihi.MinDate = DateTime.Today;
            dtpRandevuTarihi.Value = DateTime.Today;
        }

        private void HastaListesiniYukle()
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var hastalar = context.Hasta
                        .OrderBy(h => h.Ad)
                        .ThenBy(h => h.Soyad)
                        .Select(h => new
                        {
                            h.Hastaid,
                            TamAd = h.Ad + " " + h.Soyad + " - " + h.Tckn
                        })
                        .ToList();

                    cmbHasta.DataSource = hastalar;
                    cmbHasta.DisplayMember = "TamAd";
                    cmbHasta.ValueMember = "Hastaid";
                    cmbHasta.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void DoktorListesiniYukle()
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var doktorlar = (from dh in context.Dishekimi
                                     join p in context.Personel on dh.Personelid equals p.Personelid
                                     orderby p.Ad, p.Soyad
                                     select new
                                     {
                                         dh.Personelid,
                                         TamAd = "Dr. " + p.Ad + " " + p.Soyad
                                     }).ToList();

                    cmbDoktor.DataSource = doktorlar;
                    cmbDoktor.DisplayMember = "TamAd";
                    cmbDoktor.ValueMember = "Personelid";
                    cmbDoktor.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void SaatListesiniOlustur()
        {
            for (int saat = 9; saat <= 18; saat++)
            {
                cmbSaat.Items.Add($"{saat:D2}:00");
                if (saat < 18)
                {
                    cmbSaat.Items.Add($"{saat:D2}:30");
                }
            }
            cmbSaat.SelectedIndex = -1;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (cmbHasta.SelectedIndex == -1)
            {
                MessageBox.Show("Hasta seçin!");
                return;
            }

            if (cmbDoktor.SelectedIndex == -1)
            {
                MessageBox.Show("Doktor seçin!");
                return;
            }

            if (cmbSaat.SelectedIndex == -1)
            {
                MessageBox.Show("Saat seçin!");
                return;
            }

            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var yeniRandevu = new Randevu
                    {
                        Hastaid = Convert.ToInt32(cmbHasta.SelectedValue),
                        Dishekimiid = Convert.ToInt32(cmbDoktor.SelectedValue),
                        Randevutarihi = dtpRandevuTarihi.Value.Date,
                        Randevusaati = TimeSpan.Parse(cmbSaat.SelectedItem.ToString()),
                        Durum = "Beklemede"
                    };

                    context.Randevu.Add(yeniRandevu);
                    context.SaveChanges();

                    MessageBox.Show("Randevu başarıyla oluşturuldu!", "Başarılı");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n\nNot: Muhtemelen çakışma var!");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
