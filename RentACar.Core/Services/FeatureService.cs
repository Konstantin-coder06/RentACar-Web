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
    public class FeatureService : IFeatureService
    {
        IRepository<Feature> repository;
        public FeatureService(IRepository<Feature> repository)
        {
            this.repository = repository;
        }
        public async Task Add(Feature entity)
        {
            await repository.Add(entity);
        }

        public async Task<IEnumerable<Feature>> AllWithInclude(params Expression<Func<Feature, object>>[] filters)
        {
            var features=await repository.AllWithInclude(filters);
            return features.ToList();
        }

        public void Delete(Feature entity)
        {
            repository.Delete(entity);
        }

        public async Task<IEnumerable<Feature>> FindAll(Expression<Func<Feature, bool>> predicate)
        {
            var features = await repository.FindAll(predicate);
            return features.ToList();
        }

        public async Task<Feature> FindOne(Expression<Func<Feature, bool>> predicate)
        {
            return await repository.FindOne(predicate);
        }

        public async Task<IEnumerable<Feature>> GetAll()
        {
            var features = await repository.GetAll();
            return features.ToList();
        }



        public async Task Save()
        {
          await repository.Save();
        }
        public async Task<bool> AnyAsync(Expression<Func<Feature, bool>> predicate)
        {
            return await repository.AnyAsync(predicate);
        }
        public void Update(Feature entity)
        {
            repository.Update(entity);
        }

        public async Task<int> CountAsync(Expression<Func<Feature, bool>> predicate)
        {
           return await repository.CountAsync(predicate);   
        }

        public async Task<int> Count()
        {
            return await repository.Count();
        }

        public async Task<IEnumerable<Feature>> GetAllOrderBy(Expression<Func<Feature, object>> predicate)
        {
            return await repository.GetAllOrderBy(predicate);
        }

        public async Task<IEnumerable<Feature>> FindAllLimited(Expression<Func<Feature, bool>> predicate, int limit)
        {
            return await repository.FindAllLimited(predicate, limit);
        }

        public async Task<Feature> GetById(int id)
        {
            return await repository.FindOne(x=>x.Id == id);
        }

        public async Task<List<Feature>> GetAllFeaturesByIds(IEnumerable<int> ids)
        {
            Feature feature = new Feature();
            List<Feature> features = new List<Feature>();
            foreach (var x in ids)
            {
                feature = await GetById(x);
                features.Add(feature);
            }
            return features;
        }

        public async Task<bool> IsThereFeatureWithThisName(string featureName)
        {
            var feature = await repository.FindOne(x => x.NameOfFeatures.ToLower()==featureName.ToLower());
            if(feature == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
