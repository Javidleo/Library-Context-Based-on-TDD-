using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace BookDataAccess.Migrations
{
    public partial class newsoft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiractionDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "InUse",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiractionDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InUse",
                table: "Books");
        }
    }
}
