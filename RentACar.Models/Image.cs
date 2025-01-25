using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url {  get; set; }
        public int CarId {  get; set; }
        public Car Car { get; set; }    
    }
}
