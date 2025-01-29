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
        public void Add(Feature entity)
        {
            repository.Add(entity);
        }

        public IEnumerable<Feature> AllWithInclude(params Expression<Func<Feature, object>>[] filters)
        {
            return repository.AllWithInclude(filters).ToList();
        }

        public void Delete(Feature entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<Feature> FindAll(Expression<Func<Feature, bool>> predicate)
        {
            return repository.FindAll(predicate).ToList();
        }

        public Feature FindOne(Expression<Func<Feature, bool>> predicate)
        {
            return repository.FindOne(predicate);
        }

        public IEnumerable<Feature> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public void Update(Feature entity)
        {
            repository.Update(entity);
        }
    }
}
