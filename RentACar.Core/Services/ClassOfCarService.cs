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
    public class ClassOfCarService : IClassOfCarService
    {
        IRepository<ClassOfCar> repository;
        public ClassOfCarService(IRepository<ClassOfCar> repository)
        {
            this.repository = repository;
        }
        public void Add(ClassOfCar entity)
        {
            repository.Add(entity);
        }

        public IEnumerable<ClassOfCar> AllWithInclude(params Expression<Func<ClassOfCar, object>>[] filters)
        {
            return repository.AllWithInclude(filters).ToList();
        }

        public void Delete(ClassOfCar entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<ClassOfCar> FindAll(Expression<Func<ClassOfCar, bool>> predicate)
        {
            return repository.FindAll(predicate).ToList();
        }

        public ClassOfCar FindOne(Expression<Func<ClassOfCar, bool>> predicate)
        {
            return repository.FindOne(predicate);
        }

        public IEnumerable<ClassOfCar> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public void Update(ClassOfCar entity)
        {
            repository.Update(entity);
        }
    }
}
