using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class Feature
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NameOfFeatures { get; set; }
        
    }
}
