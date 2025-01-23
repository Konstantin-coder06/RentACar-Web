using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class CarType
    {
        public int Id { get; set; } 
        public int CarId {  get; set; }
        public Car Car { get; set; }
        public int TypeId {  get; set; }
        public CType Type { get; set; }


    }
}
