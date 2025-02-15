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
        IReservationService reservationService;
        ICustomerService customerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ICarService _carService, IImageService _imageService, IClassOfCarService _classOfCarService,IReservationService reservationService, ICustomerService customerService, IWebHostEnvironment webHostEnvironment)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            this.reservationService = reservationService;
            this.customerService = customerService;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin" )]
        public IActionResult Index()
        {
            var reservationedCars=reservationService.GetAll().ToList();
            var cars=new List<Car>();
            var customers=new List<Customer>();
            var last24Hours = DateTime.Now.AddDays(-1);
            var lastMounth=DateTime.Now.AddDays(-30);
            var resCarsForLast24Hours = reservationService.GetAll().Where(x => x.CreateTime >= last24Hours).ToList();
            var countPending=carService.FindAll(x=>x.Pending==true).Count();
            var resCarsForLastMounth=reservationService.GetAll().Where(x=>x.CreateTime >= lastMounth).ToList();
            double total24Hours = 0;
            double totalMounth = 0;
            int count = 0;
            foreach (var carx in reservationedCars)
            {
                var car = carService.FindOne(x => x.Id == carx.CarId);
                cars.Add(car);
                var customer=customerService.FindOne(x=>x.Id == carx.CustomerId);
                customers.Add(customer);
                
               
            }
            foreach(var x in resCarsForLast24Hours)
            {
                total24Hours += x.TotalPrice;
            }
            foreach(var x in resCarsForLastMounth)
            {
                totalMounth += x.TotalPrice;
                count++;
            }
            RecentReservation recentReservationViewModel = new RecentReservation()
            {
                Cars = cars,
                Customers = customers,
                TotalPriceForLast24Hours = total24Hours,
                TotalPriceForLastMounth = totalMounth,
                Count=count,
                CountPending=countPending,
            };
            
            return View(recentReservationViewModel);
        }


        [Authorize(Roles = "Company,Admin")]
        public IActionResult AddCar()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var admin = User.IsInRole("Admin");
            if (!companyId.HasValue && !admin)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var viewModel = new AddingCarWithImagesViewModel
            {
                ClassOptions = new SelectList(classOfCarService.GetAll().ToList(), "Id", "Name")
            };
            return View(viewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Company,Admin")]
        public async Task<IActionResult> AddCar(AddingCarWithImagesViewModel viewModel)
        {
            viewModel.ClassOptions = new SelectList(classOfCarService.GetAll().ToList(), "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

           
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var admin = User.IsInRole("Admin");
            if (!companyId.HasValue && !admin)
            {
                return RedirectToAction("AccessDenied", "Account");
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
                    CarCompanyId = companyId.Value 
                };

                carService.Add(car);
                carService.Save();

                if (viewModel.Images?.Count > 0)
                {
                    await imageService.ProcessImages(viewModel.Images, car.Id);
                }

                return RedirectToAction("Index", "Car");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving car. Please try again. " + ex.Message);
                return View(viewModel);
            }


        }
    }
}
