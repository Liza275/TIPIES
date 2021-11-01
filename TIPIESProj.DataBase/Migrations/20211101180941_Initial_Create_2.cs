using Microsoft.EntityFrameworkCore.Migrations;

namespace TIPIESProj.DataBase.Migrations
{
    public partial class Initial_Create_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_CreditId",
                table: "TransactionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_DebetId",
                table: "TransactionLogs");

            migrationBuilder.AlterColumn<int>(
                name: "DebetId",
                table: "TransactionLogs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreditId",
                table: "TransactionLogs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_CreditId",
                table: "TransactionLogs",
                column: "CreditId",
                principalTable: "ChartOfAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_DebetId",
                table: "TransactionLogs",
                column: "DebetId",
                principalTable: "ChartOfAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_CreditId",
                table: "TransactionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_DebetId",
                table: "TransactionLogs");

            migrationBuilder.AlterColumn<int>(
                name: "DebetId",
                table: "TransactionLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditId",
                table: "TransactionLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_CreditId",
                table: "TransactionLogs",
                column: "CreditId",
                principalTable: "ChartOfAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLogs_ChartOfAccounts_DebetId",
                table: "TransactionLogs",
                column: "DebetId",
                principalTable: "ChartOfAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
