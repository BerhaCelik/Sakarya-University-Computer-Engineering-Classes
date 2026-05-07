using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormFaturaListe : Form
    {
        public FormFaturaListe()
        {
            InitializeComponent();
        }

        private void FormFaturaListe_Load(object sender, EventArgs e)
        {
            FaturalariYukle();
        }

        private void FaturalariYukle()
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var faturalar = (from f in context.Fatura
                                     join r in context.Randevu on f.Randevuid equals r.Randevuid
                                     join h in context.Hasta on r.Hastaid equals h.Hastaid
                                     orderby f.Faturatarihi descending
                                     select new
                                     {
                                         FaturaID = f.Faturaid,
                                         RandevuID = f.Randevuid,
                                         Hasta = h.Ad + " " + h.Soyad,
                                         Tarih = f.Faturatarihi,
                                         Tutar = f.Toplamtutar,
                                         Durum = f.Odemedurumu
                                     }).ToList();

                    dgvFaturalar.DataSource = faturalar;

                    dgvFaturalar.Columns["FaturaID"].HeaderText = "Fatura No";
                    dgvFaturalar.Columns["FaturaID"].Width = 80;
                    dgvFaturalar.Columns["RandevuID"].HeaderText = "Randevu No";
                    dgvFaturalar.Columns["RandevuID"].Width = 80;
                    dgvFaturalar.Columns["Hasta"].HeaderText = "Hasta";
                    dgvFaturalar.Columns["Hasta"].Width = 200;
                    dgvFaturalar.Columns["Tarih"].HeaderText = "Tarih";
                    dgvFaturalar.Columns["Tutar"].HeaderText = "Tutar";
                    dgvFaturalar.Columns["Durum"].HeaderText = "Ödeme Durumu";

                    // Renklendirme
                    foreach (DataGridViewRow row in dgvFaturalar.Rows)
                    {
                        string durum = row.Cells["Durum"].Value?.ToString();
                        if (durum == "Odenmedi")
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        else if (durum == "KismiOdendi")
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        else if (durum == "Odendi")
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
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
            FaturalariYukle();
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            if (dgvFaturalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Fatura seçin!", "Uyarı");
                return;
            }

            try
            {
                int faturaId = Convert.ToInt32(dgvFaturalar.SelectedRows[0].Cells["FaturaID"].Value);
                decimal toplamTutar = Convert.ToDecimal(dgvFaturalar.SelectedRows[0].Cells["Tutar"].Value);
                string durum = dgvFaturalar.SelectedRows[0].Cells["Durum"].Value.ToString();

                if (durum == "Odendi")
                {
                    MessageBox.Show("Bu fatura zaten ödenmiş!", "Bilgi");
                    return;
                }

                // Ödeme miktarı sor
                string girilenTutar = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Fatura Tutarı: {toplamTutar:C2}\n\nÖdeme Tutarı:",
                    "Ödeme Al",
                    toplamTutar.ToString());

                if (string.IsNullOrWhiteSpace(girilenTutar))
                    return;

                if (!decimal.TryParse(girilenTutar, out decimal odemeTutari) || odemeTutari <= 0)
                {
                    MessageBox.Show("Geçerli bir tutar girin!", "Hata");
                    return;
                }

                using (var context = new DisKlinigiContext())
                {
                    // Ödeme kaydı oluştur
                    var yeniOdeme = new Odeme
                    {
                        Faturaid = faturaId,
                        Odemetarihi = DateTime.Now,
                        Odenentutar = odemeTutari
                    };

                    context.Odeme.Add(yeniOdeme);

                    // Toplam ödenen hesapla
                    var toplamOdenen = context.Odeme
                        .Where(o => o.Faturaid == faturaId)
                        .Sum(o => (decimal?)o.Odenentutar) ?? 0;

                    toplamOdenen += odemeTutari;

                    // Fatura durumunu güncelle
                    var fatura = context.Fatura.Find(faturaId);

                    if (toplamOdenen >= fatura.Toplamtutar)
                        fatura.Odemedurumu = "Odendi";
                    else if (toplamOdenen > 0)
                        fatura.Odemedurumu = "KismiOdendi";

                    context.SaveChanges();

                    MessageBox.Show($"{odemeTutari:C2} tutarında ödeme alındı!", "Başarılı");
                    FaturalariYukle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}