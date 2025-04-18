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
       IClassOfCarService classOfCar;
        IReportService reportService;
        public HomeController(ILogger<HomeController> logger,IReportService reportService, ICarService carService,IClassOfCarService classOfCar)
        {
            _logger = logger;
            this.reportService= reportService;
            this.carService = carService;
            this.classOfCar = classOfCar;
        }

        public async Task<IActionResult> Index()
        {
            var standardId=await classOfCar.GetStandardId();
            var luxuryId = await classOfCar.GetLuxuryId();
            var economyId = await classOfCar.GetEconomyId();
            var businessId = await classOfCar.GetBusinessId();
            var electricId = await classOfCar.GetElectricId();
            var sportId = await classOfCar.GetSportId();
            StartEndDateWithCarsCountViewModel viewModel = new StartEndDateWithCarsCountViewModel()
            {
                StartDay = DateTime.Now.AddDays(1),
                EndDay = DateTime.Now.AddDays(4),
                
                StandardCount =await carService.CountOfCarsWithCategory(standardId),
                LuxuryCount = await carService.CountOfCarsWithCategory(luxuryId),
                EconomyCount=await carService.CountOfCarsWithCategory(economyId),
                BusinessCount=await carService.CountOfCarsWithCategory(businessId),
                ElectricCount=await carService.CountOfCarsWithCategory(electricId),
                SportCount=await carService.CountOfCarsWithCategory(sportId),


                MinPriceStandard=await carService.MinPriceOfCarByCategory(standardId),
                MinPriceLuxury=await carService.MinPriceOfCarByCategory(luxuryId),
                MinPriceEconomy=await carService.MinPriceOfCarByCategory(economyId),
                MinPriceBusiness=await carService.MinPriceOfCarByCategory(businessId),
                MinPriceElectric=await carService.MinPriceOfCarByCategory(electricId),
                MinPriceSport=await carService.MinPriceOfCarByCategory(sportId),
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
            if (model.StartDay == model.EndDay)
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
