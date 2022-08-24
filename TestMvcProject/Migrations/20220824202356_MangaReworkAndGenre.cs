using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class MangaReworkAndGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Favorites",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Mangas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Mangas",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoredBy",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleJapanese",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Volumes",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "Animies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MangaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreManga",
                columns: table => new
                {
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MangaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreManga", x => new { x.GenreId, x.MangaId });
                    table.ForeignKey(
                        name: "FK_GenreManga_Genres_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreManga_Mangas_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animies_AnimeId",
                table: "Animies",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreManga_MangaId",
                table: "GenreManga",
                column: "MangaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animies_Genres_AnimeId",
                table: "Animies",
                column: "AnimeId",
                principalTable: "Genres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animies_Genres_AnimeId",
                table: "Animies");

            migrationBuilder.DropTable(
                name: "GenreManga");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Animies_AnimeId",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Favorites",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "ScoredBy",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "TitleJapanese",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "Volumes",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Animies");
        }
    }
}
