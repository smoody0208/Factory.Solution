using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class StatusToMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Machines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Machines");
        }
    }
}
