using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomski_backend.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stanje",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "Stanje",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
