using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class FkFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Positions_AuthorId",
                table: "AnimePosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPosition_Authors_AuthorsId",
                table: "AuthorPosition");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Anime");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Anime");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "AuthorPosition",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorPosition_AuthorsId",
                table: "AuthorPosition",
                newName: "IX_AuthorPosition_PositionId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AnimePosition",
                newName: "PositionsId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimePosition_AuthorId",
                table: "AnimePosition",
                newName: "IX_AnimePosition_PositionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimePosition_Positions_PositionsId",
                table: "AnimePosition",
                column: "PositionsId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPosition_Authors_PositionId",
                table: "AuthorPosition",
                column: "PositionId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimePosition_Positions_PositionsId",
                table: "AnimePosition");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPosition_Authors_PositionId",
                table: "AuthorPosition");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "AuthorPosition",
                newName: "AuthorsId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorPosition_PositionId",
                table: "AuthorPosition",
                newName: "IX_AuthorPosition_AuthorsId");

            migrationBuilder.RenameColumn(
                name: "PositionsId",
                table: "AnimePosition",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimePosition_PositionsId",
                table: "AnimePosition",
                newName: "IX_AnimePosition_AuthorId");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "Positions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Positions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Anime",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Anime",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MangaId",
                table: "Anime",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimePosition_Positions_AuthorId",
                table: "AnimePosition",
                column: "AuthorId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPosition_Authors_AuthorsId",
                table: "AuthorPosition",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
