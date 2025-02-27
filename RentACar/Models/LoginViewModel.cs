using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class LoginViewModel
    {
        [Required]

        [EmailAddress]

        public string Email { get; set; }



        

        [DataType(DataType.Password)]

        public string? Password { get; set; }



        [Display(Name = "Remember me?")]

        public bool RememberMe { get; set; }
        [DataType(DataType.Password)]

        public string? NewPassword {  get; set; }
    }
}
