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
        public CarCompanyService(IRepository<CarCompany> carCompanyRepository)
        {
            this.carCompanyRepository = carCompanyRepository;
        }
        public async Task Add(CarCompany entity)
        {
            await carCompanyRepository.Add(entity);
        }

        public async Task<IEnumerable<CarCompany>> AllWithInclude(params Expression<Func<CarCompany, object>>[] filters)
        {
           var company=await carCompanyRepository.AllWithInclude(filters);
            return company.ToList();
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
    }
}
