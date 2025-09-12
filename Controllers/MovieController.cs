using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        // In-memory movie list
        private static readonly List<Movie> _movies = new()
        {
            new Movie
            {
                Id = 1,
                Title = "Inception",
                Description = "A mind-bending thriller",
                Director = "Christopher Nolan",
                Genre = "Sci-Fi",
                Rating = 8.8,
                PosterUrl = "/images/inception.jpg"
            }
        };

        private static int _nextId = 2;

        // LIST / INDEX with optional search
        public IActionResult Index(string? search)
        {
            var movies = _movies.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                movies = movies.Where(m =>
                    m.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                    m.Genre.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                    m.Director.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                );
            }

            ViewData["Search"] = search;
            return View(movies);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            return movie is null ? NotFound() : View(movie);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);

            movie.Id = _nextId++;
            _movies.Add(movie);

            return RedirectToAction(nameof(Index));
        }

        // EDIT
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            return movie is null ? NotFound() : View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movie updatedMovie)
        {
            if (!ModelState.IsValid) return View(updatedMovie);

            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie is null) return NotFound();

            movie.Title = updatedMovie.Title;
            movie.Description = updatedMovie.Description;
            movie.Director = updatedMovie.Director;
            movie.Genre = updatedMovie.Genre;
            movie.Rating = updatedMovie.Rating;
            movie.PosterUrl = updatedMovie.PosterUrl;

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            return movie is null ? NotFound() : View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                _movies.Remove(movie);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
