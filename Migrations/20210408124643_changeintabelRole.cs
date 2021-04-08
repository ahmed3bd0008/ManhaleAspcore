using Microsoft.EntityFrameworkCore.Migrations;

namespace ManhaleAspNetCore.Migrations
{
    public partial class changeintabelRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PowerOfRole",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PowerOfRole",
                table: "AspNetRoles");
        }
    }
}
