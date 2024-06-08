using Movies.DAL.Models;

namespace Movies.API.ViewModel
{
    public class MoviesByGenresViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
        public double Rental { get; set; }
        public string PosterURL { get; set; }
    }
}
