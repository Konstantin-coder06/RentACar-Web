namespace RentACar.Models
{
    public class CompanyIndexViewModel
    {
        public List<TopCarsViewModel> AllCars { get; set; }
        
        public List<CustomerReservationedCarViewModel> Customers { get; set; }
        public double TotalPriceForLast24Hours { get; set; }
        public double TotalPriceForLastWeek { get; set; }
        public double TotalPriceForLastMounth { get; set; }
        public double TotalPriceForLast24HoursBefore24Hours { get; set; }
        public double TotalPriceForLastWeekBeforeWeek { get; set; }
        public double TotalPriceForLastMounthBeforeMonth { get; set; }
        public int Count { get; set; }
        public int CountPending { get; set; }
        public List<Car> Pending { get; set; }
        public int ReportCount { get; set; }
        public int ProcentPerDay { get; set; }
        public int ProcentPerWeek { get; set; }
        public int ProcentPerMonth { get; set; }
        public string? CompanyName {  get; set; }
        public List<TopCarsViewModel> FilteredCars { get; set; }
    }
}
