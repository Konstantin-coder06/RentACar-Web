namespace RentACar.Models
{
    public class CarImagesWithDatesViewModel
    {
        public List<CarWithImages> CarWithImages { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
