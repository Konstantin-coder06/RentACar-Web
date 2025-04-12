using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public CompanyController(ICarService carService,IReservationService reservationService, ICustomerService customerService,ICarCompanyService carCompanyService, IImageService  imageService) 
        {
          this.carService = carService;
            this.reservationService = reservationService;
            this.customerService = customerService;
            this.carCompanyService = carCompanyService;
            this.imageService = imageService;
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

            var countPending = await carService.PendingCarsCount();

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
    }
}
