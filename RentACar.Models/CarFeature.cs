using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class CarFeature
    {
        [Required]
        public int Id {  get; set; }
        [Required]
        public int CarId {  get; set; }
        
        public Car Car { get; set; }
        [Required]
        public int FeatureId {  get; set; }
        public Feature Feature { get; set; }
    }
}
