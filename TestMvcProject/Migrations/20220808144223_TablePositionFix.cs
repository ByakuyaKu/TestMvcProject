using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class TablePositionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Authors_AuthorId",
                table: "Positions");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Positions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Authors_AuthorId",
                table: "Positions",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Authors_AuthorId",
                table: "Positions");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Positions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Authors_AuthorId",
                table: "Positions",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
