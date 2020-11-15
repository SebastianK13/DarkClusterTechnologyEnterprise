using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class ServiceRequestForeignKeyChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AssigmentGroup_GroupId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Applications",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AssigmentGroup_GroupId",
                table: "Applications",
                column: "GroupId",
                principalTable: "AssigmentGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AssigmentGroup_GroupId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Applications",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AssigmentGroup_GroupId",
                table: "Applications",
                column: "GroupId",
                principalTable: "AssigmentGroup",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
