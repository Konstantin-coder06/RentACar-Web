using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
           
        }

        public IQueryable<T> AllWithInclude(params Expression<Func<T, object>>[] filters)
        {
            IQueryable<T> queries = dbSet;
            foreach (var x in filters)
            {
                queries = queries.Include(x);
            }
            return queries;
        }

        public async Task<bool> AnyAsync(Expression<Func<T,bool>>predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.CountAsync(predicate);
        }

        public void Delete(T entity)
        {
            dbContext.Remove(entity);
          
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            return await  dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllLimited(Expression<Func<T, bool>> predicate, int limit)
        {
            return await dbSet.Where(predicate).Take(limit).ToListAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllOrderBy(Expression<Func<T, object>> predicate)
        {
            return await dbSet.OrderBy(predicate).ToListAsync();
        }

        public async Task Save()
        {
          await dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
           dbContext.Update(entity); 
    
        }
    }
}
