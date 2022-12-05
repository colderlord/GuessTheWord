using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GuessWord.API.Migrations
{
    /// <inheritdoc />
    public partial class Initialized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuessGame",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuessGame", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuessGameId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryItem_GuessGame_GuessGameId",
                        column: x => x.GuessGameId,
                        principalTable: "GuessGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Attempts = table.Column<int>(type: "integer", nullable: false),
                    WordLength = table.Column<int>(type: "integer", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    GuessGameId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_GuessGame_GuessGameId",
                        column: x => x.GuessGameId,
                        principalTable: "GuessGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WordModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Word = table.Column<string>(type: "text", nullable: false),
                    HistoryItemId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordModel_HistoryItem_HistoryItemId",
                        column: x => x.HistoryItemId,
                        principalTable: "HistoryItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LetterModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Char = table.Column<char>(type: "character(1)", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    WordModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LetterModel_WordModel_WordModelId",
                        column: x => x.WordModelId,
                        principalTable: "WordModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryItem_GuessGameId",
                table: "HistoryItem",
                column: "GuessGameId");

            migrationBuilder.CreateIndex(
                name: "IX_LetterModel_WordModelId",
                table: "LetterModel",
                column: "WordModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_GuessGameId",
                table: "Settings",
                column: "GuessGameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordModel_HistoryItemId",
                table: "WordModel",
                column: "HistoryItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LetterModel");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "WordModel");

            migrationBuilder.DropTable(
                name: "HistoryItem");

            migrationBuilder.DropTable(
                name: "GuessGame");
        }
    }
}
