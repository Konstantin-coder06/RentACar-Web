namespace RentACar.Models
{
    public class CarWithImages
    {
        public Car Car { get; set; }
        public List<Image> Images { get; set; }
        public bool IsSelfPick {  get; set; }
        public string CustomAddress {  get; set; }
        public bool IsReturningBackAtSamePlace {  get; set; }   
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
