using Microsoft.AspNetCore.Cors.Infrastructure;
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

        public async Task<IEnumerable<Car>> AllWithInclude(params Expression<Func<Car, object>>[] filters)
        {
            var cars= await carRepository.AllWithInclude(filters);
            return cars.ToList();
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
            return carRepository.CountAsync(x => x.Pending == true);
        }

        public Task<int> CountAsync(Expression<Func<Car, bool>> predicate)
        {
           return carRepository.CountAsync(predicate);
        }

        public Task<IEnumerable<Car>> GetAllOrderBy(Expression<Func<Car, object>> predicate)
        {
            return carRepository.GetAllOrderBy(predicate);
        }

        public async Task<IEnumerable<Car>> FindAllPendingCars()
        {

            return await carRepository.FindAllLimited(x => x.Pending == true, 8);
        }

        public async Task<int> Count()
        {
            return await carRepository.Count();
        }

        public async Task<IEnumerable<Car>> FindAllLimited(Expression<Func<Car, bool>> predicate, int limit)
        {
            return await carRepository.FindAllLimited(predicate,limit);
        }
        public async Task<List<(string brand, string model, int count)>> GetTop10ReservedCars(List<int> carIds)
        {
            var topCars = new List<(string brand, string model, int count)>();
            
            foreach (var carId in carIds)
            {
                var car = await carRepository.FindOne(cr => cr.Id == carId);
                var brand=car.Brand;
                var model=car.Model;
                var count = carIds.Count(id => id == carId);
                topCars.Add((brand, model, count));
            }

            return topCars;
        }

        public async Task<IEnumerable<Car>> GetAllCarsOfCompany(int companyId)
        {
           return await carRepository.FindAll(x=>x.CarCompanyId == companyId);
        }

        public async Task<IEnumerable<Car>> GetAllNotReservetedAndNotPendingCars(List<int> reservedCarIds)
        {
            return await carRepository.FindAll(car => !reservedCarIds.Contains(car.Id) && !car.Pending);
        }

        public async Task<Car> FindById(int carId)
        {
            return await carRepository.FindOne(x=>x.Id== carId);
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


                result = await carRepository.FindAll(x => x.Pending == true && x.CarCompanyId == companyId);
            }
            else
            {
                result = await carRepository.FindAll(x => x.Pending == true);
            }
            return result.OrderBy(x => x.CreatedAt).ToList();
        }
    }
}
