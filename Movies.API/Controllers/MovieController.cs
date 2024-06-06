using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet("get-movie-by-genre")]
        public async Task<ActionResult> GetMovieByGenre() 
        {
            var context = new DAL.Context.MovieDbContext();
            //var idGenre = context.MovieGenres.FirstOrDefault(x=>x.GenreID == id);
 
            var lst = from a in context.Movies
                      join b in context.MovieGenres on a.Id equals b.MovieID
                      join c in context.Genres on b.GenreID equals c.Id
                      select new ViewModel.MoviesByGenresViewModel
                      {
                          Id = a.Id,
                          Title = a.Title,
                          Genres = c.Name,
                          PosterURL = a.PosterURL,
                      };
            var result = await lst.ToListAsync();
            return Ok(result);
                      
        }

    }
}
