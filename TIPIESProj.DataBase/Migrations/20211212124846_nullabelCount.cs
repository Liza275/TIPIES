using Microsoft.EntityFrameworkCore.Migrations;

namespace TIPIESProj.DataBase.Migrations
{
    public partial class nullabelCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "TransactionLogs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "TransactionLogs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
