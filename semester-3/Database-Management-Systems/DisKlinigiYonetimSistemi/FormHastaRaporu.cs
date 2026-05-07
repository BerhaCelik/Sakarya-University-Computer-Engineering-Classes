using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DisKlinigiYonetimSistemi.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DisKlinigiYonetimSistemi
{
    public partial class FormHastaRaporu : Form
    {
        public FormHastaRaporu()
        {
            InitializeComponent();
        }

        private void FormHastaRaporu_Load(object sender, EventArgs e)
        {
            HastaListesiniYukle();
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

        private void btnRaporGetir_Click(object sender, EventArgs e)
        {
            if (cmbHasta.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen hasta seçin!", "Uyarı");
                return;
            }

            try
            {
                using (var context = new DisKlinigiContext())
                {
                    int hastaId = Convert.ToInt32(cmbHasta.SelectedValue);

                    // SQL FONKSİYONU ÇAĞIRMA
                    var sqlQuery = "SELECT * FROM fn_hasta_randevu_gecmisi(@p0)";

                    var connection = context.Database.GetDbConnection();
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sqlQuery;
                        command.Parameters.Add(new NpgsqlParameter("p0", hastaId));

                        using (var reader = command.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);

                            dgvRapor.DataSource = dt;

                            if (dt.Rows.Count > 0)
                            {
                                // Sütun başlıklarını düzenle
                                foreach (DataGridViewColumn col in dgvRapor.Columns)
                                {
                                    col.HeaderText = col.Name.ToUpper();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bu hasta için randevu kaydı bulunamadı!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n\nNot: SQL fonksiyonu mevcut değil olabilir.");
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}