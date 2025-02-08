using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class CarCompany
    {
        //[Required]
        public int Id { get; set; }
       // [Required]
        public string? Name { get; set; }
        //[Required]
        public string? Description { get; set; }
       // [Required]
        public string? City { get; set; }
        //[Required]
        public string? Country {  get; set; }
       // [Required]
        public string? Address {  get; set; }
        public string? UserId { get; set; }  // Връзка към IdentityUser
        public ApplicationUser User { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
