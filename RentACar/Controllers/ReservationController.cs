using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;
using System.Globalization;

namespace RentACar.Controllers
{
    public class ReservationController : Controller
    {
        ICarService carService;
        ICarCompanyService carCompanyService;
        IImageService imageService;
        IReservationService reservationService;
        ICarFeatureService carFeatureService;
        IFeatureService featureService;
        public ReservationController(ICarService carService, ICarCompanyService carCompanyService, IImageService imageService, IReservationService reservationService, ICarFeatureService carFeatureService, IFeatureService featureService)
        {
            this.carService = carService;
            this.carCompanyService = carCompanyService;
            this.imageService = imageService;
            this.reservationService = reservationService;
            this.carFeatureService = carFeatureService;
            this.featureService = featureService;
        }
        [Authorize(Roles ="User")]
        public async Task<IActionResult> Reservation(int id)
        {
            DateTime? startDay = null;
            DateTime? endDay = null;

            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(startDayStr,"yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None
                );
            }
            var endDayStr = HttpContext.Session.GetString("EndDate");
            if (!string.IsNullOrEmpty(endDayStr))
            {
                endDay = DateTime.ParseExact(endDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Register", "Account");
            }
            var car = await carService.FindById(id);
            var featuresOfACar = await carFeatureService.GetByCarIDAllFeatures(car.Id);
            var features = new List<Feature>();
            Feature feature = new Feature();
            foreach (var x in featuresOfACar)
            {
                feature = await featureService.GetById(x);
                features.Add(feature);
            }

            var carWithImages = new CarWithImages
            {
                Car = car,
                Images = await imageService.GetImagesOrderByOrderCarId(car.Id),
                StartDate = startDay,
                EndDate = endDay,
                Features = features,
                IsSelfPick = true,
                IsReturnOptionsEnabled = false,
                IsAddressInputVisible = false,
            };


            return View(carWithImages);
        }
        [HttpPost]
        public async Task<IActionResult> Reservation(CarWithImages carWithImages, string submitButton)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            if (carWithImages.Car == null || carWithImages.Car.Id == 0)
            {
                carWithImages.Car = await carService.FindById(carWithImages.Car.Id);
            }
            if (carWithImages.Car == null)
            {
                ModelState.AddModelError("", "Car not found.");
                return View(carWithImages);
            }
            carWithImages.Images = await imageService.GetImagesOrderByOrderCarId(carWithImages.Car.Id);
            if (submitButton == "ChangeDate")
            {
                var newStartDate = carWithImages.StartDate;
                var newEndDate = carWithImages.EndDate;

                if (!newStartDate.HasValue || !newEndDate.HasValue || newStartDate >= newEndDate)
                {
                    ModelState.AddModelError("", "Invalid dates: Start date must be before end date.");
                    return View(carWithImages);
                }
                bool hasConflict = await reservationService.HasOverlappingReservation(carWithImages.Car.Id, newStartDate.Value, newEndDate.Value);
                if (!hasConflict)
                {
                    HttpContext.Session.SetString("StartDate", newStartDate.Value.ToString("yyyy-MM-dd"));
                    HttpContext.Session.SetString("EndDate", newEndDate.Value.ToString("yyyy-MM-dd"));
                    return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
                }
                else
                {
                    TempData["HasConflict"] = true;
                    TempData["ProposedStartDate"] = newStartDate.Value.ToString("yyyy-MM-dd");
                    TempData["ProposedEndDate"] = newEndDate.Value.ToString("yyyy-MM-dd");

                    var car = await carService.FindById(carWithImages.Car.Id);
                    var featuresOfACar = await carFeatureService.GetByCarIDAllFeatures(car.Id);
                    var features = await featureService.GetAllFeaturesByIds(featuresOfACar);

                    carWithImages.Car = car;
                    carWithImages.Images = await imageService.GetImagesOrderByOrderCarId(carWithImages.Car.Id);
                    carWithImages.StartDate = newStartDate;
                    carWithImages.EndDate = newEndDate;
                    carWithImages.Features = features;
                    return View(carWithImages);
                }
            }
            else if (submitButton == "KeepDates")
            {
                if (carWithImages.StartDate.HasValue && carWithImages.EndDate.HasValue)
                {
                    HttpContext.Session.SetString("StartDate", carWithImages.StartDate.Value.ToString("yyyy-MM-dd"));
                    HttpContext.Session.SetString("EndDate", carWithImages.EndDate.Value.ToString("yyyy-MM-dd"));
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Start and end dates are required.");
                    var car = await carService.FindById(carWithImages.Car.Id);
                    var featuresOfACar = await carFeatureService.GetByCarIDAllFeatures(car.Id);
                    var features = await featureService.GetAllFeaturesByIds(featuresOfACar);
                    carWithImages.Features = features;
                    return View(carWithImages);
                }
            }
            else if (submitButton == "RevertDates")
            {

                return RedirectToAction("Reservation", new { id = carWithImages.Car.Id });
            }
            else if (submitButton == "BookNow")
            {

                if (!carWithImages.StartDate.HasValue || !carWithImages.EndDate.HasValue)
                {
                    ModelState.AddModelError("", "Start and end dates are required.");

                    return View(carWithImages);
                }

                DateTime startDay = carWithImages.StartDate.Value;
                DateTime endDay = carWithImages.EndDate.Value;
                int totalDays = (int)(endDay - startDay).TotalDays;

                if (totalDays <= 0)
                {
                    ModelState.AddModelError("", "End date must be after start date.");
                    return View(carWithImages);
                }


                bool hasConflict = await reservationService.HasOverlappingReservation(carWithImages.Car.Id, startDay, endDay);
                if (hasConflict)
                {
                    ModelState.AddModelError("", "The car is already reserved for the selected dates.");
                    return View(carWithImages);
                }


                Reservation reservation = new Reservation()
                {
                    StartDate = startDay,
                    EndDate = endDay,
                    IsSelfPick = carWithImages.IsSelfPick,
                    PaidDeliveryPlace = carWithImages.CustomAddress,
                    IsReturnBackAtSamePlace = carWithImages.IsReturningBackAtSamePlace,
                    CarId = carWithImages.Car.Id,
                    CustomerId = userId.Value,
                    CreateTime = DateTime.Now,
                };
                var price = await carService.GetPricePerDayByCarId(carWithImages.Car.Id);
                reservation.TotalPrice = reservationService.TotalPriceForOneReservation(reservation, totalDays, price, 
                    carWithImages.IsSelfPick, carWithImages.IsReturningBackAtSamePlace);

                TempData["TotalPrice"] = reservation.TotalPrice.ToString(CultureInfo.InvariantCulture);
                TempData["IsSelfPick"] = carWithImages.IsSelfPick.ToString(); 
                TempData["PaidDeliveryPlace"] = carWithImages.CustomAddress;
                TempData["IsReturnBackAtSamePlace"] = carWithImages.IsReturningBackAtSamePlace.ToString(); 
                TempData["CarId"] = carWithImages.Car.Id.ToString(); 
                TempData["CustomerId"] = userId.Value.ToString();
                TempData["success"] = $"Days {totalDays} Total price: {reservation.TotalPrice}";

                return RedirectToAction("FinalStepsReservation", new { id = carWithImages.Car.Id });
            }

