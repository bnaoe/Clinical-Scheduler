using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateOrdersRouteFreqTimeNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CodeValues_AdminFreqId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AdminFreqId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CodeValues_AdminFreqId",
                table: "Orders",
                column: "AdminFreqId",
                principalTable: "CodeValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CodeValues_AdminFreqId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AdminFreqId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CodeValues_AdminFreqId",
                table: "Orders",
                column: "AdminFreqId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
