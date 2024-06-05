using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;
using Movies.DAL.Models;
using Movies.WebApp.Services;
using Newtonsoft.Json;

namespace Movies.WebApp.Controllers
{
    public class AccountController : Controller
    {
        AssetServices _serAss;

        public AccountController()
        {
            _serAss = new AssetServices();
        }

        public IActionResult ListAccountManager()
        {
            var lstUsers = _serAss.GetAll();
            return View(lstUsers);
        }

        public IActionResult ListOfRentedMovies()
        {
            return View();
        }

        public IActionResult DetailsAccount(Guid id)
        {
            var User = _serAss.GetById(id); 

            return View(User);
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(User user)
        {
            _serAss.CreateUser(user);
            return RedirectToAction("ListAccountManager");
        }

        public IActionResult UpdateAccount(Guid id)
        {
            var User = _serAss.GetById(id);
            return View(User);
        }

        [HttpPost]
        public IActionResult UpdateAccount(User user)
        {
            _serAss.UpdateUser(user);
            return RedirectToAction("ListAccountManager");
        }

        public IActionResult DeleteAccount(Guid id)
        {
            _serAss.DeleteUser(id);
            return RedirectToAction("ListAccountManager");
        }
    }
}
