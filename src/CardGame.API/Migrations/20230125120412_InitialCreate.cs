// <copyright file="20230125120412_InitialCreate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace CardGame.API.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardGames",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeckId = table.Column<string>(type: "text", nullable: true),
                    HasWinner = table.Column<bool>(type: "boolean", nullable: false),
                    RoundsPlayed = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGames", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "CardGamePlayer",
                columns: table => new
                {
                    CardGamesGameId = table.Column<int>(type: "integer", nullable: false),
                    PlayersPlayerId = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardGamePlayer", x => new { x.CardGamesGameId, x.PlayersPlayerId });
                    table.ForeignKey(
                        name: "FK_CardGamePlayer_CardGames_CardGamesGameId",
                        column: x => x.CardGamesGameId,
                        principalTable: "CardGames",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardGamePlayer_Players_PlayersPlayerId",
                        column: x => x.PlayersPlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundInfo",
                columns: table => new
                {
                    RoundInfoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardValue = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: true),
                    PlayerId1 = table.Column<int>(type: "integer", nullable: true),
                    CardGameGameId = table.Column<int>(type: "integer", nullable: true),
                    PlayerId = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundInfo", x => x.RoundInfoId);
                    table.ForeignKey(
                        name: "FK_RoundInfo_CardGames_CardGameGameId",
                        column: x => x.CardGameGameId,
                        principalTable: "CardGames",
                        principalColumn: "GameId");
                    table.ForeignKey(
                        name: "FK_RoundInfo_CardGames_GameId",
                        column: x => x.GameId,
                        principalTable: "CardGames",
                        principalColumn: "GameId");
                    table.ForeignKey(
                        name: "FK_RoundInfo_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_RoundInfo_Players_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardGamePlayer_PlayersPlayerId",
                table: "CardGamePlayer",
                column: "PlayersPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundInfo_CardGameGameId",
                table: "RoundInfo",
                column: "CardGameGameId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundInfo_GameId",
                table: "RoundInfo",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundInfo_PlayerId",
                table: "RoundInfo",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundInfo_PlayerId1",
                table: "RoundInfo",
                column: "PlayerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardGamePlayer");

            migrationBuilder.DropTable(
                name: "RoundInfo");

            migrationBuilder.DropTable(
                name: "CardGames");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
