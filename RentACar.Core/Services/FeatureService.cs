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
    public class FeatureService : IFeatureService
    {
        IRepository<Feature> repository;
        public FeatureService(IRepository<Feature> repository)
        {
            this.repository = repository;
        }
        public async Task Add(Feature entity)
        {
            await repository.Add(entity);
        }

        public async Task<IEnumerable<Feature>> AllWithInclude(params Expression<Func<Feature, object>>[] filters)
        {
            var features=await repository.AllWithInclude(filters);
            return features.ToList();
        }

        public void Delete(Feature entity)
        {
            repository.Delete(entity);
        }

        public async Task<IEnumerable<Feature>> FindAll(Expression<Func<Feature, bool>> predicate)
        {
            var features = await repository.FindAll(predicate);
            return features.ToList();
        }

        public async Task<Feature> FindOne(Expression<Func<Feature, bool>> predicate)
        {
            return await repository.FindOne(predicate);
        }

        public async Task<IEnumerable<Feature>> GetAll()
        {
            var features = await repository.GetAll();
            return features.ToList();
        }



        public async Task Save()
        {
          await repository.Save();
        }

        public void Update(Feature entity)
        {
            repository.Update(entity);
        }
    }
}
