using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppCustomersDb
{
    public partial class ServiceModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Services_ServiceId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ServiceId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceId",
                table: "Services",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "CustomersLocation",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StreetAddress",
                table: "CustomersLocation",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "CustomersLocation",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "CustomersLocation",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_InvoiceId",
                table: "Services",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Invoices_InvoiceId",
                table: "Services",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Invoices_InvoiceId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_InvoiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "CustomersLocation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "StreetAddress",
                table: "CustomersLocation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "CustomersLocation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "CustomersLocation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ServiceId",
                table: "Invoices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Services_ServiceId",
                table: "Invoices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
