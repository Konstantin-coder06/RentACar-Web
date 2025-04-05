using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class AddTypeOfCarViewModel
    {
        [Required]
        public string Name {  get; set; }
        [Required]
        public int SeatCapacity {  get; set; }
    }
}
