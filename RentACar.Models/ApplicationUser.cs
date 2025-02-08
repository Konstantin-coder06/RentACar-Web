using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class ApplicationUser:IdentityUser
    {
        public CarCompany CarCompany { get; set; }
    }
}
