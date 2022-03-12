using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBF.Infra.Data.Migrations
{
    public partial class FirstCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Locality = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamOriginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamDentinyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfer_Team_TeamDentinyId",
                        column: x => x.TeamDentinyId,
                        principalTable: "Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfer_Team_TeamOriginId",
                        column: x => x.TeamOriginId,
                        principalTable: "Team",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId",
                table: "Player",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_PlayerId",
                table: "Transfer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_TeamDentinyId",
                table: "Transfer",
                column: "TeamDentinyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_TeamOriginId",
                table: "Transfer",
                column: "TeamOriginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfer");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
