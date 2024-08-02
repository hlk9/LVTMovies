using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool? Gender { get; set; }
        public byte Status { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public virtual ICollection<Rental>? Rentals { get; set; }
        public virtual ICollection<WhishList>? WhishLists { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public virtual Role? Role { get; set; }

    }
}
