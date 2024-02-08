using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collaborative_Resource_Management_System.Data.Migrations
{
    public partial class FebDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "InventoryItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
