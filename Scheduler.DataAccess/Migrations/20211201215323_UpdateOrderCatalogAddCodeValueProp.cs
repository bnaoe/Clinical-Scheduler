using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateOrderCatalogAddCodeValueProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodeValueId",
                table: "OrderCatalogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderCatalogs_CodeValueId",
                table: "OrderCatalogs",
                column: "CodeValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCatalogs_CodeValues_CodeValueId",
                table: "OrderCatalogs",
                column: "CodeValueId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCatalogs_CodeValues_CodeValueId",
                table: "OrderCatalogs");

            migrationBuilder.DropIndex(
                name: "IX_OrderCatalogs_CodeValueId",
                table: "OrderCatalogs");

            migrationBuilder.DropColumn(
                name: "CodeValueId",
                table: "OrderCatalogs");
        }
    }
}
