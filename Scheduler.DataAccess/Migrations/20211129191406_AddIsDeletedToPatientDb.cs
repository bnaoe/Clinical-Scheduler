using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class AddIsDeletedToPatientDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Patients");
        }
    }
}
