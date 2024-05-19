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
    }
}
