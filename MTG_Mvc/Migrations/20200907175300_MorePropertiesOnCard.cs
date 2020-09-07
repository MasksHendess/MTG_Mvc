using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class MorePropertiesOnCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "artist",
                table: "cards",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "cmc",
                table: "cards",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "flavourText",
                table: "cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rarity",
                table: "cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "text",
                table: "cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "artist",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "cmc",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "flavourText",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "rarity",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "text",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "type",
                table: "cards");
        }
    }
}
