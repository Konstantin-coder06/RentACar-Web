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
    public class CarFeatureService : ICarFeatureService
    {
        IRepository<CarFeature> carFeatureRepository;
        public CarFeatureService(IRepository<CarFeature> carFeatureRepository)
        {
            this.carFeatureRepository = carFeatureRepository;
        }
        public async Task Add(CarFeature entity)
        {
         await carFeatureRepository.Add(entity);
        }

        public async Task<IEnumerable<CarFeature>> AllWithInclude(params Expression<Func<CarFeature, object>>[] filters)
        {
           var feature= await carFeatureRepository.AllWithInclude(filters);
            return feature.ToList();
        }

        public void Delete(CarFeature entity)
        {
           carFeatureRepository.Delete(entity);
        }

        public async Task<IEnumerable<CarFeature>> FindAll(Expression<Func<CarFeature, bool>> predicate)
        {
            var feature= await carFeatureRepository.FindAll(predicate); 
            return feature.ToList();
        }

        public async Task<CarFeature> FindOne(Expression<Func<CarFeature, bool>> predicate)
        {
           return await carFeatureRepository.FindOne(predicate);
        }
        public async Task<bool> AnyAsync(Expression<Func<CarFeature, bool>> predicate)
        {
            return await carFeatureRepository.AnyAsync(predicate);
        }
        public async Task<IEnumerable<CarFeature>> GetAll()
        {
            var feature= await carFeatureRepository.GetAll();
            return feature.ToList();
        }

        public async Task<IEnumerable<int>> GetByCarIDAllFeatures(int carID)
        {

            var carFeatures = await carFeatureRepository.FindAll(x => x.CarId == carID);
            return carFeatures.Select(x => x.FeatureId).ToList();
        }

        public async Task Save()
        {
            await carFeatureRepository.Save();
        }

        public void Update(CarFeature entity)
        {
           carFeatureRepository.Update(entity);
        }

        public  async Task<int> CountAsync(Expression<Func<CarFeature, bool>> predicate)
        {
            return await carFeatureRepository.CountAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await carFeatureRepository.Count();    
        }

        public async Task<IEnumerable<CarFeature>> GetAllOrderBy(Expression<Func<CarFeature, object>> predicate)
        {
           return await carFeatureRepository.GetAllOrderBy(predicate);
        }

        public async Task<IEnumerable<CarFeature>> FindAllLimited(Expression<Func<CarFeature, bool>> predicate, int limit)
        {
            return await carFeatureRepository.FindAllLimited(predicate, limit);
        }

        public async Task<IEnumerable<Feature>> GetByCarIDAllFeatureNames(int carID)
        {
            var carFeatures = await carFeatureRepository.FindAll(x => x.CarId == carID);
            return carFeatures.Select(x => x.Feature).ToList();
        }

       
    }
}
