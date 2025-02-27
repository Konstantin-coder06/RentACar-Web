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
        public void Add(CarCompany entity)
        {
            carCompanyRepository.Add(entity);
        }

        public IEnumerable<CarCompany> AllWithInclude(params Expression<Func<CarCompany, object>>[] filters)
        {
            throw new NotImplementedException();
        }

        public void Delete(CarCompany entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarCompany> FindAll(Expression<Func<CarCompany, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CarCompany FindOne(Expression<Func<CarCompany, bool>> predicate)
        {
            return carCompanyRepository.FindOne(predicate);
        }

        public IEnumerable<CarCompany> GetAll()
        {
            return carCompanyRepository.GetAll().ToList();
        }

        public CarCompany GetByUserId(string id)
        {
           return carCompanyRepository.FindOne(x=>x.UserId== id);
        }

        public void Save()
        {
            carCompanyRepository.Save();
        }

        public void Update(CarCompany entity)
        {
            throw new NotImplementedException();
        }
    }
}
