using Microsoft.EntityFrameworkCore.Migrations;

namespace TSUKAT.Infrastructure.Migrations
{
    public partial class DefaultGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Group_GroupId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "User",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Group_GroupId",
                table: "User",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Group_GroupId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true,
                oldDefaultValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Group_GroupId",
                table: "User",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
