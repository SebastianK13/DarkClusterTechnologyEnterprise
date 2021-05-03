using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class StatusMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notification",
                table: "Statuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notification",
                table: "Statuses");
        }
    }
}
