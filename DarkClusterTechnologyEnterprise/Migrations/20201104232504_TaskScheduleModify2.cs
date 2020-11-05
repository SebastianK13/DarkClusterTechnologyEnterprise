using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations
{
    public partial class TaskScheduleModify2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskSchedules",
                columns: table => new
                {
                    TaskId = table.Column<string>(nullable: false),
                    Task = table.Column<string>(nullable: false),
                    TaskDesc = table.Column<string>(nullable: false),
                    TaskBegin = table.Column<DateTime>(nullable: false),
                    TaskDeadline = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSchedules", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskSchedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskSchedules_EmployeeId",
                table: "TaskSchedules",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskSchedules");
        }
    }
}
