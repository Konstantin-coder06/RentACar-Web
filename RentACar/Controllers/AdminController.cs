using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class AdminController : Controller
    {
        ICarService carService;
        IImageService imageService;
        IClassOfCarService classOfCarService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ICarService _carService, IImageService _imageService, IClassOfCarService _classOfCarService, IWebHostEnvironment webHostEnvironment)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                    ClassOfCarId = viewModel.ClassOfCarId,
                    CarCompanyId = 1
                };

                // Save the car object
                carService.Add(car);
                carService.Save();

                // Process images if provided
                if (viewModel.Images?.Count > 0)
                {
                    await imageService.ProcessImages(viewModel.Images, car.Id);
                }

                return RedirectToAction("Index","Car");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving car. Please try again." + ex.Message);
                return View(viewModel);
            }
        }

       

         
    }
}
