using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage ="Age is required")]
        public int Age {  get; set; }
        [Required]
        public string Email { get; set;  }
        [Required]
        public string Password {  get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
