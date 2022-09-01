using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class Namingfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAuthor_Animies_AuthorId",
                table: "AnimeAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Animies_GenreId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Animies_MangaId",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Mangas_AnimeId",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Animies_AnimiesId",
                table: "AnimePosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManga_Mangas_AuthorId",
                table: "AuthorManga");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreManga_Mangas_GenreId",
                table: "GenreManga");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Animies_AnimeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Mangas_MangaId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mangas",
                table: "Mangas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animies",
                table: "Animies");

            migrationBuilder.RenameTable(
                name: "Mangas",
                newName: "Manga");

            migrationBuilder.RenameTable(
                name: "Animies",
                newName: "Anime");

            migrationBuilder.RenameColumn(
                name: "AnimiesId",
                table: "AnimePosition",
                newName: "AnimeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manga",
                table: "Manga",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anime",
                table: "Anime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAuthor_Anime_AuthorId",
                table: "AnimeAuthor",
                column: "AuthorId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Anime_GenreId",
                table: "AnimeGenre",
                column: "GenreId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Anime_MangaId",
                table: "AnimeManga",
                column: "MangaId",
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
                name: "FK_AnimePosition_Anime_AnimeId",
                table: "AnimePosition",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManga_Manga_AuthorId",
                table: "AuthorManga",
                column: "AuthorId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreManga_Manga_GenreId",
                table: "GenreManga",
                column: "GenreId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Anime_AnimeId",
                table: "Images",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Manga_MangaId",
                table: "Images",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAuthor_Anime_AuthorId",
                table: "AnimeAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Anime_GenreId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Anime_MangaId",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Manga_AnimeId",
                table: "AnimeManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Anime_AnimeId",
                table: "AnimePosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManga_Manga_AuthorId",
                table: "AuthorManga");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreManga_Manga_GenreId",
                table: "GenreManga");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Anime_AnimeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Manga_MangaId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manga",
                table: "Manga");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anime",
                table: "Anime");

            migrationBuilder.RenameTable(
                name: "Manga",
                newName: "Mangas");

            migrationBuilder.RenameTable(
                name: "Anime",
                newName: "Animies");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "AnimePosition",
                newName: "AnimiesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mangas",
                table: "Mangas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animies",
                table: "Animies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAuthor_Animies_AuthorId",
                table: "AnimeAuthor",
                column: "AuthorId",
                principalTable: "Animies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Animies_GenreId",
                table: "AnimeGenre",
                column: "GenreId",
                principalTable: "Animies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Animies_MangaId",
                table: "AnimeManga",
                column: "MangaId",
                principalTable: "Animies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Mangas_AnimeId",
                table: "AnimeManga",
                column: "AnimeId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimePosition_Animies_AnimiesId",
                table: "AnimePosition",
                column: "AnimiesId",
                principalTable: "Animies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManga_Mangas_AuthorId",
                table: "AuthorManga",
                column: "AuthorId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreManga_Mangas_GenreId",
                table: "GenreManga",
                column: "GenreId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Animies_AnimeId",
                table: "Images",
                column: "AnimeId",
                principalTable: "Animies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Mangas_MangaId",
                table: "Images",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id");
        }
    }
}
