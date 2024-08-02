using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("MovieID")]
        public int MovieID { get; set; }
        public virtual Movie? Movie { get; set; }
        [ForeignKey("GenreID")]
        public int GenreID { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
