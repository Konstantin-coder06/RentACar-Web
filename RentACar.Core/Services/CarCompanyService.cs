using Microsoft.AspNetCore.Identity;
using RentACar.Core.IServices;
using RentACar.DataAccess.IRepository;
using RentACar.DataAccess.IRepository.Repository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class CarCompanyService : ICarCompanyService
    {
        IRepository<CarCompany> carCompanyRepository;
        private readonly UserManager<IdentityUser> userManager;
        public CarCompanyService(IRepository<CarCompany> carCompanyRepository, UserManager<IdentityUser>userManager)
        {
            this.carCompanyRepository = carCompanyRepository;
            this.userManager = userManager;
        }
        public async Task Add(CarCompany entity)
        {
            await carCompanyRepository.Add(entity);
        }

        public IQueryable<CarCompany> AllWithInclude(params Expression<Func<CarCompany, object>>[] filters)
        {
            return carCompanyRepository.AllWithInclude(filters);
        }

        public void Delete(CarCompany entity)
        {
            carCompanyRepository.Delete(entity);
        }

        public async Task<IEnumerable<CarCompany>> FindAll(Expression<Func<CarCompany, bool>> predicate)
        {
           var company= await carCompanyRepository.FindAll(predicate);
            return company.ToList();
        }

        public async Task<CarCompany> FindOne(Expression<Func<CarCompany, bool>> predicate)
        {
            return await carCompanyRepository.FindOne(predicate);
        }
        public async Task<bool> AnyAsync(Expression<Func<CarCompany, bool>> predicate)
        {
            return await carCompanyRepository.AnyAsync(predicate);
        }
        public async Task<IEnumerable<CarCompany>> GetAll()
        {
            var company= await carCompanyRepository.GetAll();
            return company.ToList();
        }

        public async Task<CarCompany> GetByUserId(string id)
        {
           return await carCompanyRepository.FindOne(x=>x.UserId== id);
        }

        public async Task Save()
        {
           await carCompanyRepository.Save();
        }

        public void Update(CarCompany entity)
        {
            carCompanyRepository.Update(entity);
        }

        public async Task<int> CountAsync(Expression<Func<CarCompany, bool>> predicate)
        {
            return await carCompanyRepository.CountAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await carCompanyRepository.Count();
        }

        public async Task<IEnumerable<CarCompany>> GetAllOrderBy(Expression<Func<CarCompany, object>> predicate)
        {
           return await carCompanyRepository.GetAllOrderBy(predicate);
        }

        public async Task<IEnumerable<CarCompany>> FindAllLimited(Expression<Func<CarCompany, bool>> predicate, int limit)
        {
           return await carCompanyRepository.FindAllLimited(predicate, limit);
        }

        public async Task<CarCompany> GetById(int? id)
        {
            return await carCompanyRepository.FindOne(x=>x.Id==id);
        }

        public async Task<string> GetNameById(int id)
        {
            var company=await carCompanyRepository.FindOne(x=>x.Id == id);
            return company.Name;

        }
        public async Task<string> GetAddressById(int id)
        {
            var company = await carCompanyRepository.FindOne(x => x.Id == id);
            return company.Address;

        }

        public async Task<string> GetCompanyEmail(int id)
        {
            var carCompany = await carCompanyRepository.FindOne(x=>x.Id==id);
            var identityUser = await userManager.FindByIdAsync(carCompany.UserId);
            var email = identityUser?.Email;
            return email;
        }
       
    }
}
