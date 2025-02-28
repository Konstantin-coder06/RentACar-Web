using NuGet.Protocol;

namespace RentACar.Models
{
    public class CustomerEmailPhoneViewModel
    {
        public Customer Customer { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
