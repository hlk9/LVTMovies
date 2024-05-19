using Microsoft.AspNetCore.Mvc;

namespace Movies.WebApp.Controllers
{
    public class AssetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Personal_Page()
        {
            return View(); 
        }

    }
}
