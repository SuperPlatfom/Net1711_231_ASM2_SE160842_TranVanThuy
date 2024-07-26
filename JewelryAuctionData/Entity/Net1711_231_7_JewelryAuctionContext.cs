using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace JewelryAuctionData.Entity
{
    public partial class Net1711_231_7_JewelryAuctionContext : DbContext
    {
        public Net1711_231_7_JewelryAuctionContext()
        {
        }

        public Net1711_231_7_JewelryAuctionContext(DbContextOptions<Net1711_231_7_JewelryAuctionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AuctionResult> AuctionResults { get; set; } = null!;
        public virtual DbSet<AuctionSection> AuctionSections { get; set; } = null!;
        public virtual DbSet<Bidder> Bidders { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Jewelry> Jewelries { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<RequestAuction> RequestAuctions { get; set; } = null!;
        public virtual DbSet<RequestAuctionDetail> RequestAuctionDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string ConnectionStr = config.GetConnectionString("DB");

                optionsBuilder.UseSqlServer(ConnectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK_Account_1");

                entity.ToTable("Account");

                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Customer");
            });
            modelBuilder.Entity<AuctionResult>(entity =>
            {
                entity.ToTable("AuctionResult");

                entity.Property(e => e.AuctionResultId).HasColumnName("AuctionResultID");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.AuctionId).HasColumnName("AuctionID");

                entity.Property(e => e.BidderId).HasColumnName("BidderID");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TransactionTime).HasColumnType("datetime");

                entity.HasOne(d => d.Auction)
                    .WithMany(p => p.AuctionResults)
                    .HasForeignKey(d => d.AuctionId)
                    .HasConstraintName("FK__AuctionRe__Aucti__4E88ABD4");

                entity.HasOne(d => d.Bidder)
                    .WithMany(p => p.AuctionResults)
                    .HasForeignKey(d => d.BidderId)
                    .HasConstraintName("FK__AuctionRe__Bidde__4F7CD00D");
            });

            modelBuilder.Entity<AuctionSection>(entity =>
            {
                entity.HasKey(e => e.AuctionId)
                    .HasName("PK__AuctionS__51004A2C80ACB0A4");

                entity.ToTable("AuctionSection");

                entity.Property(e => e.AuctionId).HasColumnName("AuctionID");

                entity.Property(e => e.BidderId).HasColumnName("BidderID");

                entity.Property(e => e.Discription).HasMaxLength(255);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.InitialPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.JewelryId).HasColumnName("JewelryID");

                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Bidder)
                    .WithMany(p => p.AuctionSections)
                    .HasForeignKey(d => d.BidderId)
                    .HasConstraintName("FK__AuctionSe__Bidde__4AB81AF0");

                entity.HasOne(d => d.Jewelry)
                    .WithMany(p => p.AuctionSections)
                    .HasForeignKey(d => d.JewelryId)
                    .HasConstraintName("FK__AuctionSe__Jewel__49C3F6B7");

                entity.HasOne(d => d.RequestDetail)
                    .WithMany(p => p.AuctionSections)
                    .HasForeignKey(d => d.RequestDetailId)
                    .HasConstraintName("FK__AuctionSe__Reque__4BAC3F29");
            });

            modelBuilder.Entity<Bidder>(entity =>
            {
                entity.ToTable("Bidder");

                entity.Property(e => e.BidderId).HasColumnName("BidderID");

                entity.Property(e => e.CurrentBidPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bidders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Bidder__Customer__3E52440B");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CompanyName).HasMaxLength(255);

                entity.Property(e => e.Discription).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Customer__Compan__398D8EEE");
            });

            modelBuilder.Entity<Jewelry>(entity =>
            {
                entity.ToTable("Jewelry");

                entity.Property(e => e.JewelryId).HasColumnName("JewelryID");

                entity.Property(e => e.Discription).HasMaxLength(255);

                entity.Property(e => e.JewelryName).HasMaxLength(255);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.AuctionResultId).HasColumnName("AuctionResultID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Fees).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.JewelryId).HasColumnName("JewelryID");

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.PaymentStatus).HasMaxLength(50);

                entity.Property(e => e.PaymentTime).HasColumnType("datetime");

                entity.Property(e => e.Percent).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.AuctionResult)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AuctionResultId)
                    .HasConstraintName("FK__Payment__Auction__52593CB8");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Payment__Custome__534D60F1");

                entity.HasOne(d => d.Jewelry)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.JewelryId)
                    .HasConstraintName("FK__Payment__Jewelry__5441852A");
            });

            modelBuilder.Entity<RequestAuction>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__RequestA__33A8519A0F6C26E2");

                entity.ToTable("RequestAuction");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.JewelryId).HasColumnName("JewelryID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RequestAuctions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__RequestAu__Custo__412EB0B6");

                entity.HasOne(d => d.Jewelry)
                    .WithMany(p => p.RequestAuctions)
                    .HasForeignKey(d => d.JewelryId)
                    .HasConstraintName("FK__RequestAu__Jewel__4222D4EF");
            });

            modelBuilder.Entity<RequestAuctionDetail>(entity =>
            {
                entity.HasKey(e => e.RequestDetailId)
                    .HasName("PK__RequestA__DC528B7041C53479");

                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.JewelryId).HasColumnName("JewelryID");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RequestAuctionDetails)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__RequestAu__Custo__45F365D3");

                entity.HasOne(d => d.Jewelry)
                    .WithMany(p => p.RequestAuctionDetails)
                    .HasForeignKey(d => d.JewelryId)
                    .HasConstraintName("FK__RequestAu__Jewel__46E78A0C");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestAuctionDetails)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__RequestAu__Reque__44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
