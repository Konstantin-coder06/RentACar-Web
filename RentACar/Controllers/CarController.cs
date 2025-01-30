using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public CarController(ICarService _carService, IImageService _imageService, IClassOfCarService _classOfCarService)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
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
                if (queries.Any(x =>searchBar.Contains(x.Brand)))
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
            var car = carService.FindOne(x=>x.Id == id);
            var carWithImages =  new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            };

            
            return View(carWithImages);
        }
        public IActionResult AddCar()
        {
            var classesOfCar=classOfCarService.GetAll().ToList();
            var model = new AddingCarwithAllClassesViewModel()
            {
                ClassesOfCars = new SelectList(classesOfCar, "Id", "Name")
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddCar(Car car) 
        {
           
           return RedirectToAction("Index");
        }

    }
}

