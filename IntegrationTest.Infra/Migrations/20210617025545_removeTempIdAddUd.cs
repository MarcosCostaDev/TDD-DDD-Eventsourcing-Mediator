using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationTest.Infra.Migrations
{
    public partial class removeTempIdAddUd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_InvoiceProducts_TempId",
                table: "InvoiceProducts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_InvoiceProducts_TempId1",
                table: "InvoiceProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "InvoiceProducts");

            migrationBuilder.RenameColumn(
                name: "TempId",
                table: "InvoiceProducts",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProducts_ProductId",
                table: "InvoiceProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceProducts_ProductId",
                table: "InvoiceProducts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InvoiceProducts",
                newName: "TempId");

            migrationBuilder.AddColumn<int>(
                name: "TempId1",
                table: "InvoiceProducts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_InvoiceProducts_TempId",
                table: "InvoiceProducts",
                column: "TempId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_InvoiceProducts_TempId1",
                table: "InvoiceProducts",
                column: "TempId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceProducts",
                table: "InvoiceProducts",
                columns: new[] { "ProductId", "InvoiceId" });
        }
    }
}
