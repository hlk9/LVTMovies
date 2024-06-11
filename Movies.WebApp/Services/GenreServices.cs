using Movies.DAL.Models;
using Newtonsoft.Json;

namespace Movies.WebApp.Services
{
    public class GenreServices
    {
        HttpClient client;
        string baseURL = "https://localhost:7279/";
        public List<Genre> GetAllGenre()
        {
            client = new HttpClient();
            string url = baseURL + "Genre/Get-All-GenreActive";
            var res = client.GetStringAsync(url).Result;
            var lst = JsonConvert.DeserializeObject<List<Genre>>(res);
            return lst;
        }

        public bool AddGenre(Genre genre)
        {
            client = new HttpClient();
            string requestUrl = baseURL + "Genre/Add-Grenre";
            var response = client.PostAsJsonAsync(requestUrl, genre).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
