using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace lab2.Models;

public partial class BankDepositsContext : DbContext
{
    public BankDepositsContext()
    {
    }

    public BankDepositsContext(DbContextOptions<BankDepositsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amountofbill> Amountofbills { get; set; }

    public virtual DbSet<Bankdeposit> Bankdeposits { get; set; }

    public virtual DbSet<Contributor> Contributors { get; set; }

//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=BankDeposits;Username=postgres;Password=12345678");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Username=postgres;Password=12345678;Database=BankDeposits");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amountofbill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("amountofbills_pkey");

            entity.ToTable("amountofbills");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ContributorId).HasColumnName("contributor_id");

            entity.HasOne(d => d.Contributor).WithMany(p => p.Amountofbills)
                .HasForeignKey(d => d.ContributorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("amountofbills_contributor_id_fkey");
        });

        modelBuilder.Entity<Bankdeposit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bankdeposits_pkey");

            entity.ToTable("bankdeposits");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContributorId).HasColumnName("contributor_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DepositAmount).HasColumnName("deposit_amount");

            entity.HasOne(d => d.Contributor).WithMany(p => p.Bankdeposits)
                .HasForeignKey(d => d.ContributorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bankdeposits_contributor_id_fkey");
        });

        modelBuilder.Entity<Contributor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contributors_pkey");

            entity.ToTable("contributors");

            entity.HasIndex(e => new { e.PassportSerial, e.PassportNumber }, "contributors_passport_serial_passport_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(20)
                .HasColumnName("account_number");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(20)
                .HasColumnName("passport_number");
            entity.Property(e => e.PassportSerial)
                .HasMaxLength(10)
                .HasColumnName("passport_serial");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
