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
        public CompanyController(ICarService carService,IReservationService reservationService, ICustomerService customerService,ICarCompanyService carCompanyService) 
        {
          this.carService = carService;
            this.reservationService = reservationService;
            this.customerService = customerService;
            this.carCompanyService = carCompanyService;
        }
        [Authorize(Roles ="Company")]
        public IActionResult Index()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var company=carCompanyService.FindOne(x=>x.Id == companyId);
            var cars=carService.FindAll(x=>x.CarCompanyId== companyId).ToList();
            var reservationedCars=new List<Reservation>();
            var reservatedCars = new List<Car>();
            var customers = new List<CustomerReservationedCarViewModel>();
            var last24Hours = DateTime.Now.AddDays(-1);
            var last24After24Hours = DateTime.Now.AddDays(-2);
            var lastMonth = DateTime.Now.AddDays(-30);
            var lastMonthAfterMonth = DateTime.Now.AddDays(-60);
            var lastWeek = DateTime.Now.AddDays(-7);
            var lastWeekBeforeWeek = DateTime.Now.AddDays(-14);
           
            var resCarsForLast24Hours = new List<Reservation>();
            var resCarsForLast24After24Hours = new List<Reservation>();
            var resCarsForLastMounth = new List<Reservation>(); 
            var resCarsForLastMonthBeforeMonth = new List<Reservation>();
            var resCarsForLastWeek = new List<Reservation>();
            var resCarsForLastWeekBeforeWeek = new List<Reservation>();
            foreach (var c in cars)
            {
                var reservations = reservationService.GetAll().Where(x => x.CarId == c.Id).OrderByDescending(x => x.CreateTime).ToList();
                if (reservations.Count > 0)
                {
                    reservatedCars.Add(c);
                }
                var ttresCarsForLast24Hours = reservationService.GetAll().Where(x => x.CreateTime >= last24Hours && x.CarId == c.Id).ToList();
                var ttresCarsForLast24After24Hours = reservationService.FindAll(x => x.CreateTime >= last24After24Hours && x.CreateTime <= last24Hours && x.CarId == c.Id).ToList();

                var ttresCarsForLastMounth = reservationService.GetAll().Where(x => x.CreateTime >= lastMonth && x.CarId == c.Id).ToList();
                var ttresCarsForLastMonthBeforeMonth = reservationService.FindAll(x => x.CreateTime >= lastMonthAfterMonth && x.CreateTime <= lastMonth && x.CarId == c.Id).ToList();

                var ttresCarsForLastWeek = reservationService.GetAll().Where(x => x.CreateTime >= lastWeek && x.CarId == c.Id).ToList();
                var ttresCarsForLastWeekBeforeWeek = reservationService.FindAll(x => x.CreateTime >= lastWeekBeforeWeek && x.CreateTime <= lastWeek && x.CarId == c.Id).ToList();
                reservationedCars.AddRange(reservations);
                resCarsForLast24Hours.AddRange(ttresCarsForLast24Hours);
                resCarsForLast24After24Hours.AddRange(ttresCarsForLast24After24Hours);
                resCarsForLastMounth.AddRange(ttresCarsForLastMounth);
                resCarsForLastMonthBeforeMonth.AddRange(ttresCarsForLastMonthBeforeMonth);
                resCarsForLastWeek.AddRange(ttresCarsForLastWeek);
                resCarsForLastWeekBeforeWeek.AddRange(ttresCarsForLastWeekBeforeWeek);
                foreach (var reservation in reservations)
                {


                    var customer = customerService.FindOne(x => x.Id == reservation.CustomerId);

                    if (customer != null)
                    {
                        customers.Add(new CustomerReservationedCarViewModel
                        {
                            Customer = customer,
                            Brand = c.Brand,
                            Model = c.Model,
                        });
                    }
                }
            }
            var carsCount = reservationedCars.GroupBy(x => x.CarId).Select(g => new { CarId = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList();

            var carsWithCount = carsCount
             .Select(x =>
             {
                 var car = carService.FindOne(cr => cr.Id == x.CarId && cr.CarCompanyId==companyId);
                 return new TopCarsViewModel
                 {
                     Brand = car.Brand,
                     Model = car.Model,
                     CarId = car.Id,
                     Count = x.Count
                 };
             })
             .ToList();
            //var cars = new List<Car>();
          
          

            var countPending = carService.FindAll(x => x.Pending == true && x.CarCompanyId==companyId).Count();

          

          
          
            double total24Hours = 0;
            double totalMounth = 0;
            double total24before24hours = 0;
            double totalMonthBeforeMonth = 0;
            double totalWeek = 0;
            double totalWeekBeforeWeek = 0;
            int count = 0;
            
            foreach (var x in resCarsForLastMonthBeforeMonth)
            {
                totalMonthBeforeMonth += x.TotalPrice;
            }
            foreach (var x in resCarsForLast24After24Hours)
            {
                total24before24hours += x.TotalPrice;
            }
            foreach (var x in resCarsForLast24Hours)
            {
                total24Hours += x.TotalPrice;
            }
            foreach (var x in resCarsForLastMounth)
            {
                totalMounth += x.TotalPrice;
                count++;
            }
            foreach (var x in resCarsForLastWeek)
            {
                totalWeek += x.TotalPrice;
            }
            foreach (var x in resCarsForLastWeekBeforeWeek)
            {
                totalWeekBeforeWeek += x.TotalPrice;
            }
            double difference24 = total24Hours - total24before24hours;

            int percent24 = 0;

            if (total24before24hours != 0)
            {
                percent24 = (int)((difference24 / total24before24hours) * 100);
            }
            else
            {
                if (total24Hours > 0)
                {
                    percent24 = 100;
                }
                else
                {
                    percent24 = 0;
                }
            }
            double differenceWeek = totalWeek - totalWeekBeforeWeek;
            int percentWeek = 0;
            if (totalWeekBeforeWeek != 0)
            {
                percentWeek = (int)((differenceWeek / totalWeekBeforeWeek) * 100);
            }
            else
            {
                if (totalWeek > 0)
                {
                    percentWeek = 100;
                }
                else
                {
                    percentWeek = 0;
                }
            }
            double differenceMonth = totalMounth - totalMonthBeforeMonth;
            int percentMonth = 0;
            if (totalMonthBeforeMonth != 0)
            {
                percentMonth = (int)((differenceMonth / totalMonthBeforeMonth) * 100);
            }
            else
            {
                if (totalMounth > 0)
                {
                    percentMonth = 100;
                }
                else
                {
                    percentMonth = 0;
                }
            }

            CompanyIndexViewModel viewModel = new CompanyIndexViewModel()
            {
                AllCars = carsWithCount,
                CountPending = countPending,
                TotalPriceForLast24Hours = total24Hours,
                TotalPriceForLast24HoursBefore24Hours = total24before24hours,
                TotalPriceForLastMounth = totalMounth,
                TotalPriceForLastMounthBeforeMonth = totalMonthBeforeMonth,
                TotalPriceForLastWeek = totalWeek,
                TotalPriceForLastWeekBeforeWeek = totalWeekBeforeWeek,
                ProcentPerDay = percent24,
                ProcentPerMonth = percentMonth,
                ProcentPerWeek = percentWeek,
                Count = reservationedCars.Count,
                Customers = customers,
                CompanyName = company.Name
            };
            return View(viewModel);
        }
    }
}
