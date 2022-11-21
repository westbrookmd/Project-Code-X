using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCodeX.Migrations
{
    public partial class User_AlignWithIdentityDefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUsers",
                newName: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AspNetUsers",
                newName: "Id");
        }
    }
}
