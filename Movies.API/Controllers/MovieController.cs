using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet(Name = "MovieDetail")]
        public Movie Get(int id)
        {
            var context = new DAL.Context.MovieDbContext();
           return  context.Movies.Find(id);
        }
    }
}
