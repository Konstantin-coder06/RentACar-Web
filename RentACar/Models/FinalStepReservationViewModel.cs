using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class FinalStepReservationViewModel
    {
        [Required]
        public string CompanyName {  get; set; }
        [Required]
        public string Address {  get; set; }
        [Required]
        public DateTime StartDay { get; set; }
        [Required]
        public DateTime EndDay { get; set; }
        [Required]
        public int TotalDays {  get; set; }
        [Required]
        public string ImageHref {  get; set; }
        [Required]
        public string Brand {  get; set; }
        [Required]
        public string Model {  get; set; }
        [Required]
        public int Year {  get; set; }
        [Required]
        public string Transmittion {  get; set; }
        [Required]
        public double LimitForOneDay { get; set; }
        [Required]
        public double TotalPriceWithoutTheDiscount {  get; set; }
        [Required]
        public double DifferenceTotalPriceWithDiscounted {  get; set; }
        [Required]
        public double TotalPrice {  get; set; }
        [Required]
        public double PriceOfTaxes {  get; set; }
        [Required]
        public int CarId
        {
            get; set;
        }
        [Required]
        public string TakingPlace {  get; set; }
        [Required]
        public string ReturningPlace {  get; set; }
    }
}
