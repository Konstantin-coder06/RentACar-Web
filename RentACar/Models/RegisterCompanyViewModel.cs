using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class RegisterCompanyViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Description {  get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country {  get; set; }
        [Required]
        public string Address {  get; set; }
    }
}
