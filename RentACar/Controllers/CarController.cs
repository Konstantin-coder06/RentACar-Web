using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarController(ICarService _carService, IImageService _imageService, IClassOfCarService _classOfCarService, IWebHostEnvironment webHostEnvironment)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var cars = carService.GetAll().ToList();


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


        public IActionResult Filters()
        {
            return View();
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

        [HttpPost]
        [Route("Car/Filters")]
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

            if (carWithFilters.SelectedClasses != null && carWithFilters.SelectedClasses.Any())
            {
                queries = queries.Where(x => carWithFilters.SelectedClasses.Contains(x.ClassOfCar.ToString()));
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
            var car = carService.FindOne(x => x.Id == id);
            var carWithImages = new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            };


            return View(carWithImages);
        }
        [HttpGet]
        public IActionResult AddCar()
        {
            var viewModel = new AddingCarWithImagesViewModel
            {
                ClassOptions = new SelectList(classOfCarService.GetAll().ToList(), "Id", "Name")
            };
            return View(viewModel);
        }

        // POST: Car/AddCar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCar(AddingCarWithImagesViewModel viewModel)
        {
            // Re-fetch class options for the dropdown in case the model is invalid
            viewModel.ClassOptions = new SelectList(classOfCarService.GetAll().ToList(), "Id", "Name");
            // Validate view model
            if (!ModelState.IsValid)
            {

                return View(viewModel);
            }

            try
            {
                // Map the viewModel directly to a Car object
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
                    ClassOfCarId = viewModel.ClassOfCarId
                };

                // Save the car object
                carService.Add(car);
                carService.Save();

                // Process images if provided
                if (viewModel.Images?.Count > 0)
                {
                    await ProcessImages(viewModel.Images, car.Id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Error saving car. Please try again.");
                return View(viewModel);
            }
        }

        private async Task ProcessImages(List<IFormFile> images, int carId)
        {
            // Use IWebHostEnvironment to get the web root path dynamically
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Add image entry to the database
                    imageService.Add(new Image
                    {
                        Url = $"/uploads/{uniqueFileName}",
                        CarId = carId
                    });
                }
            }

             imageService.Save(); // Assuming SaveAsync is implemented
        }
    

    /*if (addingCarwithAllClassesViewviewModel.Car == null)
    {
        viewModelState.AddviewModelError("Car", "Car information is required");

        return View(addingCarwithAllClassesViewviewModel);
    }

    var car = addingCarwithAllClassesViewModel.Car;

    if (string.IsNullOrWhiteSpace(car.Color))
    {
        ModelState.AddModelError("Color", "Color is required");
    }
    if (string.IsNullOrWhiteSpace(car.Brand))
    {
        ModelState.AddModelError("Brand", "Brand is required");
    }
    if (string.IsNullOrWhiteSpace(car.Model))
    {
        ModelState.AddModelError("Model", "Model is required");
    }
    if (string.IsNullOrWhiteSpace(car.Gearbox))
    {
        ModelState.AddModelError("Gearbox", "Gearbox is required");
    }
    if (string.IsNullOrWhiteSpace(car.PricePerDay.ToString()))
    {
        ModelState.AddModelError("PricePerDay", "Price per day is required");
    }
    if (ModelState.IsValid)
    {
        carService.Add(car);

        foreach (var image in addingCarwithAllClassesViewModel.Images)
        {
            imageService.Add(image);
        }
        return RedirectToAction("Index");
    }
    var classesOfCar = classOfCarService.GetAll().ToList();
    var model = new AddingCarwithAllClassesViewModel()
    {
        ClassesOfCars = new SelectList(classesOfCar, "Id", "Name")
    };
    addingCarwithAllClassesViewModel.ClassesOfCars = model.ClassesOfCars;
    return View(addingCarwithAllClassesViewModel);*/
}

}


