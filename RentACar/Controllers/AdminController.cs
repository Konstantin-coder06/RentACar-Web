using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class AdminController : Controller
    {
        ICarService carService;
        IReservationService reservationService;
        ICustomerService customerService;  
        IReportService reportService;
      
      
       
        public AdminController(ICarService _carService, IReservationService reservationService, 
            ICustomerService customerService, IReportService reportServicе)
        {
            this.carService = _carService;
            this.reservationService = reservationService;
            this.customerService = customerService;     
            this.reportService = reportServicе;
        
            
            
        }

        [Authorize(Roles = "Admin" )]
        public async Task<IActionResult> Index()
        {
            var reservationedCars = await reservationService.GetAllByOrderByCreateTime();
            var cars=new List<Car>();
            var customers=new List<CustomerReservationedCarViewModel>();          
            var countPending = await carService.PendingCarsCount();
            var pending = await carService.FindAllPendingCarsForAdmin();
            var reportCount = await reportService.Count();
            var resCarsForLast24Hours = await reservationService.FindAllForLast24Hours();
            var resCarsForLast24After24Hours = await reservationService.FindAllForLast24HoursBefore24Hours();
            var resCarsForLastMounth = await reservationService.FindAllForLastMonth();
            var resCarsForLastMonthBeforeMonth = await reservationService.FindAllForPreviousMonth();
            var resCarsForLastWeek = await reservationService.FindAllForLastWeek();
            var resCarsForLastWeekBeforeWeek = await reservationService.FindAllForWeekBeforeLast();

            
            double total24Hours = await reservationService.TotalPriceForOnePeriodOfTime(resCarsForLast24Hours);
            double totalMounth = await reservationService.TotalPriceForOnePeriodOfTime(resCarsForLastMounth);
            double total24before24hours = await reservationService.TotalPriceForOnePeriodOfTime(resCarsForLast24After24Hours);
            double totalMonthBeforeMonth = await reservationService.TotalPriceForOnePeriodOfTime(resCarsForLastMonthBeforeMonth);
            double totalWeek = await reservationService.TotalPriceForOnePeriodOfTime(resCarsForLastWeek);
            double totalWeekBeforeWeek = await reservationService.TotalPriceForOnePeriodOfTime(resCarsForLastWeekBeforeWeek);
            int count =await reservationService.Count();
            var customerData = await reservationService.GetCustomersWithReservedCars();
            customers = customerData.Select(x => new CustomerReservationedCarViewModel
            {
                Customer = x.customer,
                Brand = x.brand,
                Model = x.model,
                Image = x.image,
            }).ToList();
           
            int percentages24 = await reservationService.PercentagesOfDifferentPeriods(total24Hours, total24before24hours);
            int percentagesWeek = await reservationService.PercentagesOfDifferentPeriods(totalWeek, totalWeekBeforeWeek);
            int percentagesMonth = await reservationService.PercentagesOfDifferentPeriods(totalMounth,totalMonthBeforeMonth);
            

            List<CustomerEmailPhoneViewModel> allCustomersEmails = new List<CustomerEmailPhoneViewModel>();
            var customerDataWithEmailsPhones = await customerService.GetCustomersWithEmailsAndPhoneNumbers();
            allCustomersEmails=customerDataWithEmailsPhones.Select(x=>new CustomerEmailPhoneViewModel
            {
                Customer=x.customer,
                Email=x.email,
                PhoneNumber=x.phoneNumber,
            }).ToList();
          
            RecentReservation recentReservationViewModel = new RecentReservation()
            {
                Cars = cars,
                Customers = customers,
                AllCustomers=allCustomersEmails,
                TotalPriceForLast24Hours = total24Hours,
                TotalPriceForLastMounth = totalMounth,
                TotalPriceForLastWeek = totalWeek,
                TotalPriceForLast24HoursBefore24Hours = total24before24hours,
                TotalPriceForLastWeekBeforeWeek = totalWeekBeforeWeek,
                TotalPriceForLastMounthBeforeMonth = totalMonthBeforeMonth,
                Count = count,
                CountPending = countPending,
                Pending = pending.ToList(),
                ReportCount = reportCount,
                ProcentPerDay = percentages24,
                ProcentPerWeek = percentagesWeek,
                ProcentPerMonth = percentagesMonth,
            };
            
            return View(recentReservationViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllReports()
        {

            var reports = await reportService.GetAllReportsWithCustomersName();
            var reportsView = new List<ReportViewModel>();
            reportsView = reports.Select(x => new ReportViewModel
            {
                Title = x.title,
                Description = x.description,
                Customer = x.customer,
                CreatedAt = x.CreatedAt
            }).ToList();
          
            var reportsViewModel = new ListOfReportsViewModel()
            {
                ReportViewModels = reportsView
            };
            if (reportsViewModel == null)
            {
                return BadRequest();
            }
            return View(reportsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchByCustomerName(string searchbar)
        {
            var reports = await reportService.GetReportsByCustomerName(searchbar);
            var reportsViewModel = new ListOfReportsViewModel
            {
                ReportViewModels = reports.Select(report => new ReportViewModel
                {
                    Title = report.Title,
                    Description = report.Description,
                    CreatedAt = report.CreateAt,
                    Customer = report.Customer 
                }).ToList()
            };
            TempData["ShowClearButton"] = true;
            return View("AllReports", reportsViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FilterByTitle(ListOfReportsViewModel listOfReportsViewModel)
        {
            try
            {

                var allReports = await reportService.GetAll();
                var reports = reportService.GetReportsByTitle(listOfReportsViewModel.TitleFilter);
                var reportsViewModel = new ListOfReportsViewModel
                {
                    ReportViewModels = reports.Select(report => new ReportViewModel
                    {
                        Title = report.Title,
                        Description = report.Description,
                        CreatedAt = report.CreateAt,
                        Customer = report.Customer
                    }).ToList()
                    
                };
                TempData["ShowClearButton"] = true;
                return View("AllReports", reportsViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",  ex.Message);
                return View("AllReports",listOfReportsViewModel);
            };
            
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Filters(ListOfReportsViewModel listOfReportsViewModel)
        {
            List<(string title,string description,Customer customer,DateTime CreatedAt)> reports=new List<(string, string, Customer, DateTime)>();
            if (listOfReportsViewModel.StartTime >listOfReportsViewModel.EndTime)
            {
                ModelState.AddModelError("StartDate", "The start date must be earlier than the end date.");
                return View("Index");
            }
            else
            {
                if (listOfReportsViewModel.StartTime == null && listOfReportsViewModel.EndTime == null)
                {
                    reports = await reportService.GetAllReportsWithCustomersName();

                }
                else if (listOfReportsViewModel.StartTime != null && listOfReportsViewModel.EndTime == null)
                {
                     reports= await reportService.GetAllReportsWithCustomersNameWithStartDate(listOfReportsViewModel.StartTime);

                }
                else if (listOfReportsViewModel.StartTime == null && listOfReportsViewModel.EndTime != null)
                {
                    reports = await reportService.GetAllReportsWithCustomersNameWithEndDate(listOfReportsViewModel.EndTime);

                }
                else
                {
                    reports = await reportService.GetAllReportsWithCustomersNameWithStartEndDate(listOfReportsViewModel.StartTime,listOfReportsViewModel.EndTime);
                }

                var view = new List<ReportViewModel>();

                
                var reportsView = new List<ReportViewModel>();
                reportsView = reports.Select(x => new ReportViewModel
                {
                    Title = x.title,
                    Description = x.description,
                    Customer = x.customer,
                    CreatedAt = x.CreatedAt
                }).ToList();
                TempData["ShowClearButton"] = true;
                var reportsViewModel = new ListOfReportsViewModel()
                {
                    ReportViewModels = reportsView
                };      
                return View("AllReports", reportsViewModel);
            }
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ClearFilters()
        {
            var reports = await reportService.GetAllReportsWithCustomersName();
            var reportsView = new List<ReportViewModel>();
            reportsView = reports.Select(x => new ReportViewModel
            {
                Title = x.title,
                Description = x.description,
                Customer = x.customer,
                CreatedAt = x.CreatedAt
            }).ToList();

            var reportsViewModel = new ListOfReportsViewModel()
            {
                ReportViewModels = reportsView
            };
            TempData["ShowClearButton"] = false;
            if (reportsViewModel == null)
            {
                return BadRequest();
            }
            return RedirectToAction("AllReports",reportsViewModel);
        }
        [Authorize(Roles ="Admin,Company")]
        public async Task<IActionResult> Analytics()
        {      
            var startDate = DateTime.Now.AddDays(-30);       
            bool isCompany = User.IsInRole("Company");
            List<int> companyCarIds = null;
            IEnumerable<Reservation> resLast24Hours;
            IEnumerable<Reservation> resLast24HoursPrev;
            IEnumerable<Reservation> resLastWeek;
            IEnumerable<Reservation> resLastWeekPrev;
            IEnumerable<Reservation> resLastMonth;
            IEnumerable<Reservation> resLastMonthPrev;
            int companyPendingCarsCount = 0;
            if (isCompany)
            {
                var companyId = HttpContext.Session.GetInt32("CompanyId");
                if (!companyId.HasValue) return BadRequest("No company ID found.");

                var companyCars = await carService.GetAllCarsOfCompany(companyId.Value);
                companyCarIds = companyCars.Select(c => c.Id).ToList();

                resLast24Hours = await reservationService.FindAllForLast24HoursCompany(companyCarIds);
                resLast24HoursPrev = await reservationService.FindAllForLast24HoursBefore24HoursCompany(companyCarIds);
                resLastWeek = await reservationService.FindAllForLastWeekCompany(companyCarIds);
                resLastWeekPrev = await reservationService.FindAllForWeekBeforeLastCompany(companyCarIds);
                resLastMonth = await reservationService.FindAllForLastMonthCompany(companyCarIds);
                resLastMonthPrev = await reservationService.FindAllForPreviousMonthCompany(companyCarIds);
              
                companyPendingCarsCount= isCompany? companyCars.Count(x => x.Pending) : 0;
            }
            else if (User.IsInRole("Admin"))
            {            
                resLast24Hours = await reservationService.FindAllForLast24Hours();
                resLast24HoursPrev = await reservationService.FindAllForLast24HoursBefore24Hours();
                resLastWeek = await reservationService.FindAllForLastWeek();
                resLastWeekPrev = await reservationService.FindAllForWeekBeforeLast();
                resLastMonth = await reservationService.FindAllForLastMonth();
                resLastMonthPrev = await reservationService.FindAllForPreviousMonth();
            }
            else
            {
                return BadRequest("User must be Admin or Company.");
            }
            var totalLast24Hours = await reservationService.TotalPriceForOnePeriodOfTime(resLast24Hours);
            var totalLast24HoursPrev = await reservationService.TotalPriceForOnePeriodOfTime(resLast24HoursPrev);
            var totalLastWeek = await reservationService.TotalPriceForOnePeriodOfTime(resLastWeek);
            var totalLastWeekPrev = await reservationService.TotalPriceForOnePeriodOfTime(resLastWeekPrev);
            var totalLastMonth = await reservationService.TotalPriceForOnePeriodOfTime(resLastMonth);
            var totalLastMonthPrev = await reservationService.TotalPriceForOnePeriodOfTime(resLastMonthPrev);          
            var difference24 = await reservationService.DifferenceOfPriceBetweenTwoPeriods(totalLast24Hours,totalLast24HoursPrev);
            var differenceWeek = await reservationService.DifferenceOfPriceBetweenTwoPeriods(totalLastWeek,totalLastWeekPrev);
            var differenceMonth = await reservationService.DifferenceOfPriceBetweenTwoPeriods(totalLastMonth, totalLastMonthPrev);
            var differenceReservation24 = await reservationService.DifferenceOfPriceBetweenTwoPeriods(resLast24Hours.Count(), resLast24HoursPrev.Count());
            var differenceReservationWeek = await reservationService.DifferenceOfPriceBetweenTwoPeriods(resLastWeek.Count(),resLastWeekPrev.Count());
            var differenceReservationMonth = await reservationService.DifferenceOfPriceBetweenTwoPeriods(resLastMonth.Count(), resLastMonthPrev.Count());        
            var allReservations = await reservationService.GetAllIfItIsNotCompany(companyCarIds);     
            var top10CarIds = await reservationService.GetTop10ReservedCarIdsByStartDate(startDate,companyCarIds);
            var top10Cars = await carService.GetTop10ReservedCars(top10CarIds);
            
            var top10 = top10Cars.Select(x => new TopCarsViewModel
            {
                Brand = x.brand,
                Model = x.model,
                Count = x.count,
            }).ToList();
            var approvedCarsCount = await carService.CountAsync(x => x.Pending);       
            var reportCount = await reportService.Count();       
            var analyticsViewModel = new AnalyticsViewModel
            {
                TotalLast24Hours = totalLast24Hours,
                Count24Hours = resLast24Hours.Count(),
                TotalLastWeek = totalLastWeek,
                CountWeek = resLastWeek.Count(),
                TotalLastMonth = totalLastMonth,
                CountMonth = resLastMonth.Count(),
                TotalPriceForLast24HoursBefore24Hours = totalLast24HoursPrev,
                TotalPriceForLastWeekBeforeWeek = totalLastWeekPrev,
                TotalPriceForLastMounthBeforeMonth = totalLastMonthPrev,             
                CountAllReservations = allReservations.Count(),
                Top10Cars = top10,
                PendingsCount = approvedCarsCount,
                CompanyCarPendingCount = companyPendingCarsCount,
                ReportCount = reportCount,
                Difference24 = difference24,
                DifferenceWeek = differenceWeek,
                DifferenceMonth = differenceMonth,
                DifferenceReservation24 = differenceReservation24,
                DifferenceReservationWeek = differenceReservationWeek,
                DifferenceReservationMonth = differenceReservationMonth,
                TotalReservationPreviousDay = resLast24HoursPrev.Count(),
                TotalReservationPreviousWeek = resLastWeekPrev.Count(),
                TotalReservationPreviousMonth = resLastMonthPrev.Count()
            };
           
            return View(analyticsViewModel);
        }
       
       
    }
}
