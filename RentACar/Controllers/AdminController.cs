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
        IImageService imageService;
        IClassOfCarService classOfCarService;
        IReservationService reservationService;
        ICustomerService customerService;
        CloudinaryService cloudinaryService;
        IReportService reportService;
        ICarCompanyService carCompanyService;
        
       
        public AdminController(ICarService _carService, IImageService _imageService, 
            IClassOfCarService _classOfCarService,IReservationService reservationService, 
            ICustomerService customerService,CloudinaryService cloudinaryService,IReportService reportServicе,
            ICarCompanyService carCompanyService)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            this.reservationService = reservationService;
            this.customerService = customerService;
            this.cloudinaryService = cloudinaryService;
            this.reportService = reportServicе;
            this.carCompanyService = carCompanyService;
           
            
        }
        [Authorize(Roles = "Admin" )]
        public async Task<IActionResult> Index()
        {
            var reservationedCars = await reservationService.GetAllByOrderByCreateTime();
            var cars=new List<Car>();
            var customers=new List<CustomerReservationedCarViewModel>();
            var customers1 = await customerService.GetAll();
            var countPending = await carService.PendingCarsCount();

            var resCarsForLast24Hours = await reservationService.FindAllForLast24Hours();
            var resCarsForLast24After24Hours = await reservationService.FindAllForLast24HoursBefore24Hours();
            var resCarsForLastMounth = await reservationService.FindAllForLastMonth();
            var resCarsForLastMonthBeforeMonth = await reservationService.FindAllForPreviousMonth();
            var resCarsForLastWeek = await reservationService.FindAllForLastWeek();
            var resCarsForLastWeekBeforeWeek = await reservationService.FindAllForWeekBeforeLast();

            var pending = await carService.FindAllPendingCars();
            var reportCount = await reportService.Count();
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

        public async Task<IActionResult> Search(string searchbar)
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

            return View("AllReports", reportsViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Filters(ListOfReportsViewModel listOfReportsViewModel)
        {
            List<(string title,string description,Customer customer,DateTime CreatedAt)> reports=new List<(string, string, Customer, DateTime)>();
            if (listOfReportsViewModel.StartTime >listOfReportsViewModel.EndTime)
            {
                ModelState.AddModelError("StartDate", "Must be ");
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

                var reportsViewModel = new ListOfReportsViewModel()
                {
                    ReportViewModels = reportsView
                };      
                return View("AllReports", reportsViewModel);
            }
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
        [Authorize(Roles = "Company,Admin")]
        public async Task<IActionResult> AddCar()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var admin = User.IsInRole("Admin");

            if (!companyId.HasValue && !admin)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            var classOptions = await classOfCarService.GetAll();
            var companies = await carCompanyService.GetAll();       
            var viewModel = new AddingCarWithImagesViewModel
            {
                ClassOptions = new SelectList(classOptions, "Id", "Name"),
                Companies = new SelectList(companies, "Id", "Name")
            };
            return View(viewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Company,Admin")]
        public async Task<IActionResult> AddCar(AddingCarWithImagesViewModel viewModel)
        {
            var classes=await classOfCarService.GetAll();
            viewModel.ClassOptions = new SelectList(classes, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (string.IsNullOrWhiteSpace(viewModel.OrderOfImages))
            {
                ModelState.AddModelError("", "Image order information is missing.");
                return View(viewModel);
            }
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            var admin = User.IsInRole("Admin");
            if (!companyId.HasValue && !admin)
            {
                return BadRequest("Maika ti user");
            }
                
                var orderIndexesStrings = viewModel.OrderOfImages.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var orderIndexes = new List<int>();

         
            foreach (var indexStr in orderIndexesStrings)
            {
                if (!int.TryParse(indexStr, out int index))
                {
                    ModelState.AddModelError("", "Invalid image order data provided.");
                    return View(viewModel);
                }
                orderIndexes.Add(index);
            }

           
            if (orderIndexes.Count != viewModel.Images.Count)
            {
                ModelState.AddModelError("", "The number of images does not match the provided order.");
                return View(viewModel);
            }

           
            var orderedImages = new List<IFormFile>();
            foreach (var idx in orderIndexes)
            {
                if (idx < 0 || idx >= viewModel.Images.Count)
                {
                    ModelState.AddModelError("", "Invalid image index in order data.");
                    return View(viewModel);
                }
                orderedImages.Add(viewModel.Images[idx]);
            }

           
            var savedImagePaths = new List<string>();
            foreach (var file in orderedImages)
            {
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("", "One of the uploaded files is empty or invalid.");
                    return View(viewModel);
                }

               
                var filePath = await cloudinaryService.UploadImageAsync(file);
                if (string.IsNullOrEmpty(filePath))
                {
                    ModelState.AddModelError("", "Failed to save one of the images.");
                    return View(viewModel);
                }
                savedImagePaths.Add(filePath);
            }
            try
            {
                var car = new Car
                {
                    Brand = viewModel.Brand,
                    Model = viewModel.Model,
                    Gearbox = viewModel.Gearbox,
                    Year = viewModel.Year,
                    PricePerDay = viewModel.PricePerDay,
                    PricePerWeek = viewModel.PricePerWeek,
                    MileageLimitForDay = viewModel.MileageLimitForDay,
                    MileageLimitForWeek = viewModel.MileageLimitForWeek,
                    AdditionalMileageCharge = viewModel.AdditionalMileageCharge,
                    EngineCapacity = viewModel.EngineCapacity,
                    Color = viewModel.Color,
                    Available = viewModel.Available,
                    Description = viewModel.Description,
                    DriveTrain = viewModel.DriveTrain,
                    HorsePower = viewModel.HorsePower,
                    ZeroToHundred = viewModel.ZeroToHundred,
                    TopSpeed = viewModel.TopSpeed,
                    ClassOfCarId = viewModel.ClassOfCarId,
                    Pending = true,
                    CreatedAt = DateTime.UtcNow
                };
                if (admin)
                {
                    car.CarCompanyId = viewModel.CompanyId;
                }
                else
                {
                    car.CarCompanyId = companyId.Value;
                }
                await carService.Add(car);
                await carService.Save();

                for (int i = 0; i < savedImagePaths.Count; i++)
                {
                    var carImage = new Image
                    {
                        CarId = car.Id,
                        Url = savedImagePaths[i],
                        Order = i 
                    };

                    await imageService.Add(carImage);
                    await imageService.Save();
                }


                return RedirectToAction("Index", "Car");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving car. Please try again. " + ex.Message);
                return View(viewModel);
            }
          

        }
        public IActionResult Settings()
        {
            return View();
        }
    }
}
