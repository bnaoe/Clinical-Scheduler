using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateEncounterDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_CodeValues_ApptStatusId",
                table: "Encounters");

            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_CodeValues_ApptTypeId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_ApptStatusId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_ApptTypeId",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "ApptStatusId",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "ApptTypeId",
                table: "Encounters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApptStatusId",
                table: "Encounters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApptTypeId",
                table: "Encounters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ApptStatusId",
                table: "Encounters",
                column: "ApptStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ApptTypeId",
                table: "Encounters",
                column: "ApptTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_CodeValues_ApptStatusId",
                table: "Encounters",
                column: "ApptStatusId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_CodeValues_ApptTypeId",
                table: "Encounters",
                column: "ApptTypeId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
