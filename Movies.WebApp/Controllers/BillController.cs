using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;

namespace Movies.WebApp.Controllers
{
    public class BillController : Controller
    {
        MovieDbContext _context;

        public BillController()
        {
            _context = new MovieDbContext();
        }

        public IActionResult ListBillManager()
        {
            return View();
        }
    }
}
