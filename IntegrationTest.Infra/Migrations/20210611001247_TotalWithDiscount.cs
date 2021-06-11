using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationTest.Infra.Migrations
{
    public partial class TotalWithDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalWithDiscount",
                table: "Invoices",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalWithDiscount",
                table: "Invoices");
        }
    }
}
