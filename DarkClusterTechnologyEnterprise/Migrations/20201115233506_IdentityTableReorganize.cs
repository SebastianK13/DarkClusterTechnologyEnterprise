using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations
{
    public partial class IdentityTableReorganize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Employees_EmployeeId",
                table: "Earnings");

            migrationBuilder.DropIndex(
                name: "IX_Earnings_EmployeeId",
                table: "Earnings");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Earnings");

            migrationBuilder.AddColumn<string>(
                name: "EarningsId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EarningsId",
                table: "Employees",
                column: "EarningsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Earnings_EarningsId",
                table: "Employees",
                column: "EarningsId",
                principalTable: "Earnings",
                principalColumn: "SalaryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Earnings_EarningsId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EarningsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EarningsId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Earnings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Earnings_EmployeeId",
                table: "Earnings",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Earnings_Employees_EmployeeId",
                table: "Earnings",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
