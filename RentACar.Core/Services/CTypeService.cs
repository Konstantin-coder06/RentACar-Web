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
    public class CTypeService : ICTypeService
    {
        IRepository<CType> repository;
        public CTypeService(IRepository<CType> repository)
        {
            this.repository = repository;
        }
        public void Add(CType entity)
        {
            repository.Add(entity);
        }

        public IEnumerable<CType> AllWithInclude(params Expression<Func<CType, object>>[] filters)
        {
           return repository.AllWithInclude(filters).ToList();
        }

        public void Delete(CType entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<CType> FindAll(Expression<Func<CType, bool>> predicate)
        {
            return repository.FindAll(predicate).ToList();
        }

        public CType FindOne(Expression<Func<CType, bool>> predicate)
        {
            return repository.FindOne(predicate);
        }

        public IEnumerable<CType> GetAll()
        {
           return repository.GetAll().ToList();
        }

        public void Save()
        {
            repository.Save();  
        }

        public void Update(CType entity)
        {
            repository.Update(entity);
        }
    }
}
