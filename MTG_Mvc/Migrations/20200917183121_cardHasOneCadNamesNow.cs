using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class cardHasOneCadNamesNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_doubleName_cards_cardid",
                table: "doubleName_cards");

            migrationBuilder.CreateIndex(
                name: "IX_doubleName_cards_cardid",
                table: "doubleName_cards",
                column: "cardid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_doubleName_cards_cardid",
                table: "doubleName_cards");

            migrationBuilder.CreateIndex(
                name: "IX_doubleName_cards_cardid",
                table: "doubleName_cards",
                column: "cardid");
        }
    }
}
