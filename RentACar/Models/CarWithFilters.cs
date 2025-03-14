using System.Linq.Expressions;

namespace RentACar.Models
{
    public class CarWithFilters
    {

        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 0;
        public List<ClassOfCar> Classes { get; set; } = new List<ClassOfCar>();
        public List<int> SelectedClassIds { get; set; } = new List<int>();
        public List<string> CarBrands { get; set; } = new List<string>();
        public List<string> SelectedBrands { get;set; } = new List<string>();
        public List<string>Colors { get; set; }= new List<string>();
        public List<string> SelectedColors { get;set; } = new List<string>();
        public List<string> DriveTrains { get; set; }=new List<string>();
        public List<string>SelectedDriveTrains { get; set; }=new List<string>();

    }
}
