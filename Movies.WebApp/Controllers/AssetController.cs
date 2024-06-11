using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Context;
using Movies.DAL.Models;
using Movies.DAL.ViewModel;
using Movies.WebApp.Services;

namespace Movies.WebApp.Controllers
{
    public class AssetController : Controller
    {
        MovieDbContext _context = new MovieDbContext();
        AssetServices _serAss = new AssetServices();
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //if (username == null && password == null)
            //{
            //    return View();
            //}
            //else
            //{
            //    var acc = _context.Users.FirstOrDefault(p => p.UserName == username && p.Password == password);
            //    if (acc == null) return Content("Tài khoản ko tồn tại");
            //    else
            //    {
            //        TempData["acc"] = username;

            //        HttpContext.Session.SetString("username",  acc.UserName.ToString()); // Lưu username vào session
            //        HttpContext.Session.SetString("id", acc.Id.ToString());
            //        var role = _context.Roles.FirstOrDefault(x => x.Id == acc.RoleId)?.Name;
            //        HttpContext.Session.SetString("Role", role);

            //        return RedirectToAction("Index","Home");
            //    }
            //}


            var a = new LoginViewModels();
            a.UserName = username;
            a.Password = password;
            // goij service 
            
            if(_serAss.Login(a) == true)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return BadRequest();
            }

        }


        public IActionResult SignUp()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            try
            {
                user.Id =Guid.NewGuid();
                user.Status = 1;
                user.RoleId = 3;
                _serAss.CreateUser(user);
                TempData["SuccessMessage"] = "Create account successfully!";
                return RedirectToAction("Login","Asset");
            }
            catch 
            {

                return View();
            }
        }



        public IActionResult Personal_Page()
        {
            return View(); 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("Role");
            return RedirectToAction("Index", "Home");
        }

    }
}
