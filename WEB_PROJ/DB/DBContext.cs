using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WEB_PROJ
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DB;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "email_constraint")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('user_id_seq'::regclass)");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.DateBirth).HasColumnName("date_birth");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.Facebook)
                    .HasMaxLength(25)
                    .HasColumnName("facebook");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .HasColumnName("first_name");

                entity.Property(e => e.Instagram)
                    .HasMaxLength(25)
                    .HasColumnName("instagram");

                entity.Property(e => e.IsVerificated)
                    .HasColumnName("is_verificated")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .HasColumnName("last_name");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("login");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("password");

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("password_salt");

                entity.Property(e => e.PatronymicName)
                    .HasMaxLength(25)
                    .HasColumnName("patronymic_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.PursePerfectMoneyUsd)
                    .HasMaxLength(50)
                    .HasColumnName("purse_perfect_money_usd");

                entity.Property(e => e.RegionName)
                    .HasMaxLength(50)
                    .HasColumnName("region_name");

                entity.Property(e => e.Skype)
                    .HasMaxLength(25)
                    .HasColumnName("skype");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Telegram)
                    .HasMaxLength(25)
                    .HasColumnName("telegram");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(25)
                    .HasColumnName("twitter");

                entity.Property(e => e.Vk)
                    .HasMaxLength(25)
                    .HasColumnName("vk");

                entity.Property(e => e.Youtube)
                    .HasMaxLength(25)
                    .HasColumnName("youtube");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("users_status_id_fkey");
            });

            modelBuilder.HasSequence("user_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
