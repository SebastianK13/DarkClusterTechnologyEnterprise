using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class TaskRequestForeignKeyNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AccountForms_AccountFormId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "AccountFormId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AccountForms_AccountFormId",
                table: "Tasks",
                column: "AccountFormId",
                principalTable: "AccountForms",
                principalColumn: "AccountRequestId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AccountForms_AccountFormId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "AccountFormId",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AccountForms_AccountFormId",
                table: "Tasks",
                column: "AccountFormId",
                principalTable: "AccountForms",
                principalColumn: "AccountRequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
