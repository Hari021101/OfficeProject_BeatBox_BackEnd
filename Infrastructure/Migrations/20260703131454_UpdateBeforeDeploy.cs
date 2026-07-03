using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBeforeDeploy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnRequests_Orders_OrderId1",
                table: "ReturnRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReturnRequests_OrderId1",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "ReturnRequests");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ReturnRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnRequests_OrderId",
                table: "ReturnRequests",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnRequests_Orders_OrderId",
                table: "ReturnRequests",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnRequests_Orders_OrderId",
                table: "ReturnRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReturnRequests_OrderId",
                table: "ReturnRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "ReturnRequests",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "ReturnRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnRequests_OrderId1",
                table: "ReturnRequests",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnRequests_Orders_OrderId1",
                table: "ReturnRequests",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
