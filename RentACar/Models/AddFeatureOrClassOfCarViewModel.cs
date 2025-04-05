using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class AddFeatureOrClassOfCarViewModel
    {
        [Required]
        public string Name {  get; set; }
    }
}
