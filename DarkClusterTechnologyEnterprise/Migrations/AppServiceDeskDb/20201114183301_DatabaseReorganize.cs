using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class DatabaseReorganize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Applications_ApplicationId",
                table: "Conversations");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_ApplicationId",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Applicant",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Critical",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Responsible",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UserLocation",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Conversations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Applications",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Applications",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Applications",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContactPerson",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImpactId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestedPerson",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UrgencyId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "RequestId");

            migrationBuilder.CreateTable(
                name: "AssigmentGroup",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigmentGroup", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Expired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Members_AssigmentGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AssigmentGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationService",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationService", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_ApplicationService_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categorizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorizations_ApplicationService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ApplicationService",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_RequestId",
                table: "Conversations",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CategoryId",
                table: "Applications",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_GroupId",
                table: "Applications",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ImpactId",
                table: "Applications",
                column: "ImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PriorityId",
                table: "Applications",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_StatusId",
                table: "Applications",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UrgencyId",
                table: "Applications",
                column: "UrgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationService_CategoryId",
                table: "ApplicationService",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorizations_ServiceId",
                table: "Categorizations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GroupId",
                table: "Members",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Categorizations_CategoryId",
                table: "Applications",
                column: "CategoryId",
                principalTable: "Categorizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AssigmentGroup_GroupId",
                table: "Applications",
                column: "GroupId",
                principalTable: "AssigmentGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Impacts_ImpactId",
                table: "Applications",
                column: "ImpactId",
                principalTable: "Impacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Priorities_PriorityId",
                table: "Applications",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Statuses_StatusId",
                table: "Applications",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Urgencies_UrgencyId",
                table: "Applications",
                column: "UrgencyId",
                principalTable: "Urgencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Applications_RequestId",
                table: "Conversations",
                column: "RequestId",
                principalTable: "Applications",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Categorizations_CategoryId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AssigmentGroup_GroupId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Impacts_ImpactId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Priorities_PriorityId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Statuses_StatusId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Urgencies_UrgencyId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Applications_RequestId",
                table: "Conversations");

            migrationBuilder.DropTable(
                name: "Categorizations");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "ApplicationService");

            migrationBuilder.DropTable(
                name: "AssigmentGroup");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_RequestId",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_CategoryId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_GroupId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ImpactId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PriorityId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_StatusId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UrgencyId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ImpactId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "RequestedPerson",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "UrgencyId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Conversations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Applicant",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationStatus",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Critical",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Responsible",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserLocation",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "ApplicationId");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ApplicationId",
                table: "Conversations",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Applications_ApplicationId",
                table: "Conversations",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
