using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface IReservationService:IRepository<Reservation>
    {
        Task<bool> HasOverlappingReservation(int carId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Reservation>> GetAllByOrderByCreateTime();
        Task<IEnumerable<Reservation>> FindAllForLast24Hours();
        Task<IEnumerable<Reservation>> FindAllForLast24HoursBefore24Hours();
        Task<IEnumerable<Reservation>> FindAllForLastWeek();
        Task<IEnumerable<Reservation>> FindAllForWeekBeforeLast();
        Task<IEnumerable<Reservation>> FindAllForLastMonth();
        Task<IEnumerable<Reservation>> FindAllForPreviousMonth();
        Task<double> TotalPriceForOnePeriodOfTime(IEnumerable<Reservation>list);
        Task<int> PercentagesOfDifferentPeriods(double firstPeriod, double lastPeriod);
    }
}
