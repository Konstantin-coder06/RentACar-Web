using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class CompanyEditInfoViewModel
    {
        [Required]
        public string CompanyName {  get; set; }
        [Required]
        public string CompanyDescription {  get; set; }
        [Required]
        public string CompanyEmail {  get; set; }
        [Required]
        public string CompanyCity {  get; set; }
        [Required]
        public string CompanyCountry {  get; set; }
        [Required]
        public string CompanyAddress {  get; set; }
        public string? CurrentPassword {  get; set; }
        public string? NewPassword {  get; set; }
        public string? ConfirmNewPassword {  get; set; }
    }
}
