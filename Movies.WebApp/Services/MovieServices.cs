using Movies.DAL.Models;
using Movies.DAL.ViewModels;
using Newtonsoft.Json;

namespace Movies.WebApp.Services
{
    public class MovieServices
    {
        HttpClient client;
        string baseURL = "https://localhost:7279/";
        private readonly IConfiguration _configuration;
        public MovieServices()
        {
            
        }
        public MovieServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Movie> GetAllMovies()
        {
            client = new HttpClient();
            string url =  baseURL+"Movie/Get-All-Movies";
            var res = client.GetStringAsync(url).Result;
            var list = JsonConvert.DeserializeObject<List<Movie>>(res);
            return list;

        }

        public List<Genre> GetAllGenre()
        {
            client = new HttpClient();
            string url = baseURL + "Movie/Get-All-Genre";
            var res = client.GetStringAsync(url).Result;
            var lst = JsonConvert.DeserializeObject<List<Genre>>(res);

            return lst;
        }

        public List<Movie> GetAllMoviesByGenreId(int id)
        {
            client = new HttpClient();
            string requestUrl = baseURL + $@"Movie/get-movie-by-genre-id?id={id}";
            var response =client.GetStringAsync(requestUrl).Result;
            var list = JsonConvert.DeserializeObject<List<Movie>>(response);
            return list;
        }

        public Movie GetById(int id)
        {
            client = new HttpClient();
            string requestUrl = baseURL + $@"Movie?id={id}";
            var response = client.GetStringAsync(requestUrl).Result;
            var list = JsonConvert.DeserializeObject<Movie>(response);
            return list;
        }

    }
}
