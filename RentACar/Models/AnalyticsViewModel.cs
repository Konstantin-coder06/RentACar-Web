using NuGet.Protocol;

namespace RentACar.Models
{
    public class AnalyticsViewModel
    {
        public double TotalLast24Hours {  get; set; }
        public int Count24Hours {  get; set; }
        public double TotalLastWeek {  get; set; }
        public int CountWeek { get; set; }
        public double TotalLastMonth { get; set; }
        public double TotalPriceForLast24HoursBefore24Hours { get; set; }
        public double TotalPriceForLastWeekBeforeWeek { get; set; }
        public double TotalPriceForLastMounthBeforeMonth { get; set; }
        public int CountMonth { get; set; }
        public double AvgReservationDuration {  get; set; }
        public int CountAllReservations {  get; set; }
        public List<TopCarsViewModel> Top10Cars {  get; set; }
        public int ReportCount {  get; set; }
        public int PendingsCount { get; set; }
        public double Difference24 {  get; set; }
        public double DifferenceWeek { get;set; }
        public double DifferenceMonth { get;set; }
        public double DifferenceReservation24 {  get; set; }
        public double DifferenceReservationWeek { get; set; }
        public double DifferenceReservationMonth {  get; set; }
        public int TotalReservationPreviousDay {  get; set; }
        public int TotalReservationPreviousWeek { get;set; }
        public int TotalReservationPreviousMonth { get; set;}

    }
}
