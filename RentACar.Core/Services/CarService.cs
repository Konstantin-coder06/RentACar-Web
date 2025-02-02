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
        public void Add(Car entity)
        {
            carRepository.Add(entity);
        }

        public IEnumerable<Car> AllWithInclude(params Expression<Func<Car, object>>[] filters)
        {
            return carRepository.AllWithInclude(filters).ToList();
        }

        public void Delete(Car entity)
        {
           carRepository.Delete(entity);
        }

        public IEnumerable<Car> FindAll(Expression<Func<Car, bool>> predicate)
        {
            return carRepository.FindAll(predicate).ToList();
        }

        public Car FindOne(Expression<Func<Car, bool>> predicate)
        {
           return carRepository.FindOne(predicate);
        }

        public IEnumerable<Car> GetAll()
        {
           return carRepository.GetAll().ToList();
        }

        public void Save()
        {
           carRepository.Save();
        }

        public void Update(Car entity)
        {
            carRepository.Update(entity);
        }
    }
}
