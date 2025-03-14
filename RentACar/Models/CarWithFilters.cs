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

    }
}
