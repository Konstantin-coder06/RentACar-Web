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
            throw new NotImplementedException();
        }

        public IEnumerable<Image> AllWithInclude(params Expression<Func<Image, object>>[] filters)
        {
            throw new NotImplementedException();
        }

        public void Delete(Image entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Image> FindAll(Expression<Func<Image, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Image FindOne(Expression<Func<Image, bool>> predicate)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
