using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class ReportViewModel
    {
        [Required]
        public  string Title {  get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public Customer Customer { get; set; }
    }
}
