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
        public void Add(Customer entity)
        {
            repository.Add(entity);
        }

        public IEnumerable<Customer> AllWithInclude(params Expression<Func<Customer, object>>[] filters)
        {
            return repository.AllWithInclude(filters).ToList();
        }

        public void Delete(Customer entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<Customer> FindAll(Expression<Func<Customer, bool>> predicate)
        {
            return repository.FindAll(predicate).ToList();
        }

        public Customer FindOne(Expression<Func<Customer, bool>> predicate)
        {
            return repository.FindOne(predicate);
        }

        public IEnumerable<Customer> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public void Save()
        {
           repository.Save();
        }

        public void Update(Customer entity)
        {
            repository.Update(entity);
        }
    }
}
