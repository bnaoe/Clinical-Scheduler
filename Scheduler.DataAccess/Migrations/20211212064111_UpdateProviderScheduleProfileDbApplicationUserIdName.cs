using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateProviderScheduleProfileDbApplicationUserIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderScheduleProfiles_AspNetUsers_ApplicationUserId",
                table: "ProviderScheduleProfiles");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ProviderScheduleProfiles",
                newName: "ProviderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderScheduleProfiles_ApplicationUserId",
                table: "ProviderScheduleProfiles",
                newName: "IX_ProviderScheduleProfiles_ProviderUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderScheduleProfiles_AspNetUsers_ProviderUserId",
                table: "ProviderScheduleProfiles",
                column: "ProviderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderScheduleProfiles_AspNetUsers_ProviderUserId",
                table: "ProviderScheduleProfiles");

            migrationBuilder.RenameColumn(
                name: "ProviderUserId",
                table: "ProviderScheduleProfiles",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderScheduleProfiles_ProviderUserId",
                table: "ProviderScheduleProfiles",
                newName: "IX_ProviderScheduleProfiles_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderScheduleProfiles_AspNetUsers_ApplicationUserId",
                table: "ProviderScheduleProfiles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
