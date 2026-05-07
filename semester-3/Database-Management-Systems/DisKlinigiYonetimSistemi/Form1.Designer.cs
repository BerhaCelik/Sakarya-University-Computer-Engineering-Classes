namespace DisKlinigiYonetimSistemi
{
    partial class FormAnaMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.randevuİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHastaKayit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHastaListesi = new System.Windows.Forms.ToolStripMenuItem();
            this.randevuİşlemleriToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuYeniRandevu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRandevuListesi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOdemeAl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFaturaListesi = new System.Windows.Forms.ToolStripMenuItem();
            this.ödemeAlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAylikRapor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHastaRaporu = new System.Windows.Forms.ToolStripMenuItem();
            this.aylıkRaporToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKritikStok = new System.Windows.Forms.ToolStripMenuItem();
            this.stokYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randevuİşlemleriToolStripMenuItem,
            this.randevuİşlemleriToolStripMenuItem1,
            this.menuOdemeAl,
            this.menuAylikRapor});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // randevuİşlemleriToolStripMenuItem
            // 
            this.randevuİşlemleriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHastaKayit,
            this.menuHastaListesi});
            this.randevuİşlemleriToolStripMenuItem.Name = "randevuİşlemleriToolStripMenuItem";
            this.randevuİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.randevuİşlemleriToolStripMenuItem.Text = "Hasta İşlemleri";
            // 
            // menuHastaKayit
            // 
            this.menuHastaKayit.Name = "menuHastaKayit";
            this.menuHastaKayit.Size = new System.Drawing.Size(139, 22);
            this.menuHastaKayit.Text = "Hasta Kayıt";
            this.menuHastaKayit.Click += new System.EventHandler(this.menuHastaKayit_Click);
            // 
            // menuHastaListesi
            // 
            this.menuHastaListesi.Name = "menuHastaListesi";
            this.menuHastaListesi.Size = new System.Drawing.Size(139, 22);
            this.menuHastaListesi.Text = "Hasta Listesi";
            this.menuHastaListesi.Click += new System.EventHandler(this.menuHastaListesi_Click);
            // 
            // randevuİşlemleriToolStripMenuItem1
            // 
            this.randevuİşlemleriToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuYeniRandevu,
            this.menuRandevuListesi});
            this.randevuİşlemleriToolStripMenuItem1.Name = "randevuİşlemleriToolStripMenuItem1";
            this.randevuİşlemleriToolStripMenuItem1.Size = new System.Drawing.Size(112, 20);
            this.randevuİşlemleriToolStripMenuItem1.Text = "Randevu İşlemleri";
            // 
            // menuYeniRandevu
            // 
            this.menuYeniRandevu.Name = "menuYeniRandevu";
            this.menuYeniRandevu.Size = new System.Drawing.Size(180, 22);
            this.menuYeniRandevu.Text = "Yeni Randevu";
            this.menuYeniRandevu.Click += new System.EventHandler(this.menuYeniRandevu_Click);
            // 
            // menuRandevuListesi
            // 
            this.menuRandevuListesi.Name = "menuRandevuListesi";
            this.menuRandevuListesi.Size = new System.Drawing.Size(180, 22);
            this.menuRandevuListesi.Text = "Randevu Listesi";
            this.menuRandevuListesi.Click += new System.EventHandler(this.menuRandevuListesi_Click);
            // 
            // menuOdemeAl
            // 
            this.menuOdemeAl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFaturaListesi,
            this.ödemeAlToolStripMenuItem});
            this.menuOdemeAl.Name = "menuOdemeAl";
            this.menuOdemeAl.Size = new System.Drawing.Size(99, 20);
            this.menuOdemeAl.Text = "Fatura İşlemleri";
            // 
            // menuFaturaListesi
            // 
            this.menuFaturaListesi.Name = "menuFaturaListesi";
            this.menuFaturaListesi.Size = new System.Drawing.Size(180, 22);
            this.menuFaturaListesi.Text = "Fatura Listesi";
            this.menuFaturaListesi.Click += new System.EventHandler(this.menuFaturaListesi_Click);
            // 
            // ödemeAlToolStripMenuItem
            // 
            this.ödemeAlToolStripMenuItem.Name = "ödemeAlToolStripMenuItem";
            this.ödemeAlToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ödemeAlToolStripMenuItem.Text = "Ödeme Al";
            // 
            // menuAylikRapor
            // 
            this.menuAylikRapor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHastaRaporu,
            this.aylıkRaporToolStripMenuItem,
            this.menuKritikStok,
            this.stokYönetimiToolStripMenuItem});
            this.menuAylikRapor.Name = "menuAylikRapor";
            this.menuAylikRapor.Size = new System.Drawing.Size(63, 20);
            this.menuAylikRapor.Text = "Raporlar";
            // 
            // menuHastaRaporu
            // 
            this.menuHastaRaporu.Name = "menuHastaRaporu";
            this.menuHastaRaporu.Size = new System.Drawing.Size(180, 22);
            this.menuHastaRaporu.Text = "Hasta Raporu";
            this.menuHastaRaporu.Click += new System.EventHandler(this.menuHastaRaporu_Click);
            // 
            // aylıkRaporToolStripMenuItem
            // 
            this.aylıkRaporToolStripMenuItem.Name = "aylıkRaporToolStripMenuItem";
            this.aylıkRaporToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aylıkRaporToolStripMenuItem.Text = "Gelir Rapor";
            this.aylıkRaporToolStripMenuItem.Click += new System.EventHandler(this.aylıkRaporToolStripMenuItem_Click);
            // 
            // menuKritikStok
            // 
            this.menuKritikStok.Name = "menuKritikStok";
            this.menuKritikStok.Size = new System.Drawing.Size(180, 22);
            this.menuKritikStok.Text = "Kritik Stok";
            this.menuKritikStok.Click += new System.EventHandler(this.menuKritikStok_Click);
            // 
            // stokYönetimiToolStripMenuItem
            // 
            this.stokYönetimiToolStripMenuItem.Name = "stokYönetimiToolStripMenuItem";
            this.stokYönetimiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stokYönetimiToolStripMenuItem.Text = "Stok Yönetimi";
            this.stokYönetimiToolStripMenuItem.Click += new System.EventHandler(this.stokYönetimiToolStripMenuItem_Click);
            // 
            // FormAnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAnaMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diş Kliniği Yönetim Sistemi";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem randevuİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuHastaKayit;
        private System.Windows.Forms.ToolStripMenuItem menuHastaListesi;
        private System.Windows.Forms.ToolStripMenuItem randevuİşlemleriToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuYeniRandevu;
        private System.Windows.Forms.ToolStripMenuItem menuRandevuListesi;
        private System.Windows.Forms.ToolStripMenuItem menuOdemeAl;
        private System.Windows.Forms.ToolStripMenuItem menuFaturaListesi;
        private System.Windows.Forms.ToolStripMenuItem ödemeAlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAylikRapor;
        private System.Windows.Forms.ToolStripMenuItem menuHastaRaporu;
        private System.Windows.Forms.ToolStripMenuItem aylıkRaporToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuKritikStok;
        private System.Windows.Forms.ToolStripMenuItem stokYönetimiToolStripMenuItem;
    }
}

