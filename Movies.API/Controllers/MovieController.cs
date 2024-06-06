using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet(Name = "MovieDetail")]
        public Movie Get(int id)
        {
            var context = new DAL.Context.MovieDbContext();
           return  context.Movies.Find(id);
        }

        [HttpGet("Get-All-Movies")]
        public List<Movie> GetAllMovies()
        {
            var context = new DAL.Context.MovieDbContext();
            var list = new List<Movie>();
            list = context.Movies.ToList();
            return list;
        }
    }
}
