using Microsoft.EntityFrameworkCore.Migrations;

namespace MTG_Mvc.Migrations
{
    public partial class MainBoard_SideBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isMainBoard",
                table: "cards",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMainBoard",
                table: "cards");
        }
    }
}
