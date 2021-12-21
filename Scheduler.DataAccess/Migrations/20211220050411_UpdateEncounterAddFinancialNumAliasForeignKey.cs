using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateEncounterAddFinancialNumAliasForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinancialNumAliasId",
                table: "Encounters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_FinancialNumAliasId",
                table: "Encounters",
                column: "FinancialNumAliasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_FinancialNumAliases_FinancialNumAliasId",
                table: "Encounters",
                column: "FinancialNumAliasId",
                principalTable: "FinancialNumAliases",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_FinancialNumAliases_FinancialNumAliasId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_FinancialNumAliasId",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "FinancialNumAliasId",
                table: "Encounters");
        }
    }
}
