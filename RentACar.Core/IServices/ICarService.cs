﻿using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.IServices
{
    public interface ICarService:IRepository<Car>
    {
        Task<int> CountOfCarsWithCategory(int categoryId);  
        Task<double> MinPriceOfCarByCategory(int categoryId);
        Task<IEnumerable<Car>> FindAllPendingCarsForAdmin();
        Task<IEnumerable<Car>> FindAllPendingCars();
        Task<int> PendingCarsCount();
        Task<List<(string brand, string model, int count)>> GetTop10ReservedCars(List<int> carIds);
        Task<IEnumerable<Car>> GetAllCarsOfCompany(int companyId);
        Task<IEnumerable<Car>> GetAllNotReservetedAndNotPendingCars(List<int> reservedCarIds);
        Task<Car>FindById(int carId);
        Task SetCarUnavailable(int id);
        Task<IEnumerable<Car>> GetAllPendingCompanyCars(int? companyId);
        Task<double>GetPricePerDayByCarId(int carId);
        Task<IEnumerable<Car>> GetCarsBySearchBrandAndModel(string[] terms);
        Task<IEnumerable<Car>> GetAllNotReservationedCarsForOnePeriod(IEnumerable<Car> cars,List<Reservation> reservations);
        Task<IEnumerable<Car>> GetFilteredCarsAsync(double minPrice, double maxPrice, List<int> selectedClassIds, List<string> selectedBrands, List<string> selectedColors, List<string> selectedDriveTrains);
        Task<List<string>> GetAllBrandsDistinct();
        Task<List<string>> GetAllColorsDistinct();
        Task<List<string>> GetAllDriveTrainsDistinct();
        Task<int> GetCompanyIdByCarId(int carId);
        
    }
}
