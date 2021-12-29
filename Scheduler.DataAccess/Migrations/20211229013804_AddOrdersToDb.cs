using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicalScheduler.Migrations
{
    public partial class AddOrdersToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderingDtTm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narrative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncounterId = table.Column<int>(type: "int", nullable: false),
                    OrderingUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderCatalogId = table.Column<int>(type: "int", nullable: false),
                    AdminRouteId = table.Column<int>(type: "int", nullable: false),
                    AdminFreqId = table.Column<int>(type: "int", nullable: false),
                    AdminTimeId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_OrderingUserId",
                        column: x => x.OrderingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_CodeValues_AdminFreqId",
                        column: x => x.AdminFreqId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_CodeValues_AdminRouteId",
                        column: x => x.AdminRouteId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_CodeValues_AdminTimeId",
                        column: x => x.AdminTimeId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_CodeValues_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_CodeValues_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "CodeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderCatalogs_OrderCatalogId",
                        column: x => x.OrderCatalogId,
                        principalTable: "OrderCatalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AdminFreqId",
                table: "Orders",
                column: "AdminFreqId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AdminRouteId",
                table: "Orders",
                column: "AdminRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AdminTimeId",
                table: "Orders",
                column: "AdminTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EncounterId",
                table: "Orders",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCatalogId",
                table: "Orders",
                column: "OrderCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderingUserId",
                table: "Orders",
                column: "OrderingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
