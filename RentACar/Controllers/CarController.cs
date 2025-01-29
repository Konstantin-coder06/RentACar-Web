using Microsoft.AspNetCore.Mvc;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class CarController : Controller
    {
        ICarService carService;
        IImageService imageService;
       
        public CarController(ICarService _carService, IImageService _imageService)
        {
            this.carService = _carService;
            this.imageService = _imageService;
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
        public IActionResult Index(string searchBar)
        {
            var queries = carService.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchBar))
            {
                if (queries.Any(x => x.Brand.ToLower().Contains(searchBar.ToLower())))
                {
                    queries = queries.Where(x => x.Brand.ToLower().Contains(searchBar.ToLower()));
                }
                else
                {
                    queries = queries.Where(x => x.Model.ToLower().Contains(searchBar.ToLower()));
                }
            }

            // Принудително зареждане в паметта и разбъркване
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

            return View(viewModel);
        }
        public IActionResult Filters()
        {
            return View();
        }

    }

}

