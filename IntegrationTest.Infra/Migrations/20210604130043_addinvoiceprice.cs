using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationTest.Infra.Migrations
{
    public partial class addinvoiceprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Invoices",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Invoices",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0399951c-a322-4298-90cd-712958392496"),
                column: "Price",
                value: 7.2999999999999998);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("24911ce9-9174-4613-8ea0-91e6ab4a9f6f"),
                column: "Price",
                value: 2.5);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33903b03-f351-4d7e-bec5-121444f38444"),
                column: "Price",
                value: 9.25);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("41c0f761-70c9-42a4-a0bc-058c1ecf4d57"),
                column: "Price",
                value: 4.5);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5e70170e-884d-4e73-ae4a-8855e19e349c"),
                column: "Price",
                value: 5.5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Invoices");
        }
    }
}
