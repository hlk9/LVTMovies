﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Context;
using Movies.DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net.Http.Headers;
using System.Security.Cryptography;

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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Movie?id=" + id);
                if (res.IsSuccessStatusCode)
                {
                    var moviesRes = res.Content.ReadAsStringAsync().Result;
                    movie = JsonConvert.DeserializeObject<Movie>(moviesRes);
                    if (movie != null)
                    {
                        return View(movie);
                    }
                    else
                    {

                        var detailOptions = new RestClientOptions("https://api.themoviedb.org/3/movie/" +id+ "?language=vi-VN");
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

                        JArray genreArray = (JArray)detailJson["genres"];
                        foreach (var genreItem in genreArray)
                        {

                            MovieGenre newMovieGenre = new MovieGenre();
                            newMovieGenre.MovieID = (int)detailJson["id"];
                            newMovieGenre.GenreID = (int)genreItem["id"];
                            _context.MovieGenres.Add(newMovieGenre);                          
                        }
                        _context.SaveChanges();
                        return View(newMovie);

                    }
                }
                return NotFound("Không có phim này");
            }





        }
        public async Task< ActionResult> ListOfMoviesGenres()
        {
            HttpClient client = new HttpClient();
            string requestURL = @"https://localhost:7279/api/Movie/get-movie-by-genre";
            var response = await client.GetStringAsync(requestURL);
            List<Movie> result = JsonConvert.DeserializeObject<List<Movie>>(response);
            return View(result);
        }
        public IActionResult ListOfMoviesByActors()
        {
            return View();
        }

        public IActionResult ListMovieManager()
        {
            var lst = _context.Movies.ToList();
            return View(lst);
        }
    }
}
