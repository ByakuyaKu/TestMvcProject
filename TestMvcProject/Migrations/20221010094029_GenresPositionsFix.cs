using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class GenresPositionsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Anime_GenreId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Genres_AnimeId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Anime_AnimeId1",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Manga_AnimeId",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPosition_Authors_PositionId",
                table: "AuthorPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPosition_Positions_AuthorId",
                table: "AuthorPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreManga_Genres_MangaId",
                table: "GenreManga");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreManga_Manga_GenreId",
                table: "GenreManga");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "GenreManga",
                newName: "GenresId");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "AuthorPosition",
                newName: "PositionsId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AuthorPosition",
                newName: "AuthorsId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorPosition_PositionId",
                table: "AuthorPosition",
                newName: "IX_AuthorPosition_PositionsId");

            migrationBuilder.RenameColumn(
                name: "AnimeId1",
                table: "AnimeManga",
                newName: "MangaId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeManga_AnimeId1",
                table: "AnimeManga",
                newName: "IX_AnimeManga_MangaId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "AnimeGenre",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeGenre_GenreId",
                table: "AnimeGenre",
                newName: "IX_AnimeGenre_GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Anime_AnimeId",
                table: "AnimeGenre",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Genres_GenresId",
                table: "AnimeGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Anime_AnimeId",
                table: "AnimeManga",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Manga_MangaId",
                table: "AnimeManga",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPosition_Authors_AuthorsId",
                table: "AuthorPosition",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPosition_Positions_PositionsId",
                table: "AuthorPosition",
                column: "PositionsId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreManga_Genres_GenresId",
                table: "GenreManga",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreManga_Manga_MangaId",
                table: "GenreManga",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Anime_AnimeId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Genres_GenresId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Anime_AnimeId",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Manga_MangaId",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPosition_Authors_AuthorsId",
                table: "AuthorPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPosition_Positions_PositionsId",
                table: "AuthorPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreManga_Genres_GenresId",
                table: "GenreManga");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreManga_Manga_MangaId",
                table: "GenreManga");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GenreManga",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "PositionsId",
                table: "AuthorPosition",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "AuthorPosition",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorPosition_PositionsId",
                table: "AuthorPosition",
                newName: "IX_AuthorPosition_PositionId");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "AnimeManga",
                newName: "AnimeId1");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeManga_MangaId",
                table: "AnimeManga",
                newName: "IX_AnimeManga_AnimeId1");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "AnimeGenre",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeGenre_GenresId",
                table: "AnimeGenre",
                newName: "IX_AnimeGenre_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Anime_GenreId",
                table: "AnimeGenre",
                column: "GenreId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Genres_AnimeId",
                table: "AnimeGenre",
                column: "AnimeId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Anime_AnimeId1",
                table: "AnimeManga",
                column: "AnimeId1",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Manga_AnimeId",
                table: "AnimeManga",
                column: "AnimeId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPosition_Authors_PositionId",
                table: "AuthorPosition",
                column: "PositionId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPosition_Positions_AuthorId",
                table: "AuthorPosition",
                column: "AuthorId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreManga_Genres_MangaId",
                table: "GenreManga",
                column: "MangaId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreManga_Manga_GenreId",
                table: "GenreManga",
                column: "GenreId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
