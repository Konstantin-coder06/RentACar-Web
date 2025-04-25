using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.IServices;
using RentACar.DataAccess.IRepository;
using RentACar.DataAccess.IRepository.Repository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class CarService : ICarService
    {
        IRepository<Car> carRepository;
        public CarService(IRepository<Car> _carRepository)
        {
            this.carRepository = _carRepository;
        }
        public async Task Add(Car entity)
        {
           await carRepository.Add(entity);
        }

        public IQueryable<Car> AllWithInclude(params Expression<Func<Car, object>>[] filters)
        {
            return carRepository.AllWithInclude(filters);
        }

        public async Task<int> CountOfCarsWithCategory(int categoryId)
        {
            var cars = await carRepository.FindAll(x => x.ClassOfCarId == categoryId);
            return cars.Count();
        }

        public void Delete(Car entity)
        {
           carRepository.Delete(entity);
        }

        public async Task<IEnumerable<Car>> FindAll(Expression<Func<Car, bool>> predicate)
        {
            var cars=await carRepository.FindAll(predicate);
            return cars.ToList();
        }
        public async Task<bool> AnyAsync(Expression<Func<Car, bool>> predicate)
        {
            return await carRepository.AnyAsync(predicate);
        }
        public async Task<Car> FindOne(Expression<Func<Car, bool>> predicate)
        {
           return await carRepository.FindOne(predicate);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            var cars = await carRepository.GetAll();
            return cars.ToList();
        }

        public async Task<double> MinPriceOfCarByCategory(int categoryId)
        {
            var cars = await carRepository.FindAll(x => x.ClassOfCarId == categoryId);
            cars=cars.ToList();
            return cars.OrderBy(x => x.PricePerDay).Select(x => x.PricePerDay).First();
        }

        public async Task Save()
        {
           await carRepository.Save();
        }

        public void Update(Car entity)
        {
            carRepository.Update(entity);
        }

        public Task<int> PendingCarsCount()
        {
            return carRepository.CountAsync(x => x.Pending == true && x.Available == true);
        }
        public Task<int> PendingCompanyCarsCount(int companyId)
        {
            return carRepository.CountAsync(x=>x.CarCompanyId == companyId && x.Pending==true &&x.Available == true);
        }
        public Task<int> CountAsync(Expression<Func<Car, bool>> predicate)
        {
           return carRepository.CountAsync(predicate);
        }

        public Task<IEnumerable<Car>> GetAllOrderBy(Expression<Func<Car, object>> predicate)
        {
            return carRepository.GetAllOrderBy(predicate);
        }

        public async Task<IEnumerable<Car>> FindAllPendingCarsForAdmin()
        {
           var cars= await carRepository.FindAllLimited(x => x.Pending == true, 8);
            return cars.Where(x=>x.Available == true);
        }
        public async Task<IEnumerable<Car>> FindAllPendingCars()
        {

            return await carRepository.FindAll(x => x.Pending == true && x.Available==true);
        }

        public async Task<int> Count()
        {
            return await carRepository.Count();
        }
        public async Task<Car> FindById(int carId)
        {
            return await carRepository.FindOne(x => x.Id == carId);
        }

        public async Task<IEnumerable<Car>> FindAllLimited(Expression<Func<Car, bool>> predicate, int limit)
        {
            return await carRepository.FindAllLimited(predicate,limit);
        }
        public async Task<List<(string brand, string model, int count)>> GetTop10ReservedCars(List<(int CarId, int Count)> carIdsWithCounts)
        {
            var top10Cars = new List<(string brand, string model, int count)>();

            foreach (var carIdWithCount in carIdsWithCounts)
            {
                var car = await FindById(carIdWithCount.CarId); 
                if (car != null)
                {
                    top10Cars.Add((car.Brand, car.Model, carIdWithCount.Count));
                }
            }

            return top10Cars;
        }

        public async Task<IEnumerable<Car>> GetAllCarsOfCompany(int companyId)
        {
           return await carRepository.FindAll(x=>x.CarCompanyId == companyId);
        }

        public async Task<IEnumerable<Car>> GetAllNotReservetedAndNotPendingCars(List<int> reservedCarIds)
        {
            return await carRepository.FindAll(car => !reservedCarIds.Contains(car.Id) && !car.Pending);
        }

       

        public async Task SetCarUnavailable(int id)
        {
            var car = await FindById(id);
            if (car!=null)
            {
                car.Available = false;
                Update(car);
                await Save();
            }
           
        }

        public async Task<IEnumerable<Car>> GetAllPendingCompanyCars(int? companyId)
        {
            IEnumerable<Car> result=new List<Car>();
            if (companyId != null)
            {


                result = await carRepository.FindAll(x => x.Pending == true && x.CarCompanyId == companyId &&x.Available == true);
            }
            else
            {
                result = await carRepository.FindAll(x => x.Pending == true && x.Available == true);
            }
            return result.OrderBy(x => x.CreatedAt).ToList();
        }

        public async Task<double> GetPricePerDayByCarId(int carId)
        {
            var car= await carRepository.FindOne(x=>x.Id == carId);
            if (car != null)
            {
                return car.PricePerDay;
            }
            else
            {
                throw new Exception("The car is not located!");
            }
        }

        public async Task<IEnumerable<Car>> GetCarsBySearchBrandAndModel(string[] terms)
        {
            if (terms == null || !terms.Any())
            {
                return await carRepository.GetAll();
            }

            return await carRepository.FindAll(x =>
                terms.Any(term =>
                    (x.Brand != null && x.Brand.ToLower().Contains(term.ToLower())) ||
                    (x.Model != null && x.Model.ToLower().Contains(term.ToLower()))));
        }

        public async Task<IEnumerable<Car>> GetAllNotReservationedCarsForOnePeriod(IEnumerable<Car> cars, List<Reservation> reservations)
        {
            var reservedCarIds = reservations.Select(r => r.CarId).Distinct().ToList();

         
            if (cars is IQueryable<Car> queryableCars)
            {
                return await queryableCars
                    .Where(x => (reservedCarIds.Count == 0 || !reservedCarIds.Contains(x.Id)) && x.Pending == false)
                    .ToListAsync();
            }

            
            var carsList = cars.ToList();
            return carsList.Where(x =>
                (reservations.Count == 0 || !reservations.Any(r => r.CarId == x.Id)) &&
                x.Pending == false);
        }
        
        public async Task<IEnumerable<Car>> GetFilteredCarsAsync(double minPrice,double maxPrice, List<int> selectedClassIds, List<string> selectedBrands, 
            List<string> selectedColors, List<string> selectedDriveTrains)
        {
            var cars = (await GetAll()).AsQueryable();

            if (minPrice > 0)
                cars = cars.Where(x => x.PricePerDay >= minPrice);

            if (maxPrice > 0 && maxPrice > minPrice)
                cars = cars.Where(x => x.PricePerDay <= maxPrice);

            if (selectedClassIds?.Any() == true)
                cars = cars.Where(x => selectedClassIds.Contains(x.ClassOfCarId));

            if (selectedBrands?.Any() == true)
                cars = cars.Where(x => selectedBrands.Contains(x.Brand) && !x.Pending);

            if (selectedColors?.Any() == true)
                cars = cars.Where(x => selectedColors.Contains(x.Color) && !x.Pending);

            if (selectedDriveTrains?.Any() == true)
                cars = cars.Where(x => selectedDriveTrains.Contains(x.DriveTrain) && !x.Pending);

            return cars.ToList();
        }

        public async Task<List<string>> GetAllBrandsDistinct()
        {
            return (await carRepository.GetAll()).Select(x=>x.Brand).Distinct().ToList();
        }
        public async Task<List<string>> GetAllColorsDistinct()
        {
            return (await carRepository.GetAll()).Select(x => x.Color).Distinct().ToList();
        }
        public async Task<List<string>> GetAllDriveTrainsDistinct()
        {
            return (await carRepository.GetAll()).Select(x => x.DriveTrain).Distinct().ToList();
        }

        public async Task<int> GetCompanyIdByCarId(int carId)
        {
            var car = await carRepository.FindOne(x => x.Id == carId);
            return car.CarCompanyId;
        }

        public async Task<IEnumerable<Car>> GetFilteredCarsOfCompanyAsync(int companyId,double minPrice, double maxPrice, List<int> selectedClassIds, List<string> selectedBrands, List<string> selectedColors, List<string> selectedDriveTrains)
        {
            var cars = (await FindAll(x=>x.CarCompanyId==companyId)).AsQueryable();

            if (minPrice > 0)
                cars = cars.Where(x => x.PricePerDay >= minPrice);

            if (maxPrice > 0 && maxPrice > minPrice)
                cars = cars.Where(x => x.PricePerDay <= maxPrice);

            if (selectedClassIds?.Any() == true)
                cars = cars.Where(x => selectedClassIds.Contains(x.ClassOfCarId));

            if (selectedBrands?.Any() == true)
                cars = cars.Where(x => selectedBrands.Contains(x.Brand) && !x.Pending);

            if (selectedColors?.Any() == true)
                cars = cars.Where(x => selectedColors.Contains(x.Color) && !x.Pending);

            if (selectedDriveTrains?.Any() == true)
                cars = cars.Where(x => selectedDriveTrains.Contains(x.DriveTrain) && !x.Pending);

            return cars.ToList();
        }

        public async Task<IEnumerable<Car>> GetCarsOfCompanyBySearchBrandAndModel(int companyId, string[] terms)
        {
            if (terms == null || !terms.Any())
            {
                return await carRepository.FindAll(x => x.CarCompanyId == companyId);
            }

            return await carRepository.FindAll(x =>
                terms.All(term =>
                    (x.CarCompanyId == companyId && x.Brand != null && x.Brand.ToLower().Contains(term.ToLower())) ||
                    (x.CarCompanyId == companyId && x.Model != null && x.Model.ToLower().Contains(term.ToLower()))));
        }
        public async Task<List<int>> GetAllCarsIdByCompanyId(int companyId)
        {
            return (await carRepository.FindAll(x => x.CarCompanyId == companyId)).Select(x=>x.Id).ToList();
        }
        public async Task<List<Car>> SearchCarsByBrandOrModel(int companyId, string[] terms)
        {
            if (terms == null || !terms.Any())
            {
                return (await carRepository.FindAll(x => x.CarCompanyId == companyId)).ToList();
            }

            var searchTerms = terms
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.ToLower().Trim())
                .ToArray();

            if (!searchTerms.Any())
            {
                return (await carRepository.FindAll(x => x.CarCompanyId == companyId)).ToList();
            }
            return (await carRepository.FindAll(x =>
                x.CarCompanyId == companyId &&
                
                searchTerms.All(term =>
                    (x.Brand != null && x.Brand.ToLower().Contains(term)) ||
                    (x.Model != null && x.Model.ToLower().Contains(term)))))
                .ToList();
        }

        public double TotalPriceOfCar(double price, int days)
        {
            return price * days;
        }

        public double PriceOfTaxes(double price)
        {
            return price * 0.09;
        }

        public double Difference(double price, double second)
        {
            return price - second;
        }

        public async Task<IEnumerable<Car>> FilterCarsByCategories(IEnumerable<Car> cars, List<int> categoriesIds)
        {
           return  cars.Where(x => categoriesIds.Contains(x.ClassOfCarId)).ToList();
        }

        public async Task<List<Car>> OrderByPrice(List<Car> cars)
        {
            return cars.OrderBy(x=>x.PricePerDay).ToList();
        }
        public async Task<List<Car>> OrderByDescendingPrice(List<Car> cars)
        {
            return cars.OrderByDescending(x => x.PricePerDay).ToList();
        }
        public async Task<List<Car>> OrderByBrand(List<Car> cars)
        {
            return cars.OrderBy(x => x.Brand).ToList();
        }
        public async Task<List<Car>> OrderByDescendingBrand(List<Car> cars)
        {
            return cars.OrderByDescending(x => x.Brand).ToList();
        }
        public async Task<List<Car>> OrderByYear(List<Car> cars)
        {
            return cars.OrderBy(x => x.Year).ToList();
        }
        public async Task<List<Car>> OrderByDescendingYear(List<Car> cars)
        {
            return cars.OrderByDescending(x => x.Year).ToList();
        }
    }
}
