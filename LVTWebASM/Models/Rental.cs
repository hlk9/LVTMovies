using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public byte Status { get; set; }

        public virtual Payment? Payment { get; set; }

    }
}
