using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class StatusColumnNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusHistory_States_StateId",
                table: "StatusHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusHistory_Statuses_StatusId",
                table: "StatusHistory");

            migrationBuilder.DropColumn(
                name: "XYZ",
                table: "Statuses");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "StatusHistory",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "StatusHistory",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueTime",
                table: "Statuses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_StatusHistory_States_StateId",
                table: "StatusHistory",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusHistory_Statuses_StatusId",
                table: "StatusHistory",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusHistory_States_StateId",
                table: "StatusHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusHistory_Statuses_StatusId",
                table: "StatusHistory");

            migrationBuilder.DropColumn(
                name: "XYZ",
                table: "Statuses");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "StatusHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "StatusHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueTime",
                table: "Statuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_StatusHistory_States_StateId",
                table: "StatusHistory",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusHistory_Statuses_StatusId",
                table: "StatusHistory",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
