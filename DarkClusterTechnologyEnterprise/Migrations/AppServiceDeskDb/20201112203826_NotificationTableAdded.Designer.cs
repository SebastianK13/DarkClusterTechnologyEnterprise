﻿// <auto-generated />
using System;
using DarkClusterTechnologyEnterprise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    [DbContext(typeof(AppServiceDeskDbContext))]
    [Migration("20201112203826_NotificationTableAdded")]
    partial class NotificationTableAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ServiceDeskModels+ApplicationConversation", b =>
                {
                    b.Property<string>("User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Administrator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("datetime2");

                    b.HasKey("User");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ServiceDeskModels+ScheduledWork", b =>
                {
                    b.Property<int>("ServiceWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResponsibleEmployee")
                        .HasColumnType("int");

                    b.HasKey("ServiceWorkId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ServiceDeskModels+ServiceNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NotificationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ServiceDeskModels+UserApplication", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Applicant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicationStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Critical")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Responsible")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLocation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ServiceDeskModels+ApplicationConversation", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.ServiceDeskModels+UserApplication", "UserApplication")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
