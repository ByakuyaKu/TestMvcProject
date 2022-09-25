using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class ImgFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Anime_ImageId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Authors_ImageId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ImageId",
                table: "Images");

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
                name: "ImageId",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "ImageId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImageId",
                table: "Images",
                column: "ImageId");

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
        }
    }
}
