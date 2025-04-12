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
            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(int id, string statusFilter)
        {
            var reservations = await reservationService.GetReservationsByUserId(id);

            if (statusFilter != "All Reservations")
            {
                reservations = reservations.Where(r => reservationService.GetStatusOfReservation(r).Result == statusFilter).ToList();
            }

            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };

            TempData["StatusFilter"] = statusFilter;

            return View("Index", userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterDate(int id, DateTime? startDate, DateTime? endDate)
        {
            var reservations = await reservationService.GetReservationsByUserId(id);

            if (startDate.HasValue)
            {
                reservations = reservations.Where(r => r.StartDate >= startDate.Value).ToList();
            }
            if (endDate.HasValue)
            {
                reservations = reservations.Where(r => r.EndDate <= endDate.Value).ToList();
            }

            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };

            TempData["StartDateFilter"] = startDate?.ToString("yyyy-MM-dd");
            TempData["EndDateFilter"] = endDate?.ToString("yyyy-MM-dd");

            return View("Index", userViewModel);
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
        
        private async Task<List<ReservationWithCarInfViewModel>> BuildReservationViewModels(IEnumerable<Reservation> reservations)
        {
            var reservationsWithCarInf = new List<ReservationWithCarInfViewModel>();
            foreach (var x in reservations)
            {
                string status = await reservationService.GetStatusOfReservation(x);
                var car = await carService.FindById(x.CarId);
                var classOfCar = await classOfCarService.GetClassOfCarById(car.ClassOfCarId);
                var (isReserved, startDate, endDate) = await reservationService.IsTheCarReservatedForToday(car.Id);
                DateTime startDateOfTheReservatedCar = isReserved ? startDate.Value : new DateTime();
                DateTime endDateOfTheReservatedCar = isReserved ? endDate.Value : new DateTime();

                if (isReserved)
                {
                    Console.WriteLine($"Car is reserved from {startDateOfTheReservatedCar} to {endDateOfTheReservatedCar}");
                }
                else
                {
                    Console.WriteLine("Car is not reserved for today.");
                }

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
                    Duration = (int)(x.EndDate - x.StartDate).TotalDays,
                    PickUpAddress = x.PaidDeliveryPlace,
                    TotalPrice = x.TotalPrice,
                    IsTheCarReservatedForToday = isReserved,
                    StartDateOfCar = startDateOfTheReservatedCar,
                    EndDateOfCar = endDateOfTheReservatedCar,
                    CreateTime=x.CreateTime,
                };
                reservationsWithCarInf.Add(model);
            }
            return reservationsWithCarInf;
        }
    }
}
