using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReferralDeleteBehaviorToRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferredUserId",
                table: "Referrals");

            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferrerId",
                table: "Referrals");

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferredUserId",
                table: "Referrals",
                column: "ReferredUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Referrals_AspNetUsers_ReferredUserId",
                table: "Referrals");

            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferrerId",
                table: "Referrals");

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferredUserId",
                table: "Referrals",
                column: "ReferredUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_AspNetUsers_ReferrerId",
                table: "Referrals",
                column: "ReferrerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
