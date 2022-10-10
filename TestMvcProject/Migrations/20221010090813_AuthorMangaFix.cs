using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class AuthorMangaFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManga_Authors_MangaId",
                table: "AuthorManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManga_Manga_AuthorId",
                table: "AuthorManga");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AuthorManga",
                newName: "AuthorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManga_Authors_AuthorsId",
                table: "AuthorManga",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManga_Manga_MangaId",
                table: "AuthorManga",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManga_Authors_AuthorsId",
                table: "AuthorManga");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorManga_Manga_MangaId",
                table: "AuthorManga");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "AuthorManga",
                newName: "AuthorId");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MangaId",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManga_Authors_MangaId",
                table: "AuthorManga",
                column: "MangaId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorManga_Manga_AuthorId",
                table: "AuthorManga",
                column: "AuthorId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
