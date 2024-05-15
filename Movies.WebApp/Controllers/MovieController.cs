using Microsoft.AspNetCore.Mvc;

namespace Movies.WebApp.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
