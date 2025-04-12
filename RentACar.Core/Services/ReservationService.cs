using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.IServices;
using RentACar.DataAccess.IRepository;
using RentACar.DataAccess.IRepository.Repository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class ReservationService : IReservationService
    {
        IRepository<Reservation> reservationsRepository;
        ICustomerService customerService;
        ICarService carService;
        IImageService imageService;


        public ReservationService(IRepository<Reservation> reservationsRepository, ICustomerService customerService, ICarService carService, IImageService imageService)
        {
            this.reservationsRepository = reservationsRepository;
            this.customerService = customerService;
            this.carService = carService;
            this.imageService = imageService;

        }
        public async Task Add(Reservation entity)
        {
            await reservationsRepository.Add(entity);
        }

        public IQueryable<Reservation> AllWithInclude(params Expression<Func<Reservation, object>>[] filters)
        {
            return reservationsRepository.AllWithInclude(filters);
        }

        public Task<bool> AnyAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return reservationsRepository.AnyAsync(predicate);
        }

        public Task<int> CountAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return reservationsRepository.CountAsync(predicate);
        }

        public void Delete(Reservation entity)
        {
            reservationsRepository.Delete(entity);
        }

        public async Task<IEnumerable<Reservation>> FindAll(Expression<Func<Reservation, bool>> predicate)
        {
            return await reservationsRepository.FindAll(predicate);
        }

        public async Task<Reservation> FindOne(Expression<Func<Reservation, bool>> predicate)
        {
            return await reservationsRepository.FindOne(predicate);
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            var reservations = await reservationsRepository.GetAll();
            return reservations.ToList();
        }


        public async Task<IEnumerable<Reservation>> GetAllOrderBy(Expression<Func<Reservation, object>> predicate)
        {
            return await reservationsRepository.GetAllOrderBy(predicate);
        }
        public async Task<IEnumerable<Reservation>> GetAllByOrderByCreateTime()
        {
            return await reservationsRepository.GetAllOrderBy(x => x.CreateTime);
        }

        public async Task<bool> HasOverlappingReservation(int carId, DateTime startDate, DateTime endDate)
        {
            return await reservationsRepository
                .AnyAsync(r => r.CarId == carId &&
                               r.StartDate < endDate &&
                               r.EndDate > startDate);
        }
        public async Task Save()
        {
            await reservationsRepository.Save();
        }

        public void Update(Reservation entity)
        {
            reservationsRepository.Update(entity);
        }

        public async Task<IEnumerable<Reservation>> FindAllForLast24Hours()
        {
            var last24Hours = DateTime.Now.AddDays(-1);
            return await reservationsRepository.FindAll(x => x.CreateTime >= last24Hours);
        }

        public async Task<IEnumerable<Reservation>> FindAllForLast24HoursBefore24Hours()
        {
            var last24Hours = DateTime.Now.AddDays(-1);
            var last24Before24Hours = DateTime.Now.AddDays(-2);
            return await reservationsRepository.FindAll(x => x.CreateTime >= last24Before24Hours && x.CreateTime <= last24Hours);
        }

        public async Task<IEnumerable<Reservation>> FindAllForLastWeek()
        {
            var today = DateTime.UtcNow;
            var startOfLastWeek = today.AddDays(-(int)today.DayOfWeek - 6).Date;
            var endOfLastWeek = startOfLastWeek.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => x.CreateTime >= startOfLastWeek && x.CreateTime <= endOfLastWeek);
        }
        public async Task<IEnumerable<Reservation>> FindAllForWeekBeforeLast()
        {
            var today = DateTime.UtcNow;
            var startOfWeekBeforeLast = today.AddDays(-(int)today.DayOfWeek - 13).Date;
            var endOfWeekBeforeLast = startOfWeekBeforeLast.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => x.CreateTime >= startOfWeekBeforeLast && x.CreateTime <= endOfWeekBeforeLast);
        }

        public async Task<IEnumerable<Reservation>> FindAllForLastMonth()
        {
            var today = DateTime.UtcNow;
            var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfLastMonth = firstDayOfCurrentMonth.AddMonths(-1);
            var endOfLastMonth = firstDayOfCurrentMonth.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => x.CreateTime >= startOfLastMonth && x.CreateTime <= endOfLastMonth);
        }

        public async Task<IEnumerable<Reservation>> FindAllForPreviousMonth()
        {
            var today = DateTime.UtcNow;
            var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfMonthBeforeLast = firstDayOfCurrentMonth.AddMonths(-2);
            var endOfMonthBeforeLast = firstDayOfCurrentMonth.AddMonths(-1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => x.CreateTime >= startOfMonthBeforeLast && x.CreateTime <= endOfMonthBeforeLast);
        }

        public Task<int> Count()
        {
            return reservationsRepository.Count();
        }

        public async Task<double> TotalPriceForOnePeriodOfTime(IEnumerable<Reservation> list)
        {
            double total = 0;
            foreach (var reservation in list)
            {
                total += reservation.TotalPrice;
            }
            return total;
        }

        public async Task<IEnumerable<Reservation>> FindAllLimited(Expression<Func<Reservation, bool>> predicate, int limit)
        {
            return await reservationsRepository.FindAllLimited(predicate, limit);
        }

        public async Task<int> PercentagesOfDifferentPeriods(double firstPeriod, double lastPeriod)
        {
            double difference = firstPeriod - lastPeriod;

            int percentages = 0;

            if (lastPeriod != 0)
            {
                percentages = (int)((difference / lastPeriod) * 100);
            }
            else
            {
                if (firstPeriod > 0)
                {
                    percentages = 100;
                }
                else
                {
                    percentages = 0;
                }
            }
            return percentages;
        }

        public async Task<List<(Customer customer, string brand, string model, Image image)>> GetCustomersWithReservedCars()
        {
            var reservationedCars = await reservationsRepository.GetAllOrderBy(x => x.CreateTime);
            var result = new List<(Customer, string, string, Image)>();

            foreach (var carx in reservationedCars)
            {
                var customer = await customerService.FindOne(x => x.Id == carx.CustomerId);
                var car = await carService.FindOne(x => x.Id == carx.CarId);
                var image = await imageService.ImageByCarId(car.Id);

                if (customer != null && car != null)
                {
                    result.Add((customer, car.Brand, car.Model, image));
                }
            }

            return result;
        }



        public async Task<IEnumerable<Reservation>> GetAllReservationsByStartDate(DateTime startDate)
        {
            return await reservationsRepository.FindAll(x => x.CreateTime >= startDate);

        }
        public async Task<List<int>> GetTop10ReservedCarIdsByStartDate(DateTime startDate, List<int> carIds)
        {
            var reservations = await reservationsRepository.FindAll(x =>
                (carIds == null || carIds.Contains(x.CarId)) && x.StartDate >= startDate);

            return reservations.GroupBy(x => x.CarId).Select(g => new { CarId = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count)
            .Take(10).Select(x => x.CarId).ToList();
        }

        public async Task<IEnumerable<Reservation>> GetAllIfItIsNotCompany(List<int> carIds)
        {

            return await reservationsRepository.FindAll(x => carIds == null || carIds.Contains(x.CarId));
        }

        public async Task<IEnumerable<Reservation>> FindAllForLast24HoursCompany(List<int> companyCarIds)
        {
            var last24Hours = DateTime.Now.AddDays(-1);
            return await reservationsRepository.FindAll(x => companyCarIds.Contains(x.CarId) && x.CreateTime >= last24Hours);
        }

        public async Task<IEnumerable<Reservation>> FindAllForLast24HoursBefore24HoursCompany(List<int> companyCarIds)
        {
            var last24Hours = DateTime.Now.AddDays(-1);
            var last24Before24Hours = DateTime.Now.AddDays(-2);
            return await reservationsRepository.FindAll(x => companyCarIds.Contains(x.CarId) && x.CreateTime >= last24Before24Hours && x.CreateTime < last24Hours);
        }

        public async Task<IEnumerable<Reservation>> FindAllForLastWeekCompany(List<int> companyCarIds)
        {
            var today = DateTime.UtcNow;
            var startOfLastWeek = today.AddDays(-(int)today.DayOfWeek - 6).Date;
            var endOfLastWeek = startOfLastWeek.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => companyCarIds.Contains(x.CarId) && x.CreateTime >= startOfLastWeek && x.CreateTime < endOfLastWeek);
        }

        public async Task<IEnumerable<Reservation>> FindAllForWeekBeforeLastCompany(List<int> companyCarIds)
        {
            var today = DateTime.UtcNow;
            var startOfWeekBeforeLast = today.AddDays(-(int)today.DayOfWeek - 13).Date;
            var endOfWeekBeforeLast = startOfWeekBeforeLast.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => companyCarIds.Contains(x.CarId) && x.CreateTime >= startOfWeekBeforeLast && x.CreateTime < endOfWeekBeforeLast);
        }

        public async Task<IEnumerable<Reservation>> FindAllForLastMonthCompany(List<int> companyCarIds)
        {
            var today = DateTime.UtcNow;
            var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfLastMonth = firstDayOfCurrentMonth.AddMonths(-1);
            var endOfLastMonth = firstDayOfCurrentMonth.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => companyCarIds.Contains(x.CarId) && x.CreateTime >= startOfLastMonth && x.CreateTime < endOfLastMonth);
        }

        public async Task<IEnumerable<Reservation>> FindAllForPreviousMonthCompany(List<int> companyCarIds)
        {
            var today = DateTime.UtcNow;
            var firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfMonthBeforeLast = firstDayOfCurrentMonth.AddMonths(-2);
            var endOfMonthBeforeLast = firstDayOfCurrentMonth.AddMonths(-1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            return await reservationsRepository.FindAll(x => companyCarIds.Contains(x.CarId) && x.CreateTime >= startOfMonthBeforeLast && x.CreateTime < endOfMonthBeforeLast);
        }

        public async Task<double> DifferenceOfPriceBetweenTwoPeriods(double total, double totalPrev)
        {
            return (total - totalPrev);
        }

        public async Task<List<int>> GetAllReservatedCarsId(DateTime? start, DateTime? end)
        {
            var result = await reservationsRepository.FindAll(r => r.StartDate < end.Value && r.EndDate > start.Value);
            return result.Select(r => r.CarId).Distinct().ToList();
        }

        public double TotalPriceForOneReservation(Reservation reservation, int days, double price, bool isSelfPick, bool isReturnAtSamePlace)
        {
            if (days == 7)
            {
                reservation.TotalPrice = price;
            }
            else if (days > 0)
            {
                reservation.TotalPrice = (days * price) * 0.9;
            }
            if (isSelfPick == false)
            {
                reservation.TotalPrice += 25;
            }
            if (isReturnAtSamePlace == true)
            {
                reservation.TotalPrice += 25;
            }
            return reservation.TotalPrice;
        }

        public async Task<List<Car>> GetAllReservationsForCompany(IEnumerable<Car> companyCars)
        {
            List<Car> reservatedCars = new List<Car>();
            foreach (var c in companyCars)
            {
                var reservations = (await FindAll(x => x.CarId == c.Id)).OrderByDescending(x => x.CreateTime).ToList();
                if (reservations.Count > 0)
                {
                    reservatedCars.Add(c);
                }
            }
            return reservatedCars;
        }
        public List<(int CarId, int Count)> GetCarReservationCounts(List<Reservation> reservations)
        {
            return  reservations
                .GroupBy(x => x.CarId)
                .Select(g => (CarId: g.Key, Count: g.Count()))
                .OrderByDescending(x => x.Count)
                .ToList();
        }

        public async Task<List<Reservation>> GetAllReservationsContaingCompanyIds(List<int> companyCarIds)
        {
            var result = await reservationsRepository.FindAll(x => companyCarIds.Contains(x.CarId));
            return result.ToList();
        }

        public async Task<IEnumerable<Reservation>> GetAllByStartAndEndDate(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                return new List<Reservation>();
            }
            return await reservationsRepository.FindAll(x => x.EndDate >= startDate && x.StartDate <= endDate);
        }

        public async Task<int> FindIfUserHasReservationForOnePeriod(DateTime? startDay, DateTime? endDay,int userId)
        {
            if (!startDay.HasValue || !endDay.HasValue)
            {
                return 0; 
            }

            var overlappingReservations = await reservationsRepository.FindOne(
                r => r.CustomerId == userId &&
                     r.StartDate < endDay.Value &&
                     r.EndDate > startDay.Value
            );

            if (overlappingReservations != null)
            {
                return overlappingReservations.Id;
            }
            else
            {
                return 0;
            }
        
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId)
        {
            return (await reservationsRepository.FindAll(x=>x.CustomerId == userId)).OrderByDescending(x=>x.CreateTime);
        }

        public async Task<string> GetStatusOfReservation(Reservation reservation)
        {
            string status = "";
            if (reservation.StartDate < DateTime.Now && reservation.EndDate > DateTime.Now)
            {
                status = "Active";           
            }
            else if(reservation.StartDate < DateTime.Now && reservation.EndDate < DateTime.Now) 
            {
                status= "Completed";
            }
            else if (reservation.StartDate > DateTime.Now)
            {
                status = "Coming";
             
            }
            else if (reservation.IsCanceled == true)
            {
                status = "Cancelled";
            }
            
            return status;
        }

        public async Task<(bool isReserved, DateTime? startDate, DateTime? endDate)> IsTheCarReservatedForToday(int carId)
        {
          
            var today = DateTime.Today;

          
            var overlappingReservation = await reservationsRepository
                .FindOne(r => r.CarId == carId && r.StartDate.Date <= today && r.EndDate.Date >= today);
               

            
            if (overlappingReservation != null)
            {
                return (true, overlappingReservation.StartDate, overlappingReservation.EndDate);
            }
            else
            {
                return (false, null, null);
            }
        }

        public Task<Reservation> FindById(int id)
        {
            return reservationsRepository.FindOne(r => r.Id == id);
        }

        public async Task<List<Reservation>> GetAllReservationOfCompanyCars(List<int> companyCarIds)
        {
            List<Reservation>reservations=new List<Reservation>();
            foreach(var x in companyCarIds)
            {
              reservations.AddRange((await reservationsRepository.FindAll(r => r.CarId == x)));
            }
            return reservations.OrderByDescending(x=>x.Id).ToList();
        }
    }
}

        
    
