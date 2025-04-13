using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class CarWithImages
    {
        public Car Car { get; set; }
        public List<Image> Images { get; set; }
        public bool IsSelfPick {  get; set; }
        public string CustomAddress {  get; set; }
        public bool IsReturningBackAtSamePlace {  get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDay { get; set; }
        public List<Feature> Features { get; set; }
        public bool IsReturnOptionsEnabled { get; set; }
        public bool IsAddressInputVisible { get; set; }
        public string AddressOfCompany {  get; set; }
    }
}
