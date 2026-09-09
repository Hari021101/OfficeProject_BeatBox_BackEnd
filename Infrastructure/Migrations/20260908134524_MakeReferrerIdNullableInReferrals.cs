using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeReferrerIdNullableInReferrals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferrerId",
                table: "Referrals");

            migrationBuilder.AlterColumn<string>(
                name: "ReferrerId",
                table: "Referrals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferrerId",
                table: "Referrals",
                column: "ReferrerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferrerId",
                table: "Referrals");

            migrationBuilder.AlterColumn<string>(
                name: "ReferrerId",
                table: "Referrals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferrerId",
                table: "Referrals",
                column: "ReferrerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
