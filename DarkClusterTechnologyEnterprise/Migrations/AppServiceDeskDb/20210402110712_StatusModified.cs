using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class StatusModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Statuses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Statuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Statuses");
        }
    }
}
