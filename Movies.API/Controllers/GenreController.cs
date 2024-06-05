using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {

        [HttpGet("GetGenreById")]
        public Genre GetById(int id)
        {
            var context = new DAL.Context.MovieDbContext();
            return context.Genres.Find(id);
        }

        [HttpGet("GetAllGenreActive")]
        public ICollection<Genre> GetAllGenre()
        {
            var context = new DAL.Context.MovieDbContext();
            var list = context.Genres.Where(x => x.Status == 1).ToList();
            return list;
        }
    }
}
