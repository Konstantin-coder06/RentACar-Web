using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class AddReportViewModel
    {
        [Required]
        public string Title {  get; set; }
        [Required]
        [StringLength(500, MinimumLength = 50, ErrorMessage = "The Description must be between 50 to 500 characters.")]
        public string Description { get; set; }
      
    }
}
