using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Director { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Range(0, 10)]
        public double Rating { get; set; }

        [Url(ErrorMessage = "Enter a valid URL")]
        public string? PosterUrl { get; set; }
    }
}
