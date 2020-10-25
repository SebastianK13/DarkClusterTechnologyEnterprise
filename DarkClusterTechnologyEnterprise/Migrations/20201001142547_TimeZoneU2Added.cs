using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations
{
    public partial class TimeZoneU2Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZoneId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    ZonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeZoneName = table.Column<string>(nullable: true),
                    TimeDifference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.ZonId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ZoneId",
                table: "Employees",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Zone_ZoneId",
                table: "Employees",
                column: "ZoneId",
                principalTable: "Zone",
                principalColumn: "ZonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Zone_ZoneId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ZoneId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "Employees");
        }
    }
}
