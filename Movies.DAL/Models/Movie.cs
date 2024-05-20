using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(500)]
        public string Title { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(3000)]
        public string? Description { get; set; }
        public string? StreamURL { get; set; }
        public double RentalPrice { get; set; }
        public double SalePrice { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(30)]
        public string Status { get; set; }
        public double Bugget { get; set; }
        public string BackdropURL { get; set; }
        public string PosterURL { get; set; }


        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        public virtual ICollection<WhishList> WhishLists { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }

    }
}
