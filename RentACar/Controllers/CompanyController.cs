using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;
using RentACar.Utility;
using System.ComponentModel.Design;

namespace RentACar.Controllers
{
    public class CompanyController : Controller
    {
        ICarService carService;
        IReservationService reservationService;
        ICustomerService customerService;
        ICarCompanyService carCompanyService;
        IImageService imageService;
        IClassOfCarService classOfCarService;
        private readonly UserManager<IdentityUser> userManager;
        public CompanyController(ICarService carService,IReservationService reservationService, ICustomerService customerService,ICarCompanyService carCompanyService, IImageService  imageService,IClassOfCarService classOfCarService,UserManager<IdentityUser>userManager) 
        {
          this.carService = carService;
            this.reservationService = reservationService;
            this.customerService = customerService;
            this.carCompanyService = carCompanyService;
            this.imageService = imageService;
            this.classOfCarService = classOfCarService;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Index(string searchTerm = null)
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (!companyId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var company = await carCompanyService.GetById(companyId);
            if (company == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string[] searchTerms = string.IsNullOrWhiteSpace(searchTerm)
                ? Array.Empty<string>()
                : searchTerm.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var allCompanyCars = await carService.GetAllCarsOfCompany(companyId.Value);
            List<int> companyCarIds = allCompanyCars.Select(c => c.Id).ToList();

            var allReservations = await reservationService.GetAllReservationsContaingCompanyIds(companyCarIds);
            var reservationedCars = allReservations.ToList();
           
            var customers = new List<CustomerReservationedCarViewModel>();
            var filteredCars = searchTerms.Any() ? await carService.GetCarsOfCompanyBySearchBrandAndModel(companyId.Value, searchTerms) : allCompanyCars;

            foreach (var reservation in reservationedCars.TakeLast(4))
            {
                var customer = await customerService.FindById(reservation.CustomerId);
                var car = await carService.FindById(reservation.CarId);
                var image = await imageService.ImageByCarId(reservation.CarId);
                if (customer != null && car != null)
                {
                    customers.Add(new CustomerReservationedCarViewModel
                    {
                        Customer = customer,
                        Brand = car.Brand,
                        Model = car.Model,
                        Image = image,
                    });
                }
            }

            var resLast24Hours = await reservationService.FindAllForLast24HoursCompany(companyCarIds);
            var resLast24HoursPrev = await reservationService.FindAllForLast24HoursBefore24HoursCompany(companyCarIds);
            var resLastMonth = await reservationService.FindAllForLastMonthCompany(companyCarIds);
            var resLastMonthPrev = await reservationService.FindAllForPreviousMonthCompany(companyCarIds);
            var resLastWeekPrev = await reservationService.FindAllForLastWeekCompany(companyCarIds);
            var resLastWeek = await reservationService.FindAllForWeekBeforeLastCompany(companyCarIds);

            var carsCount = reservationService.GetCarReservationCounts(reservationedCars);
            var carsWithCount = new List<TopCarsViewModel>();
            foreach (var x in carsCount)
            {
                var car = allCompanyCars.FirstOrDefault(c => c.Id == x.CarId);
                if (car != null)
                {
                    carsWithCount.Add(new TopCarsViewModel
                    {
                        Brand = car.Brand,
                        Model = car.Model,
                        CarId = car.Id,
                        Count = x.Count
                    });
                }
            }

            var countPending = await carService.PendingCompanyCarsCount(companyId.Value);

            var totalLast24Hours = await reservationService.TotalPriceForOnePeriodOfTime(resLast24Hours);
            var totalLast24HoursPrev = await reservationService.TotalPriceForOnePeriodOfTime(resLast24HoursPrev);
            var totalLastWeek = await reservationService.TotalPriceForOnePeriodOfTime(resLastWeek);
            var totalLastWeekPrev = await reservationService.TotalPriceForOnePeriodOfTime(resLastWeekPrev);
            var totalLastMonth = await reservationService.TotalPriceForOnePeriodOfTime(resLastMonth);
            var totalLastMonthPrev = await reservationService.TotalPriceForOnePeriodOfTime(resLastMonthPrev);
            var difference24 = await reservationService.DifferenceOfPriceBetweenTwoPeriods(totalLast24Hours, totalLast24HoursPrev);
            var differenceWeek = await reservationService.DifferenceOfPriceBetweenTwoPeriods(totalLastWeek, totalLastWeekPrev);
            var differenceMonth = await reservationService.DifferenceOfPriceBetweenTwoPeriods(totalLastMonth, totalLastMonthPrev);
            var differenceReservation24 = await reservationService.DifferenceOfPriceBetweenTwoPeriods(resLast24Hours.Count(), resLast24HoursPrev.Count());
            var differenceReservationWeek = await reservationService.DifferenceOfPriceBetweenTwoPeriods(resLastWeek.Count(), resLastWeekPrev.Count());
            var differenceReservationMonth = await reservationService.DifferenceOfPriceBetweenTwoPeriods(resLastMonth.Count(), resLastMonthPrev.Count());
            int percentages24 = await reservationService.PercentagesOfDifferentPeriods(totalLast24Hours, totalLast24HoursPrev);
            int percentagesWeek = await reservationService.PercentagesOfDifferentPeriods(totalLastWeek, totalLastWeekPrev);
            int percentagesMonth = await reservationService.PercentagesOfDifferentPeriods(totalLastMonth, totalLastMonthPrev);

            CompanyIndexViewModel viewModel = new CompanyIndexViewModel()
            {
                AllCars = carsWithCount,
                CountPending = countPending,
                TotalPriceForLast24Hours = totalLast24Hours,
                TotalPriceForLast24HoursBefore24Hours = totalLast24HoursPrev,
                TotalPriceForLastMounth = totalLastMonth,
                TotalPriceForLastMounthBeforeMonth = totalLastMonthPrev,
                TotalPriceForLastWeek = totalLastWeek,
                TotalPriceForLastWeekBeforeWeek = totalLastWeekPrev,
                ProcentPerDay = percentages24,
                ProcentPerMonth = percentagesMonth,
                ProcentPerWeek = percentagesWeek,
                Count = reservationedCars.Count,
                Customers = customers,
                CompanyName = company.Name,
                FilteredCars = filteredCars.Select(car =>
                {
                    var carCount = carsCount.FirstOrDefault(c => c.CarId == car.Id);
                    return new TopCarsViewModel
                    {
                        Brand = car.Brand,
                        Model = car.Model,
                        CarId = car.Id,
                        Count = carCount != default ? carCount.Count : 0
                    };
                }).ToList()
            };

            ViewData["SearchTerm"] = searchTerm;
            return View(viewModel);
        }
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> ViewReservations(int id)
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (!companyId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var companyCars = await carService.GetAllCarsIdByCompanyId(companyId.Value);
            var reservations = await reservationService.GetAllReservationOfCompanyCars(companyCars);

            var statusFilter = TempData["StatusFilter"]?.ToString() ?? "All Reservations";
            if (statusFilter != "All Reservations")
            {
                var reservationStatuses = new List<(Reservation Reservation, string Status)>();
                foreach (var reservation in reservations)
                {
                    string status = await reservationService.GetStatusOfReservation(reservation);
                    reservationStatuses.Add((reservation, status));
                }

                reservations = await reservationService.GetAllReservationByStatus(reservationStatuses,statusFilter);

                if (!reservations.Any() && statusFilter != "Unknown")
                {
                    var unexpectedStatuses = reservationStatuses.Select(rs => rs.Status).Distinct();
                    Console.WriteLine($"No reservations found for filter '{statusFilter}'. Available statuses: {string.Join(", ", unexpectedStatuses)}");
                }
            }

            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };

            TempData["StatusFilter"] = statusFilter;

            return View(userViewModel);
        }
        [HttpPost]
        public  IActionResult Filter(int id, string statusFilter)
        {
            var company = HttpContext.Session.GetInt32("CompanyId");
            if (!company.HasValue)
            {
                return RedirectToAction("Register", "Account");
            }    
            TempData["StatusFilter"] = statusFilter;
            return RedirectToAction("ViewReservations", new { id });
        }
        private async Task<List<ReservationWithCarInfViewModel>> BuildReservationViewModels(IEnumerable<Reservation> reservations)
        {
            var reservationsWithCarInf = new List<ReservationWithCarInfViewModel>();
            foreach (var x in reservations)
            {
                string status = await reservationService.GetStatusOfReservation(x);
                var car = await carService.FindById(x.CarId);
                var classOfCar = await classOfCarService.GetClassOfCarById(car.ClassOfCarId);
             var customer=await customerService.FindById(x.CustomerId);

                var image = await imageService.ImageByCarId(car.Id);
                var carCompany = await carCompanyService.GetById(car.CarCompanyId);
                if (x.PaidDeliveryPlace == null)
                {
                    x.PaidDeliveryPlace = carCompany.Address;
                }

                var model = new ReservationWithCarInfViewModel
                {
                    ReservationId = x.Id,
                    Status = status,
                    CarBrand = car.Brand,
                    CarModel = car.Model,
                    CarClass = classOfCar.Name,
                    CarImageHref = image.Url,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Duration = reservationService.CalculateTotalDays(x),
                    PickUpAddress = x.PaidDeliveryPlace,
                    TotalPrice = x.TotalPrice,
                    CreateTime = x.CreateTime,
                    CreatedBy=customer.Name
                };
                reservationsWithCarInf.Add(model);
            }
            return reservationsWithCarInf;
        }
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Settings()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (!companyId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }
            var company=await carCompanyService.GetById(companyId);
            CompanyEditInfoViewModel companyEditInfoViewModel = new CompanyEditInfoViewModel()
            {
                CompanyName = company.Name,
                CompanyAddress = company.Address,
                CompanyCity = company.City,
                CompanyCountry = company.Country,
                CompanyDescription = company.Description,
                CompanyEmail = await carCompanyService.GetCompanyEmail(companyId.Value),
               
            };

            return View(companyEditInfoViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult>EditCompanyInfo(CompanyEditInfoViewModel viewModel)
        {
            try{             
                var companyId = HttpContext.Session.GetInt32("CompanyId");
                if (!companyId.HasValue)
                {
                    ModelState.AddModelError("", "Session expired. Please log in again.");
                    return View("Settings", viewModel);
                }
                var company = await carCompanyService.GetById(companyId.Value);
                if (company == null)
                {
                    ModelState.AddModelError("", "Company not found. Please log in again.");
                    return View("Settings", viewModel);
                }
                if (ModelState.IsValid)
                {              
                    company.Name = viewModel.CompanyName;
                    company.Address = viewModel.CompanyAddress;
                    company.City = viewModel.CompanyCity;
                    company.Country = viewModel.CompanyCountry;
                    company.Description = viewModel.CompanyDescription;                
                    carCompanyService.Update(company);
                    await carCompanyService.Save();         
                    var user = await userManager.FindByIdAsync(company.UserId);
                    if (user != null)
                    {
                        user.Email = viewModel.CompanyEmail;
                        var result = await userManager.UpdateAsync(user);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return View("Settings", viewModel);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Associated user not found.");
                        return View("Settings", viewModel);
                    }
                    return RedirectToAction("Index");
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving changes: {ex.Message}");
                return View("Settings", viewModel);
            }
            return View("Settings", viewModel);
        }
    }
}
