using System;
using System.Drawing;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormGelirRaporu : Form
    {
        public FormGelirRaporu()
        {
            InitializeComponent();
        }

        private void FormGelirRaporu_Load(object sender, EventArgs e)
        {
            // Varsayılan: Bu ay
            dtpBaslangic.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpBitis.Value = DateTime.Today;
        }

        private void btnRaporGetir_Click(object sender, EventArgs e)
        {
            if (dtpBaslangic.Value > dtpBitis.Value)
            {
                MessageBox.Show("Başlangıç tarihi bitiş tarihinden büyük olamaz!");
                return;
            }

            try
            {
                using (var context = new DisKlinigiContext())
                {
                    // Tarihleri string formatına çevir
                    string baslangic = dtpBaslangic.Value.ToString("yyyy-MM-dd");
                    string bitis = dtpBitis.Value.ToString("yyyy-MM-dd");

                    var sqlQuery = $"SELECT * FROM fn_tarih_araliginda_gelir('{baslangic}', '{bitis}')";

                    var connection = context.Database.GetDbConnection();
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sqlQuery;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal toplamFatura = reader.GetDecimal(0);
                                decimal toplamOdenen = reader.GetDecimal(1);
                                decimal bekleyenTutar = reader.GetDecimal(2);
                                long faturaSayisi = reader.GetInt64(3);
                                long odenenFatura = reader.GetInt64(4);

                                lblToplamFatura.Text = $"Toplam Fatura Tutarı: {toplamFatura:C2}";
                                lblToplamOdenen.Text = $"Toplam Ödenen Tutar: {toplamOdenen:C2}";
                                lblBekleyenTutar.Text = $"Bekleyen Tutar: {bekleyenTutar:C2}";
                                lblFaturaSayisi.Text = $"Toplam Fatura Sayısı: {faturaSayisi}";
                                lblOdenenFatura.Text = $"Ödenen Fatura Sayısı: {odenenFatura}";

                                lblToplamOdenen.ForeColor = Color.Green;
                                lblBekleyenTutar.ForeColor = bekleyenTutar > 0 ? Color.Red : Color.Green;
                            }
                            else
                            {
                                MessageBox.Show("Seçilen tarih aralığında fatura bulunamadı!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n\nDetay: {ex.InnerException?.Message}");
            }
        }

        private void btnBuAy_Click(object sender, EventArgs e)
        {
            dtpBaslangic.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpBitis.Value = DateTime.Today;
            btnRaporGetir_Click(null, null);
        }

        private void btnBuYil_Click(object sender, EventArgs e)
        {
            dtpBaslangic.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpBitis.Value = DateTime.Today;
            btnRaporGetir_Click(null, null);
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}