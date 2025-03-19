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

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
           
        }

        public async Task<IEnumerable<T>> AllWithInclude(params Expression<Func<T, object>>[] filters)
        {
            IQueryable<T> queries = dbSet;
            foreach(var  x in filters) 
            {
                queries=queries.Include(x);
            }
            return await queries.ToListAsync();
        }

        public void Delete(T entity)
        {
            dbContext.Remove(entity);
          
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
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
