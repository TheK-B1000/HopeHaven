using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collaborative_Resource_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddItemTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetTag",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MinimumQuantity",
                table: "InventoryItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PricePerUnit",
                table: "InventoryItems",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityAvailable",
                table: "InventoryItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetTag",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "MinimumQuantity",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "QuantityAvailable",
                table: "InventoryItems");
        }
    }
}
