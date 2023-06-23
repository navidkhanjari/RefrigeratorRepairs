using Microsoft.EntityFrameworkCore.Migrations;

namespace RefrigeratorRepairs.MODEL.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImageName",
                table: "SiteSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoImageName",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
