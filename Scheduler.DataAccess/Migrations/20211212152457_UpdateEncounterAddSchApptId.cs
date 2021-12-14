using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateEncounterAddSchApptId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_AspNetUsers_ApplicationUserId",
                table: "Encounters");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Encounters",
                newName: "ProviderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Encounters_ApplicationUserId",
                table: "Encounters",
                newName: "IX_Encounters_ProviderUserId");

            migrationBuilder.AddColumn<int>(
                name: "SchApptId",
                table: "Encounters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_SchApptId",
                table: "Encounters",
                column: "SchApptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_AspNetUsers_ProviderUserId",
                table: "Encounters",
                column: "ProviderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_SchAppts_SchApptId",
                table: "Encounters",
                column: "SchApptId",
                principalTable: "SchAppts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_AspNetUsers_ProviderUserId",
                table: "Encounters");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_SchAppts_SchApptId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_SchApptId",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "SchApptId",
                table: "Encounters");

            migrationBuilder.RenameColumn(
                name: "ProviderUserId",
                table: "Encounters",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Encounters_ProviderUserId",
                table: "Encounters",
                newName: "IX_Encounters_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_AspNetUsers_ApplicationUserId",
                table: "Encounters",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
