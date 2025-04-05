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
        public async Task Add(CType entity)
        {
           await repository.Add(entity);
        }

        public async Task<IEnumerable<CType>> AllWithInclude(params Expression<Func<CType, object>>[] filters)
        {
            var cType = await repository.AllWithInclude(filters);
            return cType.ToList();
        }

        public void Delete(CType entity)
        {
            repository.Delete(entity);
        }

        public async Task<IEnumerable<CType>> FindAll(Expression<Func<CType, bool>> predicate)
        {
            var cType = await repository.FindAll(predicate);
            return cType.ToList();
        }

        public async Task<CType> FindOne(Expression<Func<CType, bool>> predicate)
        {
            return await repository.FindOne(predicate);
        }

        public async Task<IEnumerable<CType>> GetAll()
        {
            var cType = await repository.GetAll();
            return cType.ToList();
        }

        public async Task Save()
        {
           await repository.Save();  
        }
        public async Task<bool> AnyAsync(Expression<Func<CType, bool>> predicate)
        {
            return await repository.AnyAsync(predicate);
        }
        public void Update(CType entity)
        {
            repository.Update(entity);
        }

        public async Task<int> CountAsync(Expression<Func<CType, bool>> predicate)
        {
            return await repository.CountAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await repository.Count();
        }

        public async Task<IEnumerable<CType>> GetAllOrderBy(Expression<Func<CType, object>> predicate)
        {
            return await repository.GetAllOrderBy(predicate);
        }

        public async Task<IEnumerable<CType>> FindAllLimited(Expression<Func<CType, bool>> predicate, int limit)
        {
            return await repository.FindAllLimited(predicate, limit);
        }
        public async Task<bool> IsThereTypeWithThisName(string typeName, int seatCapacity)
        {
            var type = await repository.FindOne(x => x.Name.ToLower() == typeName.ToLower() && x.SeatCapacity==seatCapacity);
            if (type == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
