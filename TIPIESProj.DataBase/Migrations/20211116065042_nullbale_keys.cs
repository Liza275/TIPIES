using Microsoft.EntityFrameworkCore.Migrations;

namespace TIPIESProj.DataBase.Migrations
{
    public partial class nullbale_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OperationLogs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "OperationLogs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BuyerId",
                table: "OperationLogs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Buyers_BuyerId",
                table: "OperationLogs",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Divisions_DivisionId",
                table: "OperationLogs",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_Products_ProductId",
                table: "OperationLogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OperationLogs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "OperationLogs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BuyerId",
                table: "OperationLogs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
    }
}
