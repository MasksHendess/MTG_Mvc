using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class DecklistCardTypeAmounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "avarageCMC",
                table: "decklists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cardsAmount",
                table: "decklists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "creaturesAmount",
                table: "decklists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "instantsAmount",
                table: "decklists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "landsAmount",
                table: "decklists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sorceriesAmount",
                table: "decklists",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avarageCMC",
                table: "decklists");

            migrationBuilder.DropColumn(
                name: "cardsAmount",
                table: "decklists");

            migrationBuilder.DropColumn(
                name: "creaturesAmount",
                table: "decklists");

            migrationBuilder.DropColumn(
                name: "instantsAmount",
                table: "decklists");

            migrationBuilder.DropColumn(
                name: "landsAmount",
                table: "decklists");

            migrationBuilder.DropColumn(
                name: "sorceriesAmount",
                table: "decklists");
        }
    }
}
