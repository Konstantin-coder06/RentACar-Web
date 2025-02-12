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
        [HttpPost]
        public IActionResult Index(StartEndDateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ��������� �� ����� � TempData
                TempData["StartDay"] = model.StartDay;
                TempData["EndDay"] = model.EndDay;

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
