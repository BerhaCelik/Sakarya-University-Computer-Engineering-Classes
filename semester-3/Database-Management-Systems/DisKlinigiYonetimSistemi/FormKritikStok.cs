using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormKritikStok : Form
    {
        public FormKritikStok()
        {
            InitializeComponent();
        }

        private void FormKritikStok_Load(object sender, EventArgs e)
        {
            KritikStoklariYukle();
        }

        private void KritikStoklariYukle()
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    // SQL fonksiyonu yerine LINQ
                    var kritikStoklar = context.Stok
                        .Where(s => s.Miktar < s.Minimumstok)
                        .Select(s => new
                        {
                            StokID = s.Stokid,
                            MalzemeAdi = s.Malzemeadi,
                            StokKodu = s.Stokkodu,
                            MevcutMiktar = s.Miktar,
                            MinimumStok = s.Minimumstok,
                            EksikMiktar = s.Minimumstok - s.Miktar,
                            Durum = s.Miktar == 0 ? "Tükendi" :
                                   s.Miktar < (s.Minimumstok * 0.5) ? "Kritik" : "Uyarı",
                            Birim = s.Birim
                        })
                        .OrderBy(s => s.MevcutMiktar)
                        .ToList();

                    dgvKritikStok.DataSource = kritikStoklar;

                    if (kritikStoklar.Count > 0)
                    {
                        // Sütun başlıkları
                        dgvKritikStok.Columns["StokID"].HeaderText = "ID";
                        dgvKritikStok.Columns["StokID"].Width = 50;
                        dgvKritikStok.Columns["MalzemeAdi"].HeaderText = "Malzeme Adı";
                        dgvKritikStok.Columns["MalzemeAdi"].Width = 200;
                        dgvKritikStok.Columns["StokKodu"].HeaderText = "Stok Kodu";
                        dgvKritikStok.Columns["StokKodu"].Width = 100;
                        dgvKritikStok.Columns["MevcutMiktar"].HeaderText = "Mevcut";
                        dgvKritikStok.Columns["MevcutMiktar"].Width = 80;
                        dgvKritikStok.Columns["MinimumStok"].HeaderText = "Minimum";
                        dgvKritikStok.Columns["MinimumStok"].Width = 80;
                        dgvKritikStok.Columns["EksikMiktar"].HeaderText = "Eksik";
                        dgvKritikStok.Columns["EksikMiktar"].Width = 80;
                        dgvKritikStok.Columns["Durum"].HeaderText = "Durum";
                        dgvKritikStok.Columns["Durum"].Width = 100;
                        dgvKritikStok.Columns["Birim"].HeaderText = "Birim";
                        dgvKritikStok.Columns["Birim"].Width = 70;

                        // Satırları renklendir
                        foreach (DataGridViewRow row in dgvKritikStok.Rows)
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

                        lblBaslik.Text = $"⚠ {kritikStoklar.Count} adet kritik stok bulundu!";
                        lblBaslik.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblBaslik.Text = "✓ Tüm stoklar yeterli seviyede!";
                        lblBaslik.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            KritikStoklariYukle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}