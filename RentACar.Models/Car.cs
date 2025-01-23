using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class Car
    {
        
        public int Id { get; set; }
        public string Brand { get; set; }   
        public string Model { get; set; }
        public string Images { get; set; }
        public string Gearbox {  get; set; }
        public int Year { get; set; }   
        public double PricePerDay {  get; set; }
        public double PricePerWeek {  get; set; }
        public double MileageLimitForDay {  get; set; }
        public double MileageLimitForWeek {  get; set; }
        public double AdditionalMileageCharge { get; set; }
        public double EngineCapacity {  get; set; }
        public string Color {  get; set; }
        public bool Available {  get; set; }
       public string Description {  get; set; }
       
       
    }
}
