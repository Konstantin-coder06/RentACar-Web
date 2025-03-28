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
        Task<IEnumerable<Reservation>> FindAllForLast24HoursCompany(List<int>companyCarIds);
        Task<IEnumerable<Reservation>> FindAllForLast24HoursBefore24HoursCompany(List<int> companyCarIds);
        Task<IEnumerable<Reservation>> FindAllForLastWeekCompany(List<int> companyCarIds);
        Task<IEnumerable<Reservation>> FindAllForWeekBeforeLastCompany(List<int> companyCarIds);
        Task<IEnumerable<Reservation>> FindAllForLastMonthCompany(List<int> companyCarIds);
        Task<IEnumerable<Reservation>> FindAllForPreviousMonthCompany(List<int> companyCarIds);
        Task<double> TotalPriceForOnePeriodOfTime(IEnumerable<Reservation>list);
        Task<int> PercentagesOfDifferentPeriods(double firstPeriod, double lastPeriod);
        Task<List<(Customer customer, string brand, string model, Image image)>> GetCustomersWithReservedCars();
        Task<IEnumerable<Reservation>> GetAllReservationsByStartDate(DateTime startDate);
        Task<List<int>> GetTop10ReservedCarIdsByStartDate(DateTime startDate,List<int>carIds);
        Task<IEnumerable<Reservation>> GetAllIfItIsNotCompany(List<int>carIds);
        Task<double> DifferenceOfPriceBetweenTwoPeriods(double total, double totalPrev);

    }
}
