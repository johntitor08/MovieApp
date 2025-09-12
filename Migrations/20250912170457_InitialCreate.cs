using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Director = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    PosterUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Genre", "PosterUrl", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, "A mind-bending thriller", "Christopher Nolan", "Sci-Fi, Thriller", "/images/inception.jpg", 8.8000000000000007, "Inception" },
                    { 2, "A computer hacker learns about the true nature of reality.", "Lana Wachowski, Lilly Wachowski", "Action, Sci-Fi", "/images/matrix.jpg", 8.6999999999999993, "The Matrix" },
                    { 3, "A team travels through a wormhole in space to save humanity.", "Christopher Nolan", "Adventure, Drama, Sci-Fi", "/images/interstellar.jpg", 8.5999999999999996, "Interstellar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
