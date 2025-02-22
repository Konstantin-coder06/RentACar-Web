namespace RentACar.Models
{
    public class RecentReservation
    {
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }
        public double TotalPriceForLast24Hours {  get; set; }
        public double TotalPriceForLastWeek {  get; set; }
        public double TotalPriceForLastMounth {  get; set; }
        public double TotalPriceForLast24HoursBefore24Hours { get; set; }
        public double TotalPriceForLastWeekBeforeWeek { get; set; }
        public double TotalPriceForLastMounthBeforeMonth { get; set; }
        public int Count {  get; set; }
        public int CountPending {  get; set; }
        public List<Car>Pending { get; set; }
        public int ReportCount {  get; set; }
        public int ProcentPerDay {  get; set; }
        public int ProcentPerWeek {  get; set; }
        public int ProcentPerMonth { get; set; }
    }
}
