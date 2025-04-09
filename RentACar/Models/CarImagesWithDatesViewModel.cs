namespace RentACar.Models
{
    public class CarImagesWithDatesViewModel
    {
        public List<CarWithImages> CarWithImages { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Sort { get; set; }
        public List<string>SelectedCategories { get; set; }=new List<string>();
        public string SearchTerm { get; set; }
        public string SortTerm {  get; set; }
        public string? BrandOfCar {  get; set; }
        public string? ModelOfCar {  get; set; }
    }
}
