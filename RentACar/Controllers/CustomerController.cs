using Microsoft.AspNetCore.Mvc;
using RentACar.Core.IServices;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class CustomerController : Controller
    {
        IReportService reportService;
        public CustomerController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Report()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Report(AddReportViewModel reportViewModel)
        {
            if(ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                if (!userId.HasValue)
                {
                    return RedirectToAction("Register", "Account");
                }
                RentACar.Models.Report report = new RentACar.Models.Report()
                {
                    Title = reportViewModel.Title,
                    Description = reportViewModel.Description,
                    CustomerId=userId.Value,
                    CreateAt = DateTime.Now,
                };
                reportService.Add(report);
                reportService.Save();
                return RedirectToAction("Index", "Home");
            }
            return View(reportViewModel);
        }
    }
}
