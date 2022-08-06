using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimeToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnimeStarts = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnimeEnds = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeriesCount = table.Column<int>(type: "int", nullable: true),
                    SeriesRealesed = table.Column<int>(type: "int", nullable: true),
                    ProducerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProducerSecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProducerThirdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mangas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MangaStarts = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MangaEnds = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChaptersCount = table.Column<int>(type: "int", nullable: true),
                    ChaptersRealesed = table.Column<int>(type: "int", nullable: true),
                    AuthorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorSecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorThirdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mangas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animies");

            migrationBuilder.DropTable(
                name: "Mangas");
        }
    }
}
