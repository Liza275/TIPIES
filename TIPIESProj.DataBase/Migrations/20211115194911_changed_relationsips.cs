using Microsoft.EntityFrameworkCore.Migrations;

namespace TIPIESProj.DataBase.Migrations
{
    public partial class changed_relationsips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisions_OperationLogs_DivisionId",
                table: "Divisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_OperationLogs_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "OperationLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "OperationLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OperationLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_BuyerId",
                table: "OperationLogs",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_DivisionId",
                table: "OperationLogs",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_ProductId",
                table: "OperationLogs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Buyers_BuyerId",
                table: "OperationLogs",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Divisions_DivisionId",
                table: "OperationLogs",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Products_ProductId",
                table: "OperationLogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_Buyers_BuyerId",
                table: "OperationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_Divisions_DivisionId",
                table: "OperationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_Products_ProductId",
                table: "OperationLogs");

            migrationBuilder.DropIndex(
                name: "IX_OperationLogs_BuyerId",
                table: "OperationLogs");

            migrationBuilder.DropIndex(
                name: "IX_OperationLogs_DivisionId",
                table: "OperationLogs");

            migrationBuilder.DropIndex(
                name: "IX_OperationLogs_ProductId",
                table: "OperationLogs");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "OperationLogs");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "OperationLogs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OperationLogs");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisions_OperationLogs_DivisionId",
                table: "Divisions",
                column: "DivisionId",
                principalTable: "OperationLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OperationLogs_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "OperationLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
