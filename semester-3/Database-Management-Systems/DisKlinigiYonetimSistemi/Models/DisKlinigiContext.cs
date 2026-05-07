using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace DisKlinigiYonetimSistemi.Models
{
    public partial class DisKlinigiContext : DbContext
    {
        public DisKlinigiContext()
        {
        }

        public DisKlinigiContext(DbContextOptions<DisKlinigiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asistan> Asistan { get; set; }
        public virtual DbSet<Calismasaati> Calismasaati { get; set; }
        public virtual DbSet<Dis> Dis { get; set; }
        public virtual DbSet<Dishekimi> Dishekimi { get; set; }
        public virtual DbSet<Diskayit> Diskayit { get; set; }
        public virtual DbSet<Fatura> Fatura { get; set; }
        public virtual DbSet<Hasta> Hasta { get; set; }
        public virtual DbSet<Ilac> Ilac { get; set; }
        public virtual DbSet<Odeme> Odeme { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<Randevu> Randevu { get; set; }
        public virtual DbSet<Randevutedavi> Randevutedavi { get; set; }
        public virtual DbSet<Recete> Recete { get; set; }
        public virtual DbSet<Receteilac> Receteilac { get; set; }
        public virtual DbSet<Resepsiyonist> Resepsiyonist { get; set; }
        public virtual DbSet<Stok> Stok { get; set; }
        public virtual DbSet<Stokhareket> Stokhareket { get; set; }
        public virtual DbSet<Tedavituru> Tedavituru { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=DisKlinigiDB;Username=postgres;Password=\"151204Berha#\"");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asistan>(entity =>
            {
                entity.HasKey(e => e.Personelid)
                    .HasName("asistan_pkey");

                entity.ToTable("asistan");

                entity.HasComment("Asistan tablosu - Personel child");

                entity.Property(e => e.Personelid)
                    .HasColumnName("personelid")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Personel)
                    .WithOne(p => p.Asistan)
                    .HasForeignKey<Asistan>(d => d.Personelid)
                    .HasConstraintName("fk_asistan_personel");
            });

            modelBuilder.Entity<Calismasaati>(entity =>
            {
                entity.ToTable("calismasaati");

                entity.HasComment("Personel çalışma saatleri - Zayıf varlık");

                entity.HasIndex(e => new { e.Personelid, e.Gun })
                    .HasName("uq_personel_gun")
                    .IsUnique();

                entity.Property(e => e.Calismasaatiid).HasColumnName("calismasaatiid");

                entity.Property(e => e.Baslangicsaati)
                    .HasColumnName("baslangicsaati")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Bitissaati)
                    .HasColumnName("bitissaati")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Gun)
                    .IsRequired()
                    .HasColumnName("gun")
                    .HasMaxLength(15);

                entity.Property(e => e.Personelid).HasColumnName("personelid");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.Calismasaati)
                    .HasForeignKey(d => d.Personelid)
                    .HasConstraintName("fk_calismasaati_personel");
            });

            modelBuilder.Entity<Dis>(entity =>
            {
                entity.HasKey(e => e.Disno)
                    .HasName("dis_pkey");

                entity.ToTable("dis");

                entity.HasComment("Diş referans tablosu (32 diş) - Güçlü varlık");

                entity.Property(e => e.Disno)
                    .HasColumnName("disno")
                    .ValueGeneratedNever();

                entity.Property(e => e.Disadi)
                    .IsRequired()
                    .HasColumnName("disadi")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dishekimi>(entity =>
            {
                entity.HasKey(e => e.Personelid)
                    .HasName("dishekimi_pkey");

                entity.ToTable("dishekimi");

                entity.HasComment("Diş hekimi tablosu - Personel child");

                entity.Property(e => e.Personelid)
                    .HasColumnName("personelid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Uzmanlikalani)
                    .HasColumnName("uzmanlikalani")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Personel)
                    .WithOne(p => p.Dishekimi)
                    .HasForeignKey<Dishekimi>(d => d.Personelid)
                    .HasConstraintName("fk_dishekimi_personel");
            });

            modelBuilder.Entity<Diskayit>(entity =>
            {
                entity.ToTable("diskayit");

                entity.HasComment("Hasta diş durumu kayıtları - Zayıf varlık");

                entity.HasIndex(e => e.Hastaid)
                    .HasName("idx_diskayit_hasta");

                entity.HasIndex(e => new { e.Hastaid, e.Disno })
                    .HasName("uq_hasta_dis")
                    .IsUnique();

                entity.Property(e => e.Diskayitid).HasColumnName("diskayitid");

                entity.Property(e => e.Disdurumu)
                    .IsRequired()
                    .HasColumnName("disdurumu")
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'Saglikli'::character varying");

                entity.Property(e => e.Disno).HasColumnName("disno");

                entity.Property(e => e.Hastaid).HasColumnName("hastaid");

                entity.Property(e => e.Sonkontroltarihi)
                    .HasColumnName("sonkontroltarihi")
                    .HasColumnType("date");

                entity.HasOne(d => d.DisnoNavigation)
                    .WithMany(p => p.Diskayit)
                    .HasForeignKey(d => d.Disno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_diskayit_dis");

                entity.HasOne(d => d.Hasta)
                    .WithMany(p => p.Diskayit)
                    .HasForeignKey(d => d.Hastaid)
                    .HasConstraintName("fk_diskayit_hasta");
            });

            modelBuilder.Entity<Fatura>(entity =>
            {
                entity.ToTable("fatura");

                entity.HasComment("Fatura kayıtları - Zayıf varlık");

                entity.HasIndex(e => e.Odemedurumu)
                    .HasName("idx_fatura_durum");

                entity.HasIndex(e => e.Randevuid)
                    .HasName("fatura_randevuid_key")
                    .IsUnique();

                entity.Property(e => e.Faturaid).HasColumnName("faturaid");

                entity.Property(e => e.Faturatarihi)
                    .HasColumnName("faturatarihi")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Odemedurumu)
                    .IsRequired()
                    .HasColumnName("odemedurumu")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'Odenmedi'::character varying");

                entity.Property(e => e.Randevuid).HasColumnName("randevuid");

                entity.Property(e => e.Toplamtutar)
                    .HasColumnName("toplamtutar")
                    .HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.Randevu)
                    .WithOne(p => p.Fatura)
                    .HasForeignKey<Fatura>(d => d.Randevuid)
                    .HasConstraintName("fk_fatura_randevu");
            });

            modelBuilder.Entity<Hasta>(entity =>
            {
                entity.ToTable("hasta");

                entity.HasComment("Hasta bilgileri - Güçlü varlık");

                entity.HasIndex(e => e.Tckn)
                    .HasName("idx_hasta_tckn");

                entity.HasIndex(e => new { e.Ad, e.Soyad })
                    .HasName("idx_hasta_adsoyad");

                entity.Property(e => e.Hastaid).HasColumnName("hastaid");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasColumnName("ad")
                    .HasMaxLength(50);

                entity.Property(e => e.Adres).HasColumnName("adres");

                entity.Property(e => e.Dogumtarihi)
                    .HasColumnName("dogumtarihi")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Kayittarihi)
                    .HasColumnName("kayittarihi")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Soyad)
                    .IsRequired()
                    .HasColumnName("soyad")
                    .HasMaxLength(50);

                entity.Property(e => e.Tckn)
                    .IsRequired()
                    .HasColumnName("tckn")
                    .HasMaxLength(11)
                    .IsFixedLength();

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnName("telefon")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Ilac>(entity =>
            {
                entity.ToTable("ilac");

                entity.HasComment("İlaç listesi - Güçlü varlık");

                entity.HasIndex(e => e.Ilacadi)
                    .HasName("ilac_ilacadi_key")
                    .IsUnique();

                entity.Property(e => e.Ilacid).HasColumnName("ilacid");

                entity.Property(e => e.Ilacadi)
                    .IsRequired()
                    .HasColumnName("ilacadi")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Odeme>(entity =>
            {
                entity.ToTable("odeme");

                entity.HasComment("Ödeme kayıtları - Zayıf varlık");

                entity.Property(e => e.Odemeid).HasColumnName("odemeid");

                entity.Property(e => e.Faturaid).HasColumnName("faturaid");

                entity.Property(e => e.Odemetarihi)
                    .HasColumnName("odemetarihi")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Odenentutar)
                    .HasColumnName("odenentutar")
                    .HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.Fatura)
                    .WithMany(p => p.Odeme)
                    .HasForeignKey(d => d.Faturaid)
                    .HasConstraintName("fk_odeme_fatura");
            });

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.ToTable("personel");

                entity.HasComment("Ana personel tablosu - Kalıtım parent");

                entity.HasIndex(e => e.Email)
                    .HasName("personel_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Tckn)
                    .HasName("idx_personel_tckn");

                entity.Property(e => e.Personelid).HasColumnName("personelid");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasColumnName("ad")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Isebaslamatarihi)
                    .HasColumnName("isebaslamatarihi")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Soyad)
                    .IsRequired()
                    .HasColumnName("soyad")
                    .HasMaxLength(50);

                entity.Property(e => e.Tckn)
                    .IsRequired()
                    .HasColumnName("tckn")
                    .HasMaxLength(11)
                    .IsFixedLength();

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnName("telefon")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Randevu>(entity =>
            {
                entity.ToTable("randevu");

                entity.HasComment("Randevu kayıtları - Zayıf varlık");

                entity.HasIndex(e => e.Dishekimiid)
                    .HasName("idx_randevu_doktor");

                entity.HasIndex(e => e.Durum)
                    .HasName("idx_randevu_durum");

                entity.HasIndex(e => e.Hastaid)
                    .HasName("idx_randevu_hasta");

                entity.HasIndex(e => new { e.Randevutarihi, e.Randevusaati })
                    .HasName("idx_randevu_tarih");

                entity.HasIndex(e => new { e.Dishekimiid, e.Randevutarihi, e.Randevusaati })
                    .HasName("uq_randevu_slot")
                    .IsUnique();

                entity.Property(e => e.Randevuid).HasColumnName("randevuid");

                entity.Property(e => e.Dishekimiid).HasColumnName("dishekimiid");

                entity.Property(e => e.Durum)
                    .IsRequired()
                    .HasColumnName("durum")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'Beklemede'::character varying");

                entity.Property(e => e.Hastaid).HasColumnName("hastaid");

                entity.Property(e => e.Randevusaati)
                    .HasColumnName("randevusaati")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Randevutarihi)
                    .HasColumnName("randevutarihi")
                    .HasColumnType("date");

                entity.HasOne(d => d.Dishekimi)
                    .WithMany(p => p.Randevu)
                    .HasForeignKey(d => d.Dishekimiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_randevu_dishekimi");

                entity.HasOne(d => d.Hasta)
                    .WithMany(p => p.Randevu)
                    .HasForeignKey(d => d.Hastaid)
                    .HasConstraintName("fk_randevu_hasta");
            });

            modelBuilder.Entity<Randevutedavi>(entity =>
            {
                entity.ToTable("randevutedavi");

                entity.HasComment("Randevu-Tedavi ara tablosu - N:M ilişki");

                entity.Property(e => e.Randevutedaviid).HasColumnName("randevutedaviid");

                entity.Property(e => e.Baslangicsaati)
                    .HasColumnName("baslangicsaati")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Bitissaati)
                    .HasColumnName("bitissaati")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Disno).HasColumnName("disno");

                entity.Property(e => e.Randevuid).HasColumnName("randevuid");

                entity.Property(e => e.Tedavituruid).HasColumnName("tedavituruid");

                entity.Property(e => e.Uygulayandishekimiid).HasColumnName("uygulayandishekimiid");

                entity.HasOne(d => d.DisnoNavigation)
                    .WithMany(p => p.Randevutedavi)
                    .HasForeignKey(d => d.Disno)
                    .HasConstraintName("fk_randevutedavi_dis");

                entity.HasOne(d => d.Randevu)
                    .WithMany(p => p.Randevutedavi)
                    .HasForeignKey(d => d.Randevuid)
                    .HasConstraintName("fk_randevutedavi_randevu");

                entity.HasOne(d => d.Tedavituru)
                    .WithMany(p => p.Randevutedavi)
                    .HasForeignKey(d => d.Tedavituruid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_randevutedavi_tedavituru");

                entity.HasOne(d => d.Uygulayandishekimi)
                    .WithMany(p => p.Randevutedavi)
                    .HasForeignKey(d => d.Uygulayandishekimiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_randevutedavi_dishekimi");
            });

            modelBuilder.Entity<Recete>(entity =>
            {
                entity.ToTable("recete");

                entity.HasComment("Reçete kayıtları - Zayıf varlık");

                entity.Property(e => e.Receteid).HasColumnName("receteid");

                entity.Property(e => e.Dishekimiid).HasColumnName("dishekimiid");

                entity.Property(e => e.Randevuid).HasColumnName("randevuid");

                entity.Property(e => e.Recetetarihi)
                    .HasColumnName("recetetarihi")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.HasOne(d => d.Dishekimi)
                    .WithMany(p => p.Recete)
                    .HasForeignKey(d => d.Dishekimiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_recete_dishekimi");

                entity.HasOne(d => d.Randevu)
                    .WithMany(p => p.Recete)
                    .HasForeignKey(d => d.Randevuid)
                    .HasConstraintName("fk_recete_randevu");
            });

            modelBuilder.Entity<Receteilac>(entity =>
            {
                entity.ToTable("receteilac");

                entity.HasComment("Reçete-İlaç ara tablosu - N:M ilişki");

                entity.HasIndex(e => new { e.Receteid, e.Ilacid })
                    .HasName("uq_recete_ilac")
                    .IsUnique();

                entity.Property(e => e.Receteilacid).HasColumnName("receteilacid");

                entity.Property(e => e.Ilacid).HasColumnName("ilacid");

                entity.Property(e => e.Receteid).HasColumnName("receteid");

                entity.HasOne(d => d.Ilac)
                    .WithMany(p => p.Receteilac)
                    .HasForeignKey(d => d.Ilacid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_receteilac_ilac");

                entity.HasOne(d => d.Recete)
                    .WithMany(p => p.Receteilac)
                    .HasForeignKey(d => d.Receteid)
                    .HasConstraintName("fk_receteilac_recete");
            });

            modelBuilder.Entity<Resepsiyonist>(entity =>
            {
                entity.HasKey(e => e.Personelid)
                    .HasName("resepsiyonist_pkey");

                entity.ToTable("resepsiyonist");

                entity.HasComment("Resepsiyonist tablosu - Personel child");

                entity.Property(e => e.Personelid)
                    .HasColumnName("personelid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Vardiyaturu)
                    .HasColumnName("vardiyaturu")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Personel)
                    .WithOne(p => p.Resepsiyonist)
                    .HasForeignKey<Resepsiyonist>(d => d.Personelid)
                    .HasConstraintName("fk_resepsiyonist_personel");
            });

            modelBuilder.Entity<Stok>(entity =>
            {
                entity.ToTable("stok");

                entity.HasComment("Malzeme stok takibi - Güçlü varlık");

                entity.HasIndex(e => e.Miktar)
                    .HasName("idx_stok_miktar")
                    .HasFilter("(miktar < minimumstok)");

                entity.HasIndex(e => e.Stokkodu)
                    .HasName("stok_stokkodu_key")
                    .IsUnique();

                entity.Property(e => e.Stokid).HasColumnName("stokid");

                entity.Property(e => e.Birim)
                    .IsRequired()
                    .HasColumnName("birim")
                    .HasMaxLength(20);

                entity.Property(e => e.Malzemeadi)
                    .IsRequired()
                    .HasColumnName("malzemeadi")
                    .HasMaxLength(100);

                entity.Property(e => e.Miktar).HasColumnName("miktar");

                entity.Property(e => e.Minimumstok).HasColumnName("minimumstok");

                entity.Property(e => e.Stokkodu)
                    .IsRequired()
                    .HasColumnName("stokkodu")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stokhareket>(entity =>
            {
                entity.HasKey(e => e.Hareketid)
                    .HasName("stokhareket_pkey");

                entity.ToTable("stokhareket");

                entity.HasComment("Stok giriş/çıkış hareketleri - Zayıf varlık");

                entity.Property(e => e.Hareketid).HasColumnName("hareketid");

                entity.Property(e => e.Harekettipi)
                    .IsRequired()
                    .HasColumnName("harekettipi")
                    .HasMaxLength(20);

                entity.Property(e => e.Miktar).HasColumnName("miktar");

                entity.Property(e => e.Stokid).HasColumnName("stokid");

                entity.Property(e => e.Tarih)
                    .HasColumnName("tarih")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Stok)
                    .WithMany(p => p.Stokhareket)
                    .HasForeignKey(d => d.Stokid)
                    .HasConstraintName("fk_stokhareket_stok");
            });

            modelBuilder.Entity<Tedavituru>(entity =>
            {
                entity.ToTable("tedavituru");

                entity.HasComment("Tedavi türleri ve fiyatları - Güçlü varlık");

                entity.HasIndex(e => e.Tedavikodu)
                    .HasName("tedavituru_tedavikodu_key")
                    .IsUnique();

                entity.Property(e => e.Tedavituruid).HasColumnName("tedavituruid");

                entity.Property(e => e.Birimfiyat)
                    .HasColumnName("birimfiyat")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Tedaviadi)
                    .IsRequired()
                    .HasColumnName("tedaviadi")
                    .HasMaxLength(100);

                entity.Property(e => e.Tedavikodu)
                    .IsRequired()
                    .HasColumnName("tedavikodu")
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
