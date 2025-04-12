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
     
        public int Id { get; set; }
     
        public string? Name { get; set; }
     
        public string? Description { get; set; }
      
        public string? City { get; set; }
       
        public string? Country {  get; set; }
       
        public string? Address {  get; set; }
        public string? UserId { get; set; }  
        public ApplicationUser User { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
