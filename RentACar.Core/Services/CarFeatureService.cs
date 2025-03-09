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
    public class CarFeatureService : ICarFeatureService
    {
        IRepository<CarFeature> carFeatureRepository;
        public CarFeatureService(IRepository<CarFeature> carFeatureRepository)
        {
            this.carFeatureRepository = carFeatureRepository;
        }
        public void Add(CarFeature entity)
        {
          carFeatureRepository.Add(entity);
        }

        public IEnumerable<CarFeature> AllWithInclude(params Expression<Func<CarFeature, object>>[] filters)
        {
           return carFeatureRepository.AllWithInclude(filters);
        }

        public void Delete(CarFeature entity)
        {
           carFeatureRepository.Delete(entity);
        }

        public IEnumerable<CarFeature> FindAll(Expression<Func<CarFeature, bool>> predicate)
        {
            return carFeatureRepository.FindAll(predicate).ToList(); 
        }

        public CarFeature FindOne(Expression<Func<CarFeature, bool>> predicate)
        {
           return carFeatureRepository.FindOne(predicate);
        }

        public IEnumerable<CarFeature> GetAll()
        {
            return carFeatureRepository.GetAll();
        }

        public IEnumerable<int> GetByCarIDAllFeatures(int carID)
        {
             
            return carFeatureRepository.FindAll(x => x.CarId == carID).Select(x=>x.FeatureId).ToList();
        }

        public void Save()
        {
            carFeatureRepository.Save();
        }

        public void Update(CarFeature entity)
        {
           carFeatureRepository.Update(entity);
        }
    }
}
