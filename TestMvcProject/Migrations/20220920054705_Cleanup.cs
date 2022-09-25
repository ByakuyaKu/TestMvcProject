using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class Cleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Anime_AnimeId",
                table: "AnimePosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Positions_PositionsId",
                table: "AnimePosition");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "PositionsId",
                table: "AnimePosition",
                newName: "AnimeId1");

            migrationBuilder.RenameIndex(
                name: "IX_AnimePosition_PositionsId",
                table: "AnimePosition",
                newName: "IX_AnimePosition_AnimeId1");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImageId",
                table: "Images",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimePosition_Anime_AnimeId1",
                table: "AnimePosition",
                column: "AnimeId1",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimePosition_Positions_AnimeId",
                table: "AnimePosition",
                column: "AnimeId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Anime_ImageId",
                table: "Images",
                column: "ImageId",
                principalTable: "Anime",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Authors_ImageId",
                table: "Images",
                column: "ImageId",
                principalTable: "Authors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Manga_ImageId",
                table: "Images",
                column: "ImageId",
                principalTable: "Manga",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Anime_AnimeId1",
                table: "AnimePosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Positions_AnimeId",
                table: "AnimePosition");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Anime_ImageId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Authors_ImageId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Manga_ImageId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ImageId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "AnimeId1",
                table: "AnimePosition",
                newName: "PositionsId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimePosition_AnimeId1",
                table: "AnimePosition",
                newName: "IX_AnimePosition_PositionsId");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "Manga",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Manga",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Manga",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MangaId",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "FK_AnimePosition_Anime_AnimeId",
                table: "AnimePosition",
                column: "AnimeId",
                principalTable: "Anime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimePosition_Positions_PositionsId",
                table: "AnimePosition",
                column: "PositionsId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
