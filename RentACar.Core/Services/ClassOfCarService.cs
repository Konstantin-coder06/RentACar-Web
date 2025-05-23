﻿using RentACar.Core.IServices;
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
        public async Task Add(ClassOfCar entity)
        {
           await repository.Add(entity);
        }

        public IQueryable<ClassOfCar> AllWithInclude(params Expression<Func<ClassOfCar, object>>[] filters)
        {
            return repository.AllWithInclude(filters);
        }

        public void Delete(ClassOfCar entity)
        {
            repository.Delete(entity);
        }

        public async Task<IEnumerable<ClassOfCar>> FindAll(Expression<Func<ClassOfCar, bool>> predicate)
        {
            var classOfCars = await repository.FindAll(predicate);
            return classOfCars.ToList();
        }

        public async Task<ClassOfCar> FindOne(Expression<Func<ClassOfCar, bool>> predicate)
        {
            return await repository.FindOne(predicate);
        }

        public async Task<IEnumerable<ClassOfCar>> GetAll()
        {
            var classOfCars = await repository.GetAll();
            return classOfCars.ToList();
        }

     
        public async Task<List<ClassOfCar>> GetClassOptionsAsync()
        {
            var classOfCars = await repository.GetAll();
            return classOfCars.ToList();
        }

        public void Update(ClassOfCar entity)
        {
            repository.Update(entity);
        }
        public async Task<bool> AnyAsync(Expression<Func<ClassOfCar, bool>> predicate)
        {
            return await repository.AnyAsync(predicate);
        }
        public async Task Save()
        {
            await repository.Save();
        }

        public async Task<int> CountAsync(Expression<Func<ClassOfCar, bool>> predicate)
        {
            return await repository.CountAsync(predicate);
        }

        public async Task<int> Count()
        {
            return await repository.Count();
        }

        public async Task<IEnumerable<ClassOfCar>> GetAllOrderBy(Expression<Func<ClassOfCar, object>> predicate)
        {
            return await repository.GetAllOrderBy(predicate);
        }

        public async Task<IEnumerable<ClassOfCar>> FindAllLimited(Expression<Func<ClassOfCar, bool>> predicate, int limit)
        {
            return await repository.FindAllLimited(predicate, limit);
        }

        public async Task<List<int>> GetAllClassSelectedIds(List<string> selectedCategories)
        {
            var result = await repository.FindAll(x => selectedCategories.Contains(x.Name));
            return  result.Select(x => x.Id).ToList();
        }
        public async Task<bool> IsThereClassWithThisName(string className)
        {
            var feature = await repository.FindOne(x => x.Name.ToLower() == className.ToLower());
            if (feature == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<ClassOfCar> GetClassOfCarById(int id)
        {
            return await repository.FindOne(x => x.Id == id);
        }
        public async Task<int> GetStandardId()
        {
            var type = await repository.FindOne(x => x.Name == "Standard");
            return type.Id;
        }
        public async Task<int> GetLuxuryId()
        {
            var type = await repository.FindOne(x => x.Name == "Luxury");
            return type.Id;
        }
        public async Task<int> GetEconomyId()
        {
            var type = await repository.FindOne(x => x.Name == "Economy");
            return type.Id;
        }
        public async Task<int> GetBusinessId()
        {
            var type = await repository.FindOne(x => x.Name == "Business");
            return type.Id;
        }
        public async Task<int> GetElectricId()
        {
            var type = await repository.FindOne(x => x.Name == "Electric");
            return type.Id;
        }
        public async Task<int> GetSportId()
        {
            var type = await repository.FindOne(x => x.Name == "Sport");
            return type.Id;
        }
    }
}
