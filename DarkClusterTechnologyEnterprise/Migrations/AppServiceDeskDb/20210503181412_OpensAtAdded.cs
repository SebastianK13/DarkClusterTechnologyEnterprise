using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class OpensAtAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OpensAt",
                table: "Statuses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpensAt",
                table: "Statuses");
        }
    }
}
