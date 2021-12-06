using Microsoft.EntityFrameworkCore.Migrations;

namespace TIPIESProj.DataBase.Migrations
{
    public partial class adddivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "TransactionLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_DivisionId",
                table: "TransactionLogs",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_Divisions_DivisionId",
                table: "TransactionLogs",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_Divisions_DivisionId",
                table: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLogs_DivisionId",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "TransactionLogs");
        }
    }
}
