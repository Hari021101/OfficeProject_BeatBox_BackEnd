using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomEngravingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EngravingPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsEngravingAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EngravingDate",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngravingMessage",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngravingName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EngravingPrice",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPersonalised",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EngravingDate",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngravingMessage",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngravingName",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EngravingPrice",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPersonalised",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngravingPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsEngravingAvailable",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EngravingDate",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "EngravingMessage",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "EngravingName",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "EngravingPrice",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "IsPersonalised",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "EngravingDate",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "EngravingMessage",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "EngravingName",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "EngravingPrice",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IsPersonalised",
                table: "CartItems");
        }
    }
}
