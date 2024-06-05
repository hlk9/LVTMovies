using Movies.DAL.Models;
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


      
    }
}
