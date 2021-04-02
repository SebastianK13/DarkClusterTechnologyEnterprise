using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class AffectedIMAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AffectedIM",
                table: "Incidents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_AffectedIM",
                table: "Incidents",
                column: "AffectedIM");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Incidents_AffectedIM",
                table: "Incidents",
                column: "AffectedIM",
                principalTable: "Incidents",
                principalColumn: "IncidentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Incidents_AffectedIM",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_AffectedIM",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "AffectedIM",
                table: "Incidents");
        }
    }
}
