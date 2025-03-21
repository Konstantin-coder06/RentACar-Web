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

        public async Task<IActionResult> Index()
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

            
            List<int> reservedCarIds = new List<int>();
            if (startDay.HasValue && endDay.HasValue)
            {
                reservedCarIds = (await reservationService.GetAll())
                    .Where(r => r.StartDate < endDay.Value && r.EndDate > startDay.Value)
                    .Select(r => r.CarId)
                    .Distinct()
                    .ToList();
            }

            var cars = new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars = (await carService.GetAll()).ToList();
            }
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId.HasValue)
            {
                cars = (await carService.FindAll(x => x.CarCompanyId == companyId)).ToList();
            }
            if (!User.IsInRole("Admin") && !companyId.HasValue)
            {
                cars = (await carService.FindAll(car => !reservedCarIds.Contains(car.Id) && !car.Pending)).ToList();
            }
            var selectedCategoriesStr = HttpContext.Session.GetString("SelectedCategories");
            var selectedCategories = new List<string>();
            if (!string.IsNullOrEmpty(selectedCategoriesStr))
            {

               selectedCategories= selectedCategoriesStr.Split(',').ToList();
            }
            if (selectedCategories.Any())
            {
                var categoryIds = (await classOfCarService.FindAll(x => selectedCategories.Contains(x.Name))).Select(x => x.Id).ToList();
                cars = cars.Where(x => categoryIds.Contains(x.ClassOfCarId)).ToList();
            }
            var carsWithImages = new List<CarWithImages>();

            foreach (var car in cars)
            {
                var images = await imageService.GetImagesByCarId(car.Id);
                carsWithImages.Add(new CarWithImages
                {
                    Car = car,
                    Images = images.ToList()
                });
            }

            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
                SelectedCategories = selectedCategories
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
           
        }
        [HttpPost]
        [Route("Car/Search")]
        public async Task<IActionResult> Search(string searchBar)
        {
            var queries = (await carService.GetAll()).AsQueryable();

            if (!string.IsNullOrEmpty(searchBar))
            {
               
                var searchTerms = searchBar.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

              
                queries = queries.Where(x => searchTerms.All(term =>
                    x.Brand.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    x.Model.Contains(term, StringComparison.OrdinalIgnoreCase)));
            }

          
            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");

            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);

            List<Reservation> overlappingReservations = new List<Reservation>();
            if (startDay.HasValue && endDay.HasValue)
            {
                overlappingReservations = (await reservationService.FindAll(x => x.EndDate >= startDay && x.StartDate <= endDay)).ToList();
            }

            var cars = queries.AsEnumerable().Where(x => (overlappingReservations.Count == 0 ||
                                                        !overlappingReservations.Any(r => r.CarId == x.Id)) &&
                                                        x.Pending == false).ToList();


            var carsWithImages = new List<CarWithImages>();

            foreach (var car in cars)
            {
                var images = await imageService.GetImagesByCarId(car.Id);
                carsWithImages.Add(new CarWithImages
                {
                    Car = car,
                    Images = images.ToList()
                });
            }

            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
                SearchTerm = searchBar
            };

            return View("Index", viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Sort(string sortOrder)
        {
            var sortedCars = new List<Car>();
            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");

            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);

            List<int> reservedCarIds = new List<int>();
            if (startDay.HasValue && endDay.HasValue)
            {
                reservedCarIds =(await reservationService.FindAll(r => r.StartDate < endDay.Value && r.EndDate > startDay.Value))
                    .Select(r => r.CarId)
                    .Distinct()
                    .ToList();
            }
            var cars=new List<Car>();
            cars = (await carService.FindAll(car => !reservedCarIds.Contains(car.Id) && !car.Pending)).ToList();
            switch (sortOrder)
            {
                case "Name (A-Z)":
                    sortedCars = cars.OrderBy(c => c.Brand).ToList();
                    break;
                case "Name (Z-A)":
                    sortedCars = cars.OrderByDescending(c => c.Brand).ToList();
                    break;
                case "Year (Oldest First)":
                    sortedCars = cars.OrderBy(c => c.Year).ToList();
                    break;
                case "Year (Newest First)":
                    sortedCars = cars.OrderByDescending(c => c.Year).ToList();
                    break;
                case "default":
                    sortedCars = cars;
                    break;
                case "Price (Lowest First)":
                    sortedCars= cars.OrderBy(c=>c.PricePerDay).ToList();
                    break;
                case "Price (Highest First)":
                    sortedCars = cars.OrderByDescending(c => c.PricePerDay).ToList();
                    break;
            }
            var carsWithImages = new List<CarWithImages>();

            foreach (var car in sortedCars)
            {
                var images = await imageService.GetImagesByCarId(car.Id);
                carsWithImages.Add(new CarWithImages
                {
                    Car = car,
                    Images = images.ToList()
                });
            }

            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
                SortTerm =sortOrder,
            };
            return View("Index", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> FilterDates(CarImagesWithDatesViewModel carImagesView)
        {
            if (!carImagesView.StartDate.HasValue || !carImagesView.EndDate.HasValue)
            {
                return BadRequest("Please provide both start and end dates.");
            }
            HttpContext.Session.SetString("StartDate", carImagesView.StartDate.Value.ToString("yyyy-MM-dd"));
            HttpContext.Session.SetString("EndDate", carImagesView.EndDate.Value.ToString("yyyy-MM-dd"));
            var overlappingReservations =(await reservationService.FindAll(r => r.StartDate < carImagesView.EndDate && r.EndDate > carImagesView.StartDate))
                .Select(r => r.CarId)
                .Distinct()
                .ToList();
            List<Car> cars = new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars =(await carService.GetAll()).ToList();
            }
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId.HasValue)
            {
                cars = (await carService.FindAll(x => x.CarCompanyId == companyId)).ToList();
            }
            if (!User.IsInRole("Admin") && !companyId.HasValue)
            {
                cars =(await carService.FindAll(car => !overlappingReservations.Contains(car.Id) && !car.Pending))
                    .ToList();
            }
            var carsWithImages = new List<CarWithImages>();
            foreach (var car in cars)
            {
                var images = await imageService.GetImagesByCarId(car.Id);
                carsWithImages.Add(new CarWithImages
                {
                    Car = car,
                    Images = images.ToList()
                });
            }      
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Filters()
        {
            var model = new CarWithFilters
            {
                Classes = (await classOfCarService.GetAll()).ToList(),
                CarBrands=(await carService.GetAll()).Select(x=>x.Brand).Distinct().ToList(),
                Colors=(await carService.GetAll()).Select(x=>x.Color).Distinct().ToList(),
                DriveTrains=(await carService.GetAll()).Select(x=>x.DriveTrain).Distinct().ToList(),
            };
            return View(model);
        }
        [HttpPost]
        
        public async Task<IActionResult> Filters(CarWithFilters carWithFilters)
        {
            IQueryable<Car> queries = (await carService.GetAll()).AsQueryable();

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
            if (carWithFilters.SelectedBrands != null && carWithFilters.SelectedBrands.Any())
            {
                queries= queries.Where(x=>carWithFilters.SelectedBrands.Contains(x.Brand) &&!x.Pending);

            }
            if (carWithFilters.SelectedColors != null && carWithFilters.SelectedColors.Any())
            {
                queries = queries.Where(x => carWithFilters.SelectedColors.Contains(x.Color) && !x.Pending);

            }
            if (carWithFilters.SelectedDriveTrains != null && carWithFilters.SelectedDriveTrains.Any())
            {
                queries = queries.Where(x => carWithFilters.SelectedDriveTrains.Contains(x.DriveTrain) && !x.Pending);

            }
            var cars = queries.ToList();
            var carsWithImages = new List<CarWithImages>();
            foreach (var car in cars)
            {
                var images = await imageService.GetImagesByCarId(car.Id);
                carsWithImages.Add(new CarWithImages
                {
                    Car = car,
                    Images = images.ToList()
                });
            }
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
        public async Task<IActionResult> Sorting(int sort)
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

         
            List<Reservation> reservations =(await reservationService.FindAll(x => (!startDay.HasValue || x.StartDate >= startDay) &&
                                                                            (!endDay.HasValue || x.StartDate <= endDay))).ToList();

          
            var cars = new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars = (await carService.GetAll()).ToList();
            }
            else if (HttpContext.Session.GetInt32("CompanyId").HasValue)
            {
                var companyId = HttpContext.Session.GetInt32("CompanyId").Value;
                cars = (await carService.FindAll(x => x.CarCompanyId == companyId)).ToList();
            }
            else
            {
                var reservedCarIds = reservations.Select(r => r.CarId).ToList();
                cars = (await carService.GetAll())
                    .Where(car => !reservedCarIds.Contains(car.Id) && !car.Pending)
                    .ToList();
            }
            switch (sort)
            {
                case 1:
                    cars = cars.OrderBy(x => x.PricePerDay).ToList();
                    break;
                case 2:
                    cars = cars.OrderByDescending(x => x.PricePerDay).ToList();
                    break;
                default:
                    break;
            }


            var carsWithImages = new List<CarWithImages>();

            foreach (var car in cars)
            {
                var images = await imageService.GetImagesByCarId(car.Id);
                carsWithImages.Add(new CarWithImages
                {
                    Car = car,
                    Images = images.ToList()
                });
            }

           
            var viewModel = new CarImagesWithDatesViewModel
            {
                CarWithImages = carsWithImages,
                StartDate = startDay,
                EndDate = endDay,
                Sort = sort,
               
            };

         
            return View("Index", viewModel);
        
        }
        public async Task<IActionResult> Reservation(int id)
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
                    DateTimeStyles.None 
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


            var car =await carService.FindOne(x => x.Id == id);
            var featuresOfACar = (await carFeatureService.GetByCarIDAllFeatures(car.Id)).ToList();
            var features = new List<Feature>();
            Feature feature = new Feature();
            foreach (var x in featuresOfACar)
            {
                feature =await featureService.FindOne(f => f.Id == x);
                features.Add(feature);
            }

            var carWithImages = new CarWithImages
            {
                Car = car,
                Images = (await imageService.GetImagesByCarId(car.Id)).OrderBy(x => x.Order).ToList(),
                StartDate = startDay,
                EndDate = endDay,
                Features = features,
                IsSelfPick=true,
                IsReturnOptionsEnabled=false,
                IsAddressInputVisible=false,
            };


            return View(carWithImages);
        }
        [HttpPost]
        public async Task<IActionResult> Reservation(CarWithImages carWithImages,string submitButton)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            // Load car and images if not already present
            if (carWithImages.Car == null || carWithImages.Car.Id == 0)
            {
                carWithImages.Car = await carService.FindOne(x => x.Id == carWithImages.Car.Id);
            }
            if (carWithImages.Car == null)
            {
                ModelState.AddModelError("", "Car not found.");
                return View(carWithImages);
            }
            carWithImages.Images = (await imageService.GetImagesByCarId(carWithImages.Car.Id)).OrderBy(x => x.Order).ToList();

            if (submitButton == "ChangeDate")
            {
                var newStartDate = carWithImages.StartDate;
                var newEndDate = carWithImages.EndDate;

                if (!newStartDate.HasValue || !newEndDate.HasValue || newStartDate >= newEndDate)
                {
                    ModelState.AddModelError("", "Invalid dates: Start date must be before end date.");
                    return View(carWithImages);
                }

                // Check for overlapping reservations
                bool hasConflict = await reservationService.HasOverlappingReservation(
                    carWithImages.Car.Id,
                    newStartDate.Value,
                    newEndDate.Value
                );

                if (!hasConflict)
                {
                    // No conflict, update session and redirect
                    HttpContext.Session.SetString("StartDate", newStartDate.Value.ToString("yyyy-MM-dd"));
                    HttpContext.Session.SetString("EndDate", newEndDate.Value.ToString("yyyy-MM-dd"));
                    return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
                }
                else
                {
                    // Conflict detected, prepare for confirmation
                    TempData["HasConflict"] = true;
                    TempData["ProposedStartDate"] = newStartDate.Value.ToString("yyyy-MM-dd");
                    TempData["ProposedEndDate"] = newEndDate.Value.ToString("yyyy-MM-dd");
                    // Set the form to show proposed dates
                    var car = await carService.FindOne(x => x.Id ==carWithImages.Car.Id);
                    var featuresOfACar = (await carFeatureService.GetByCarIDAllFeatures(car.Id)).ToList();
                    var features = new List<Feature>();
                    Feature feature = new Feature();
                    foreach (var x in featuresOfACar)
                    {
                        feature = await featureService.FindOne(f => f.Id == x);
                        features.Add(feature);
                    }
                    carWithImages.Car = car;
                    carWithImages.Images = (await imageService.GetImagesByCarId(car.Id)).OrderBy(x => x.Order).ToList();
                    carWithImages.StartDate = newStartDate;
                    carWithImages.EndDate = newEndDate;
                    carWithImages.Features = features;
                    return View(carWithImages);
                }
            }
            else if (submitButton == "KeepDates")
            {
                // User chose to keep the conflicting dates
                var proposedStartDate = DateTime.Parse(TempData["ProposedStartDate"]?.ToString());
                var proposedEndDate = DateTime.Parse(TempData["ProposedEndDate"]?.ToString());
                HttpContext.Session.SetString("StartDate", proposedStartDate.ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("EndDate", proposedEndDate.ToString("yyyy-MM-dd"));
                return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
            }
            else if (submitButton == "RevertDates")
            {
                // User chose to revert, keep old dates in session
                return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
            }
            else if (submitButton == "BookNow")
            {
                if (!carWithImages.StartDate.HasValue || !carWithImages.EndDate.HasValue)
                {
                    ModelState.AddModelError("", "Start and end dates are required.");
                    return View(carWithImages);
                }

                DateTime startDay = carWithImages.StartDate.Value;
                DateTime endDay = carWithImages.EndDate.Value;
                int totalDays = (int)(endDay - startDay).TotalDays;

                if (totalDays <= 0)
                {
                    ModelState.AddModelError("", "End date must be after start date.");
                    return View(carWithImages);
                }

                // Check for overlapping reservations before booking
                bool hasConflict = await reservationService.HasOverlappingReservation(
                    carWithImages.Car.Id,
                    startDay,
                    endDay
                );
                if (hasConflict)
                {
                    ModelState.AddModelError("", "The car is already reserved for the selected dates.");
                    return View(carWithImages);
                }

                // Proceed with booking
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
                else if (totalDays > 0)
                {
                    reservation.TotalPrice = (totalDays * carWithImages.Car.PricePerDay) * 0.9;
                }

                await reservationService.Add(reservation);
                await reservationService.Save();
                TempData["success"] = $"Days {totalDays} Total price: {reservation.TotalPrice}";
                return RedirectToAction("Index", "Home");
            }

            return View(carWithImages);
            /* if (submitButton == "ChangeDate")
             {
                 HttpContext.Session.SetString("StartDate", carWithImages.StartDate?.ToString("yyyy-MM-dd"));
                 HttpContext.Session.SetString("EndDate", carWithImages.EndDate?.ToString("yyyy-MM-dd"));
                 return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
             }
             if (submitButton == "UpdateOptions")
             {
                 // Update form state based on IsSelfPick
                 carWithImages.IsReturnOptionsEnabled = !carWithImages.IsSelfPick;
                 carWithImages.IsAddressInputVisible = !carWithImages.IsSelfPick;
                 return RedirectToAction("Reservation",carWithImages.Car.Id); // Re-render the form
             }


             var userId = HttpContext.Session.GetInt32("UserId");
             if (!userId.HasValue)
             {
                 return RedirectToAction("AccessDenied", "Account");
             }


             if (carWithImages.Car == null || carWithImages.Car.Id == 0)
             {
                 carWithImages.Car =await carService.FindOne(x => x.Id == carWithImages.Car.Id);
             }
             if (carWithImages.Car == null)
             {
                 ModelState.AddModelError("", "Car not found.");
                 return View(carWithImages);
             }


             carWithImages.Images = (await imageService.GetImagesByCarId(carWithImages.Car.Id)).OrderBy(x => x.Order).ToList();

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
                 var car = await carService.FindOne(x => x.Id == carWithImages.Car.Id);
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
                 await reservationService.Add(reservation);
                 await reservationService.Save();
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
         public async Task<IActionResult> Pendings()
         {
             var companyId = HttpContext.Session.GetInt32("CompanyId");
             var cars=new List<Car>();
             if (companyId.HasValue)
             {
                 cars = (await carService.FindAll(x => x.Pending == true && x.CarCompanyId == companyId)).OrderBy(x => x.CreatedAt).ToList();
             }
             else
             {


                 cars = (await carService.FindAll(x => x.Pending == true)).OrderBy(x => x.CreatedAt).ToList();
             }
             var carImages = new List<CarWithImages>();

             foreach (var car in cars)
             {
                 var images = await imageService.GetImagesByCarId(car.Id);
                 carImages.Add(new CarWithImages
                 {
                     Car = car,
                     Images = images.ToList()
                 });
             }

             var carImagesViewModel= new CarImagesViewModel() 
             {
                 CarWithImages = carImages,
             };
             return View(carImagesViewModel);
         }
         public int CarId { get; set; } = 0;
         public async Task<IActionResult> EditPendingCar(int id)
         {
             if (id == 0 ||id == null) 
             { 
                 return NotFound();
             }
             var car= await carService.FindOne(x => x.Id == id);
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
                 Images = (await imageService.GetImagesByCarId(car.Id)).Select(img => new ImageViewModel { Id = img.Id, Url = img.Url, Order=img.Order }).ToList(),
                 ClassOfCarId =car.ClassOfCarId,
                 ClassOptions =  new SelectList((await classOfCarService.GetAll()).ToList(), "Id", "Name")
             };
             return View(viewModel);*/

        }
        [HttpPost("Car/EditPendingCar/{id?}")]
        public async Task<IActionResult> EditPendingCar(int? id, EditCarWithImagesPendingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


            var car = await carService.FindOne(x => x.Id == id);
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
            await carService.Save();
           
            return RedirectToAction("Index", "Car");
            
          
        }
        [HttpPost]
        public async Task<IActionResult> UpdateImageOrder([FromBody] ImageOrderUpdateRequest request)
        {
            if (request == null || request.OrderedImageIds == null || !request.OrderedImageIds.Any())
            {
                return Json(new { success = false, message = "Invalid request" });
            }

         
            int order = 1;
            foreach (var imageId in request.OrderedImageIds)
            {
                var image = await imageService.FindByid(int.Parse(imageId));
                if (image != null)
                {
                    image.Order = order;
                    imageService.Update(image);
                    await imageService.Save();
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