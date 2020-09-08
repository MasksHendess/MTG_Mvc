using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class SpellingError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "thoughness",
                table: "cards");

            migrationBuilder.AddColumn<string>(
                name: "toughness",
                table: "cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "toughness",
                table: "cards");

            migrationBuilder.AddColumn<string>(
                name: "thoughness",
                table: "cards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
