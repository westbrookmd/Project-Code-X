using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectCodeX.Models
{
    public partial class ProjectCodeXContext : IdentityDbContext<User>
    {
        public ProjectCodeXContext()
        {
        }

        public ProjectCodeXContext(DbContextOptions<ProjectCodeXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Donation> Donations { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<PurchLineItem> PurchLineItems { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=ProjectCodeX;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Address)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("LName");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.Amount).HasColumnType("smallmoney");

                entity.Property(e => e.DonationDate).HasColumnType("date");

                entity.Property(e => e.Notes)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.AmountRaised).HasColumnType("smallmoney");

                entity.Property(e => e.Cost).HasColumnType("smallmoney");

                entity.Property(e => e.Date).HasColumnType("datetime2");

                entity.Property(e => e.EventType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Balance).HasColumnType("smallmoney");

                entity.Property(e => e.Due).HasColumnType("smallmoney");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Members__UserID__2704CA5F");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                    .HasName("PK__News__9C6270C8AA5042D2");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.Author)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PublishDate).HasColumnType("date");

                entity.Property(e => e.Summary)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchLineItem>(entity =>
            {
                entity.HasKey(e => e.PlineId)
                    .HasName("PK__PurchLin__B03E3F446C912B89");

                entity.Property(e => e.PlineId).HasColumnName("PLineID");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.PurchId).HasColumnName("PurchID");

                entity.HasOne(d => d.Purch)
                    .WithMany(p => p.PurchLineItems)
                    .HasForeignKey(d => d.PurchId)
                    .HasConstraintName("FK__PurchLine__Purch__2739D489");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.PurchId)
                    .HasName("PK__Purchase__25A0C5AE42A58AFD");

                entity.ToTable("Purchase");

                entity.Property(e => e.PurchId).HasColumnName("PurchID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("smallmoney");

                entity.Property(e => e.PurchDate).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("LName");

                entity.Property(e => e.NextBillDate).HasColumnType("date");

                entity.Property(e => e.PayMethod)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("State");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
