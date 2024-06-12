using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.ViewModels
{
    public class BillMovieViewModel
    {
        public int MovieId { get; set; }
       public int RentalId { get; set; }
        [DisplayName("Tên Phim")]
        public string MovieName { get; set; }
        public string UserName  { get; set; }
        [DisplayName("Ngày thuê")]
        public DateTime RentalDate { get; set; }
        [DisplayName("Ngày hết hạn")]
        public DateTime ReturnDate { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
        [DisplayName("Giá thuê")]
        public double Price { get; set; }
    }
}
