using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations
{
    public partial class PositionReplacedByForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
