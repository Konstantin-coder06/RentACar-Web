using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class EditCarWithImagesPendingViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Gearbox is required")]
        public string Gearbox { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Price per day is required")]
        public double PricePerDay { get; set; }

        [Required(ErrorMessage = "Price per week is required")]
        public double PricePerWeek { get; set; }

        [Required(ErrorMessage = "Mileage limit for day is required")]
        public double MileageLimitForDay { get; set; }

        [Required(ErrorMessage = "Mileage limit for week is required")]
        public double MileageLimitForWeek { get; set; }

        [Required(ErrorMessage = "Additional Mileage charge is required")]
        public double AdditionalMileageCharge { get; set; }

        [Required(ErrorMessage = "Engine capacity is required")]
        public double EngineCapacity { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }

        public bool Available { get; set; } = true;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DriveTrain is required")]
        public string DriveTrain { get; set; }

        [Required(ErrorMessage = "Horse power is required")]
        public int HorsePower { get; set; }

        [Required(ErrorMessage = "0-100km/h is required")]
        public double ZeroToHundred { get; set; }

        [Required(ErrorMessage = "Top speed is required")]
        public double TopSpeed { get; set; }

        [Required(ErrorMessage = "Class of car is required")]
        public int ClassOfCarId { get; set; }
        public SelectList? ClassOptions { get; set; }
        public List<string>? Features { get; set; } 
        public List<string>? SelectedFeatures { get; set; }

        public List<ImageViewModel>? Images { get; set; }=new List<ImageViewModel>();
        [Required]
        public int TypeId { get; set; }
        public SelectList? TypeOptions { get; set; }
        [Required]
        public bool IsConvertible {  get; set; }

    }
}
