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
    public class CategoryService : ICategoryService
    {
        IRepository<Category> repository;
        public CategoryService(IRepository<Category> repository)
        {
            this.repository = repository;
        }
        public void Add(Category entity)
        {
            repository.Add(entity);
        }

        public IEnumerable<Category> AllWithInclude(params Expression<Func<Category, object>>[] filters)
        {
            return repository.AllWithInclude(filters).ToList();
        }

        public void Delete(Category entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<Category> FindAll(Expression<Func<Category, bool>> predicate)
        {
            return repository.FindAll(predicate).ToList();
        }

        public Category FindOne(Expression<Func<Category, bool>> predicate)
        {
            return repository.FindOne(predicate);
        }

        public IEnumerable<Category> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public void Update(Category entity)
        {
            repository.Update(entity);
        }
    }
}
