using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class PT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "power",
                table: "cards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thoughness",
                table: "cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "power",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "thoughness",
                table: "cards");
        }
    }
}
