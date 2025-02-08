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
    }
}
