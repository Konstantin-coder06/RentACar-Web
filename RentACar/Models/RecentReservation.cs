namespace RentACar.Models
{
    public class RecentReservation
    {
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }
        public int TotalPriceForLast24Hours {  get; set; }
        public int TotalPriceForLastMounth {  get; set; }
        public int Count {  get; set; }
    }
}
