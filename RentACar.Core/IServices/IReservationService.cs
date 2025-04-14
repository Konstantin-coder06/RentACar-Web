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
        Task<bool> HasOverlappingReservation(int carId, DateTime startDate, DateTime endDate, int? excludeReservationId = null);
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
        Task<List<(int CarId, int Count)>> GetTop10ReservedCarIds();
        Task<IEnumerable<Reservation>> GetAllIfItIsNotCompany(List<int>carIds);
        Task<double> DifferenceOfPriceBetweenTwoPeriods(double total, double totalPrev);
        Task<List<int>> GetAllReservatedCarsId(DateTime? start, DateTime? end);
        double TotalPriceForOneReservation(Reservation reservation, int days, double price,bool isSelfPick, bool isReturnAtSamePlace);
        Task<List<Car>> GetAllReservationsForCompany(IEnumerable<Car> companyCars);

        List<(int CarId, int Count)> GetCarReservationCounts(List<Reservation> reservations);
        Task<List<Reservation>> GetAllReservationsContaingCompanyIds(List<int> companyCarIds);
        Task<IEnumerable<Reservation>> GetAllByStartAndEndDate(DateTime? startDate, DateTime? endDate);
        Task<int> FindIfUserHasReservationForOnePeriod(DateTime? startDay, DateTime? endDay, int userId);
        Task<IEnumerable<Reservation>>GetReservationsByUserId(int userId);
        Task<string>GetStatusOfReservation(Reservation reservation);
        Task<(bool isReserved, DateTime? startDate, DateTime? endDate)> IsTheCarReservatedForToday(int carId);
        Task<Reservation>FindById(int id);
        Task<List<Reservation>>GetAllReservationOfCompanyCars(List<int> companyCarIds);
        Task<bool>IsCarReservationForTomorrow(int carId);
        Task<(DateTime startDate, DateTime endDate)> GetEarliestAvailableDates(int carId, int minimumDurationDays = 2);
        Task<List<Reservation>> GetAllReservationFilteredByStatus(IEnumerable<Reservation> reservations, string status);
        int CalculateStartDateDifference(Reservation reservation);
        int CalculateTotalDays(Reservation reservation);
        int TotalDaysByDates(DateTime startDate, DateTime endDate);
        Task<List<Reservation>> GetAllReservationByStatus(List<(Reservation Reservation, string Status)> reservationsStatuses, string filter);
        Task<List<Reservation>> GetAllStartEndDate(DateTime? startDay, DateTime? endDay);
    }
}
