using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Gearbox { get; set; }
        public int Year { get; set; }
        public double PricePerDay { get; set; }
        public double PricePerWeek { get; set; }
        public double MileageLimitForDay { get; set; }
        public double MileageLimitForWeek { get; set; }
        public double AdditionalMileageCharge { get; set; }
        public double EngineCapacity { get; set; }
        public string Color { get; set; }
        public bool Available { get; set; }
        public string Description { get; set; }
        public string DriveTrain { get; set; }
        public int HorsePower { get; set; }
        public double ZeroToHundred { get; set; }
        public double TopSpeed { get; set; }

        public int ClassOfCarId { get; set; }
        public ClassOfCar ClassOfCar { get; set; }

        // Ensure you're using CarCompanyId for the foreign key
        public int CarCompanyId { get; set; }
        public CarCompany CarCompany { get; set; }

        public ICollection<Image> Images { get; set; }
    }

}
