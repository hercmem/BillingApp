using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BillingApp.Models;

public partial class BillingDbContext : DbContext
{
    public BillingDbContext()
    {
    }

    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillsCall> BillsCalls { get; set; }

    public virtual DbSet<Call> Calls { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneProgram> PhonePrograms { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-LMTM1U2\\SQLEXPRESS;Database=BillingDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("admin");

            entity.Property(e => e.AdminId).HasColumnName("Admin_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_admin_app_users");
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("app_users");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("example@example.com");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("defaultpassword");
            entity.Property(e => e.Property)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.Property(e => e.BillId).HasColumnName("Bill_ID");
            entity.Property(e => e.Costs).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");

            entity.Property(e => e.IsPaid)
                .HasColumnName("IsPaid")
                .HasDefaultValue(false);

            entity.HasOne(d => d.PhoneNumberNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.PhoneNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bills_phones1");
        });

        modelBuilder.Entity<BillsCall>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.BillId).HasColumnName("Bill_ID");
            entity.Property(e => e.CallId).HasColumnName("Call_ID");

            entity.HasOne(d => d.Bill).WithMany()
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillsCalls_Bills");

            entity.HasOne(d => d.Call).WithMany()
                .HasForeignKey(d => d.CallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillsCalls_Calls");
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.Property(e => e.CallId).HasColumnName("Call_ID");
            entity.Property(e => e.Description).HasColumnType("text");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients");

            entity.Property(e => e.ClientId).HasColumnName("Client_id");
            entity.Property(e => e.Afm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AFM");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.PhoneNumberNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.PhoneNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_clients_phones1");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_clients_app_users");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => e.PhoneNumber);

            entity.ToTable("phones");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.ProgramName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("programName");

            entity.HasOne(d => d.ProgramNameNavigation).WithMany(p => p.Phones)
                .HasForeignKey(d => d.ProgramName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_phones_programs");
        });

        modelBuilder.Entity<PhoneProgram>(entity =>
        {
            entity.HasKey(e => e.ProgramName).HasName("PK_programs");

            entity.ToTable("phonePrograms");

            entity.Property(e => e.ProgramName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("programName");
            entity.Property(e => e.Benfits)
                .HasColumnType("text")
                .HasColumnName("benfits");
            entity.Property(e => e.Charge).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.ToTable("sellers");

            entity.Property(e => e.SellerId).HasColumnName("Seller_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Sellers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sellers_app_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
