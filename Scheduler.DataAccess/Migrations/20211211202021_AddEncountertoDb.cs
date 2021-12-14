using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class AddEncountertoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InsuranceId = table.Column<int>(type: "int", nullable: false),
                    HealthPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ApptTypeId = table.Column<int>(type: "int", nullable: false),
                    ApptStatusId = table.Column<int>(type: "int", nullable: false),
                    AdmitDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DschDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForVisit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsentGiven = table.Column<bool>(type: "bit", nullable: false),
                    PrivacyNotice = table.Column<bool>(type: "bit", nullable: false),
                    GuarantorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encounters_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encounters_CodeValues_ApptStatusId",
                        column: x => x.ApptStatusId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encounters_CodeValues_ApptTypeId",
                        column: x => x.ApptTypeId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encounters_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encounters_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encounters_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ApplicationUserId",
                table: "Encounters",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ApptStatusId",
                table: "Encounters",
                column: "ApptStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ApptTypeId",
                table: "Encounters",
                column: "ApptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_InsuranceId",
                table: "Encounters",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_LocationId",
                table: "Encounters",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_PatientId",
                table: "Encounters",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Encounters");
        }
    }
}
