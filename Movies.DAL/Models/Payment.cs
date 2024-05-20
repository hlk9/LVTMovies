using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public Guid UserID { get; set; }
        [ForeignKey("UserID")] 
        public virtual User User { get; set; }
        public int RentalID { get; set; }
        [ForeignKey("RentalID")]
        public virtual Rental Rental { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public byte Status { get; set; }

       
    }
}
