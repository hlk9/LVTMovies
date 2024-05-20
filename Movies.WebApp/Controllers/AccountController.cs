using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;

namespace Movies.WebApp.Controllers
{
    public class AccountController : Controller
    {
        MovieDbContext _context;

        public AccountController()
        {
            _context = new MovieDbContext();    
        }

        public IActionResult ListAccountManager()
        {
            var lstAccount = _context.Users.ToList();
            return View(lstAccount);
        }

        public IActionResult ListOfRentedMovies()
        {
            var lstRented = _context.Users.ToList();
            return View(lstRented);
        }

        public IActionResult Details(Guid id)
        {
            var objAccount = _context.Users.Find(id);

            return View(objAccount);
        }
    }
}
