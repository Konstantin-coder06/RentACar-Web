using Microsoft.AspNetCore.Mvc;
using RentACar.Core.IServices;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class UserController : Controller
    {
        IReservationService reservationService;
        public UserController(IReservationService reservationService)
        {     
            this.reservationService = reservationService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var reservations=await reservationService.GetReservationsByUserId(id);
            List<string> statuses = new List<string>();
            foreach(var x in reservations)
            {
                string status=await reservationService.GetStatusOfReservation(x);
                statuses.Add(status);
            }
            ReservationWithCarInfViewModel model = new ReservationWithCarInfViewModel()
            {
                ReservationId = 2,
                //Status=statuses,
                
            };
            return View();
        }
    }
}
