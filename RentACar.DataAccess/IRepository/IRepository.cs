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
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindAll(Expression<Func<T,bool>> predicate);
        T FindOne(Expression<Func<T,bool>> predicate);
        IEnumerable<T> AllWithInclude(params Expression<Func<T, object>>[] filters);
        
    }
}
