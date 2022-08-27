using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class AnimeTableRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animies_Genres_AnimeId",
                table: "Animies");

            migrationBuilder.DropIndex(
                name: "IX_Animies_AnimeId",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Mangas");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "Animies",
                newName: "GenreId");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Favorites",
                table: "Animies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkCanonical",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Animies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Premiered",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Animies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "Animies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoredBy",
                table: "Animies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleJapanese",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Volumes",
                table: "Animies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimeGenre",
                columns: table => new
                {
                    AnimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeGenre", x => new { x.AnimeId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AnimeGenre_Animies_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Animies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeGenre_Genres_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeGenre_GenreId",
                table: "AnimeGenre",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeGenre");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Favorites",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "LinkCanonical",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Premiered",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "ScoredBy",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "TitleJapanese",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Volumes",
                table: "Animies");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Animies",
                newName: "AnimeId");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animies_AnimeId",
                table: "Animies",
                column: "AnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animies_Genres_AnimeId",
                table: "Animies",
                column: "AnimeId",
                principalTable: "Genres",
                principalColumn: "Id");
        }
    }
}
