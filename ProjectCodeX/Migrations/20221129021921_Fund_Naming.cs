using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCodeX.Migrations
{
    public partial class Fund_Naming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Purchase",
                newName: "Total");

            migrationBuilder.AddColumn<string>(
                name: "PurchName",
                table: "PurchLineItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchName",
                table: "PurchLineItems");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Purchase",
                newName: "Price");
        }
    }
}
