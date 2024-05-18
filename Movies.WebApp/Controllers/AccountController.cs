using Microsoft.AspNetCore.Mvc;

namespace Movies.WebApp.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
