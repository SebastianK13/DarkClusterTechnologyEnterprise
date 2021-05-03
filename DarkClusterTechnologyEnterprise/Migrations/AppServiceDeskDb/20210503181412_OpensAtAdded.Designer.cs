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
    [Migration("20210503181412_OpensAtAdded")]
    partial class OpensAtAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ApplicationConversation", b =>
                {
                    b.Property<string>("User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Administrator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("User");

                    b.HasIndex("RequestId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.AssigmentGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.ToTable("AssigmentGroup");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Categorization", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Categorizations");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.CloserDue", b =>
                {
                    b.Property<int>("CloserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Due")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CloserId");

                    b.ToTable("CloserDues");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Impact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Impacts");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Incident", b =>
                {
                    b.Property<int>("IncidentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AffectedIM")
                        .HasColumnType("int");

                    b.Property<string>("Assignee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ContactPerson")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("HistoryId")
                        .HasColumnType("int");

                    b.Property<int>("ImpactId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAssociated")
                        .HasColumnType("bit");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("RequestedPerson")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UrgencyId")
                        .HasColumnType("int");

                    b.HasKey("IncidentId");

                    b.HasIndex("AffectedIM");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GroupId");

                    b.HasIndex("HistoryId");

                    b.HasIndex("ImpactId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("UrgencyId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberId");

                    b.HasIndex("GroupId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.NewAccountForm", b =>
                {
                    b.Property<int>("AccountRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("SuperiorId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeZoneId")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountRequestId");

                    b.ToTable("AccountForms");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ScheduledWork", b =>
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

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ServiceRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Assignee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ContactPerson")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("HistoryId")
                        .HasColumnType("int");

                    b.Property<int>("ImpactId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("RequestedPerson")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UrgencyId")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GroupId");

                    b.HasIndex("HistoryId");

                    b.HasIndex("ImpactId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("UrgencyId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StateId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Expired")
                        .HasColumnType("bit");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("HistoryId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NotNotification")
                        .HasColumnType("bit");

                    b.Property<string>("Notification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OpensAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("StatusId");

                    b.HasIndex("GroupId");

                    b.HasIndex("HistoryId");

                    b.HasIndex("StateId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.StatusHistory", b =>
                {
                    b.Property<int>("ChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CloserId")
                        .HasColumnType("int");

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("ChangeId");

                    b.HasIndex("CloserId");

                    b.HasIndex("StatusId");

                    b.ToTable("StatusHistory");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.TaskRequest", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountFormId")
                        .HasColumnType("int");

                    b.Property<string>("Assignee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ContactPerson")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("HistoryId")
                        .HasColumnType("int");

                    b.Property<int>("ImpactId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("RequestedPerson")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UrgencyId")
                        .HasColumnType("int");

                    b.HasKey("TaskId");

                    b.HasIndex("AccountFormId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("GroupId");

                    b.HasIndex("HistoryId");

                    b.HasIndex("ImpactId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("UrgencyId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Urgency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Urgencies");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ApplicationConversation", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.ServiceRequest", "ServiceRequest")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Categorization", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Incident", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Incident", null)
                        .WithMany("AffectedIncidents")
                        .HasForeignKey("AffectedIM");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Categorization", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.AssigmentGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.StatusHistory", "History")
                        .WithMany()
                        .HasForeignKey("HistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Impact", "Impact")
                        .WithMany()
                        .HasForeignKey("ImpactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Urgency", "Urgency")
                        .WithMany()
                        .HasForeignKey("UrgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Member", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.AssigmentGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.ServiceRequest", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Categorization", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.AssigmentGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.StatusHistory", "History")
                        .WithMany()
                        .HasForeignKey("HistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Impact", "Impact")
                        .WithMany()
                        .HasForeignKey("ImpactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Urgency", "Urgency")
                        .WithMany()
                        .HasForeignKey("UrgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.Status", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.AssigmentGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.StatusHistory", null)
                        .WithMany("Status")
                        .HasForeignKey("HistoryId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.StatusHistory", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.CloserDue", "CloserDue")
                        .WithMany()
                        .HasForeignKey("CloserId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Status", "ActiveStatus")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("DarkClusterTechnologyEnterprise.Models.TaskRequest", b =>
                {
                    b.HasOne("DarkClusterTechnologyEnterprise.Models.NewAccountForm", "AccountForm")
                        .WithMany()
                        .HasForeignKey("AccountFormId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Categorization", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.AssigmentGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.StatusHistory", "History")
                        .WithMany()
                        .HasForeignKey("HistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Impact", "Impact")
                        .WithMany()
                        .HasForeignKey("ImpactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkClusterTechnologyEnterprise.Models.Urgency", "Urgency")
                        .WithMany()
                        .HasForeignKey("UrgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
