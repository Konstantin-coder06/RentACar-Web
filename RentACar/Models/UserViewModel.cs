namespace RentACar.Models
{
    public class UserViewModel
    {
        public int ReservationId {  get; set; }
        public List<ReservationWithCarInfViewModel> ReservationCar {  get; set; }
    }
}
