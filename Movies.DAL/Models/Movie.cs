using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Tên Phim")]
        public string Title { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(3000)]
        [DisplayName("Mô tả")]
        public string? Description { get; set; }
        [DisplayName("Link Stream")]
        public string? StreamURL { get; set; }
        [DisplayName("Giá Thuê")]
        public double RentalPrice { get; set; }
        [DisplayName("Giả giá")]
        public double SalePrice { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(30)]
        [DisplayName("Trạng thái")]
        public string Status { get; set; }
        [DisplayName("Kinh phí")]
        public double Bugget { get; set; }
        [DisplayName("Ảnh nền")]
        public string BackdropURL { get; set; }
        [DisplayName("Ảnh Poster")]
        public string PosterURL { get; set; }


        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        public virtual ICollection<WhishList> WhishLists { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }

    }
}
