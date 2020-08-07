using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Machines",
                newName: "Complete");

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "MachineId", "Complete", "Description", "Name" },
                values: new object[] { 1, false, null, "Dreamweaver" });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "MachineId", "Complete", "Description", "Name" },
                values: new object[] { 2, false, null, "Bubblewrappinator" });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "MachineId", "Complete", "Description", "Name" },
                values: new object[] { 3, false, null, "Laughbox" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "MachineId",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Complete",
                table: "Machines",
                newName: "Status");
        }
    }
}
