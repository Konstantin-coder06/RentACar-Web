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
       
        IReportService reportService;
        public HomeController(ILogger<HomeController> logger,IReportService reportService)
        {
            _logger = logger;
            this.reportService= reportService;
        }

        public IActionResult Index()
        {
            
            return View();
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

    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
