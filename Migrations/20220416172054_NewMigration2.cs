using Microsoft.EntityFrameworkCore.Migrations;

namespace diplomski_backend.Migrations
{
    public partial class NewMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slike",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Slike",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
