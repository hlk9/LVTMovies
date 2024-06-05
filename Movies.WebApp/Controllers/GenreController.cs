using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;
using Movies.DAL.Models;

namespace Movies.WebApp.Controllers
{
    public class GenreController : Controller
    {
        MovieDbContext _context = new MovieDbContext();
        public IActionResult Index()
        {
            IEnumerable<Genre> lst = _context.Genres.ToList();
            return View(lst);
        }
        public IActionResult Create()
        {
            return View();
        }

    }
}
