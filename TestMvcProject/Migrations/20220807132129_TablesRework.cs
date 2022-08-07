using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class TablesRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AuthorFirstName",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AuthorSecondName",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "ProducerFirstName",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "ProducerSecondName",
                table: "Animies");

            migrationBuilder.DropColumn(
                name: "ProducerThirdName",
                table: "Animies");

            migrationBuilder.RenameColumn(
                name: "AuthorThirdName",
                table: "Mangas",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MangaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Animies_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Authors_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ItemCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MangaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Animies_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Images_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Images_Mangas_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animies_MangaId",
                table: "Animies",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AnimeId",
                table: "Authors",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_MangaId",
                table: "Authors",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnimeId",
                table: "Images",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_MangaId",
                table: "Images",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_AuthorId",
                table: "Positions",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animies_Mangas_MangaId",
                table: "Animies",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animies_Mangas_MangaId",
                table: "Animies");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Animies_MangaId",
                table: "Animies");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Mangas",
                newName: "AuthorThirdName");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "Mangas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AuthorFirstName",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorSecondName",
                table: "Mangas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProducerFirstName",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProducerSecondName",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProducerThirdName",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
