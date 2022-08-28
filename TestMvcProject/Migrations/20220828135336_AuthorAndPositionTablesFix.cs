using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class AuthorAndPositionTablesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnimeId",
                table: "Positions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberFavorites",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimePosition",
                columns: table => new
                {
                    AnimiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimePosition", x => new { x.AnimiesId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_AnimePosition_Animies_AnimiesId",
                        column: x => x.AnimiesId,
                        principalTable: "Animies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimePosition_Positions_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimePosition_AuthorId",
                table: "AnimePosition",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimePosition");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "MemberFavorites",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Authors");
        }
    }
}
