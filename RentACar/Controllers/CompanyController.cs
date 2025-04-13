using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;
using RentACar.Utility;

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
        [Authorize(Roles ="Company")]
        public async Task<IActionResult> Index()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var company = await carCompanyService.GetById(companyId);
            var companyCars = await carService.GetAllCarsOfCompany(companyId.Value);
            List<int> companyCarIds = companyCars.Select(c => c.Id).ToList();

           
            var allReservations = await reservationService.GetAllReservationsContaingCompanyIds(companyCarIds);
            var reservationedCars = allReservations.ToList();
            var reservatedCars = await reservationService.GetAllReservationsForCompany(companyCars);
            var customers = new List<CustomerReservationedCarViewModel>();

          
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
                var car = await carService.FindById(x.CarId);
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
                CompanyName = company.Name
            };
            return View(viewModel);
           
        }
        public async Task<IActionResult> ViewReservations(int id)
        {
            var company = HttpContext.Session.GetInt32("CompanyId");//await carCompanyService.GetById(id);
            var companyCars = await carService.GetAllCarsIdByCompanyId(company.Value);
            var reservations = await reservationService.GetAllReservationOfCompanyCars(companyCars);
            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };
            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Filter(int id, string statusFilter)
        {
            var company = HttpContext.Session.GetInt32("CompanyId");
            if (!company.HasValue)
            {
                return RedirectToAction("Register", "Account");
            }

            var companyCars = await carService.GetAllCarsIdByCompanyId(company.Value);
            var reservations = await reservationService.GetAllReservationOfCompanyCars(companyCars);

            if (statusFilter != "All Reservations")
            {
                var reservationStatuses = new List<(Reservation Reservation, string Status)>();
                foreach (var reservation in reservations)
                {
                    string status = await reservationService.GetStatusOfReservation(reservation);
                    reservationStatuses.Add((reservation, status));
                }

                reservations = reservationStatuses
                    .Where(rs => rs.Status == statusFilter)
                    .Select(rs => rs.Reservation)
                    .ToList();

              
                if (!reservations.Any() && statusFilter != "Unknown")
                {
                    var unexpectedStatuses = reservationStatuses.Select(rs => rs.Status).Distinct();
                    Console.WriteLine($"No reservations found for filter '{statusFilter}'. Available statuses: {string.Join(", ", unexpectedStatuses)}");
                }
            }

            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };

            TempData["StatusFilter"] = statusFilter;

            return View("ViewReservations", userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterDate(int id, DateTime? startDate, DateTime? endDate)
        {
            var company = HttpContext.Session.GetInt32("CompanyId");
            var companyCars = await carService.GetAllCarsIdByCompanyId(company.Value);
            var reservations = await reservationService.GetAllReservationOfCompanyCars(companyCars);

            if (startDate.HasValue)
            {
                reservations = reservations.Where(r => r.StartDate >= startDate.Value).ToList();
            }
            if (endDate.HasValue)
            {
                reservations = reservations.Where(r => r.EndDate <= endDate.Value).ToList();
            }

            var reservationsWithCarInf = await BuildReservationViewModels(reservations);
            var userViewModel = new UserViewModel { ReservationCar = reservationsWithCarInf };

            TempData["StartDateFilter"] = startDate?.ToString("yyyy-MM-dd");
            TempData["EndDateFilter"] = endDate?.ToString("yyyy-MM-dd");

            return View("ViewReservations", userViewModel);
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
                    Duration = (int)(x.EndDate - x.StartDate).TotalDays,
                    PickUpAddress = x.PaidDeliveryPlace,
                    TotalPrice = x.TotalPrice,
                    CreateTime = x.CreateTime,
                    CreatedBy=customer.Name
                };
                reservationsWithCarInf.Add(model);
            }
            return reservationsWithCarInf;
        }
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
        public async Task<IActionResult>EditCompanyInfo(CompanyEditInfoViewModel viewModel)
        {
            try
            {
                // Get company ID from session or another source
                var companyId = HttpContext.Session.GetInt32("CompanyId");
                if (!companyId.HasValue)
                {
                    ModelState.AddModelError("", "Session expired. Please log in again.");
                    return View("Settings", viewModel);
                }

                // Fetch the company
                var company = await carCompanyService.GetById(companyId.Value);
                if (company == null)
                {
                    ModelState.AddModelError("", "Company not found. Please log in again.");
                    return View("Settings", viewModel);
                }

                if (ModelState.IsValid)
                {
                    // Update company details
                    company.Name = viewModel.CompanyName;
                    company.Address = viewModel.CompanyAddress;
                    company.City = viewModel.CompanyCity;
                    company.Country = viewModel.CompanyCountry;
                    company.Description = viewModel.CompanyDescription;

                    // Save company changes
                    carCompanyService.Update(company);
                    await carCompanyService.Save();

                    // Update the email in IdentityUser
                    var user = await userManager.FindByIdAsync(company.UserId); // Assuming UserId links to IdentityUser
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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving changes: {ex.Message}");
                return View("Settings", viewModel);
            }

            return View("Settings", viewModel);
        }
    }
}
