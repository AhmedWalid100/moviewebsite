using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesProject.Migrations
{
    /// <inheritdoc />
    public partial class thirtysix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosterURL",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterURL",
                table: "Actors");
        }
    }
}
