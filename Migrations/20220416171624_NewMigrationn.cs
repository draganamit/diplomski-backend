using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomski_backend.Migrations
{
    public partial class NewMigrationn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slike",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slike",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Product");
        }
    }
}
