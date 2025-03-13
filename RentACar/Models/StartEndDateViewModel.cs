using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace RentACar.Models
{
    public class StartEndDateViewModel
    {
        [Required(ErrorMessage ="Start date is required")]
        [DataType(DataType.Date)]
        
        public DateTime? StartDay { get; set; }
        [Required]
        [DataType(DataType.Date)]
     
        public DateTime? EndDay { get; set; }
    }
}
