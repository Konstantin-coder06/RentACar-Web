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



            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");
            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);

            List<Reservation> reservations = reservationService.FindAll(x => (!startDay.HasValue || x.StartDate >= startDay) &&
                 (!endDay.HasValue || x.StartDate <= endDay)).ToList();
            var cars = carService.GetAll().Where(car => reservations.All(r => r.CarId != car.Id)).ToList();
            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            var viewModel = new CarImagesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate=startDay,
                EndDate=endDay,

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

            var cars = queries.AsEnumerable().ToList();
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
        [HttpPost]
        public IActionResult FilterDates(CarImagesViewModel carImagesView)
        {
            List<Reservation> reservations = reservationService.FindAll(x => x.StartDate >= carImagesView.StartDate && x.StartDate <= carImagesView.EndDate).ToList();
            var cars = carService.GetAll()
                                 .Where(car => reservations.All(r => r.CarId != car.Id))
                                 .ToList();
            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();
            //datata
            var viewModel = new CarImagesViewModel
            {
                CarWithImages = carsWithImages
            };


            return View("Index",viewModel);
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
            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");

            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("Register", "Account");
            }

            var car = carService.FindOne(x => x.Id == id);
            var carWithImages = new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).OrderBy(x=>x.Order).ToList(),
                StartDate=startDay,
                EndDate=endDay

            };


            return View(carWithImages);
        }
        [HttpPost]
        public IActionResult Reservation(CarWithImages carWithImages,string submitButton)
        {
            if (submitButton == "ChangeDate")
            {
                HttpContext.Session.SetString("StartDate", carWithImages.StartDate?.ToString("o"));
                HttpContext.Session.SetString("EndDate", carWithImages.EndDate?.ToString("o"));
                return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
            }

           

            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            // Валидиране на Car
            if (carWithImages.Car == null || carWithImages.Car.Id == 0)
            {
                carWithImages.Car = carService.FindOne(x => x.Id == carWithImages.Car.Id);
            }
            if (carWithImages.Car == null)
            {
                ModelState.AddModelError("", "Car not found.");
                return View(carWithImages);
            }

            // Зареждане на снимки
            carWithImages.Images = imageService.GetImagesByCarId(carWithImages.Car.Id).OrderBy(x => x.Order).ToList();

            if (carWithImages.IsSelfPick)
            {
                if (!carWithImages.StartDate.HasValue || !carWithImages.EndDate.HasValue)
                {
                    ModelState.AddModelError("", "Start and end dates are required.");
                    return View(carWithImages);
                }

                DateTime startDay = carWithImages.StartDate.Value;
                DateTime endDay = carWithImages.EndDate.Value;
                int totalDays =(endDay.Day - startDay.Day);

                if (totalDays <= 0)
                {
                    ModelState.AddModelError("", "End date must be after start date.");
                    return View(carWithImages);
                }

                Reservation reservation = new Reservation()
                {
                    StartDate = startDay,
                    EndDate = endDay,
                    IsSelfPick = carWithImages.IsSelfPick,
                    PaidDeliveryPlace = carWithImages.CustomAddress,
                    IsReturnBackAtSamePlace = carWithImages.IsReturningBackAtSamePlace,
                    CarId = carWithImages.Car.Id,
                    CustomerId = userId.Value,
                    CreateTime = DateTime.Now,
                    
                };
                if (totalDays == 7) 
                {
                    reservation.TotalPrice = carWithImages.Car.PricePerWeek;
                }
                if (totalDays > 0 && totalDays != 7) 
                {
                    reservation.TotalPrice = (totalDays * carWithImages.Car.PricePerDay) * 0.9;
                }
                reservationService.Add(reservation);
                reservationService.Save();
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest($"Model is invalid: {string.Join(", ", errors)}");
            }
            return View(carWithImages);
        
            /* if (ModelState.IsValid)
             {
                 var userId = HttpContext.Session.GetInt32("UserId");

                 if (!userId.HasValue)
                 {
                     return RedirectToAction("AccessDenied", "Account");
                 }
                 if (carWithImages.IsSelfPick == true)
                 {
                    var startDayStr = HttpContext.Session.GetString("StartDate");
                    var endDayStr = HttpContext.Session.GetString("EndDate");

                    DateTime startDay =  DateTime.Parse(startDayStr);
                    DateTime endDay =  DateTime.Parse(endDayStr);
                     Reservation reservation = new Reservation()
                     {
                         StartDate = DateTime.Now,
                         EndDate = DateTime.Now.AddDays(3),
                         IsSelfPick = carWithImages.IsSelfPick,
                         PaidDeliveryPlace = carWithImages.CustomAddress,
                         IsReturnBackAtSamePlace = carWithImages.IsReturningBackAtSamePlace,
                         CarId = carWithImages.Car.Id,
                         CustomerId = userId.Value,
                         CreateTime = DateTime.Now,
                     };
                     if (endDay.Day - startDay.Day != 7 && endDay.Day-startDay.Day>0)
                    {
                         reservation.TotalPrice = ((endDay.Day - startDay.Day) * carWithImages.Car.PricePerDay) - carWithImages.Car.PricePerDay * 0.1;                         
                    };                  
                     if (endDay.Day - startDay.Day == 7)
                     {
                         reservation.TotalPrice = carWithImages.Car.PricePerWeek;                      


                     }
                    reservationService.Add(reservation);
                    reservationService.Save();
                     return RedirectToAction("Index", "Car");
                 }
             }
             return View(carWithImages);*/
        }
        public IActionResult Pendings()
        {
            var cars = carService.FindAll(x => x.Pending == true).OrderBy(x=>x.CreatedAt);

            var carImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList(),
            }).ToList();
           
            var carImagesViewModel= new CarImagesViewModel() 
            {
                CarWithImages = carImages,
            };
            return View(carImagesViewModel);
        }
        public int CarId { get; set; } = 0;
        public IActionResult EditPendingCar(int id)
        {
            if (id == 0 || id == null) 
            { 
                return NotFound();
            }
            var car=carService.FindOne(x => x.Id == id);
            CarId = car.Id;
            var viewModel = new EditCarWithImagesPendingViewModel
            {
                Brand=car.Brand,
                Model=car.Model,
                Gearbox=car.Gearbox,
                Year=car.Year,
                PricePerDay=car.PricePerDay,
                PricePerWeek=car.PricePerWeek,
                MileageLimitForDay=car.MileageLimitForDay,
                MileageLimitForWeek=car.MileageLimitForWeek,
                AdditionalMileageCharge=car.AdditionalMileageCharge,
                EngineCapacity=car.EngineCapacity,
                Color=car.Color,
                Description=car.Description,
                DriveTrain=car.DriveTrain,
                HorsePower=car.HorsePower,
                ZeroToHundred=car.ZeroToHundred,
                TopSpeed=car.TopSpeed,
                Images = imageService.GetImagesByCarId(car.Id).Select(img => new ImageViewModel { Id = img.Id, Url = img.Url, Order=img.Order }).ToList(),
                ClassOfCarId =car.ClassOfCarId,
                ClassOptions = new SelectList(classOfCarService.GetAll().ToList(), "Id", "Name")
            };
            return View(viewModel);
            
        }
        [HttpPost("Car/EditPendingCar/{id?}")]
        public IActionResult EditPendingCar(int? id,EditCarWithImagesPendingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Retrieve the existing car using the ID from the view model.
            var car = carService.FindOne(x=>x.Id==id);
            if (car == null)
            {
                return NotFound();
            }

            // Update the properties
            car.Brand = viewModel.Brand;
            car.Model = viewModel.Model;
            car.Gearbox = viewModel.Gearbox;
            car.Year = viewModel.Year;
            car.PricePerDay = viewModel.PricePerDay;
            car.PricePerWeek = viewModel.PricePerWeek;
            car.MileageLimitForDay = viewModel.MileageLimitForDay;
            car.MileageLimitForWeek = viewModel.MileageLimitForWeek;
            car.AdditionalMileageCharge = viewModel.AdditionalMileageCharge;
            car.EngineCapacity = viewModel.EngineCapacity;
            car.Color = viewModel.Color;
            car.Description = viewModel.Description;
            car.DriveTrain = viewModel.DriveTrain;
            car.HorsePower = viewModel.HorsePower;
            car.ZeroToHundred = viewModel.ZeroToHundred;
            car.TopSpeed = viewModel.TopSpeed;

            // Update the pending status
            car.Pending = false;

            // Save the changes
            carService.Update(car);
            carService.Save();

            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public IActionResult UpdateImageOrder([FromBody] ImageOrderUpdateRequest request)
        {
            if (request == null || request.OrderedImageIds == null || !request.OrderedImageIds.Any())
            {
                return Json(new { success = false, message = "Invalid request" });
            }

            // Логика за актуализиране на реда в базата
            int order = 1;
            foreach (var imageId in request.OrderedImageIds)
            {
                var image = imageService.FindByid(int.Parse(imageId));
                if (image != null)
                {
                    image.Order = order;
                    imageService.Update(image);
                    imageService.Save();
                    order++;
                }
            }

            return Json(new { success = true });
        }

        // Клас за получаване на новия ред
        public class ImageOrderUpdateRequest
        {
            public List<string> OrderedImageIds { get; set; }
        }
    }
}