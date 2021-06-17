using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationTest.Infra.Migrations
{
    public partial class addTempId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TempId",
                table: "InvoiceProducts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "TempId1",
                table: "InvoiceProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_InvoiceProducts_TempId1",
                table: "InvoiceProducts",
                column: "TempId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_InvoiceProducts_TempId1",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "InvoiceProducts");

            migrationBuilder.AlterColumn<int>(
                name: "TempId",
                table: "InvoiceProducts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }
    }
}
