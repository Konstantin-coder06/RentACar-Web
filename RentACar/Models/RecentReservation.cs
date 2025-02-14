namespace RentACar.Models
{
    public class RecentReservation
    {
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }
        public double TotalPriceForLast24Hours {  get; set; }
        public double TotalPriceForLastMounth {  get; set; }
        public int Count {  get; set; }
        public int CountPending {  get; set; }
    }
}
