using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class CarController : Controller
    {
        ICarService carService;
        IImageService imageService;
        IClassOfCarService classOfCarService;
        IReservationService reservationService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarController(ICarService _carService, IImageService _imageService, IClassOfCarService _classOfCarService,IReservationService reservationService, IWebHostEnvironment webHostEnvironment)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            this.reservationService = reservationService;
            _webHostEnvironment = webHostEnvironment;
        }
     
        public IActionResult Index()
        { 

 
            var startDay = TempData["StartDay"] as DateTime?;
            var endDay = TempData["EndDay"] as DateTime?;
            List<Reservation> reservations = reservationService.FindAll(x => x.StartDate >= startDay && x.StartDate <=endDay).ToList();
            var cars = carService.GetAll()
                                 .Where(car => reservations.All(r => r.CarId != car.Id))
                                 .ToList();
            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            var viewModel = new CarImagesViewModel
            {
                CarWithImages = carsWithImages
            };

            return View(viewModel);
        }


       

        [HttpPost]
        [Route("Car/Search")]
        public IActionResult Search(string searchBar)
        {
            var queries = carService.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchBar))
            {
                if (queries.Any(x => searchBar.Contains(x.Brand)))
                {
                    queries = queries.Where(x => searchBar.Contains(x.Brand));
                }
                else
                {
                    queries = queries.Where(x => searchBar.Contains(x.Model));
                }
            }

            var cars = queries.AsEnumerable().OrderBy(x => Guid.NewGuid()).ToList();
            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            var viewModel = new CarImagesViewModel
            {
                CarWithImages = carsWithImages
            };

            return View("Index", viewModel);
        }

        public IActionResult Filters()
        {
            var model = new CarWithFilters
            {
                Classes = classOfCarService.GetAll().ToList(),
            };
            return View(model);
        }
        [HttpPost]
        
        public IActionResult Filters(CarWithFilters carWithFilters)
        {
            IQueryable<Car> queries = carService.GetAll().AsQueryable();

            if (carWithFilters.MinPrice > 0)
            {
                queries = queries.Where(x => x.PricePerDay >= carWithFilters.MinPrice);
            }

            if (carWithFilters.MaxPrice > 0 && carWithFilters.MaxPrice > carWithFilters.MinPrice)
            {
                queries = queries.Where(x => x.PricePerDay <= carWithFilters.MaxPrice);
            }

            if (carWithFilters.SelectedClassIds != null && carWithFilters.SelectedClassIds.Any())
            {
                
                queries = queries.Where(x => carWithFilters.SelectedClassIds.Contains(x.ClassOfCarId));
            }

            var cars = queries.ToList();
            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            var viewModel = new CarImagesViewModel
            {
                CarWithImages = carsWithImages
            };

            return View("Index", viewModel);
        }
        public IActionResult Reservation(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var car = carService.FindOne(x => x.Id == id);
            var carWithImages = new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            };


            return View(carWithImages);
        }
        [HttpPost]
        public IActionResult Reservation(CarWithImages carWithImages)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                if (!userId.HasValue)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                if (carWithImages.IsSelfPick == true)
                {


                    Reservation reservation = new Reservation()
                    {
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(3),
                        IsSelfPick = carWithImages.IsSelfPick,
                        PaidDeliveryPlace = carWithImages.CustomAddress,
                        IsReturnBackAtSamePlace = carWithImages.IsReturningBackAtSamePlace,
                        CarId = carWithImages.Car.Id,
                        CustomerId = userId.Value
                    };
                    reservationService.Add(reservation);
                    reservationService.Save();
                    return RedirectToAction("Index", "Car");
                }
            }
            return View(carWithImages);
        }
    }
}