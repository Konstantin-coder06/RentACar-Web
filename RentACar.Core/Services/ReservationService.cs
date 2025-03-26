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
        public ReservationService(IRepository<Reservation> reservationsRepository)
        {
            this.reservationsRepository = reservationsRepository;
        }
        public async Task Add(Reservation entity)
        {
          await reservationsRepository.Add(entity);
        }

        public async Task<IEnumerable<Reservation>> AllWithInclude(params Expression<Func<Reservation, object>>[] filters)
        {
            var reservations=await reservationsRepository.AllWithInclude(filters);
            return reservations.ToList();
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
            var reservations=await reservationsRepository.FindAll(predicate);
            return reservations.ToList();
        }

        public async Task<Reservation> FindOne(Expression<Func<Reservation, bool>> predicate)
        {
            return await reservationsRepository.FindOne(predicate);
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            var reservations=await reservationsRepository.GetAll();
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
            var last24Before24Hours= DateTime.Now.AddDays(-2);
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
            throw new NotImplementedException();
        }

        public async Task<double> TotalPriceForOnePeriodOfTime(IEnumerable<Reservation>list)
        {
            double total = 0;
            foreach (var reservation in list) 
            {
                total += reservation.TotalPrice;
            }
            return total;
        }

        public Task<IEnumerable<Reservation>> FindAllLimited(Expression<Func<Reservation, bool>> predicate, int limit)
        {
            throw new NotImplementedException();
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
    }
}
