using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class TablePositionFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Authors_AuthorId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_AuthorId",
                table: "Positions");

            migrationBuilder.CreateTable(
                name: "AuthorPosition",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPosition", x => new { x.AuthorId, x.AuthorsId });
                    table.ForeignKey(
                        name: "FK_AuthorPosition_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorPosition_Positions_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPosition_AuthorsId",
                table: "AuthorPosition",
                column: "AuthorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorPosition");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_AuthorId",
                table: "Positions",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Authors_AuthorId",
                table: "Positions",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}
