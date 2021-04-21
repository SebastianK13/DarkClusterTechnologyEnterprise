using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class RequestHistoryModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_StatusHistory_HistoryId",
                table: "Statuses");

            migrationBuilder.AlterColumn<int>(
                name: "HistoryId",
                table: "Statuses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_StatusHistory_HistoryId",
                table: "Statuses",
                column: "HistoryId",
                principalTable: "StatusHistory",
                principalColumn: "ChangeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_StatusHistory_HistoryId",
                table: "Statuses");

            migrationBuilder.AlterColumn<int>(
                name: "HistoryId",
                table: "Statuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_StatusHistory_HistoryId",
                table: "Statuses",
                column: "HistoryId",
                principalTable: "StatusHistory",
                principalColumn: "ChangeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
