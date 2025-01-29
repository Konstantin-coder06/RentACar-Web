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
    public class ImageService:IImageService
    {
        IRepository<Image> repository;
        public ImageService(IRepository<Image> _repository)
        {
            this.repository = _repository;
        }
        public void Add(Image entity)
        {
            repository.Add(entity);
        }

        public IEnumerable<Image> AllWithInclude(params Expression<Func<Image, object>>[] filters)
        {
            return repository.AllWithInclude(filters).ToList();
        }

        public void Delete(Image entity)
        {
            repository.Delete(entity);
        }

        public IEnumerable<Image> FindAll(Expression<Func<Image, bool>> predicate)
        {
            return repository.FindAll(predicate).ToList();
        }

        public Image FindOne(Expression<Func<Image, bool>> predicate)
        {
            return repository.FindOne(predicate);
        }

        public IEnumerable<Image> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public IEnumerable<Image> GetImagesByCarId(int carId)
        {
            return repository.FindAll(x => x.CarId == carId).ToList();
        }

        public void Update(Image entity)
        {
           repository.Update(entity);
        }
    }
}
