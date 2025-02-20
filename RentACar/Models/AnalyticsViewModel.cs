namespace RentACar.Models
{
    public class AnalyticsViewModel
    {
        public double TotalLast24Hours {  get; set; }
        public int Count24Hours {  get; set; }
        public double TotalLastWeek {  get; set; }
        public int CountWeek { get; set; }
        public double TotalLastMonth { get; set; }
        public int CountMonth { get; set; }
        public double AvgReservationDuration {  get; set; }
        public int CountAllReservations {  get; set; }
        public List<TopCarsViewModel> Top10Cars {  get; set; }

    }
}
