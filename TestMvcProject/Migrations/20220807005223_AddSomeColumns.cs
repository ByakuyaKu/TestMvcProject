using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestMvcProject.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Animies",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Animies");
        }
    }
}
