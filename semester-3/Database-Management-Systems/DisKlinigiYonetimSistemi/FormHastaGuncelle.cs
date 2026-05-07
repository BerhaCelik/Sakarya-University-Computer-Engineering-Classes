using System;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormHastaGuncelle : Form
    {
        private int _hastaId;

        public FormHastaGuncelle(int hastaId)
        {
            InitializeComponent();
            _hastaId = hastaId;
            dtpDogumTarihi.MaxDate = DateTime.Now.AddDays(-1);
        }

        private void FormHastaGuncelle_Load(object sender, EventArgs e)
        {
            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var hasta = context.Hasta.Find(_hastaId);

                    if (hasta != null)
                    {
                        txtAd.Text = hasta.Ad;
                        txtSoyad.Text = hasta.Soyad;
                        txtTCKN.Text = hasta.Tckn;
                        txtTelefon.Text = hasta.Telefon;
                        txtEmail.Text = hasta.Email;
                        txtAdres.Text = hasta.Adres;
                        dtpDogumTarihi.Value = DateTime.Now.AddYears(-25);
                    }
                    else
                    {
                        MessageBox.Show("Hasta bulunamadı!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Ad alanı boş bırakılamaz!");
                return;
            }

            try
            {
                using (var context = new DisKlinigiContext())
                {
                    var hasta = context.Hasta.Find(_hastaId);

                    if (hasta != null)
                    {
                        hasta.Ad = txtAd.Text.Trim();
                        hasta.Soyad = txtSoyad.Text.Trim();
                        hasta.Dogumtarihi = dtpDogumTarihi.Value.Date;
                        hasta.Telefon = txtTelefon.Text.Trim();
                        hasta.Email = txtEmail.Text.Trim();
                        hasta.Adres = txtAdres.Text.Trim();

                        context.SaveChanges();
                        MessageBox.Show("Hasta güncellendi!", "Başarılı");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}