using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserPointsApiBackend.Migrations
{
    public partial class AddedRankcolumntoUserPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Users",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Users");
        }
    }
}
