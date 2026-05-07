using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormStokListe : Form
    {
        public FormStokListe()
        {
            InitializeComponent();
        }

        private void FormStokListe_Load(object sender, EventArgs e)
        {
            StoklariYukle();
        }

        private void StoklariYukle()
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var stoklar = context.Stok
                        .Select(s => new
                        {
                            s.Stokid,
                            s.Malzemeadi,
                            s.Stokkodu,
                            s.Miktar,
                            s.Minimumstok,
                            s.Birim,
                            Durum = s.Miktar == 0 ? "Tükendi" :
                                   s.Miktar < s.Minimumstok ? "Kritik" :
                                   s.Miktar <= (s.Minimumstok * 1.2) ? "Uyarı" : "Normal"
                        })
                        .OrderBy(s => s.Miktar)
                        .ToList();

                    dgvStok.DataSource = stoklar;

                    dgvStok.Columns["Stokid"].HeaderText = "ID";
                    dgvStok.Columns["Stokid"].Width = 50;
                    dgvStok.Columns["Malzemeadi"].HeaderText = "Malzeme Adı";
                    dgvStok.Columns["Malzemeadi"].Width = 200;
                    dgvStok.Columns["Stokkodu"].HeaderText = "Stok Kodu";
                    dgvStok.Columns["Stokkodu"].Width = 100;
                    dgvStok.Columns["Miktar"].HeaderText = "Miktar";
                    dgvStok.Columns["Miktar"].Width = 80;
                    dgvStok.Columns["Minimumstok"].HeaderText = "Min. Stok";
                    dgvStok.Columns["Minimumstok"].Width = 80;
                    dgvStok.Columns["Birim"].HeaderText = "Birim";
                    dgvStok.Columns["Birim"].Width = 70;
                    dgvStok.Columns["Durum"].HeaderText = "Durum";
                    dgvStok.Columns["Durum"].Width = 100;

                    // Renklendirme
                    foreach (DataGridViewRow row in dgvStok.Rows)
                    {
                        string durum = row.Cells["Durum"].Value?.ToString();
                        if (durum == "Tükendi")
                        {
                            row.DefaultCellStyle.BackColor = Color.DarkRed;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (durum == "Kritik")
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (durum == "Uyarı")
                        {
                            row.DefaultCellStyle.BackColor = Color.Orange;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (dgvStok.SelectedRows.Count == 0)
            {
                MessageBox.Show("Stok kalemi seçin!", "Uyarı");
                return;
            }

            int stokId = Convert.ToInt32(dgvStok.SelectedRows[0].Cells["Stokid"].Value);
            string malzeme = dgvStok.SelectedRows[0].Cells["Malzemeadi"].Value.ToString();

            string miktar = Microsoft.VisualBasic.Interaction.InputBox(
                $"{malzeme}\n\nGiriş Miktarı:",
                "Stok Girişi",
                "0");

            if (string.IsNullOrWhiteSpace(miktar))
                return;

            if (int.TryParse(miktar, out int girisMiktari) && girisMiktari > 0)
            {
                try
                {
                    using (var context = new DisKlinigiContext())
                    {
                        var stokHareket = new Stokhareket
                        {
                            Stokid = stokId,
                            Harekettipi = "Giris",
                            Miktar = girisMiktari,
                            Tarih = DateTime.Now
                        };

                        context.Stokhareket.Add(stokHareket);
                        context.SaveChanges();

                        MessageBox.Show($"{girisMiktari} adet stok girişi yapıldı!\n\n" +
                                       "✓ TETİKLEYİCİ çalıştı ve stok otomatik güncellendi!",
                                       "Başarılı");

                        StoklariYukle();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (dgvStok.SelectedRows.Count == 0)
            {
                MessageBox.Show("Stok kalemi seçin!", "Uyarı");
                return;
            }

            int stokId = Convert.ToInt32(dgvStok.SelectedRows[0].Cells["Stokid"].Value);
            string malzeme = dgvStok.SelectedRows[0].Cells["Malzemeadi"].Value.ToString();
            int mevcut = Convert.ToInt32(dgvStok.SelectedRows[0].Cells["Miktar"].Value);

            string miktar = Microsoft.VisualBasic.Interaction.InputBox(
                $"{malzeme}\nMevcut Stok: {mevcut}\n\nÇıkış Miktarı:",
                "Stok Çıkışı",
                "0");

            if (string.IsNullOrWhiteSpace(miktar))
                return;

            if (int.TryParse(miktar, out int cikisMiktari) && cikisMiktari > 0)
            {
                try
                {
                    using (var context = new DisKlinigiContext())
                    {
                        var stokHareket = new Stokhareket
                        {
                            Stokid = stokId,
                            Harekettipi = "Cikis",
                            Miktar = cikisMiktari,
                            Tarih = DateTime.Now
                        };

                        context.Stokhareket.Add(stokHareket);
                        context.SaveChanges();

                        MessageBox.Show($"{cikisMiktari} adet stok çıkışı yapıldı!\n\n" +
                                       "✓ TETİKLEYİCİ çalıştı!",
                                       "Başarılı");

                        StoklariYukle();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"TETİKLEYİCİ HATASI!\n\n" +
                                   "Muhtemelen yetersiz stok var.\n\n" +
                                   $"Hata: {ex.Message}",
                                   "Hata");
                }
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            StoklariYukle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}