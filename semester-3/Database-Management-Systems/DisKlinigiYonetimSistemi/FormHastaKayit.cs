using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormHastaKayit : Form
    {
        public FormHastaKayit()
        {
            InitializeComponent();
            // Doğum tarihi varsayılan değeri (25 yaşında biri)
            dtpDogumTarihi.Value = DateTime.Now.AddYears(-25);
            dtpDogumTarihi.MaxDate = DateTime.Now.AddDays(-1); // En fazla dün
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Validasyon - Ad kontrolü
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Ad alanı boş bırakılamaz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAd.Focus();
                return;
            }

            // Validasyon - Soyad kontrolü
            if (string.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                MessageBox.Show("Soyad alanı boş bırakılamaz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoyad.Focus();
                return;
            }

            // Validasyon - TCKN kontrolü
            if (txtTCKN.Text.Length != 11)
            {
                MessageBox.Show("TCKN 11 haneli olmalıdır!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTCKN.Focus();
                return;
            }

            // Validasyon - Telefon kontrolü
            if (string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                MessageBox.Show("Telefon alanı boş bırakılamaz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefon.Focus();
                return;
            }

            try
            {
                using (var context = new DisKlinigiContext())
                {
                    // TCKN tekrar kontrolü
                    bool tcknVar = context.Hasta.Any(h => h.Tckn == txtTCKN.Text);
                    if (tcknVar)
                    {
                        MessageBox.Show("Bu TCKN ile kayıtlı hasta zaten var!",
                            "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Yeni hasta oluştur
                    var yeniHasta = new Hasta
                    {
                        Ad = txtAd.Text.Trim(),
                        Soyad = txtSoyad.Text.Trim(),
                        Tckn = txtTCKN.Text.Trim(),
                        Dogumtarihi = dtpDogumTarihi.Value.Date,
                        Telefon = txtTelefon.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Adres = txtAdres.Text.Trim(),
                        Kayittarihi = DateTime.Now
                    };

                    context.Hasta.Add(yeniHasta);
                    context.SaveChanges();

                    MessageBox.Show("Hasta başarıyla kaydedildi!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TemizleFormu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TemizleFormu()
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtTCKN.Clear();
            txtTelefon.Clear();
            txtEmail.Clear();
            txtAdres.Clear();

            dtpDogumTarihi.Value = DateTime.Now.AddYears(-25);

            txtAd.Focus();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
