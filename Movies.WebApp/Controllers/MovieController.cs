using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Movies.WebApp.Controllers
{
    public class MovieController : Controller
    {
        string baseURL = "https://localhost:7279/";

        public IActionResult Index()
        {
            return View();
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
    }
}
