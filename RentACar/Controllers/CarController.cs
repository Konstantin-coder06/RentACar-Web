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
    }

}

