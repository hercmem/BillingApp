﻿// <auto-generated />
using BillingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BillingApp.Migrations
{
    [DbContext(typeof(BillingDbContext))]
    [Migration("20250127231214_SyncAppUsersTable")]
    partial class SyncAppUsersTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BillingApp.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Admin_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_id");

                    b.HasKey("AdminId");

                    b.HasIndex("UserId");

                    b.ToTable("admin", (string)null);
                });

            modelBuilder.Entity("BillingApp.Models.AppUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasDefaultValue("example@example.com");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("First_Name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Last_Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValue("defaultpassword");

                    b.Property<string>("Property")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("app_users", (string)null);
                });

            modelBuilder.Entity("BillingApp.Models.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Bill_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillId"));

                    b.Property<decimal>("Costs")
                        .HasColumnType("decimal(7, 2)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("phoneNumber");

                    b.HasKey("BillId");

                    b.HasIndex("PhoneNumber");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("BillingApp.Models.BillsCall", b =>
                {
                    b.Property<int>("BillId")
                        .HasColumnType("int")
                        .HasColumnName("Bill_ID");

                    b.Property<int>("CallId")
                        .HasColumnType("int")
                        .HasColumnName("Call_ID");

                    b.HasIndex("BillId");

                    b.HasIndex("CallId");

                    b.ToTable("BillsCalls");
                });

            modelBuilder.Entity("BillingApp.Models.Call", b =>
                {
                    b.Property<int>("CallId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Call_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CallId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CallId");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("BillingApp.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Client_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Afm")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("AFM");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("phoneNumber");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_id");

                    b.HasKey("ClientId");

                    b.HasIndex("PhoneNumber");

                    b.HasIndex("UserId");

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("BillingApp.Models.Phone", b =>
                {
                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("phoneNumber");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("programName");

                    b.HasKey("PhoneNumber");

                    b.HasIndex("ProgramName");

                    b.ToTable("phones", (string)null);
                });

            modelBuilder.Entity("BillingApp.Models.PhoneProgram", b =>
                {
                    b.Property<string>("ProgramName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("programName");

                    b.Property<string>("Benfits")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("benfits");

                    b.Property<decimal>("Charge")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("ProgramName")
                        .HasName("PK_programs");

                    b.ToTable("phonePrograms", (string)null);
                });

            modelBuilder.Entity("BillingApp.Models.Seller", b =>
                {
                    b.Property<int>("SellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Seller_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SellerId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_id");

                    b.HasKey("SellerId");

                    b.HasIndex("UserId");

                    b.ToTable("sellers", (string)null);
                });

            modelBuilder.Entity("BillingApp.Models.Admin", b =>
                {
                    b.HasOne("BillingApp.Models.AppUser", "User")
                        .WithMany("Admins")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_admin_app_users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BillingApp.Models.Bill", b =>
                {
                    b.HasOne("BillingApp.Models.Phone", "PhoneNumberNavigation")
                        .WithMany("Bills")
                        .HasForeignKey("PhoneNumber")
                        .IsRequired()
                        .HasConstraintName("FK_Bills_phones1");

                    b.Navigation("PhoneNumberNavigation");
                });

            modelBuilder.Entity("BillingApp.Models.BillsCall", b =>
                {
                    b.HasOne("BillingApp.Models.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .IsRequired()
                        .HasConstraintName("FK_BillsCalls_Bills");

                    b.HasOne("BillingApp.Models.Call", "Call")
                        .WithMany()
                        .HasForeignKey("CallId")
                        .IsRequired()
                        .HasConstraintName("FK_BillsCalls_Calls");

                    b.Navigation("Bill");

                    b.Navigation("Call");
                });

            modelBuilder.Entity("BillingApp.Models.Client", b =>
                {
                    b.HasOne("BillingApp.Models.Phone", "PhoneNumberNavigation")
                        .WithMany("Clients")
                        .HasForeignKey("PhoneNumber")
                        .IsRequired()
                        .HasConstraintName("FK_clients_phones1");

                    b.HasOne("BillingApp.Models.AppUser", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_clients_app_users");

                    b.Navigation("PhoneNumberNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BillingApp.Models.Phone", b =>
                {
                    b.HasOne("BillingApp.Models.PhoneProgram", "ProgramNameNavigation")
                        .WithMany("Phones")
                        .HasForeignKey("ProgramName")
                        .IsRequired()
                        .HasConstraintName("FK_phones_programs");

                    b.Navigation("ProgramNameNavigation");
                });

            modelBuilder.Entity("BillingApp.Models.Seller", b =>
                {
                    b.HasOne("BillingApp.Models.AppUser", "User")
                        .WithMany("Sellers")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_sellers_app_users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BillingApp.Models.AppUser", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Clients");

                    b.Navigation("Sellers");
                });

            modelBuilder.Entity("BillingApp.Models.Phone", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("BillingApp.Models.PhoneProgram", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
