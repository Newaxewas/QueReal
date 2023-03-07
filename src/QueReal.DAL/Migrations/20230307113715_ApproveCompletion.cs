using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueReal.DAL.Migrations
{
    public partial class ApproveCompletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedTime",
                table: "Quest",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedTime",
                table: "Quest");
        }
    }
}
