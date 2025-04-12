using NuGet.Protocol;

namespace RentACar.Models
{
    public class FinalStepReservationViewModel
    {
        public string? CompanyName {  get; set; }
        public string? Address {  get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public int? TotalDays {  get; set; }
        public string? ImageHref {  get; set; }
        public string? Brand {  get; set; }
        public string? Model {  get; set; }
        public int? Year {  get; set; }
        public string? Transmittion {  get; set; }
        public double? LimitForOneDay { get; set; }
        public double? TotalPriceWithoutTheDiscount {  get; set; }
        public double? DifferenceTotalPriceWithDiscounted {  get; set; }
        public double TotalPrice {  get; set; }
        public double? PriceOfTaxes {  get; set; }
        public int CarId
        {
            get; set;
        }
        public string TakingPlace {  get; set; }
        public string ReturningPlace {  get; set; }
    }
}
