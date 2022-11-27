using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCodeX.Migrations
{
    public partial class Consolidate_Event_Date_Time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Events",
                type: "time",
                nullable: true);
        }
    }
}
