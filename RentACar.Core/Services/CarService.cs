using RentACar.Core.IServices;
using RentACar.DataAccess.IRepository;
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
    }
}
