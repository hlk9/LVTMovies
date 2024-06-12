using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Models;
using Movies.DAL.ViewModels;
using System.Net.WebSockets;

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
            return context.Movies.Find(id);
        }
        [HttpGet("get-movie-by-genre-id")]
        public List<Movie> GetMovieByGenre(int id)
        {
            var context = new DAL.Context.MovieDbContext();

            //lấy ra tất cả id của phim theo id thể loại truyền vào
            var idMovie = context.MovieGenres.Where(x => x.GenreID == id).Select(x => x.MovieID).ToList();


            List<Movie> lstMovie = new List<Movie>();

            for (int i = 0; i < idMovie.Count; i++)
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

        [HttpDelete("Delete-Movie/{id}")]
        public bool DeleteMovie(int id)
        {
            try
            {
                var context = new DAL.Context.MovieDbContext();
                var movie = context.Movies.Find(id);
                context.Movies.Remove(movie);
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPost("Create-Movie")]
        public bool CreateMovie(Movie movie)
        {
            try
            {
                var context = new DAL.Context.MovieDbContext();

                var a=  context.Movies.Where(x => x.Title == movie.Title).FirstOrDefault();
                if (a != null)
                {
                    return false;
                }

                context.Movies.Add(movie);
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPut("Update-Movie")]
        public bool UpdateMovie(Movie movie)
        {
            try
            {
                var context = new DAL.Context.MovieDbContext();
                var up = context.Movies.Find(movie.Id);
                up.Title = movie.Title;
                up.Description = movie.Description;
                up.RentalPrice = movie.RentalPrice;
                up.SalePrice = movie.SalePrice;
                up.PosterURL = movie.PosterURL;
                up.Status = movie.Status;
                up.BackdropURL = movie.BackdropURL;
                up.StreamURL = movie.StreamURL;
                up.Bugget = movie.Bugget;

                context.Movies.Update(up);
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPost("Add-Movie-Genre")]
        public bool AddGenreForMovie(MovieGenre mg)
        {
            try
            {

                var context = new DAL.Context.MovieDbContext();

                var a = context.MovieGenres.Where(x => x.MovieID == mg.MovieID && x.GenreID == mg.GenreID).FirstOrDefault();
                if (a != null)
                {
                    return false;
                }

                context.MovieGenres.Add(mg);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        [HttpGet("Get-Genre-Name-Movie/{id}")]
        public List<string> GetGenreMovie(int id)
        {
            var context = new DAL.Context.MovieDbContext();
            var list = context.MovieGenres.Where(x => x.MovieID == id).ToList();
            List<string> lst = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                Genre genre = context.Genres.Find(list[i].GenreID);
                lst.Add(genre.Name);
            }
            return lst;
        }
    }
}
