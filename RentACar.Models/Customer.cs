
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
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Birth Day is required")]
        public DateTime BirthDay {  get; set; }

        public string UserId { get; set; }  

        public IdentityUser User { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
