using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class CustomerService : ICustomerService
    {
        IRepository<Customer> repository;
        public CustomerService(IRepository<Customer> repository)
        {
            this.repository = repository;
        }
        public async Task Add(Customer entity)
        {
           await repository.Add(entity);
        }

        public async Task<IEnumerable<Customer>> AllWithInclude(params Expression<Func<Customer, object>>[] filters)
        {
            var customers= await repository.AllWithInclude(filters);
            return customers.ToList();
        }

        public void Delete(Customer entity)
        {
            repository.Delete(entity);
        }

        public async Task<IEnumerable<Customer>> FindAll(Expression<Func<Customer, bool>> predicate)
        {
            var customers=await repository.FindAll(predicate);
            return customers.ToList();
        }

        public async Task<Customer> FindOne(Expression<Func<Customer, bool>> predicate)
        {
            return await repository.FindOne(predicate);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers=await repository.GetAll();
            return customers.ToList();
        }

        public async Task<Customer> GetByReport(int id)
        {
            return await repository.FindOne(x=>x.Id == id);
        }

        public async Task<Customer> GetByUserId(string userId)
        {
            return await repository.FindOne(x=>x.UserId==userId);
        }

        public async Task Save()
        {
           await repository.Save();
        }

        public void Update(Customer entity)
        {
            repository.Update(entity);
        }
    }
}
