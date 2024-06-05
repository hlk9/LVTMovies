using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Context;
using Movies.DAL.Models;
using Newtonsoft.Json;

namespace Movies.WebApp.Controllers
{
    public class AccountController : Controller
    {
        MovieDbContext _context;

        HttpClient _httpClient;

        public AccountController()
        {
            _context = new MovieDbContext();  
            _httpClient = new HttpClient();
        }

        public IActionResult ListAccountManager()
        {
            string requestUrl = "https://localhost:7279/api/User/GetAllUser";
            var response =  _httpClient.GetStringAsync(requestUrl).Result;
            List<User> lstUsers = JsonConvert.DeserializeObject<List<User>>(response);

            return View(lstUsers);
        }

        public IActionResult ListOfRentedMovies()
        {
            var lstRented = _context.Users.ToList();
            return View(lstRented);
        }

        public IActionResult DetailsAccount(Guid id)
        {
            string requestUrl = $"https://localhost:7279/api/User/GetUserById?id={id}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;
            User User = JsonConvert.DeserializeObject<User>(response);

            return View(User);
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(User user)
        {
            string requestUrl = "https://localhost:7279/api/User/CreateUser";
            var response = _httpClient.PostAsJsonAsync(requestUrl, user).Result;
            ViewData["result"]= response;
            return RedirectToAction("ListAccountManager");
        }
    }
}
