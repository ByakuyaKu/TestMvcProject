using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class AnimeAuthorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAuthor_Anime_AuthorId",
                table: "AnimeAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAuthor_Authors_AnimeId",
                table: "AnimeAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Anime_MangaId",
                table: "AnimeManga");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "AnimeManga",
                newName: "AnimeId1");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeManga_MangaId",
                table: "AnimeManga",
                newName: "IX_AnimeManga_AnimeId1");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AnimeAuthor",
                newName: "AuthorsId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeAuthor_AuthorId",
                table: "AnimeAuthor",
                newName: "IX_AnimeAuthor_AuthorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAuthor_Anime_AnimeId",
                table: "AnimeAuthor",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAuthor_Authors_AuthorsId",
                table: "AnimeAuthor",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Anime_AnimeId1",
                table: "AnimeManga",
                column: "AnimeId1",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAuthor_Anime_AnimeId",
                table: "AnimeAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAuthor_Authors_AuthorsId",
                table: "AnimeAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeManga_Anime_AnimeId1",
                table: "AnimeManga");

            migrationBuilder.RenameColumn(
                name: "AnimeId1",
                table: "AnimeManga",
                newName: "MangaId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeManga_AnimeId1",
                table: "AnimeManga",
                newName: "IX_AnimeManga_MangaId");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "AnimeAuthor",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeAuthor_AuthorsId",
                table: "AnimeAuthor",
                newName: "IX_AnimeAuthor_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAuthor_Anime_AuthorId",
                table: "AnimeAuthor",
                column: "AuthorId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAuthor_Authors_AnimeId",
                table: "AnimeAuthor",
                column: "AnimeId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeManga_Anime_MangaId",
                table: "AnimeManga",
                column: "MangaId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
