using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppCustomersDb
{
    public partial class ResponsibleEmployeeAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleEmployee",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleEmployee",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleEmployee",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ResponsibleEmployee",
                table: "Customers");
        }
    }
}
