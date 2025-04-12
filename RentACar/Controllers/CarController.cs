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
using static NuGet.Packaging.PackagingConstants;

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
        ICTypeService cTypeService;
        CloudinaryService cloudinaryService;
        ICarCompanyService carCompanyService;
        ICTypeService typeService;
        public CarController(ICarService _carService, IImageService _imageService, IClassOfCarService _classOfCarService,IReservationService reservationService, IFeatureService featureService, ICarFeatureService carFeatureService,ICTypeService cTypeService, CloudinaryService cloudinaryService,ICarCompanyService carCompanyService, ICTypeService typeService)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            this.reservationService = reservationService;
            this.featureService = featureService;
            this.carFeatureService = carFeatureService;
            this.cTypeService = cTypeService;
            this.cloudinaryService = cloudinaryService;
            this.typeService = typeService;
            this.carCompanyService = carCompanyService;
        }

        public async Task<IActionResult> Index()
        {          
            DateTime? startDay = null;
            DateTime? endDay = null;
            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(startDayStr, "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }
            var endDayStr = HttpContext.Session.GetString("EndDate");
            if (!string.IsNullOrEmpty(endDayStr))
            {
                endDay = DateTime.ParseExact(endDayStr, "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }       
            List<int> reservedCarIds = new List<int>();
            if (startDay.HasValue && endDay.HasValue)
            {
                reservedCarIds = await reservationService.GetAllReservatedCarsId(startDay.Value,endDay.Value);
            }
            var viewModel = new CarImagesWithDatesViewModel();
            if (User.IsInRole("User"))
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId.HasValue)
                {


                    var hasUserReservationForOnePeriod = await reservationService.FindIfUserHasReservationForOnePeriod(startDay, endDay, userId.Value);
                    if (hasUserReservationForOnePeriod !=0)
                    {
                        TempData["Conflict"] = true;
                        TempData["ProposedStartDate"] = startDay.Value.ToString("yyyy-MM-dd");
                        TempData["ProposedEndDate"] = endDay.Value.ToString("yyyy-MM-dd");
                        var car = await carService.FindById(hasUserReservationForOnePeriod);
                        viewModel.BrandOfCar = car.Brand;
                        viewModel.ModelOfCar=car.Model;
                    }
                }
                else
                {
                    return RedirectToAction("Register", "Account");
                }
            }
            IEnumerable<Car> cars=new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars = await carService.GetAll();
            }
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId.HasValue)
            {
                cars = await carService.GetAllCarsOfCompany(companyId.Value);
            }
            if (!User.IsInRole("Admin") && !companyId.HasValue)
            {
                cars = await carService.GetAllNotReservetedAndNotPendingCars(reservedCarIds);
            }
            var selectedCategoriesStr = HttpContext.Session.GetString("SelectedCategories");
            var selectedCategories = new List<string>();
            if (!string.IsNullOrEmpty(selectedCategoriesStr))
            {

               selectedCategories= selectedCategoriesStr.Split(',').ToList();
            }
            if (selectedCategories.Any())
            {
                var categoryIds = await classOfCarService.GetAllClassSelectedIds(selectedCategories);
                cars = cars.Where(x => categoryIds.Contains(x.ClassOfCarId)).ToList();
            }
            var sortOrder = HttpContext.Session.GetString("SortOrder") ?? "default";
            switch (sortOrder)
            {
                case "Name (A-Z)":
                    cars = cars.OrderBy(c => c.Brand).ToList();
                    break;
                case "Name (Z-A)":
                    cars = cars.OrderByDescending(c => c.Brand).ToList();
                    break;
                case "Year (Oldest First)":
                    cars = cars.OrderBy(c => c.Year).ToList();
                    break;
                case "Year (Newest First)":
                    cars = cars.OrderByDescending(c => c.Year).ToList();
                    break;
                case "Price (Lowest First)":
                    cars = cars.OrderBy(c => c.PricePerDay).ToList();
                    break;
                case "Price (Highest First)":
                    cars = cars.OrderByDescending(c => c.PricePerDay).ToList();
                    break;
                case "default":
                    int seed = DateTime.UtcNow.Date.GetHashCode();
                    Random random = new Random(seed);
                    cars = cars.OrderBy(x => random.Next()).ToList();
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



            viewModel.CarWithImages = carsWithImages;
            viewModel.StartDate = startDay;
            viewModel.EndDate = endDay;
            viewModel.SelectedCategories = selectedCategories;
            viewModel.SortTerm = sortOrder;
            

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult ClearSort()
        {
            HttpContext.Session.Remove("SortOrder");
            Console.WriteLine("ClearSort executed");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult FilterByCategory(string categories)
        {
            if (categories != null && categories.StartsWith("toggle_"))
            {
                string category = categories.Substring(7); 
                var selectedCategories = HttpContext.Session.GetString("SelectedCategories")?.Split(',')?.ToList() ?? new List<string>();

                if (selectedCategories.Contains(category))
                {
                    selectedCategories.Remove(category); 
                }
                else
                {
                    selectedCategories.Add(category);
                }

                HttpContext.Session.SetString("SelectedCategories", string.Join(",", selectedCategories));
            }

            return RedirectToAction("Index");
           
        }
        [HttpPost]
        [Route("Car/Search")]
        public async Task<IActionResult> Search(string searchBar)
        {
            var queries = await carService.GetAll();
            if (!string.IsNullOrEmpty(searchBar))
            {
                var searchTerms = searchBar.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (User.IsInRole("Company"))
                {
                    var companyId = HttpContext.Session.GetInt32("CompanyId");
                    if (companyId.HasValue)
                    {
                        queries = await carService.GetCarsOfCompanyBySearchBrandAndModel(companyId.Value, searchTerms);
                    }
                    else
                    {
                        throw new Exception("Please relog in! You made something incorrect");
                    }
                }
                else
                {
                    queries = await carService.GetCarsBySearchBrandAndModel(searchTerms);
                }
            }
            var startDayStr = HttpContext.Session.GetString("StartDate");
            var endDayStr = HttpContext.Session.GetString("EndDate");
            DateTime? startDay = string.IsNullOrEmpty(startDayStr) ? null : DateTime.Parse(startDayStr);
            DateTime? endDay = string.IsNullOrEmpty(endDayStr) ? null : DateTime.Parse(endDayStr);
            var overlappingReservations = await reservationService.GetAllByStartAndEndDate(startDay, endDay);
            IEnumerable<Car>cars=new List<Car>();
            if (!User.IsInRole("Company"))
            {
                 cars = await carService.GetAllNotReservationedCarsForOnePeriod(queries, overlappingReservations.ToList());
            }
            else
            {
                cars = queries;
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
                SearchTerm = searchBar
            };
            return View("Index", viewModel);

        }
        [HttpPost]
        public IActionResult Sort(string sortOrder)
        {
            HttpContext.Session.SetString("SortOrder", sortOrder);
            return RedirectToAction("Index");
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
            var overlappingReservations = await reservationService.GetAllReservatedCarsId(carImagesView.StartDate, carImagesView.EndDate);
            List<Car> cars = new List<Car>();
            if (User.IsInRole("Admin"))
            {
                cars = (await carService.GetAll()).ToList();
            }
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId.HasValue)
            {
                cars = (await carService.GetAllCarsOfCompany(companyId.Value)).ToList();
            }
            if (!User.IsInRole("Admin") && !companyId.HasValue)
            {
                cars =(await carService.GetAllNotReservetedAndNotPendingCars(overlappingReservations)).ToList();
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
                CarBrands = await carService.GetAllBrandsDistinct(),
                Colors = await carService.GetAllColorsDistinct(),
                DriveTrains = await carService.GetAllDriveTrainsDistinct()
            };
            return View(model);
        }
        [HttpPost]       
        public async Task<IActionResult> Filters(CarWithFilters carWithFilters)
        {
            IEnumerable<Car> queries=new List<Car>();
            if (User.IsInRole("Company"))
            {
                var companyId = HttpContext.Session.GetInt32("CompanyId");
                if (companyId.HasValue)
                {
                    queries = await carService.GetFilteredCarsOfCompanyAsync(companyId.Value,carWithFilters.MinPrice, carWithFilters.MaxPrice, carWithFilters.SelectedClassIds,
                        carWithFilters.SelectedBrands, carWithFilters.SelectedColors, carWithFilters.SelectedDriveTrains);
                }
                else
                {
                    throw new Exception("Please relog in! You made something incorrect");
                }
            }
            else
            {
                queries = await carService.GetFilteredCarsAsync(carWithFilters.MinPrice, carWithFilters.MaxPrice, carWithFilters.SelectedClassIds,
                    carWithFilters.SelectedBrands, carWithFilters.SelectedColors, carWithFilters.SelectedDriveTrains);
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

        [Authorize(Roles = "Company,Admin")]
        public async Task<IActionResult> AddCar()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var admin = User.IsInRole("Admin");

            if (!companyId.HasValue && !admin)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var classOptions = await classOfCarService.GetAll();
            var companies = await carCompanyService.GetAll();
            var features = (await featureService.GetAllSelectedNames()).ToList();
            var types = await typeService.GetAll();
            var viewModel = new AddingCarWithImagesViewModel
            {
                ClassOptions = new SelectList(classOptions, "Id", "Name"),
                Companies = new SelectList(companies, "Id", "Name"),
                Features = features,
                TypeOptions = new SelectList(
            types.Select(t => new
            {
                Id = t.Id,
                DisplayText = $"{t.Name} ({t.SeatCapacity} seats)"
            }),
            "Id", "DisplayText")};
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Company,Admin")]
        public async Task<IActionResult> AddCar(AddingCarWithImagesViewModel viewModel)
        {
            var classes = await classOfCarService.GetAll();
            viewModel.ClassOptions = new SelectList(classes, "Id", "Name");
            var setfeatures = (await featureService.GetAllSelectedNames()).ToList();
            viewModel.Features = setfeatures;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(viewModel.OrderOfImages))
            {
                ModelState.AddModelError("", "Image order information is missing.");
                return View(viewModel);
            }

            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var admin = User.IsInRole("Admin");
            if (!companyId.HasValue && !admin)
            {
                return BadRequest("The user can not have access to this page");
            }

            var orderIndexesStrings = viewModel.OrderOfImages.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var orderIndexes = new List<int>();

            foreach (var indexStr in orderIndexesStrings)
            {
                if (!int.TryParse(indexStr, out int index))
                {
                    ModelState.AddModelError("", "Invalid image order data provided.");
                    return View(viewModel);
                }
                orderIndexes.Add(index);
            }

            if (orderIndexes.Count != viewModel.Images.Count)
            {
                ModelState.AddModelError("", "The number of images does not match the provided order.");
                return View(viewModel);
            }

            var orderedImages = new List<IFormFile>();
            foreach (var idx in orderIndexes)
            {
                if (idx < 0 || idx >= viewModel.Images.Count)
                {
                    ModelState.AddModelError("", "Invalid image index in order data.");
                    return View(viewModel);
                }
                orderedImages.Add(viewModel.Images[idx]);
            }

            var savedImagePaths = new List<string>();
            foreach (var file in orderedImages)
            {
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("", "One of the uploaded files is empty or invalid.");
                    return View(viewModel);
                }

                var filePath = await cloudinaryService.UploadImageAsync(file);
                if (string.IsNullOrEmpty(filePath))
                {
                    ModelState.AddModelError("", "Failed to save one of the images.");
                    return View(viewModel);
                }
                savedImagePaths.Add(filePath);
            }

            IEnumerable<Feature> features = new List<Feature>();
            if (viewModel.SelectedFeatures != null && viewModel.SelectedFeatures.Any())
            {
                features = await featureService.GetAllSelectedFeatures(viewModel.SelectedFeatures);
            }

            try
            {
                var car = new Car
                {
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    Gearbox = viewModel.Gearbox,
                    Year = viewModel.Year,
                    PricePerDay = viewModel.PricePerDay,
                    PricePerWeek = viewModel.PricePerWeek,
                    MileageLimitForDay = viewModel.MileageLimitForDay,
                    MileageLimitForWeek = viewModel.MileageLimitForWeek,
                    AdditionalMileageCharge = viewModel.AdditionalMileageCharge,
                    EngineCapacity = viewModel.EngineCapacity,
                    Color = viewModel.Color,
                    Available = viewModel.Available,
                    Description = viewModel.Description,
                    DriveTrain = viewModel.DriveTrain,
                    HorsePower = viewModel.HorsePower,
                    ZeroToHundred = viewModel.ZeroToHundred,
                    TopSpeed = viewModel.TopSpeed,
                    ClassOfCarId = viewModel.ClassOfCarId,
                    Pending = true,
                    CreatedAt = DateTime.UtcNow,
                    CTypeId=viewModel.TypeId

                };
                if (admin)
                {
                    car.CarCompanyId = viewModel.CompanyId;
                }
                else
                {
                    car.CarCompanyId = companyId.Value;
                }
                await carService.Add(car);
                await carService.Save();

                for (int i = 0; i < savedImagePaths.Count; i++)
                {
                    var carImage = new Image
                    {
                        CarId = car.Id,
                        Url = savedImagePaths[i],
                        Order = i
                    };
                    await imageService.Add(carImage);
                    await imageService.Save();
                }


                if (viewModel.SelectedFeatures != null && viewModel.SelectedFeatures.Any())
                {
                    var featureList = features.ToList();
                    for (int i = 0; i < viewModel.SelectedFeatures.Count; i++)
                    {
                        var carFeature = new CarFeature
                        {
                            CarId = car.Id,
                            FeatureId = featureList[i].Id
                        };
                        await carFeatureService.Add(carFeature);
                        await carFeatureService.Save();
                    }
                }
           

                return RedirectToAction("Index", "Car");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving car. Please try again. " + ex.Message);
                return View(viewModel);
            }


        }
        public async Task<IActionResult> Pendings()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            IEnumerable<Car> cars=new List<Car>();
            if (companyId.HasValue)
            {
                cars = await carService.GetAllPendingCompanyCars(companyId.Value);
            }
            else if(User.IsInRole("Admin"))
            {
                cars=await carService.FindAllPendingCars();
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

            var carImagesViewModel = new CarImagesViewModel()
            {
                CarWithImages = carImages,
            };
            return View(carImagesViewModel);
        }
       
        public async Task<IActionResult> EditPendingCar(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var car = await carService.FindById(id);
            var images=await imageService.GetImagesByCarId(car.Id);
            var classes = await classOfCarService.GetAll();
            var allFeatures = (await featureService.GetAll()).Select(x => x.NameOfFeatures).ToList(); 
            var carFeatures = await carFeatureService.GetByCarIDAllFeatureNames(id); 
            var selectedFeatures = carFeatures.Select(f => f.NameOfFeatures).ToList(); 
            var carTypes= await cTypeService.GetAll();

            var viewModel = new EditCarWithImagesPendingViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Gearbox = car.Gearbox,
                Year = car.Year,
                PricePerDay = car.PricePerDay,
                PricePerWeek = car.PricePerWeek,
                MileageLimitForDay = car.MileageLimitForDay,
                MileageLimitForWeek = car.MileageLimitForWeek,
                AdditionalMileageCharge = car.AdditionalMileageCharge,
                EngineCapacity = car.EngineCapacity,
                Color = car.Color,
                Description = car.Description,
                DriveTrain = car.DriveTrain,
                HorsePower = car.HorsePower,
                ZeroToHundred = car.ZeroToHundred,
                TopSpeed = car.TopSpeed,
                Images = images.Select(img => new ImageViewModel { Id = img.Id, Url = img.Url, Order = img.Order }).ToList(),
                ClassOfCarId = car.ClassOfCarId,
                ClassOptions = new SelectList(classes, "Id", "Name"),
                Features = allFeatures, 
                SelectedFeatures = selectedFeatures,
                TypeId=car.CType.Id,
                TypeOptions=new SelectList(carTypes.Select(t => new
                {
                    Id = t.Id,
                    DisplayText = $"{t.Name} ({t.SeatCapacity} seats)"
                }),
                "Id", 
                "DisplayText" ),
                IsConvertible=car.IsConvertable,
                
            };
            return View(viewModel);
        }
        [HttpPost("Car/EditPendingCar/{id?}")]
        public async Task<IActionResult> EditPendingCar(int id, EditCarWithImagesPendingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var classes = await classOfCarService.GetAll();
                var images = await imageService.GetImagesByCarId(id);
                viewModel.ClassOptions = new SelectList(classes, "Id", "Name");
                viewModel.Features = (await featureService.GetAll()).Select(x => x.NameOfFeatures).ToList();
                viewModel.Images = images.Select(img => new ImageViewModel { Id = img.Id, Url = img.Url, Order = img.Order }).ToList();
                return View(viewModel);
            }


            var car = await carService.FindById(id);
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
            car.IsConvertable = viewModel.IsConvertible;
            car.ClassOfCarId = viewModel.ClassOfCarId;
            car.CTypeId = viewModel.TypeId;
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
            var allFeatures = await featureService.GetAll(); 
            var currentCarFeatures = await carFeatureService.GetByCarIDAllFeatureNames(id);
            var currentFeatureNames = currentCarFeatures.Select(f => f.NameOfFeatures).ToList(); 
            var newFeatureNames = viewModel.SelectedFeatures ?? new List<string>();

            
            var featuresToRemove = currentCarFeatures.Where(f => !newFeatureNames.Contains(f.NameOfFeatures));
            foreach (var feature in featuresToRemove)
            {
                var carFeature = await carFeatureService.FindOne(cf => cf.CarId == car.Id && cf.FeatureId == feature.Id);
                if (carFeature != null)
                {
                    carFeatureService.Delete(carFeature); 
                }
            }
            await carFeatureService.Save(); 

            
            var featuresToAdd = newFeatureNames.Where(n => !currentFeatureNames.Contains(n));
            foreach (var featureName in featuresToAdd)
            {
                var feature = allFeatures.FirstOrDefault(f => f.NameOfFeatures == featureName);
                if (feature != null)
                {
                    var carFeature = new CarFeature
                    {
                        CarId = car.Id,
                        FeatureId = feature.Id
                    };
                    await carFeatureService.Add(carFeature); 
                }
            }
            await carFeatureService.Save(); 
            return RedirectToAction("Index", "Car");


        }
        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await carService.SetCarUnavailable(id);
            return RedirectToAction("Index","Admin");
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