using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationTest.Infra.Migrations
{
    public partial class removeIdUnecessary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceProducts_ProductId",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InvoiceProducts");

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "InvoiceProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_InvoiceProducts_TempId",
                table: "InvoiceProducts",
                column: "TempId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts",
                columns: new[] { "ProductId", "InvoiceId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_InvoiceProducts_TempId",
                table: "InvoiceProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "InvoiceProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "InvoiceProducts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProducts_ProductId",
                table: "InvoiceProducts",
                column: "ProductId");
        }
    }
}
