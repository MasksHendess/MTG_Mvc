using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class New_Card_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "decklists",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deckName = table.Column<string>(nullable: true)
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
                    isMainBoard = table.Column<bool>(nullable: false),
                    text = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    rarity = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_cards_decklistid",
                table: "cards",
                column: "decklistid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "decklists");
        }
    }
}
