using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateDocumentTitleProviderUserModifiedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderUserId",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProviderUserId",
                table: "Documents",
                column: "ProviderUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_ProviderUserId",
                table: "Documents",
                column: "ProviderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_ProviderUserId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ProviderUserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ProviderUserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Documents");
        }
    }
}
