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

        public async Task<IActionResult> Index()
        {
            StartEndDateWithCarsCountViewModel viewModel = new StartEndDateWithCarsCountViewModel()
            {
                StartDay = DateTime.Now,
                EndDay = DateTime.Now.AddDays(3),
                StandardCount =await carService.CountOfCarsWithCategory(6),
                LuxuryCount = await carService.CountOfCarsWithCategory(2),
                EconomyCount=await carService.CountOfCarsWithCategory(1),
                BusinessCount=await carService.CountOfCarsWithCategory(4),
                ElectricCount=await carService.CountOfCarsWithCategory(5),
                SportCount=await carService.CountOfCarsWithCategory(3),


                MinPriceStandard=await carService.MinPriceOfCarByCategory(6),
                MinPriceLuxury=await carService.MinPriceOfCarByCategory(2),
                MinPriceEconomy=await carService.MinPriceOfCarByCategory(1),
                MinPriceBusiness=await carService.MinPriceOfCarByCategory(4),
                MinPriceElectric=await carService.MinPriceOfCarByCategory(5),
                MinPriceSport=await carService.MinPriceOfCarByCategory(3),
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
        [Authorize(Roles ="User")]
        [HttpPost]
        public async Task<IActionResult> ContactUs(AddReportViewModel reportViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                if (!userId.HasValue)
                {
                    ModelState.AddModelError("", "You cancelled your session or there was update of the site! Please relog in");
                    return View(reportViewModel);
                }
                try
                {
                    Report report = new Report()
                    {
                        Title = reportViewModel.Title,
                        Description = reportViewModel.Description,
                        CustomerId = userId.Value,
                        CreateAt = DateTime.Now,
                    };
                    await reportService.Add(report);
                    await reportService.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving report. Please try again. " + ex.Message);
                    return View(reportViewModel);
                }
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
