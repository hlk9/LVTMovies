using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Context;
using Movies.DAL.Models;
using Movies.DAL.ViewModels;
using Movies.WebApp.Services;
using Movies.WebApp.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace Movies.WebApp.Controllers
{
    public class MovieController : Controller
    {
        MovieServices _movieServices;
        string baseURL = "https://localhost:7279/";
        MovieDbContext _context;
        public MovieController()
        {
            _context = new MovieDbContext();
            _movieServices = new MovieServices();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WatchMovie(int id)
        {
            var movieService = new MovieServices();
            var movie = _movieServices.GetById(id);
            if (movie == null)
            {
                return NotFound("không tìm thấy phim");
            }

            var movieViewModel = new MovieViewModel
            {
                Title = movie.Title,
                StreamURL = movie.StreamURL
            };
            return View(movieViewModel);
        }

        public async Task<ActionResult> Detail(int id)
        {

            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/popular?language=vi-VN&page=2");
            var client3 = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI3NDc3ZWNlYjI4OWQ0YzBjMWE3M2VlNjRmODNlMjdkMyIsInN1YiI6IjY2M2EzZGNlMzU4ZGE3MDEyNDU3OGI3NCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.SSQffIQ5zchkh83x_TBRYyEa1PVZB_X0R80-yJSkPeI");
            var response = await client3.GetAsync(request);

            JObject detailJsonPopular = JObject.Parse(response.Content);

            var movies = new List<MoviePopularViewModel>();

            foreach (var item in detailJsonPopular["results"])
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
                }
            }
            ViewBag.PopularMovies = movies;



            Movie movie = new Movie();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("Movie?id=" + id);
                if (res.IsSuccessStatusCode)
                {
                    var moviesRes = res.Content.ReadAsStringAsync().Result;
                    movie = JsonConvert.DeserializeObject<Movie>(moviesRes);
                    if (movie != null)
                    {

                        List<string> genreM = new List<string>();


                        foreach (var item in _context.MovieGenres.Where(x => x.MovieID == id).ToList())
                        {
                            genreM.Add(_context.Genres.Find(item.GenreID).Name);
                        }

                        var detailOptions = new RestClientOptions("https://api.themoviedb.org/3/movie/" + id + "?language=vi-VN");
                        var detailClient = new RestClient(detailOptions);
                        var detailRequest = new RestRequest("");
                        detailRequest.AddHeader("accept", "application/json");
                        detailRequest.AddHeader("Authorization", "Authorization\", \"Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI3NDc3ZWNlYjI4OWQ0YzBjMWE3M2VlNjRmODNlMjdkMyIsInN1YiI6IjY2M2EzZGNlMzU4ZGE3MDEyNDU3OGI3NCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.SSQffIQ5zchkh83x_TBRYyEa1PVZB_X0R80-yJSkPeI");
                        var detailResponse = await detailClient.GetAsync(detailRequest);

                        JObject detailJson = JObject.Parse(detailResponse.Content);


                        ViewBag.ReleaseDate = detailJson["release_date"].ToString().Split("-")[0];

                        ViewBag.GenreM = genreM;
                        return View(movie);
                    }
                    else
                    {

                        var detailOptions = new RestClientOptions("https://api.themoviedb.org/3/movie/" + id + "?language=vi-VN");
                        var detailClient = new RestClient(detailOptions);
                        var detailRequest = new RestRequest("");
                        detailRequest.AddHeader("accept", "application/json");
                        detailRequest.AddHeader("Authorization", "Authorization\", \"Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI3NDc3ZWNlYjI4OWQ0YzBjMWE3M2VlNjRmODNlMjdkMyIsInN1YiI6IjY2M2EzZGNlMzU4ZGE3MDEyNDU3OGI3NCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.SSQffIQ5zchkh83x_TBRYyEa1PVZB_X0R80-yJSkPeI");
                        var detailResponse = await detailClient.GetAsync(detailRequest);

                        JObject detailJson = JObject.Parse(detailResponse.Content);

                        Movie newMovie = new Movie();
                        newMovie.Id = (int)detailJson["id"];
                        newMovie.Title = detailJson["title"].ToString();
                        newMovie.Description = detailJson["overview"].ToString();
                        newMovie.PosterURL = "https://image.tmdb.org/t/p/original" + detailJson["poster_path"].ToString();
                        newMovie.BackdropURL = "https://image.tmdb.org/t/p/original" + detailJson["backdrop_path"].ToString();
                        newMovie.StreamURL = "";
                        if (detailJson["status"] != null)
                        {
                            newMovie.Status = detailJson["status"].ToString();
                        }
                        else
                        {

                            newMovie.Status = "Chưa ra mắt";
                        }


                        if (detailJson["budget"] != null)
                        {
                            newMovie.Bugget = (double)detailJson["budget"];
                        }
                        else
                        {

                            newMovie.Bugget = 0;
                        }


                        newMovie.RentalPrice = 59000;
                        newMovie.SalePrice = 0;
                        _context.Movies.Add(newMovie);


                        //add genres for movie
                        List<string> genreM = new List<string>();
                        JArray genreArray = (JArray)detailJson["genres"];

                        foreach (var genreItem in genreArray)
                        {
                            genreM.Add(genreItem["name"].ToString().Replace("Phim ", ""));
                            MovieGenre newMovieGenre = new MovieGenre();
                            newMovieGenre.MovieID = (int)detailJson["id"];
                            newMovieGenre.GenreID = (int)genreItem["id"];
                            _context.MovieGenres.Add(newMovieGenre);
                        }
                        _context.SaveChanges();

                        ViewBag.GenreM = genreM;
                        return View(newMovie);

                    }
                }
                return NotFound("Không có phim này");
            }





        }
        public IActionResult ListOfMoviesGenres(int id)
        {

            var lst = _movieServices.GetAllMoviesByGenreId(id);
            ViewBag.lstGenre = new List<Genre>();
            foreach (var item in _context.Genres.ToList())
            {
                ViewBag.lstGenre.Add(item);
            }
            if (id == 0)
            {
                var lstNull = _context.Movies.ToList();
                return View(lstNull);
            }
            else
            {

                return View(lst);
            }
        }
        public IActionResult ListOfMoviesByActors()
        {
            return View();
        }

        public IActionResult ListMovieManager()
        {
            var lst = _movieServices.GetAllMovies();
            return View(lst);
        }

        public IActionResult CreateMovie()
        {
            GenreServices genreServices = new GenreServices();
            var lstGenre = genreServices.GetAllGenre();
            ViewBag.Genres = lstGenre;
            return View();
        }

        [HttpPost]
        public IActionResult CreateMovie(Movie mv, string[] genres)
        {
            if (_movieServices.CreateMovie(mv) == true)
            {
                try
                {

                    foreach (var item in genres)
                    {
                        MovieGenre mg = new MovieGenre();
                        mg.MovieID = mv.Id;
                        mg.GenreID = int.Parse(item);
                        _movieServices.AddMovieGenre(mg);
                    }
                    return RedirectToAction("ListMovieManager");
                }
                catch
                { }
            }
            return View();
        }
    }
}
