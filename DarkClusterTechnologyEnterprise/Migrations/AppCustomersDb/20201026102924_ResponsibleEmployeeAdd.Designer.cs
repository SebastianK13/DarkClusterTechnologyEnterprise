﻿// <auto-generated />
using System;
using DarkClusterTechnologyEnterprise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DarkClusterTechnologyEnterprise.Migrations.AppCustomersDb
{
    [DbContext(typeof(AppCustomersDbContext))]
    [Migration("20201026102924_ResponsibleEmployeeAdd")]
    partial class ResponsibleEmployeeAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CLocationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponsibleEmployee")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CLocationId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.CustomerLocation", b =>
                {
                    b.Property<string>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("CustomersLocation");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Invoice", b =>
                {
                    b.Property<string>("InvoiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InvoiceDetailsId")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<string>("ResponsibleEmployee")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InvoiceDetailsId");

                    b.HasIndex("PaymentId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.InvoiceDetails", b =>
                {
                    b.Property<int>("DetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DetailsId");

                    b.ToTable("InvoicesDetails");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDeadline")
                        .HasColumnType("datetime2");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InvoiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ServicePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ServiceId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Customer", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.CustomerLocation", "CustomerLocation")
                        .WithMany()
                        .HasForeignKey("CLocationId");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Invoice", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.InvoiceDetails", "InvoiceDetails")
                        .WithMany()
                        .HasForeignKey("InvoiceDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Service", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId");
                });
#pragma warning restore 612, 618
        }
    }
}