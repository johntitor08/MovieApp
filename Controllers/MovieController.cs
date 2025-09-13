using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class MovieController(MovieDbContext context) : Controller
    {
        private readonly MovieDbContext _context = context;

        public async Task<IActionResult> Index(string? name)
        {
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                string searchLower = name.ToLower();
                movies = movies.Where(m => EF.Functions.Like(m.Title.ToLower(), $"%{searchLower}%"));
            }

            ViewData["Search"] = name;
            return View(await movies.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            return movie == null ? NotFound() : View(movie);
        }

        [HttpGet("filter/{filter}", Name = "MovieFilter")]
        public async Task<IActionResult> Filter(string filter)
        {
            var movies = _context.Movies.AsQueryable();
            filter = filter.ToLower();

            if (filter == "mylist")
                movies = movies.Where(m => m.Genre != null && m.Genre.ToLower().Contains("favorite"));
            else if (filter == "popular")
                movies = movies.Where(m => m.Rating >= 8).OrderByDescending(m => m.Rating);
            else if (filter == "toprated")
                movies = movies.OrderByDescending(m => m.Rating);
            else if (filter == "comingsoon")
                movies = movies.Where(m => m.Genre != null && m.Genre.ToLower().Contains("upcoming"));
            else if (filter == "action")
                movies = movies.Where(m => m.Genre != null && m.Genre.ToLower().Contains("action"));
            else if (filter == "comedy")
                movies = movies.Where(m => m.Genre != null && m.Genre.ToLower().Contains("comedy"));
            else if (filter == "drama")
                movies = movies.Where(m => m.Genre != null && m.Genre.ToLower().Contains("drama"));

            ViewData["Filter"] = filter;
            ViewData["Search"] = null;

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
