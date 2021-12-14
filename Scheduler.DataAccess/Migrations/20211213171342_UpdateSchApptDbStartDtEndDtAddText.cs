using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateSchApptDbStartDtEndDtAddText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "SchAppts",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "SchAppts",
                newName: "end_date");

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "SchAppts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "text",
                table: "SchAppts");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "SchAppts",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "SchAppts",
                newName: "EndDateTime");
        }
    }
}
