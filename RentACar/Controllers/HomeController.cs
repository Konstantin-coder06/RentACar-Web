using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentACar.Core.IServices;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IReservationService reservationService;
        public HomeController(ILogger<HomeController> logger, IReservationService reservationService)
        {
            _logger = logger;
            this.reservationService= reservationService;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(StartEndDateViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("StartDate", model.StartDay?.ToString("yyyy-MM-dd"));
                HttpContext.Session.SetString("EndDate", model.EndDay?.ToString("yyyy-MM-dd"));

                return Redirect("/Car/Index");
            }
            return Redirect("/Home/Index");


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
