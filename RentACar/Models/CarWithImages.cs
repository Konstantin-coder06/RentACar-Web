using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class CarWithImages
    {
        public Car Car { get; set; }=new Car();
        public List<Image> Images { get; set; }= new List<Image>();
        public bool IsSelfPick {  get; set; }
        public string CustomAddress {  get; set; }
        public bool IsReturningBackAtSamePlace {  get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDay { get; set; }
        public List<Feature> Features { get; set; } = new List<Feature>();
        public bool IsReturnOptionsEnabled { get; set; }
        public bool IsAddressInputVisible { get; set; }
        public string AddressOfCompany { get; set; }
        public bool IsEdit {  get; set; }
        public int? ReservationId {  get; set; }
    }
}
