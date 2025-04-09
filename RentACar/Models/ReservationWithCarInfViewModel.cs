namespace RentACar.Models
{
    public class ReservationWithCarInfViewModel
    {
        public int ReservationId { get; set; }
        public string Status {  get; set; }
        public string CarImageHref {  get; set; }
        public string CarBrand {  get; set; }
        public string CarModel {  get; set; }
        public string CarClass {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration {  get; set; }
        public string PickUpAddress {  get; set; }
        public double TotalPrice {  get; set; }
    }
}
