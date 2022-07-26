using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomski_backend.Migrations
{
    public partial class note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerNote",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerNote",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerNote",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SellerNote",
                table: "Order");
        }
    }
}
