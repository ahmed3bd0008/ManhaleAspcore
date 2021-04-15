using Microsoft.EntityFrameworkCore.Migrations;

namespace ManhaleAspNetCore.Migrations
{
    public partial class changeInqueenmanhale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Queues_KhaliaId",
                table: "Queues");

            migrationBuilder.AddColumn<string>(
                name: "Account_ID",
                table: "manahels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Queues_KhaliaId",
                table: "Queues",
                column: "KhaliaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Queues_KhaliaId",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "Account_ID",
                table: "manahels");

            migrationBuilder.CreateIndex(
                name: "IX_Queues_KhaliaId",
                table: "Queues",
                column: "KhaliaId");
        }
    }
}
