using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class StartEndDateWithCarsCountViewModel
    {
        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]

        public DateTime? StartDay { get; set; }
        [Required(ErrorMessage ="End date is required")]
        [DataType(DataType.Date)]
        public DateTime? EndDay { get; set; }
        public int StandardCount {  get; set; }
        public double MinPriceStandard {  get; set; }
        public int LuxuryCount {  get; set; }
        public double MinPriceLuxury { get; set; }
        public int EconomyCount {  get; set; }
        public double MinPriceEconomy {  get; set; }
        public int SportCount {  get; set; }
        public double MinPriceSport { get; set; }
        public int BusinessCount {  get; set; }
        public double MinPriceBusiness { get; set; }
        public int ElectricCount {  get; set; }
        public double MinPriceElectric { get; set; }
    }
}