            return View(carWithImages);

        }
        public async Task<IActionResult> FinalStepsReservation(int id)
        { 
            var companyId = await carService.GetCompanyIdByCarId(id);

            var companyName = await carCompanyService.GetNameById(companyId) ?? "Unknown Company"; ;
            var companyAddress = await carCompanyService.GetAddressById(companyId) ?? "Unknown Address"; ;
            DateTime startDay = new DateTime();
            DateTime endDay = new DateTime();
            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(startDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }
            var endDayStr = HttpContext.Session.GetString("EndDate");
            if (!string.IsNullOrEmpty(endDayStr))
            {
                endDay = DateTime.ParseExact(endDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }
            int totalDays = (int)(endDay - startDay).TotalDays;
            var image = await imageService.ImageByCarId(id);
            var car = await carService.FindById(id);
            var price = car.PricePerDay * totalDays;
            double total = 0;
            if (TempData["TotalPrice"] != null)
            {
                total = double.Parse(TempData["TotalPrice"].ToString(), CultureInfo.InvariantCulture);
            }
            var priceOfTaxes = total * 0.09;
            FinalStepReservationViewModel model = new FinalStepReservationViewModel()
            {
                CompanyName = companyName,
                Address = companyAddress,
                StartDay = startDay,
                EndDay = endDay,
                TotalDays = totalDays,
                ImageHref = image.Url,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Transmittion = car.DriveTrain,
                LimitForOneDay = car.MileageLimitForDay,
                TotalPriceWithoutTheDiscount = price,
                DifferenceTotalPriceWithDiscounted = price - total,
                TotalPrice = total,
                PriceOfTaxes = priceOfTaxes,
                CarId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FinalStepsReservation(FinalStepReservationViewModel viewModel)
        {
            DateTime? startDay = null;
            DateTime? endDay = null;
            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(startDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            var endDayStr = HttpContext.Session.GetString("EndDate");
            if (!string.IsNullOrEmpty(endDayStr))
            {
                endDay = DateTime.ParseExact(endDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            
            bool isSelfPick = TempData["IsSelfPick"] != null && bool.Parse(TempData["IsSelfPick"].ToString());
            string paidDeliveryPlace = TempData["PaidDeliveryPlace"]?.ToString();
            bool isReturnBackAtSamePlace = TempData["IsReturnBackAtSamePlace"] != null && bool.Parse(TempData["IsReturnBackAtSamePlace"].ToString());
            int carId = TempData["CarId"] != null ? int.Parse(TempData["CarId"].ToString()) : 0;
            int customerId = TempData["CustomerId"] != null ? int.Parse(TempData["CustomerId"].ToString()) : 0;
            double totalPrice = 0;
            if(!startDay.HasValue || !endDay.HasValue) 
            {
                ModelState.AddModelError("StartDate", "StartDate and EndDate are required");
                return View(viewModel);
            }
            if (viewModel.TotalPrice!=0)
            {
                try
                {
                    totalPrice = viewModel.TotalPrice;
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("", "Invalid total price.");
                    return View(viewModel);
                }
            }
            else
            {
                ModelState.AddModelError("", "Total price is missing.");
                return View(viewModel);
            }

            if (carId == 0 || customerId == 0)
            {
                ModelState.AddModelError("", "Invalid car or customer information.");
                return View(viewModel);
            }

       
            Reservation reservation = new Reservation()
            {
                StartDate = startDay.Value,
                EndDate = endDay.Value,
                IsSelfPick = isSelfPick,
                PaidDeliveryPlace = paidDeliveryPlace,
                IsReturnBackAtSamePlace = isReturnBackAtSamePlace,
                CarId = carId,
                CustomerId = customerId,
                CreateTime = DateTime.Now,
                TotalPrice = viewModel.TotalPrice 
            };
            await reservationService.Add(reservation);
            await reservationService.Save();

               
            TempData.Clear();
            TempData["success"] = "Reservation successfully created!";
            return RedirectToAction("Index", "Home");
        }
    }
}
