using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class ChangeStatusToBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Machines",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Machines",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
