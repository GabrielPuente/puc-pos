using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBF.Infra.Data.Migrations
{
    public partial class AddEventInMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Tournament_TournamentId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Match");

            migrationBuilder.AlterColumn<Guid>(
                name: "TournamentId",
                table: "Match",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "varchar(300)", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_MatchId",
                table: "Event",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Tournament_TournamentId",
                table: "Match",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Tournament_TournamentId",
                table: "Match");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.AlterColumn<Guid>(
                name: "TournamentId",
                table: "Match",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Match",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Match",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Match",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Match",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Tournament_TournamentId",
                table: "Match",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id");
        }
    }
}
