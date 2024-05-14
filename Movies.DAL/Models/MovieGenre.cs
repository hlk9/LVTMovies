using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
