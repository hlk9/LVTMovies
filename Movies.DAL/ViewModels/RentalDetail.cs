using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.ViewModels
{
    public class RentalDetail
    {
        public int IdMovie { get; set; }
        public int PaymentId { get; set; }
        public string MovieTitle { get; set; }
        public byte RentalStatus { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
