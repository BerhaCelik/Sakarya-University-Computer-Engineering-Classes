namespace DisKlinigiYonetimSistemi
{
    partial class FormGelirRaporu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBaslangic = new System.Windows.Forms.DateTimePicker();
            this.dtpBitis = new System.Windows.Forms.DateTimePicker();
            this.btnRaporGetir = new System.Windows.Forms.Button();
            this.btnBuAy = new System.Windows.Forms.Button();
            this.btnBuYil = new System.Windows.Forms.Button();
            this.lblToplamFatura = new System.Windows.Forms.Label();
            this.lblToplamOdenen = new System.Windows.Forms.Label();
            this.lblBekleyenTutar = new System.Windows.Forms.Label();
            this.lblFaturaSayisi = new System.Windows.Forms.Label();
            this.lblOdenenFatura = new System.Windows.Forms.Label();
            this.btnKapat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Başlangıç Tarihi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bitiş Tarihi:";
            // 
            // dtpBaslangic
            // 
            this.dtpBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBaslangic.Location = new System.Drawing.Point(110, 17);
            this.dtpBaslangic.Name = "dtpBaslangic";
            this.dtpBaslangic.Size = new System.Drawing.Size(150, 20);
            this.dtpBaslangic.TabIndex = 2;
            // 
            // dtpBitis
            // 
            this.dtpBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBitis.Location = new System.Drawing.Point(370, 17);
            this.dtpBitis.Name = "dtpBitis";
            this.dtpBitis.Size = new System.Drawing.Size(150, 20);
            this.dtpBitis.TabIndex = 3;
            // 
            // btnRaporGetir
            // 
            this.btnRaporGetir.BackColor = System.Drawing.Color.LightBlue;
            this.btnRaporGetir.Location = new System.Drawing.Point(540, 15);
            this.btnRaporGetir.Name = "btnRaporGetir";
            this.btnRaporGetir.Size = new System.Drawing.Size(100, 25);
            this.btnRaporGetir.TabIndex = 4;
            this.btnRaporGetir.Text = "Rapor Getir";
            this.btnRaporGetir.UseVisualStyleBackColor = false;
            this.btnRaporGetir.Click += new System.EventHandler(this.btnRaporGetir_Click);
            // 
            // btnBuAy
            // 
            this.btnBuAy.Location = new System.Drawing.Point(650, 15);
            this.btnBuAy.Name = "btnBuAy";
            this.btnBuAy.Size = new System.Drawing.Size(80, 25);
            this.btnBuAy.TabIndex = 5;
            this.btnBuAy.Text = "Bu Ay";
            this.btnBuAy.UseVisualStyleBackColor = true;
            this.btnBuAy.Click += new System.EventHandler(this.btnBuAy_Click);
            // 
            // btnBuYil
            // 
            this.btnBuYil.Location = new System.Drawing.Point(740, 15);
            this.btnBuYil.Name = "btnBuYil";
            this.btnBuYil.Size = new System.Drawing.Size(80, 25);
            this.btnBuYil.TabIndex = 6;
            this.btnBuYil.Text = "Bu Yıl";
            this.btnBuYil.UseVisualStyleBackColor = true;
            this.btnBuYil.Click += new System.EventHandler(this.btnBuYil_Click);
            // 
            // lblToplamFatura
            // 
            this.lblToplamFatura.AutoSize = true;
            this.lblToplamFatura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamFatura.Location = new System.Drawing.Point(50, 80);
            this.lblToplamFatura.Name = "lblToplamFatura";
            this.lblToplamFatura.Size = new System.Drawing.Size(222, 20);
            this.lblToplamFatura.TabIndex = 7;
            this.lblToplamFatura.Text = "Toplam Fatura Tutarı: -";
            // 
            // lblToplamOdenen
            // 
            this.lblToplamOdenen.AutoSize = true;
            this.lblToplamOdenen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamOdenen.ForeColor = System.Drawing.Color.Green;
            this.lblToplamOdenen.Location = new System.Drawing.Point(50, 120);
            this.lblToplamOdenen.Name = "lblToplamOdenen";
            this.lblToplamOdenen.Size = new System.Drawing.Size(224, 20);
            this.lblToplamOdenen.TabIndex = 8;
            this.lblToplamOdenen.Text = "Toplam Ödenen Tutar: -";
            // 
            // lblBekleyenTutar
            // 
            this.lblBekleyenTutar.AutoSize = true;
            this.lblBekleyenTutar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBekleyenTutar.ForeColor = System.Drawing.Color.Red;
            this.lblBekleyenTutar.Location = new System.Drawing.Point(50, 160);
            this.lblBekleyenTutar.Name = "lblBekleyenTutar";
            this.lblBekleyenTutar.Size = new System.Drawing.Size(161, 20);
            this.lblBekleyenTutar.TabIndex = 9;
            this.lblBekleyenTutar.Text = "Bekleyen Tutar: -";
            // 
            // lblFaturaSayisi
            // 
            this.lblFaturaSayisi.AutoSize = true;
            this.lblFaturaSayisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFaturaSayisi.Location = new System.Drawing.Point(50, 220);
            this.lblFaturaSayisi.Name = "lblFaturaSayisi";
            this.lblFaturaSayisi.Size = new System.Drawing.Size(177, 18);
            this.lblFaturaSayisi.TabIndex = 10;
            this.lblFaturaSayisi.Text = "Toplam Fatura Sayısı: -";
            // 
            // lblOdenenFatura
            // 
            this.lblOdenenFatura.AutoSize = true;
            this.lblOdenenFatura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOdenenFatura.Location = new System.Drawing.Point(50, 250);
            this.lblOdenenFatura.Name = "lblOdenenFatura";
            this.lblOdenenFatura.Size = new System.Drawing.Size(182, 18);
            this.lblOdenenFatura.TabIndex = 11;
            this.lblOdenenFatura.Text = "Ödenen Fatura Sayısı: -";
            // 
            // btnKapat
            // 
            this.btnKapat.Location = new System.Drawing.Point(740, 450);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(100, 30);
            this.btnKapat.TabIndex = 12;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.UseVisualStyleBackColor = true;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // FormGelirRaporu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.lblOdenenFatura);
            this.Controls.Add(this.lblFaturaSayisi);
            this.Controls.Add(this.lblBekleyenTutar);
            this.Controls.Add(this.lblToplamOdenen);
            this.Controls.Add(this.lblToplamFatura);
            this.Controls.Add(this.btnBuYil);
            this.Controls.Add(this.btnBuAy);
            this.Controls.Add(this.btnRaporGetir);
            this.Controls.Add(this.dtpBitis);
            this.Controls.Add(this.dtpBaslangic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormGelirRaporu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gelir Raporu";
            this.Load += new System.EventHandler(this.FormGelirRaporu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBaslangic;
        private System.Windows.Forms.DateTimePicker dtpBitis;
        private System.Windows.Forms.Button btnRaporGetir;
        private System.Windows.Forms.Button btnBuAy;
        private System.Windows.Forms.Button btnBuYil;
        private System.Windows.Forms.Label lblToplamFatura;
        private System.Windows.Forms.Label lblToplamOdenen;
        private System.Windows.Forms.Label lblBekleyenTutar;
        private System.Windows.Forms.Label lblFaturaSayisi;
        private System.Windows.Forms.Label lblOdenenFatura;
        private System.Windows.Forms.Button btnKapat;
    }
}