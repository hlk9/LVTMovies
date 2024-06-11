using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Models;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {

        [HttpGet("GetGenreById")]
        public Genre GetById(int id)
        {
            var context = new DAL.Context.MovieDbContext();
            return context.Genres.Find(id);
        }

        [HttpGet("Get-All-GenreActive")]
        public ICollection<Genre> GetAllGenre()
        {
            var context = new DAL.Context.MovieDbContext();
            var list = context.Genres.Where(x => x.Status == 1).ToList();
            return list;
        }

        [HttpPost("Add-Grenre")]
        public bool AddGenre(Genre genre)
        {
            var context = new DAL.Context.MovieDbContext();
            context.Genres.Add(genre);
            context.SaveChanges();
            return true;
        }
    }
}
