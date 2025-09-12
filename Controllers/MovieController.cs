using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MovieApp.Controllers
{
    public class MovieController(MovieDbContext context) : Controller
    {
        private readonly MovieDbContext _context = context;

        public async Task<IActionResult> Index(string? search)
        {
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                movies = movies.Where(m =>
                    m.Title.ToLower().Contains(search) ||
                    m.Genre.ToLower().Contains(search) ||
                    m.Director.ToLower().Contains(search)
                );
            }

            ViewData["Search"] = search;
            return View(await movies.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            return movie == null ? NotFound() : View(movie);
        }

        [HttpGet("filter/{filter}", Name = "MovieFilter")]
        public async Task<IActionResult> Filter(string filter, string? search)
        {
            var movies = _context.Movies.AsQueryable();
            filter = filter.ToLower();

            movies = filter switch
            {
                "popular" => movies.Where(m => m.Rating >= 8),
                "toprated" => movies.OrderByDescending(m => m.Rating),
                "comingsoon" => movies.Where(m => m.Genre.Contains("upcoming", StringComparison.CurrentCultureIgnoreCase)),
                "action" => movies.Where(m => m.Genre.Contains("action", StringComparison.CurrentCultureIgnoreCase)),
                "comedy" => movies.Where(m => m.Genre.Contains("comedy", StringComparison.CurrentCultureIgnoreCase)),
                "drama" => movies.Where(m => m.Genre.Contains("drama", StringComparison.CurrentCultureIgnoreCase)),
                _ => movies
            };

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
            ViewData["Filter"] = filter;
            return View("Index", await movies.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (!ModelState.IsValid) return View(movie);

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            return movie == null ? NotFound() : View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie updatedMovie)
        {
            if (!ModelState.IsValid) return View(updatedMovie);

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            movie.Title = updatedMovie.Title;
            movie.Description = updatedMovie.Description;
            movie.Director = updatedMovie.Director;
            movie.Genre = updatedMovie.Genre;
            movie.Rating = updatedMovie.Rating;
            movie.PosterUrl = updatedMovie.PosterUrl;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            return movie == null ? NotFound() : View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
