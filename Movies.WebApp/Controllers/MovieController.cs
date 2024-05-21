using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Context;
using Movies.DAL.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Movies.WebApp.Controllers
{
    public class MovieController : Controller
    {
        string baseURL = "https://localhost:7279/";
        MovieDbContext _context;
        public MovieController()
        {
            _context = new MovieDbContext();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WatchMovie()
        {
            var lstWatch = _context.Users.ToList();
            return View(lstWatch);
        }

        public async Task<ActionResult> Detail(int id)
        {
            
            Movie movie = new Movie();
            using(var client = new HttpClient())
            {
               client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Movie?id=" + id);
                if(res.IsSuccessStatusCode)
                {
                    var moviesRes = res.Content.ReadAsStringAsync().Result;
                    movie = JsonConvert.DeserializeObject<Movie>(moviesRes);
                }    
            }

            return View(movie);
        }
        public IActionResult ListOfMoviesGenres()
        {
            List<MovieGenre> movieGenres = new List<MovieGenre>();
            return View(movieGenres);
        }
        public IActionResult ListOfMoviesByActors()
        {
            return View();
        }

        public IActionResult ListMovieManager()
        {
            var lst  = _context.Movies.ToList();
            return View(lst);
        }
    }
}
