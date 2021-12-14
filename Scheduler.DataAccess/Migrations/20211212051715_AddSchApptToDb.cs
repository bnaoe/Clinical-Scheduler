using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class AddSchApptToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchAppts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ProviderScheduleProfileId = table.Column<int>(type: "int", nullable: false),
                    RegistrarUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApptTypeId = table.Column<int>(type: "int", nullable: false),
                    ApptStatusId = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchAppts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchAppts_AspNetUsers_RegistrarUserId",
                        column: x => x.RegistrarUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchAppts_CodeValues_ApptStatusId",
                        column: x => x.ApptStatusId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchAppts_CodeValues_ApptTypeId",
                        column: x => x.ApptTypeId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchAppts_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchAppts_ProviderScheduleProfiles_ProviderScheduleProfileId",
                        column: x => x.ProviderScheduleProfileId,
                        principalTable: "ProviderScheduleProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchAppts_ApptStatusId",
                table: "SchAppts",
                column: "ApptStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SchAppts_ApptTypeId",
                table: "SchAppts",
                column: "ApptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchAppts_PatientId",
                table: "SchAppts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_SchAppts_ProviderScheduleProfileId",
                table: "SchAppts",
                column: "ProviderScheduleProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SchAppts_RegistrarUserId",
                table: "SchAppts",
                column: "RegistrarUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchAppts");
        }
    }
}
