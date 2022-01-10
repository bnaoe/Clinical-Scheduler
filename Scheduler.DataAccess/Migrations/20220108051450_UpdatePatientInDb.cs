using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdatePatientInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EthnicityId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhysician",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferringPhysician",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_EthnicityId",
                table: "Patients",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GenderId",
                table: "Patients",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RaceId",
                table: "Patients",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_CodeValues_EthnicityId",
                table: "Patients",
                column: "EthnicityId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_CodeValues_GenderId",
                table: "Patients",
                column: "GenderId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_CodeValues_RaceId",
                table: "Patients",
                column: "RaceId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_CodeValues_EthnicityId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_CodeValues_GenderId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_CodeValues_RaceId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_EthnicityId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_GenderId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_RaceId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EthnicityId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PrimaryPhysician",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ReferringPhysician",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Patients");
        }
    }
}
