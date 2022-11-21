using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCodeX.Data.Migrations
{
    public partial class UserAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AdditionalUserInformation",
                table: "AdditionalUserInformation");

            migrationBuilder.RenameTable(
                name: "AdditionalUserInformation",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "AdditionalUserInformation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalUserInformation",
                table: "AdditionalUserInformation",
                column: "UserID");
        }
    }
}
