using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class New_FK_CardNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cardNamesid",
                table: "cards");

            migrationBuilder.AddColumn<int>(
                name: "cardid",
                table: "doubleName_cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_doubleName_cards_cardid",
                table: "doubleName_cards",
                column: "cardid");

            migrationBuilder.AddForeignKey(
                name: "FK_doubleName_cards_cards_cardid",
                table: "doubleName_cards",
                column: "cardid",
                principalTable: "cards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doubleName_cards_cards_cardid",
                table: "doubleName_cards");

            migrationBuilder.DropIndex(
                name: "IX_doubleName_cards_cardid",
                table: "doubleName_cards");

            migrationBuilder.DropColumn(
                name: "cardid",
                table: "doubleName_cards");

            migrationBuilder.AddColumn<int>(
                name: "cardNamesid",
                table: "cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
