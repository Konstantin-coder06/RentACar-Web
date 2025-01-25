using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DataAccess.IRepository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        RentACarDbContext dbContext;
        DbSet<T> dbSet;
        public Repository(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }

        public IEnumerable<T> AllWithInclude(params Expression<Func<T, object>>[] filters)
        {
            IQueryable<T> queries = dbSet;
            foreach(var  x in filters) 
            {
                queries=queries.Include(x);
            }
            return queries.ToList();
        }

        public void Delete(T entity)
        {
            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Update(T entity)
        {
            dbContext.Update(entity);
            dbContext.SaveChanges();

        }
    }
}
