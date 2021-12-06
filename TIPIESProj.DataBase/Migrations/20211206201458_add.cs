using Microsoft.EntityFrameworkCore.Migrations;

namespace TIPIESProj.DataBase.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "TransactionLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_ProductId",
                table: "TransactionLogs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_Products_ProductId",
                table: "TransactionLogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_Products_ProductId",
                table: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLogs_ProductId",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "TransactionLogs");
        }
    }
}
