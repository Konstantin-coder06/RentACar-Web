using Microsoft.AspNetCore.Mvc;

namespace RentACar.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
