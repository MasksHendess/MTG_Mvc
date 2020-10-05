using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class requestBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "decklists",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deckName = table.Column<string>(nullable: false),
                    cardsAmount = table.Column<int>(nullable: false),
                    creaturesAmount = table.Column<int>(nullable: false),
                    sorceriesAmount = table.Column<int>(nullable: false),
                    instantsAmount = table.Column<int>(nullable: false),
                    landsAmount = table.Column<int>(nullable: false),
                    avarageCMC = table.Column<int>(nullable: false),
                    requestBody = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_decklists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    set = table.Column<string>(nullable: true),
                    imageUrl = table.Column<string>(nullable: true),
                    artist = table.Column<string>(nullable: true),
                    cmc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    manaCost = table.Column<string>(nullable: true),
                    flavourText = table.Column<string>(nullable: true),
                    text = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    rarity = table.Column<string>(nullable: true),
                    power = table.Column<string>(nullable: true),
                    toughness = table.Column<string>(nullable: true),
                    isMainBoard = table.Column<bool>(nullable: false),
                    decklistid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.id);
                    table.ForeignKey(
                        name: "FK_cards_decklists_decklistid",
                        column: x => x.decklistid,
                        principalTable: "decklists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doubleName_cards",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: true),
                    secondName = table.Column<string>(nullable: true),
                    cardid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doubleName_cards", x => x.id);
                    table.ForeignKey(
                        name: "FK_doubleName_cards_cards_cardid",
                        column: x => x.cardid,
                        principalTable: "cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cards_decklistid",
                table: "cards",
                column: "decklistid");

            migrationBuilder.CreateIndex(
                name: "IX_doubleName_cards_cardid",
                table: "doubleName_cards",
                column: "cardid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doubleName_cards");

            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "decklists");
        }
    }
}
