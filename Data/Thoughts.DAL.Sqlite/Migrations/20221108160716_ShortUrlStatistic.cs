using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thoughts.DAL.Sqlite.Migrations
{
    public partial class ShortUrlStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastReset",
                table: "ShortUrls",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Statistic",
                table: "ShortUrls",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastReset",
                table: "ShortUrls");

            migrationBuilder.DropColumn(
                name: "Statistic",
                table: "ShortUrls");
        }
    }
}
