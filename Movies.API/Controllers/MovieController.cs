using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Models;
using Movies.DAL.ViewModels;

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
        [HttpGet("get-movie-by-genre-id")]
        public List<Movie>  GetMovieByGenre(int id) 
        {
            var context = new DAL.Context.MovieDbContext();

            //lấy ra tất cả id của phim theo id thể loại truyền vào
            var idMovie = context.MovieGenres.Where(x => x.GenreID == id).Select(x => x.MovieID).ToList();


            List<Movie> lstMovie = new List<Movie>();

            for(int i = 0; i < idMovie.Count; i++) 
            {
                Movie movie = context.Movies.Find(idMovie[i]);
                lstMovie.Add(movie);
            }
            //var lst = from a in context.Movies
            //          join b in context.MovieGenres on a.Id equals b.MovieID
            //          join c in context.Genres on b.GenreID equals c.Id
            //          select new ViewModel.ListOfMoviesGenreViewModel
            //          {
                        
            //              Title = a.Title,
            //              GenreID = c.Id,
            //              Rental = a.RentalPrice,
            //              Genre = c.Name,
            //              PosterURL = a.PosterURL,
            //          };
            //var result = await lst.ToListAsync();
            return lstMovie;
                      
        }


        [HttpGet("Get-All-Movies")]
        public List<Movie> GetAllMovies()
        {
            var context = new DAL.Context.MovieDbContext();
            var list = new List<Movie>();
            list = context.Movies.ToList();
            return list;
        }

        [HttpGet("Get-All-Genre")]
        public List<Genre> GetAllGenre()
        {
            var context = new DAL.Context.MovieDbContext();
            return context.Genres.ToList();
        }
    }
}
