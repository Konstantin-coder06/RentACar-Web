using Microsoft.AspNetCore.Mvc;
using RentACar.Core.IServices;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class UserController : Controller
    {
        IReservationService reservationService;
        ICarService carService;
        IClassOfCarService classOfCarService;
        IImageService imageService;
        ICarCompanyService carCompanyService;
        public UserController(IReservationService reservationService,ICarService carService, IClassOfCarService classOfCarService,IImageService imageService,ICarCompanyService carCompanyService)
        {     
            this.reservationService = reservationService;
            this.carService = carService;
            this.classOfCarService = classOfCarService;
            this.imageService = imageService;
            this.carCompanyService = carCompanyService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var reservations = await reservationService.GetReservationsByUserId(id);
            var statusFilter = TempData["StatusFilter"]?.ToString() ?? "All Reservations";
            if (statusFilter != "All Reservations")
            {
                reservations = await reservationService.GetAllReservationFilteredByStatus(reservations, statusFilter);

            }
            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            if (TempData["SomethingHappend"]!=null)
            {
                TempData["error"] = TempData["SomethingHappend"];
            }
          
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(int id, string statusFilter)
        {
            var reservations = await reservationService.GetReservationsByUserId(id);      
            TempData["StatusFilter"] = statusFilter;
            return RedirectToAction("Index", new { id });
        }
        [HttpPost]
        public IActionResult ShowCancelConfirmation(int userId,int reservationId)
        {
            TempData["ReservationToCancel"] = reservationId;
            TempData["ShowConfirmModal"] = true;
           
            return RedirectToAction("Index", new {id=userId});
        }
        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await reservationService.FindById(id);
            if (reservation != null)
            {
                var userId= reservation.CustomerId;
                reservationService.Delete(reservation);
                await reservationService.Save();
                return RedirectToAction("Index", new {id=userId});
            }
            else
            {
                ModelState.AddModelError("", "Something happend! Please relog in");
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> BookAgain(int id)
        {
            var car = await carService.FindById(id);
            if (car == null)
            {
                return NotFound();
            }

            var isCarAvailableTomorrow = await reservationService.IsCarReservationForTomorrow(car.Id);
            if (isCarAvailableTomorrow)
            {
                HttpContext.Session.SetString("StartDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("EndDate", DateTime.Now.AddDays(3).ToString("yyyy-MM-dd"));
            }
            else
            {
                var (startDate, endDate) = await reservationService.GetEarliestAvailableDates(car.Id, minimumDurationDays: 2);
                HttpContext.Session.SetString("StartDate", startDate.ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("EndDate", endDate.ToString("yyyy-MM-dd"));
            }

            return RedirectToAction("Reservation", "Reservation", new { id = car.Id });
        }
        private async Task<List<ReservationWithCarInfViewModel>> BuildReservationViewModels(IEnumerable<Reservation> reservations)
        {
            var reservationsWithCarInf = new List<ReservationWithCarInfViewModel>();
            foreach (var x in reservations)
            {
                string status = await reservationService.GetStatusOfReservation(x);
                var car = await carService.FindById(x.CarId);
                var classOfCar = await classOfCarService.GetClassOfCarById(car.ClassOfCarId);
                var (isReserved, startDate, endDate) = await reservationService.IsTheCarReservatedForToday(car.Id);

                var image = await imageService.ImageByCarId(car.Id);
                var carCompany = await carCompanyService.GetById(car.CarCompanyId);
                if (x.PaidDeliveryPlace == null)
                {
                    x.PaidDeliveryPlace = carCompany.Address;
                }

                var model = new ReservationWithCarInfViewModel
                {
                    ReservationId = x.Id,
                    Status = status,
                    CarBrand = car.Brand,
                    CarModel = car.Model,
                    CarClass = classOfCar.Name,
                    CarImageHref = image.Url,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Duration = reservationService.CalculateTotalDays(x),
                    PickUpAddress = x.PaidDeliveryPlace,
                    TotalPrice = x.TotalPrice,
                    IsTheCarReservatedForToday = isReserved,
                    StartDateOfCar = isReserved ? startDate : null,
                    EndDateOfCar = isReserved ? endDate : null,
                    CreateTime = x.CreateTime,
                    Difference = reservationService.CalculateStartDateDifference(x),
                    CarId = car.Id,
                };
                reservationsWithCarInf.Add(model);
            }
            return reservationsWithCarInf;
        }
    }
}
