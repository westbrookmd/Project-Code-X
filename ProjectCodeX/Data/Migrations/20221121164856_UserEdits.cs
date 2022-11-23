using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCodeX.Data.Migrations
{
    public partial class UserEdits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextBillDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PayMethod",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NextBillDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PayMethod",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "User",
                newName: "FirstName");
        }
    }
}
