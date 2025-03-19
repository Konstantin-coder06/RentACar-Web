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
    }
}
