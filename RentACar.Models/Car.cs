using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Models
{
    public class Car
    {
     //   [Required]
        public int Id { get; set; }
     //   [Required]
        public string Brand { get; set; }
      //  [Required]   
        
        public string Model { get; set; }
      //  [Required]
      
        public string Gearbox {  get; set; }
       // [Required(ErrorMessage ="Year is required")]
        public int Year { get; set; }
       // [Required(ErrorMessage = "Price per day is required")]
        public double PricePerDay {  get; set; }
       
      //[Required(ErrorMessage = "Price per week is required")]
      
        public double PricePerWeek {  get; set; }
       // [Required(ErrorMessage = "Mileage limit for day is required")]
        public double MileageLimitForDay {  get; set; }
       // [Required(ErrorMessage ="Mileage limit for week is required")]
        public double MileageLimitForWeek {  get; set; }
      //  [Required(ErrorMessage ="Additional Mileage charge is required")]
        public double AdditionalMileageCharge { get; set; }
      //  [Required(ErrorMessage ="Engine capacity is required")]
        public double EngineCapacity {  get; set; }
      //  [Required]
        public string Color {  get; set; }
      
        public bool Available {  get; set; }
      //  [Required]
        public string Description {  get; set; }
       // [Required]
        public string DriveTrain {  get; set; }
       // [Required(ErrorMessage ="Horse power is required")]
        public int HorsePower {  get; set; }
       // [Required(ErrorMessage = "0-100km/h is required")]
        public double  ZeroToHundred { get; set; }
        //[Required(ErrorMessage = "Top speed is required")]
        public double TopSpeed { get; set; }
      
        public int ClassOfCarId {  get; set; }
        public ClassOfCar ClassOfCar { get; set; }
        

       public ICollection<Image> Images { get; set; }
    }
}
