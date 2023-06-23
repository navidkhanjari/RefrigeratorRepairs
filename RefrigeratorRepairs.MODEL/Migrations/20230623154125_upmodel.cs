using Microsoft.EntityFrameworkCore.Migrations;

namespace RefrigeratorRepairs.MODEL.Migrations
{
    public partial class upmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUs",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatWeDo",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "WhatWeDo",
                table: "SiteSettings");
        }
    }
}
