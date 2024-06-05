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
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        [DisplayName("Tên thể loại")]
        public string Name { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        public int Status { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; set; }

    }
}
