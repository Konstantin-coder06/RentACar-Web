using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;
using System;
using System.Globalization;

namespace RentACar.Controllers
{
    public class CarController : Controller
    {
        ICarService carService;
        IImageService imageService;
        IClassOfCarService classOfCarService;
        IReservationService reservationService;
       IFeatureService featureService;
        ICarFeatureService carFeatureService;
        public CarController(ICarService _carService, IImageService _imageService, IClassOfCarService _classOfCarService,IReservationService reservationService, IFeatureService featureService, ICarFeatureService carFeatureService)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            this.reservationService = reservationService;
            this.featureService = featureService;
            this.carFeatureService = carFeatureService;
        }

        public IActionResult Index()
        {
            DateTime? startDay = null;
            DateTime? endDay = null;

            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(startDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            var endDayStr = HttpContext.Session.GetString("EndDate");
            if (!string.IsNullOrEmpty(endDayStr))
            {
                endDay = DateTime.ParseExact(endDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            // Calculate reserved car IDs only if both dates are provided
            List<int> reservedCarIds = new List<int>();
            if (startDay.HasValue && endDay.HasValue)
            {
                reservedCarIds = reservationService.GetAll()
                    .Where(r => r.StartDate < endDay.Value && r.EndDate > startDay.Value)
                    .Select(r => r.CarId)
                    .Distinct()
                    .ToList();
            }

            var cars = new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars = carService.GetAll().ToList();
            }
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId.HasValue)
            {
                cars = carService.FindAll(x => x.CarCompanyId == companyId).ToList();
            }
            if (!User.IsInRole("Admin") && !companyId.HasValue)
            {
                cars = carService.GetAll()
                    .Where(car => !reservedCarIds.Contains(car.Id) && !car.Pending)
                    .ToList();
            }
            var selectedCategoriesStr = HttpContext.Session.GetString("SelectedCategories");
            var selectedCategories = new List<string>();
            if (!string.IsNullOrEmpty(selectedCategoriesStr))
            {

               selectedCategories= selectedCategoriesStr.Split(',').ToList();
            }
            if (selectedCategories.Any())
            {
                var categoryIds = classOfCarService.FindAll(x => selectedCategories.Contains(x.Name)).Select(x => x.Id).ToList();
                cars = cars.Where(x => categoryIds.Contains(x.ClassOfCarId)).ToList();
            }
           
            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
                SelectedCategories = selectedCategories,
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult FilterByCategory(string categories)
        {
            if (categories != null && categories.StartsWith("toggle_"))
            {
                string category = categories.Substring(7); // Extract category name, e.g., "Economy" from "toggle_Economy"
                var selectedCategories = HttpContext.Session.GetString("SelectedCategories")?.Split(',')?.ToList() ?? new List<string>();

                if (selectedCategories.Contains(category))
                {
                    selectedCategories.Remove(category); // Deselect if already selected
                }
                else
                {
                    selectedCategories.Add(category); // Select if not already selected
                }

                HttpContext.Session.SetString("SelectedCategories", string.Join(",", selectedCategories));
            }

            return RedirectToAction("Index");
           /* int categoryId=classOfCarService.FindOne(x=>x.Name== category).Id;
            var filterByCategoryCars = carService.FindAll(x => x.ClassOfCarId == categoryId && !x.Pending);
            var carsWithImages = filterByCategoryCars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();
            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");

            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);
            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
            };

            return View("Index",viewModel);*/
        }
        [HttpPost]
        [Route("Car/Search")]
        public IActionResult Search(string searchBar)
        {
            var queries = carService.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchBar))
            {
                queries = queries.Where(x => x.Brand.Contains(searchBar, StringComparison.OrdinalIgnoreCase) ||
                                             x.Model.Contains(searchBar, StringComparison.OrdinalIgnoreCase));
            }

          
            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");

            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);

         
            List<Reservation> overlappingReservations = new List<Reservation>();
            if (startDay.HasValue && endDay.HasValue)
            {
                overlappingReservations = reservationService.FindAll(x => x.EndDate >= startDay &&
                                                                          x.StartDate <= endDay).ToList();
            }

           
            var cars = queries.AsEnumerable().Where(x => (overlappingReservations.Count == 0 ||
                                                          !overlappingReservations.Any(r => r.CarId == x.Id)) &&
                                                          x.Pending == false).ToList();

            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
            };

            return View("Index", viewModel);
        
        }
        [HttpPost]
        public IActionResult FilterDates(CarImagesWithDatesViewModel carImagesView)
        {
            // Validate dates
            if (!carImagesView.StartDate.HasValue || !carImagesView.EndDate.HasValue)
            {
                return BadRequest("Please provide both start and end dates.");
            }

            // Set session variables
            HttpContext.Session.SetString("StartDate", carImagesView.StartDate.Value.ToString("yyyy-MM-dd"));
            HttpContext.Session.SetString("EndDate", carImagesView.EndDate.Value.ToString("yyyy-MM-dd"));

            // Get overlapping reservations
            var overlappingReservations = reservationService.GetAll()
                .Where(r => r.StartDate < carImagesView.EndDate && r.EndDate > carImagesView.StartDate)
                .Select(r => r.CarId)
                .Distinct()
                .ToList();

            List<Car> cars = new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars = carService.GetAll().ToList();
            }
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId.HasValue)
            {
                cars = carService.FindAll(x => x.CarCompanyId == companyId).ToList();
            }
            if (!User.IsInRole("Admin") && !companyId.HasValue)
            {
                cars = carService.GetAll()
                    .Where(car => !overlappingReservations.Contains(car.Id) && !car.Pending)
                    .ToList();
            }

            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            // Redirect to Index, which will use session data
            return RedirectToAction("Index");
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
            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");
            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);
            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
            };

            return View("Index", viewModel);
        }
        [HttpPost]
        public IActionResult Sorting(int sort)
        {
            DateTime? startDay = null;
            DateTime? endDay = null;

            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(startDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            var endDayStr = HttpContext.Session.GetString("EndDate");
            if (!string.IsNullOrEmpty(endDayStr))
            {
                endDay = DateTime.ParseExact(endDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            // Get reservations based on date range
            List<Reservation> reservations = reservationService.FindAll(x => (!startDay.HasValue || x.StartDate >= startDay) &&
                                                                            (!endDay.HasValue || x.StartDate <= endDay)).ToList();

            // Get cars based on user role or company
            var cars = new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars = carService.GetAll().ToList();
            }
            else if (HttpContext.Session.GetInt32("CompanyId").HasValue)
            {
                var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
                cars = carService.FindAll(x => x.CarCompanyId == companyId).ToList();
            }
            else
            {
                var reservedCarIds = reservations.Select(r => r.CarId).ToList();
                cars = carService.GetAll()
                    .Where(car => !reservedCarIds.Contains(car.Id) && !car.Pending)
                    .ToList();
            }

            // Apply sorting
            switch (sort)
            {
                case 1:
                    cars = cars.OrderBy(x => x.PricePerDay).ToList();
                    break;
                case 2:
                    cars = cars.OrderByDescending(x => x.PricePerDay).ToList();
                    break;
                default:
                    // No sorting applied if sort value is invalid or empty
                    break;
            }

            // Pair cars with images
            var carsWithImages = cars.Select(car => new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            }).ToList();

            // Create view model
            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
                Sort = sort,
               
            };

            // Return the Index view with the sorted data
            return View("Index", viewModel);
        
        }
        public IActionResult Reservation(int id)
        {
            DateTime? startDay = null;
            DateTime? endDay = null;

            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(
                    startDayStr,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None // 🚨 Remove AssumeUniversal
                );
            }

            var endDayStr = HttpContext.Session.GetString("EndDate");
            if (!string.IsNullOrEmpty(endDayStr))
            {
                endDay = DateTime.ParseExact(endDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("Register", "Account");
            }


            var car = carService.FindOne(x => x.Id == id);
            var featuresOfACar = carFeatureService.GetByCarIDAllFeatures(car.Id).ToList();
            var features = new List<Feature>();
            Feature feature = new Feature();
            foreach (var x in featuresOfACar)
            {
                feature = featureService.FindOne(f => f.Id == x);
                features.Add(feature);
            }

            var carWithImages = new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).OrderBy(x => x.Order).ToList(),
                StartDate = startDay,
                EndDate = endDay,
                Features = features

            };


            return View(carWithImages);
        }
        [HttpPost]
        public IActionResult Reservation(CarWithImages carWithImages,string submitButton)
        {
            if (submitButton == "ChangeDate")
            {
                HttpContext.Session.SetString("StartDate", carWithImages.StartDate?.ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("EndDate", carWithImages.EndDate?.ToString("yyyy-MM-dd"));
                return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
            }

           

            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

         
            if (carWithImages.Car == null || carWithImages.Car.Id == 0)
            {
                carWithImages.Car = carService.FindOne(x => x.Id == carWithImages.Car.Id);
            }
            if (carWithImages.Car == null)
            {
                ModelState.AddModelError("", "Car not found.");
                return View(carWithImages);
            }

         
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
                int totalDays = (int)(endDay - startDay).TotalDays;
                var car = carService.FindOne(x => x.Id == carWithImages.Car.Id);
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
                    CarId = car.Id,
                    CustomerId = userId.Value,
                    CreateTime = DateTime.Now,
                    
                };
                if (totalDays == 7) 
                {
                    reservation.TotalPrice = car.PricePerWeek;
                }
                else if (totalDays > 0) 
                {
                    reservation.TotalPrice = (totalDays * car.PricePerDay) * 0.9;
                }
                reservationService.Add(reservation);
                reservationService.Save();
                TempData["success"] = $" Days {totalDays} Total price: {car.PricePerDay}";
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest($"Model is invalid: {string.Join(", ", errors)}");
            }
            return View(carWithImages);
        
         
        }
        public IActionResult Pendings()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var cars=new List<Car>();
            if (companyId.HasValue)
            {
                cars = carService.FindAll(x => x.Pending == true && x.CarCompanyId == companyId).OrderBy(x => x.CreatedAt).ToList();
            }
            else
            {


                cars = carService.FindAll(x => x.Pending == true).OrderBy(x => x.CreatedAt).ToList();
            }
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
            if (id == 0 ||id == null) 
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
        public IActionResult EditPendingCar(int? id, EditCarWithImagesPendingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


            var car = carService.FindOne(x => x.Id == id);
            if (car == null)
            {
                return NotFound();
            }


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

            if (User.IsInRole("Company"))
            {
                car.Pending = true;
            }
            else
            {


                car.Pending = false;
            }

            carService.Update(car);
            carService.Save();
           
            return RedirectToAction("Index", "Car");
            
          
        }
        [HttpPost]
        public IActionResult UpdateImageOrder([FromBody] ImageOrderUpdateRequest request)
        {
            if (request == null || request.OrderedImageIds == null || !request.OrderedImageIds.Any())
            {
                return Json(new { success = false, message = "Invalid request" });
            }

         
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

       
        public class ImageOrderUpdateRequest
        {
            public List<string> OrderedImageIds { get; set; }
        }
    }
}