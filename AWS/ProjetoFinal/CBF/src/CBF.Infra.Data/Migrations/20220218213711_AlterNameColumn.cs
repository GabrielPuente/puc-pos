using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBF.Infra.Data.Migrations
{
    public partial class AlterNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Team_TeamDentinyId",
                table: "Transfer");

            migrationBuilder.RenameColumn(
                name: "TeamDentinyId",
                table: "Transfer",
                newName: "TeamDestinyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_TeamDentinyId",
                table: "Transfer",
                newName: "IX_Transfer_TeamDestinyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Team_TeamDestinyId",
                table: "Transfer",
                column: "TeamDestinyId",
                principalTable: "Team",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfer_Team_TeamDestinyId",
                table: "Transfer");

            migrationBuilder.RenameColumn(
                name: "TeamDestinyId",
                table: "Transfer",
                newName: "TeamDentinyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transfer_TeamDestinyId",
                table: "Transfer",
                newName: "IX_Transfer_TeamDentinyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfer_Team_TeamDentinyId",
                table: "Transfer",
                column: "TeamDentinyId",
                principalTable: "Team",
                principalColumn: "Id");
        }
    }
}
