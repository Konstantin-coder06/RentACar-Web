using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
            var car = carService.FindOne(x => x.Id == id);
            var carWithImages = new CarWithImages
            {
                Car = car,
                Images = imageService.GetImagesByCarId(car.Id).ToList()
            };


            return View(carWithImages);
        }
       
      

    }
}