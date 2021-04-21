using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class StatusModification2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Statuses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_GroupId",
                table: "Statuses",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_AssigmentGroup_GroupId",
                table: "Statuses",
                column: "GroupId",
                principalTable: "AssigmentGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_AssigmentGroup_GroupId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_GroupId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Statuses");
        }
    }
}
