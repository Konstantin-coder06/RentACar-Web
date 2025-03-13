using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ICarService carService;
       
        IReportService reportService;
        public HomeController(ILogger<HomeController> logger,IReportService reportService, ICarService carService)
        {
            _logger = logger;
            this.reportService= reportService;
            this.carService = carService;
        }

        public IActionResult Index()
        {
            StartEndDateWithCarsCountViewModel viewModel = new StartEndDateWithCarsCountViewModel()
            {
                StartDay = DateTime.Now,
                EndDay = DateTime.Now.AddDays(3),
                StandardCount = carService.CountOfCarsWithCategory(6),
                LuxuryCount = carService.CountOfCarsWithCategory(2),
                EconomyCount=carService.CountOfCarsWithCategory(1),
                BusinessCount=carService.CountOfCarsWithCategory(4),
                ElectricCount=carService.CountOfCarsWithCategory(5),
                SportCount=carService.CountOfCarsWithCategory(3),


                MinPriceStandard=carService.MinPriceOfCarByCategory(6),
                MinPriceLuxury=carService.MinPriceOfCarByCategory(2),
                MinPriceEconomy=carService.MinPriceOfCarByCategory(1),
                MinPriceBusiness=carService.MinPriceOfCarByCategory(4),
                MinPriceElectric=carService.MinPriceOfCarByCategory(5),
                MinPriceSport=carService.MinPriceOfCarByCategory(3),
            };


            return View(viewModel);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        [Authorize(Roles ="User")]
        public IActionResult ContactUs() 
        {      
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(AddReportViewModel reportViewModel)
        {
           
                if (ModelState.IsValid)
                {
                    var userId = HttpContext.Session.GetInt32("UserId");

                    if (!userId.HasValue)
                    {
                        return RedirectToAction("Register", "Account");
                    }
                    Report report = new Report()
                    {
                        Title = reportViewModel.Title,
                        Description = reportViewModel.Description,
                        CustomerId = userId.Value,
                        CreateAt = DateTime.Now,
                    };
                    reportService.Add(report);
                    reportService.Save();
                    return RedirectToAction("Index", "Home");
                }
                return View(reportViewModel);
            }
        
        [HttpPost]
        public IActionResult Index(StartEndDateWithCarsCountViewModel model)
        {
            if (model.StartDay >= model.EndDay)
            {
                ModelState.AddModelError("StartDate","The start date must be earlier than the end date.");
            }
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("StartDate", model.StartDay?.ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("EndDate", model.EndDay?.ToString("yyyy-MM-dd"));

                return Redirect("/Car/Index");
            }
            return View(model);


        }

    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
