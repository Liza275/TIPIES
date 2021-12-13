using Microsoft.EntityFrameworkCore.Migrations;

namespace TIPIESProj.DataBase.Migrations
{
    public partial class fix_foreign_key_in_translog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_OperationLogs_TransactionLogId",
                table: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLogs_TransactionLogId",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "TransactionLogId",
                table: "TransactionLogs");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_OperationLogId",
                table: "TransactionLogs",
                column: "OperationLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_OperationLogs_OperationLogId",
                table: "TransactionLogs",
                column: "OperationLogId",
                principalTable: "OperationLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_OperationLogs_OperationLogId",
                table: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_TransactionLogs_OperationLogId",
                table: "TransactionLogs");

            migrationBuilder.AddColumn<int>(
                name: "TransactionLogId",
                table: "TransactionLogs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_TransactionLogId",
                table: "TransactionLogs",
                column: "TransactionLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_OperationLogs_TransactionLogId",
                table: "TransactionLogs",
                column: "TransactionLogId",
                principalTable: "OperationLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
