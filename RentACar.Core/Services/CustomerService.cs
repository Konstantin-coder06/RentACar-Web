﻿using Microsoft.AspNetCore.Identity;
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
using RentACar;
namespace RentACar.Core.Services
{
    public class CustomerService : ICustomerService
    {
        IRepository<Customer> repository;
        private readonly UserManager<IdentityUser> userManager;
        public CustomerService(IRepository<Customer> repository, UserManager<IdentityUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }
        public async Task Add(Customer entity)
        {
           await repository.Add(entity);
        }

        public IQueryable<Customer> AllWithInclude(params Expression<Func<Customer, object>>[] filters)
        {
            return repository.AllWithInclude(filters);
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
        public async Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await repository.AnyAsync(predicate);
        }
        public void Update(Customer entity)
        {
            repository.Update(entity);
        }

        public async Task<int> CountAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await repository.CountAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await repository.Count();
        }

        public async Task<IEnumerable<Customer>> GetAllOrderBy(Expression<Func<Customer, object>> predicate)
        {
            return await repository.GetAllOrderBy(predicate);
        }

        

        public async Task<IEnumerable<Customer>> FindAllLimited(Expression<Func<Customer, bool>> predicate, int limit)
        {
            return await repository.FindAllLimited(predicate, limit);
        }
        public async Task<List<(Customer customer, string email, string phoneNumber)>> GetCustomersWithEmailsAndPhoneNumbers()
        {
            var customers = await repository.GetAll();
            var result = new List<(Customer, string, string)>();
            foreach (var customer in customers)
            {
                var identityUser = await userManager.FindByIdAsync(customer.UserId);
                var email = identityUser?.Email;
                var phoneNumber = identityUser?.PhoneNumber;
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    phoneNumber = "-";
                }
                result.Add((customer, email, phoneNumber));
            }
            return result;
        }

        public async Task<Customer> FindById(int id)
        {
            return await repository.FindOne(x=>x.Id == id);
        }
    }
}
