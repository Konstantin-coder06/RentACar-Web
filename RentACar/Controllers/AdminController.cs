﻿using CloudinaryDotNet.Actions;
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
        private readonly UserManager<IdentityUser> _userManager;
       
        public AdminController(ICarService _carService, IImageService _imageService, 
            IClassOfCarService _classOfCarService,IReservationService reservationService, 
            ICustomerService customerService,CloudinaryService cloudinaryService,IReportService reportService,
            IWebHostEnvironment webHostEnvironment, ICarCompanyService carCompanyService, UserManager<IdentityUser> userManager)
        {
            this.carService = _carService;
            this.imageService = _imageService;
            this.classOfCarService = _classOfCarService;
            this.reservationService = reservationService;
            this.customerService = customerService;
            this.cloudinaryService = cloudinaryService;
            this.reportService = reportService;
            this.carCompanyService = carCompanyService;
            _userManager = userManager;
            
        }
        [Authorize(Roles = "Admin" )]
        public async Task<IActionResult> Index()
        {
            var reservationedCars = await reservationService.GetAll();
            reservationedCars=  reservationedCars.OrderBy(x => x.CreateTime).ToList();
            var cars=new List<Car>();
            var customers=new List<CustomerReservationedCarViewModel>();
            var last24Hours = DateTime.Now.AddDays(-1);
            var last24After24Hours=DateTime.Now.AddDays(-2);
            var lastMonth=DateTime.Now.AddDays(-30);
            var lastMonthAfterMonth = DateTime.Now.AddDays(-60);
            var lastWeek = DateTime.Now.AddDays(-7);
            var lastWeekBeforeWeek = DateTime.Now.AddDays(-14);
            var customers1 = await customerService.GetAll();
            customers1=customers1.ToList();
            var resCarsForLast24Hours =await reservationService.GetAll();
            resCarsForLast24Hours=resCarsForLast24Hours.Where(x => x.CreateTime >= last24Hours).ToList();
            var resCarsForLast24After24Hours = await reservationService.FindAll(x => x.CreateTime >= last24After24Hours && x.CreateTime <= last24Hours);
            resCarsForLast24After24Hours=resCarsForLast24After24Hours.ToList();

            var countPendin = await carService.FindAll(x => x.Pending == true);
            int countPending=countPendin.Count();

            var resCarsForLastMounth = await reservationService.GetAll();
            resCarsForLastMounth=resCarsForLastMounth.Where(x=>x.CreateTime >= lastMonth).ToList();
            var resCarsForLastMonthBeforeMonth =await reservationService.FindAll(x => x.CreateTime >= lastMonthAfterMonth && x.CreateTime <= lastMonth);
            resCarsForLastMonthBeforeMonth=resCarsForLastMonthBeforeMonth.ToList();

            var resCarsForLastWeek =await reservationService.GetAll();
            resCarsForLastWeek=resCarsForLastWeek.Where(x => x.CreateTime >= lastWeek).ToList();
            var resCarsForLastWeekBeforeWeek = await reservationService.FindAll(x => x.CreateTime >= lastWeekBeforeWeek && x.CreateTime <= lastWeek);
            resCarsForLastWeekBeforeWeek=resCarsForLastWeekBeforeWeek.ToList();

            var pendin = await carService.FindAll(x => x.Pending == true);
            var pending=pendin.Take(8).ToList();
            var reportC = await reportService.GetAll();
            int reportCount=reportC.Count();
            double total24Hours = 0;
            double totalMounth = 0;
            double total24before24hours = 0;
            double totalMonthBeforeMonth = 0;
            double totalWeek = 0;
            double totalWeekBeforeWeek = 0;
            int count = 0;
            foreach (var carx in reservationedCars)
            {
               


                    var customer = await customerService.FindOne(x => x.Id == carx.CustomerId);
                var car = await carService.FindOne(x => x.Id == carx.CarId);
                    var image =await imageService.ImageByCarId(car.Id);
                    if (customer != null)
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
            foreach(var x in resCarsForLastMonthBeforeMonth)
            {
                totalMonthBeforeMonth += x.TotalPrice;
            }
            foreach(var x in resCarsForLast24After24Hours)
            {
                total24before24hours += x.TotalPrice;
            }
            foreach(var x in resCarsForLast24Hours)
            {
                total24Hours += x.TotalPrice;
            }
            foreach(var x in resCarsForLastMounth)
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
            double difference24 =total24Hours - total24before24hours;
           
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
                percentWeek = (int)((differenceWeek /totalWeekBeforeWeek) * 100);
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
            double differenceMonth = totalMounth -totalMonthBeforeMonth;
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

            List<CustomerEmailPhoneViewModel> allCustomersEmails = new List<CustomerEmailPhoneViewModel>();
            foreach (var x in customers1)
            {
                var identityUser = await _userManager.FindByIdAsync(x.UserId);

                CustomerEmailPhoneViewModel customerEmailPhoneViewModel = new CustomerEmailPhoneViewModel()
                {
                    Customer = x,
                    Email = identityUser?.Email,
                    PhoneNumber = identityUser?.PhoneNumber
                };
                if (string.IsNullOrEmpty(customerEmailPhoneViewModel.PhoneNumber))
                {
                    customerEmailPhoneViewModel.PhoneNumber = "-";
                }
                 allCustomersEmails.Add(customerEmailPhoneViewModel);
            }
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
                Pending = pending,
                ReportCount = reportCount,
                ProcentPerDay = percent24,
                ProcentPerWeek = percentWeek,
                ProcentPerMonth = percentMonth,
            };
            
            return View(recentReservationViewModel);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllReports()
        {
            var reports = await reportService.GetAll();
            reports=reports.ToList();
            var view = new List<ReportViewModel>();
            foreach (var report in reports)
            {
                var customer = await customerService.FindOne(x => x.Id == report.CustomerId);
                view.Add(new ReportViewModel
                {
                    Title = report.Title,
                    Description = report.Description,
                    Customer = customer,
                    CreatedAt = report.CreateAt
                });
            }
            var reportsViewModel = new ListOfReportsViewModel()
            {
                ReportViewModels = view.OrderBy(x => x.CreatedAt).ToList(),
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
            var customers = await customerService.FindAll(x => x.Name.ToUpper().Contains(searchbar.ToUpper()));

            var reportsViewModel = new ListOfReportsViewModel();
            var view = new List<ReportViewModel>();

            foreach (var x in customers)
            {
                var reports = await reportService.GetReportFromUser(x.Id);

                view.AddRange(reports.Select(report => new ReportViewModel
                {
                    Title = report.Title,
                    Description = report.Description,
                    CreatedAt = report.CreateAt,
                    Customer = x,
                }));
                reportsViewModel.ReportViewModels = view;
            }
            return View("AllReports", reportsViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Filters(ListOfReportsViewModel listOfReportsViewModel)
        {
            List<Report>reports;
            if (listOfReportsViewModel.StartTime >listOfReportsViewModel.EndTime)
            {
                ModelState.AddModelError("StartDate", "Must be ");
                return View("Index");
            }
            else
            {
                if (listOfReportsViewModel.StartTime == null && listOfReportsViewModel.EndTime == null)
                {
                    reports = (await reportService.GetAll()).ToList();
                    
                }
                else if (listOfReportsViewModel.StartTime != null && listOfReportsViewModel.EndTime == null)
                {
                    var reportsss = await reportService.FindAll(x => x.CreateAt >= listOfReportsViewModel.StartTime);
                    reports=reportsss.ToList();
                }
                else if (listOfReportsViewModel.StartTime == null && listOfReportsViewModel.EndTime != null)
                {
                    var reportssss =await reportService.FindAll(x => x.CreateAt <= listOfReportsViewModel.EndTime);
                    reports=reportssss.ToList();
                }
                else
                {
                    var reportss = await reportService.FindAll(x => x.CreateAt >= listOfReportsViewModel.StartTime && x.CreateAt <= listOfReportsViewModel.EndTime);
                    reports=reportss.ToList();
                }

                var view = new List<ReportViewModel>();

              
                foreach (var report in reports)
                {
                    var customer = await customerService.FindOne(x => x.Id == report.CustomerId);

                    view.Add(new ReportViewModel
                    {
                        Title = report.Title,
                        Description = report.Description,
                        Customer = customer,
                        CreatedAt = report.CreateAt
                    });
                }

               
                var reportsViewModel = new ListOfReportsViewModel
                {
                    ReportViewModels = view.OrderBy(x => x.CreatedAt).ToList()
                };
                return View("AllReports", reportsViewModel);
            }
        }
        [Authorize(Roles ="Admin,Company")]
        public async Task<IActionResult> Analytics()
        {//total revenue +, reservations per day/week/month +, average rental duration +,
         //popular cars +, peak rental reservation +, cancelation of reservations !!!-
            var last24Hours = DateTime.Now.AddDays(-1);
            var lastMonth = DateTime.Now.AddDays(-30);
            var lastWeek = DateTime.Now.AddDays(-7);
            var last24HoursBefore24Hours = DateTime.Now.AddDays(-2);
            var lastMonthBeforeMonth = DateTime.Now.AddDays(-60);
            var lastWeekBeforeWeek = DateTime.Now.AddDays(-14);
            var resCarsForLast24Hours = new List<Reservation>();
            var resCarsForLast24After24Hours = new List<Reservation>();
            var countLast24Hours = 0;
            var resCarsForLastMonth = new List<Reservation>();
            var resCarsForLastMonthBeforeMonth = new List<Reservation>();
            var countLastMonth = 0;
            var resCarsForLastWeek =new List<Reservation>();
            var resCarsForLastWeekBeforeWeek = new List<Reservation>();
            var countLastWeek = 0;
            double totalFor24Hours = 0;
            double totalMonth = 0;
            double totalWeek = 0;
            double total24before24hours = 0;
            double totalMonthBeforeMonth = 0;
            double totalWeekBeforeWeek = 0;
            var top10 = new List<TopCarsViewModel>();
            var reservations = new List<Reservation>();
           
            var reportCount = (await reportService.GetAll()).Count();
            double avg = 0;
            int pendingCount = 0;
            if (User.IsInRole("Admin"))
            {


                resCarsForLast24Hours = (await reservationService.GetAll()).Where(x => x.CreateTime >= last24Hours).ToList();
                resCarsForLast24After24Hours = (await reservationService.FindAll(x => x.CreateTime >= last24HoursBefore24Hours && x.CreateTime <= last24Hours)).ToList();

                resCarsForLastMonth = (await reservationService.GetAll()).Where(x => x.CreateTime >= lastMonth).ToList();
                resCarsForLastMonthBeforeMonth = (await reservationService.FindAll(x => x.CreateTime >= lastMonthBeforeMonth && x.CreateTime <= lastMonth)).ToList();


                resCarsForLastWeek = (await reservationService.FindAll(x => x.CreateTime >= lastWeek)).ToList();
                resCarsForLastWeekBeforeWeek = (await reservationService.FindAll(x => x.CreateTime >= lastWeekBeforeWeek && x.CreateTime <= lastWeek)).ToList();
                reservations = (await reservationService.GetAll()).ToList();
                var avgDuration = reservations.Select(x => (x.EndDate - x.StartDate).Days).Where(days => days > 0).ToList();

                if (avgDuration.Any())
                {
                    avg = avgDuration.Average();
                }
                foreach (var x in resCarsForLastMonthBeforeMonth)
                {
                    totalMonthBeforeMonth += x.TotalPrice;
                }
                foreach (var x in resCarsForLast24After24Hours)
                {
                    total24before24hours += x.TotalPrice;
                }
                foreach (var x in resCarsForLastWeekBeforeWeek)
                {
                    totalWeekBeforeWeek += x.TotalPrice;
                }
                foreach (var x in resCarsForLast24Hours)
                {
                    totalFor24Hours += x.TotalPrice;
                    countLast24Hours++;
                }
                foreach (var x in resCarsForLastWeek)
                {
                    totalWeek += x.TotalPrice;
                    countLastWeek++;
                }
                foreach (var x in resCarsForLastMonth)
                {
                    totalMonth += x.TotalPrice;
                    countLastMonth++;
                }
                int days = 30;
                var startDate = DateTime.Now.AddDays(-days);

                var reservationsApi = (await reservationService.GetAll())
                    .Where(x => x.CreateTime >= startDate)
                    .ToList();

                var top10Cars = reservationsApi
                    .GroupBy(x => x.CarId)
                    .Select(g => new { CarId = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(10)
                    .ToList();

                top10 = new List<TopCarsViewModel>();
                foreach (var x in top10Cars)
                {
                    var car = await carService.FindOne(cr => cr.Id == x.CarId);
                    top10.Add(new TopCarsViewModel
                    {
                        Brand = car.Brand,
                        Model = car.Model,
                        Count = x.Count
                    });
                }

                var peakReservations = reservationsApi
                    .GroupBy(x => x.CreateTime.Date)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .FirstOrDefault();

                if (User.IsInRole("Company"))
                {
                    var companyId = HttpContext.Session.GetInt32("CompanyId");
                    if (companyId.HasValue)
                    {
                        var cars = await carService.FindAll(x => x.CarCompanyId == companyId);
                        pendingCount = cars.Count(x => x.Pending);

                        foreach (var c in cars)
                        {
                            var last24 = (await reservationService.GetAll())
                                .Where(x => x.CreateTime >= last24Hours && x.CarId == c.Id)
                                .ToList();
                            resCarsForLast24Hours.AddRange(last24);

                            var lastLast24 = (await reservationService.FindAll(x => x.CreateTime >= last24HoursBefore24Hours && x.CreateTime <= last24Hours && x.CarId == c.Id)).ToList();
                            resCarsForLast24After24Hours.AddRange(lastLast24);

                            var lastMonthCompany = (await reservationService.GetAll())
                                .Where(x => x.CreateTime >= lastMonth && x.CarId == c.Id)
                                .ToList();
                            resCarsForLastMonth.AddRange(lastMonthCompany);

                            var lastLastMonthCompany = (await reservationService.FindAll(x => x.CreateTime >= lastMonthBeforeMonth && x.CreateTime <= lastMonth && x.CarId == c.Id)).ToList();
                            resCarsForLastMonthBeforeMonth.AddRange(lastLastMonthCompany);

                            var lastWeekCompany = (await reservationService.FindAll(x => x.CreateTime >= lastWeek && x.CarId == c.Id)).ToList();
                            resCarsForLastWeek.AddRange(lastWeekCompany);

                            var lastLastWeekCompany = (await reservationService.FindAll(x => x.CreateTime >= lastWeekBeforeWeek && x.CreateTime <= lastWeek && x.CarId == c.Id)).ToList();
                            resCarsForLastWeekBeforeWeek.AddRange(lastLastWeekCompany);

                            var reservationsCar = (await reservationService.FindAll(x => x.CarId == c.Id)).ToList();
                            reservations.AddRange(reservationsCar);
                        }

                        avgDuration = reservations.Select(x => (x.EndDate - x.StartDate).Days).Where(days => days > 0).ToList();
                    }


                    if (avgDuration.Any())
                    {
                        avg = avgDuration.Average();
                    }
                    foreach (var x in resCarsForLastMonthBeforeMonth)
                    {
                        totalMonthBeforeMonth += x.TotalPrice;
                    }
                    foreach (var x in resCarsForLast24After24Hours)
                    {
                        total24before24hours += x.TotalPrice;
                    }
                    foreach (var x in resCarsForLastWeekBeforeWeek)
                    {
                        totalWeekBeforeWeek += x.TotalPrice;
                    }
                    foreach (var x in resCarsForLast24Hours)
                    {
                        totalFor24Hours += x.TotalPrice;
                        countLast24Hours++;
                    }
                    foreach (var x in resCarsForLastWeek)
                    {
                        totalWeek += x.TotalPrice;
                        countLastWeek++;
                    }
                    foreach (var x in resCarsForLastMonth)
                    {
                        totalMonth += x.TotalPrice;
                        countLastMonth++;
                    }
                    int days1 = 30;
                    var startDate1 = DateTime.Now.AddDays(-days1);

                    var reservationsApi1 = (await reservationService.GetAll())
                        .Where(x => x.CreateTime >= startDate1)
                        .ToList();

                   

                    var top10Cars1 = reservationsApi1
         .GroupBy(x => x.CarId)
         .Select(g => new { CarId = g.Key, Count = g.Count() })
         .OrderByDescending(x => x.Count)
         .Take(10)
         .ToList();

                    top10 = new List<TopCarsViewModel>();

                    foreach (var x in top10Cars1)
                    {
                        var car = await carService.FindOne(cr => cr.Id == x.CarId);
                        top10.Add(new TopCarsViewModel
                        {
                            Brand = car.Brand,
                            Model = car.Model,
                            Count = x.Count
                        });
                    }

                    var peakReservations1 = reservationsApi1
                        .GroupBy(x => x.CreateTime.Date)
                        .Select(g => new { Date = g.Key, Count = g.Count() })
                        .OrderByDescending(x => x.Count)
                        .FirstOrDefault(); ;
                }
                else
                {
                    return BadRequest("Ei spongeBOb ne si kompaniq");
                }
            }



            var difference24 = totalFor24Hours - total24before24hours;
            var differenceWeek = totalWeek - totalWeekBeforeWeek;
            var differenceMonth=totalMonth - totalMonthBeforeMonth;
            var countPending = (await carService.FindAll(x => x.Pending == true)).Count();
            AnalyticsViewModel analyticsViewModel = new AnalyticsViewModel()
            {
                TotalLast24Hours = totalFor24Hours,
                Count24Hours = countLast24Hours,
                TotalLastMonth = totalMonth,
                CountMonth = countLastMonth,
                TotalLastWeek = totalWeek,
                TotalPriceForLast24HoursBefore24Hours = total24before24hours,
                TotalPriceForLastWeekBeforeWeek = totalWeekBeforeWeek,
                TotalPriceForLastMounthBeforeMonth = totalMonthBeforeMonth,
                CountWeek = countLastWeek,
                AvgReservationDuration = avg,
                CountAllReservations = reservations.Count(),
                Top10Cars = top10,
                PendingsCount = countPending,
                ReportCount = reportCount,
                Difference24 = difference24,
                DifferenceMonth = differenceMonth,
                DifferenceWeek = differenceWeek,
                DifferenceReservation24 = resCarsForLast24Hours.Count() - resCarsForLast24After24Hours.Count(),
                DifferenceReservationWeek = resCarsForLastWeek.Count() - resCarsForLastWeekBeforeWeek.Count(),
                DifferenceReservationMonth = resCarsForLastMonth.Count() - resCarsForLastMonthBeforeMonth.Count(),
                TotalReservationPreviousDay = resCarsForLast24After24Hours.Count(),
                TotalReservationPreviousWeek = resCarsForLastWeekBeforeWeek.Count(),
                TotalReservationPreviousMonth = resCarsForLastMonthBeforeMonth.Count(),
                CompanyCarPendingCount = pendingCount
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
