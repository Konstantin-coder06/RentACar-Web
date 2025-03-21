using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DataAccess.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> FindAll(Expression<Func<T,bool>> predicate);
        Task<T> FindOne(Expression<Func<T,bool>> predicate);
        Task<IEnumerable<T>> AllWithInclude(params Expression<Func<T, object>>[] filters);
        Task Save();
        Task<bool> AnyAsync(Expression<Func<T,bool>>predicate);
        
    }
}
