using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class StatusTableModification2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotification",
                table: "Statuses");

            migrationBuilder.AddColumn<bool>(
                name: "NotNotification",
                table: "Statuses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotNotification",
                table: "Statuses");

            migrationBuilder.AddColumn<bool>(
                name: "IsNotification",
                table: "Statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
