using Microsoft.EntityFrameworkCore.Migrations;

namespace Infnet.Etapa4.Infrastructure.Data.Migrations
{
    public partial class AddColumnImageUriAndRemoveCurriculoUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurriculoUri",
                table: "Amigo");

            migrationBuilder.AddColumn<string>(
                name: "ImageUri",
                table: "Amigo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUri",
                table: "Amigo");

            migrationBuilder.AddColumn<string>(
                name: "CurriculoUri",
                table: "Amigo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
