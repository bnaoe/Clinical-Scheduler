using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class AddProviderScheduleProfileToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProviderScheduleProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: true),
                    Tuesday = table.Column<bool>(type: "bit", nullable: true),
                    Wednesday = table.Column<bool>(type: "bit", nullable: true),
                    Thursday = table.Column<bool>(type: "bit", nullable: true),
                    Friday = table.Column<bool>(type: "bit", nullable: true),
                    Saturday = table.Column<bool>(type: "bit", nullable: true),
                    Sunday = table.Column<bool>(type: "bit", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderScheduleProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderScheduleProfiles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderScheduleProfiles_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderScheduleProfiles_ApplicationUserId",
                table: "ProviderScheduleProfiles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderScheduleProfiles_LocationId",
                table: "ProviderScheduleProfiles",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProviderScheduleProfiles");
        }
    }
}
