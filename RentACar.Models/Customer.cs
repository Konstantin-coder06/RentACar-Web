
using Microsoft.AspNetCore.Identity;
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

        public string UserId { get; set; }  // Връзка към IdentityUser​

        public IdentityUser User { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
