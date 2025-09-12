using Microsoft.EntityFrameworkCore;
using MovieApp.Models;

namespace MovieApp.Data
{
    public class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Description = "A mind-bending thriller",
                    Director = "Christopher Nolan",
                    Genre = "Sci-Fi, Thriller",
                    Rating = 8.8,
                    PosterUrl = "/images/inception.jpg"
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Matrix",
                    Description = "A computer hacker learns about the true nature of reality.",
                    Director = "Lana Wachowski, Lilly Wachowski",
                    Genre = "Action, Sci-Fi",
                    Rating = 8.7,
                    PosterUrl = "/images/matrix.jpg"
                },
                new Movie
                {
                    Id = 3,
                    Title = "Interstellar",
                    Description = "A team travels through a wormhole in space to save humanity.",
                    Director = "Christopher Nolan",
                    Genre = "Adventure, Drama, Sci-Fi",
                    Rating = 8.6,
                    PosterUrl = "/images/interstellar.jpg"
                }
            );
        }
    }
}
