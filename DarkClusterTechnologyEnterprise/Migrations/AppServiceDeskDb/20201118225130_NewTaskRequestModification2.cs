using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class NewTaskRequestModification2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AssigmentGroup_GroupId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AssigmentGroup_GroupId",
                table: "Tasks",
                column: "GroupId",
                principalTable: "AssigmentGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AssigmentGroup_GroupId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Tasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AssigmentGroup_GroupId",
                table: "Tasks",
                column: "GroupId",
                principalTable: "AssigmentGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
