using Microsoft.EntityFrameworkCore.Migrations;

namespace TSUKAT.Infrastructure.Migrations
{
    public partial class InitialMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "GroupName" },
                values: new object[] { 1, "Ungroupped" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Administrator" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "User" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessToken", "GroupId", "Name", "Password", "Surname", "UserName" },
                values: new object[] { 1, "123456789", 1, "Admin", "root", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
