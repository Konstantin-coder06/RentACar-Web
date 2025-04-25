using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.Models;
using System.Globalization;
using System.Linq.Expressions;

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
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Reservation(int id)
        {

            DateTime? startDay = null;
            DateTime? endDay = null;

            var startDayStr = HttpContext.Session.GetString("StartDate");
            if (!string.IsNullOrEmpty(startDayStr))
            {
                startDay = DateTime.ParseExact(startDayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
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
            var companyAddress = await carCompanyService.GetAddressById(car.CarCompanyId);
            var featuresOfACar = await carFeatureService.GetByCarIDAllFeatures(car.Id);
            var features = new List<Feature>();
            Feature feature = new Feature();
            foreach (var x in featuresOfACar)
            {
                feature = await featureService.GetById(x);
                features.Add(feature);
            }

            var CarWithImagesReservation = new CarWithImagesReservation
            {
                Car = car,
                Images = await imageService.GetImagesOrderByOrderCarId(car.Id),
                StartDay = startDay,
                EndDay = endDay,
                Features = features,
                IsSelfPick = true,
                IsReturnOptionsEnabled = false,
                IsAddressInputVisible = false,
                AddressOfCompany = companyAddress
            };


            return View(CarWithImagesReservation);
        }
        [HttpPost]
        public async Task<IActionResult> Reservation(CarWithImagesReservation CarWithImagesReservation, string submitButton)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            if (CarWithImagesReservation.Car == null || CarWithImagesReservation.Car.Id == 0)
            {
                CarWithImagesReservation.Car = await carService.FindById(CarWithImagesReservation.Car.Id);
            }
            if (CarWithImagesReservation.Car == null)
            {
                ModelState.AddModelError("", "Car not found.");
                return View(CarWithImagesReservation);
            }
            CarWithImagesReservation.Images = await imageService.GetImagesOrderByOrderCarId(CarWithImagesReservation.Car.Id);

            if (submitButton == "ChangeDate")
            {
                var newStartDate = CarWithImagesReservation.StartDay;
                var newEndDate = CarWithImagesReservation.EndDay;

                if (!newStartDate.HasValue || !newEndDate.HasValue || newStartDate >= newEndDate)
                {
                    ModelState.AddModelError("", "Invalid dates: Start date must be before end date.");
                    return View(CarWithImagesReservation);
                }
                bool hasConflict = await reservationService.HasOverlappingReservation(CarWithImagesReservation.Car.Id, newStartDate.Value, newEndDate.Value);
                if (!hasConflict)
                {
                    HttpContext.Session.SetString("StartDate", newStartDate.Value.ToString("yyyy-MM-dd"));
                    HttpContext.Session.SetString("EndDate", newEndDate.Value.ToString("yyyy-MM-dd"));
                    return RedirectToAction("Reservation", new { id = CarWithImagesReservation.Car.Id });
                }
                else
                {
                    var conflictingReservation = await reservationService.GetFirstConflictingReservation(CarWithImagesReservation.Car.Id, newStartDate.Value, newEndDate.Value);
                    if (conflictingReservation != null)
                    {
                        TempData["HasConflict"] = true;
                        TempData["ConflictStartDate"] = conflictingReservation.StartDate.ToString("yyyy-MM-dd");
                        TempData["ConflictEndDate"] = conflictingReservation.EndDate.ToString("yyyy-MM-dd");
                        TempData["ProposedStartDate"] = newStartDate.Value.ToString("yyyy-MM-dd");
                        TempData["ProposedEndDate"] = newEndDate.Value.ToString("yyyy-MM-dd");
                    }

                    var car = await carService.FindById(CarWithImagesReservation.Car.Id);
                    var featuresOfACar = await carFeatureService.GetByCarIDAllFeatures(car.Id);
                    var features = await featureService.GetAllFeaturesByIds(featuresOfACar);

                    CarWithImagesReservation.Car = car;
                    CarWithImagesReservation.Images = await imageService.GetImagesOrderByOrderCarId(CarWithImagesReservation.Car.Id);
                    CarWithImagesReservation.StartDay = newStartDate;
                    CarWithImagesReservation.EndDay = newEndDate;
                    CarWithImagesReservation.Features = features;
                    return View(CarWithImagesReservation);
                }
            }
            else if (submitButton == "KeepDates")
            {
                if (CarWithImagesReservation.StartDay.HasValue && CarWithImagesReservation.EndDay.HasValue)
                {
                    HttpContext.Session.SetString("StartDate", CarWithImagesReservation.StartDay.Value.ToString("yyyy-MM-dd"));
                    HttpContext.Session.SetString("EndDate", CarWithImagesReservation.EndDay.Value.ToString("yyyy-MM-dd"));
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Start and end dates are required.");
                    var car = await carService.FindById(CarWithImagesReservation.Car.Id);
                    var featuresOfACar = await carFeatureService.GetByCarIDAllFeatures(car.Id);
                    var features = await featureService.GetAllFeaturesByIds(featuresOfACar);
                    CarWithImagesReservation.Features = features;
                    return View(CarWithImagesReservation);
                }
            }
            else if (submitButton == "RevertDates")
            {
                return RedirectToAction("Reservation", new { id = CarWithImagesReservation.Car.Id });
            }
            else if (submitButton == "BookNow")
            {
                if (!CarWithImagesReservation.StartDay.HasValue || !CarWithImagesReservation.EndDay.HasValue)
                {
                    ModelState.AddModelError("", "Start and end dates are required.");
                    return View(CarWithImagesReservation);
                }

                DateTime startDay = CarWithImagesReservation.StartDay.Value;
                DateTime endDay = CarWithImagesReservation.EndDay.Value;
                int totalDays = (int)(endDay - startDay).TotalDays;

                if (totalDays <= 0)
                {
                    ModelState.AddModelError("", "End date must be after start date.");
                    return View(CarWithImagesReservation);
                }

                bool hasConflict = await reservationService.HasOverlappingReservation(CarWithImagesReservation.Car.Id, startDay, endDay);
                if (hasConflict)
                {
                    var conflictingReservation = await reservationService.GetFirstConflictingReservation(CarWithImagesReservation.Car.Id, startDay, endDay);
                    if (conflictingReservation != null)
                    {
                        ModelState.AddModelError("", "The car is already reserved for the selected dates.");
                        TempData["HasConflict"] = true;
                        TempData["ConflictStartDate"] = conflictingReservation.StartDate.ToString("yyyy-MM-dd");
                        TempData["ConflictEndDate"] = conflictingReservation.EndDate.ToString("yyyy-MM-dd");
                        TempData["ProposedStartDate"] = startDay.ToString("yyyy-MM-dd");
                        TempData["ProposedEndDate"] = endDay.ToString("yyyy-MM-dd");
                    }
                    return View(CarWithImagesReservation);
                }

                string paidDeliveryPlace = CarWithImagesReservation.IsSelfPick ? null : CarWithImagesReservation.CustomAddress;

                Reservation reservation = new Reservation()
                {
                    StartDate = startDay,
                    EndDate = endDay,
                    IsSelfPick = CarWithImagesReservation.IsSelfPick,
                    PaidDeliveryPlace = paidDeliveryPlace,
                    IsReturnBackAtSamePlace = CarWithImagesReservation.IsReturningBackAtSamePlace,
                    CarId = CarWithImagesReservation.Car.Id,
                    CustomerId = userId.Value,
                    CreateTime = DateTime.Now,
                };
                var price = await carService.GetPricePerDayByCarId(CarWithImagesReservation.Car.Id);
                reservation.TotalPrice = reservationService.TotalPriceForOneReservation(reservation, totalDays, price,
                    CarWithImagesReservation.IsSelfPick, CarWithImagesReservation.IsReturningBackAtSamePlace);

                TempData["TotalPrice"] = reservation.TotalPrice.ToString(CultureInfo.InvariantCulture);
                TempData["IsSelfPick"] = CarWithImagesReservation.IsSelfPick.ToString();
                TempData["PaidDeliveryPlace"] = paidDeliveryPlace;
                TempData["IsReturnBackAtSamePlace"] = CarWithImagesReservation.IsReturningBackAtSamePlace.ToString();
                TempData["CarId"] = CarWithImagesReservation.Car.Id.ToString();
                TempData["CustomerId"] = userId.Value.ToString();

                TempData.Keep("PaidDeliveryPlace");
                TempData.Keep("IsSelfPick");

                return RedirectToAction("FinalStepsReservation", new { id = CarWithImagesReservation.Car.Id });
            }
            return View(CarWithImagesReservation);
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> FinalStepsReservation(int id)
        {

            var companyId = await carService.GetCompanyIdByCarId(id);

            var companyName = await carCompanyService.GetNameById(companyId) ?? "Unknown Company"; ;
            var companyAddress = await carCompanyService.GetAddressById(companyId) ?? "Unknown Address"; ;
            var allCompanyCars = await carService.GetAllCarsOfCompany(companyId);
            List<int> companyCarIds = allCompanyCars.Select(c => c.Id).ToList();

            var allReservations = await reservationService.GetAllReservationsContaingCompanyIds(companyCarIds);
            var reservationedCars = allReservations.ToList();
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
            if (startDay == DateTime.MinValue || endDay == DateTime.MinValue)
            {
                return RedirectToAction("Login", "Account");
            }
                int totalDays = reservationService.TotalDaysByDates(startDay, endDay);
            var image = await imageService.ImageByCarId(id);
            var car = await carService.FindById(id);
            var price = carService.TotalPriceOfCar(car.PricePerDay, totalDays);
            double total = 0;
            if (TempData["TotalPrice"] != null)
            {
                total = double.Parse(TempData["TotalPrice"].ToString(), CultureInfo.InvariantCulture);
            }
            var priceOfTaxes = carService.PriceOfTaxes(total);
            bool isSelfPick = TempData["IsSelfPick"] != null && bool.Parse(TempData["IsSelfPick"].ToString());
            string paidDeliveryPlace = isSelfPick == false ? TempData["PaidDeliveryPlace"]?.ToString() : null;

            bool isReturnBackAtSamePlace = TempData["IsReturnBackAtSamePlace"] != null && bool.Parse(TempData["IsReturnBackAtSamePlace"].ToString());
            string returningPlace = isReturnBackAtSamePlace ? (string.IsNullOrWhiteSpace(paidDeliveryPlace) ? companyAddress : paidDeliveryPlace) : companyAddress;

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
                DifferenceTotalPriceWithDiscounted = carService.Difference(price, total),
                TotalPrice = total,
                PriceOfTaxes = priceOfTaxes,
                CarId = id,
                ReturningPlace = returningPlace,
                TakingPlace = string.IsNullOrWhiteSpace(paidDeliveryPlace) ? companyAddress : paidDeliveryPlace,
                ReservationCompanyCount=reservationedCars.Count,
            };
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "User")]

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
            if (!startDay.HasValue || !endDay.HasValue)
            {
                ModelState.AddModelError("StartDate", "StartDate and EndDate are required");
                return View(viewModel);
            }
            if (viewModel.TotalPrice != 0)
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