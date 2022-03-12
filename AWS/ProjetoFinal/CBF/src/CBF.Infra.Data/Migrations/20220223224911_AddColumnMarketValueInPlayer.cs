using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBF.Infra.Data.Migrations
{
    public partial class AddColumnMarketValueInPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MarketValue",
                table: "Player",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketValue",
                table: "Player");
        }
    }
}
