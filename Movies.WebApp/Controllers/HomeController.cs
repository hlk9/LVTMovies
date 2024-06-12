using Microsoft.AspNetCore.Mvc;
using Movies.WebApp.Models;
using System.Diagnostics;
using RestSharp;
using Newtonsoft.Json.Linq;
using Movies.WebApp.ViewModel;

namespace Movies.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/popular?language=vi-VN&page=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI3NDc3ZWNlYjI4OWQ0YzBjMWE3M2VlNjRmODNlMjdkMyIsInN1YiI6IjY2M2EzZGNlMzU4ZGE3MDEyNDU3OGI3NCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.SSQffIQ5zchkh83x_TBRYyEa1PVZB_X0R80-yJSkPeI");
            var response = await client.GetAsync(request);

            JObject detailJson = JObject.Parse(response.Content);

            var movies = new List<MoviePopularViewModel>();

            var movies2 = new List<MoviePopularViewModel>();

            foreach (var item in detailJson["results"])
            {

                if (movies.Count < 4)
                {
                    movies.Add(new MoviePopularViewModel
                    {
                        MovieId = (int)item["id"],
                        Title = item["title"].ToString(),
                        PosterPath = item["poster_path"].ToString(),
                        ReleaseDate = DateTime.Parse(item["release_date"].ToString()),
                        Rating = (double)item["vote_average"]

                    });
                }else if(movies.Count>=4 && movies2.Count<8)
                {
                       movies2.Add(new MoviePopularViewModel
                       {
                        MovieId = (int)item["id"],
                        Title = item["title"].ToString(),
                        PosterPath = item["poster_path"].ToString(),
                        ReleaseDate = DateTime.Parse(item["release_date"].ToString()),
                        Rating = (double)item["vote_average"]

                    });
                }   
                else
                {
                    break;
                }
               


            }
            ViewBag.PopularMovies = movies;

            ViewBag.PopularMovies2 = movies2;

            ViewBag.RandomMovie = movies2[new Random().Next(0, movies2.Count)].MovieId;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
