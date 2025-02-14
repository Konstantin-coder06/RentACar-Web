using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class StartEndDateViewModel
    {
        [Required]
       
        
        public DateTime? StartDay { get; set; }
        [Required]
        [DataType(DataType.Date)]
        
        public DateTime? EndDay { get; set; }
    }
}
