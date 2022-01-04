using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class UpdateOrdersRouteTimeNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CodeValues_AdminRouteId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CodeValues_AdminTimeId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AdminTimeId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AdminRouteId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CodeValues_AdminRouteId",
                table: "Orders",
                column: "AdminRouteId",
                principalTable: "CodeValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CodeValues_AdminTimeId",
                table: "Orders",
                column: "AdminTimeId",
                principalTable: "CodeValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CodeValues_AdminRouteId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CodeValues_AdminTimeId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AdminTimeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminRouteId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CodeValues_AdminRouteId",
                table: "Orders",
                column: "AdminRouteId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CodeValues_AdminTimeId",
                table: "Orders",
                column: "AdminTimeId",
                principalTable: "CodeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
