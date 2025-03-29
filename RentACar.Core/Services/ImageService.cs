using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class ImageService : IImageService
    {
        IRepository<Image> repository;
        CloudinaryService cloudinaryService;
        public ImageService(IRepository<Image> _repository,CloudinaryService cloudinaryService)
        {
            this.repository = _repository;
            this.cloudinaryService = cloudinaryService;
        }
        public async Task Add(Image entity)
        {
           await  repository.Add(entity);
        }

        public async Task<IEnumerable<Image>> AllWithInclude(params Expression<Func<Image, object>>[] filters)
        {
            var images = await repository.AllWithInclude(filters);
            return images.ToList();
        }

        public void Delete(Image entity)
        {
            repository.Delete(entity);
        }

        public async Task<IEnumerable<Image>> FindAll(Expression<Func<Image, bool>> predicate)
        {
            var images=await repository.FindAll(predicate);
            return images.ToList();
        }

        public async Task<Image> FindOne(Expression<Func<Image, bool>> predicate)
        {
           return await repository.FindOne(predicate);
        }

        public async Task<IEnumerable<Image>> GetAll()
        {
            var images=await repository.GetAll();
            return images.ToList();
        }

        public async Task<IEnumerable<Image>> GetImagesByCarId(int carId)
        {
            var images =await repository.FindAll(x => x.CarId == carId);
            return images.OrderBy(x=>x.Order).ToList();
        }

        public async Task ProcessImages(List<IFormFile> images, int carId,string imageOrder)
        {
            if (images == null || !images.Any())
                return;
            List<int> orderIndices = new List<int>();
            if (!string.IsNullOrEmpty(imageOrder))
            {
                orderIndices = imageOrder.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            }
            if (orderIndices.Count != images.Count || orderIndices.Any(i => i < 0 || i >= images.Count))
            {
                orderIndices = Enumerable.Range(0, images.Count).ToList();
            }

            int order = 1;
            foreach (var index in orderIndices)
            {
                var file = images[index];
                if (file.Length > 0)
                {
                    var imageUrl = await cloudinaryService.UploadImageAsync(file);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        await repository.Add(new Image
                        {
                            Url = imageUrl,
                            CarId = carId,
                            Order = order
                        });
                        order++;
                    }
                }
            }
            await repository.Save();
        }
        public async Task Save()
        {
          await repository.Save();
        }

        public void Update(Image entity)
        {
           repository.Update(entity);
        }

        public async Task<Image> FindByid(int id)
        {
            return await repository.FindOne(x => x.Id == id);
        }

        public async Task<Image> ImageByCarId(int carid)
        {
            return await repository.FindOne(x=>x.CarId == carid && x.Order==1);
        }

        public async Task<bool> AnyAsync(Expression<Func<Image, bool>> predicate)
        {
            return await repository.AnyAsync(predicate);
        }

        public Task<int> CountAsync(Expression<Func<Image, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Image>> GetAllOrderBy(Expression<Func<Image, object>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Image>> FindAllLimited(Expression<Func<Image, bool>> predicate, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Image>> GetImagesOrderByOrderCarId(int carId)
        {
            var result = await GetImagesByCarId(carId);
           return result.OrderBy(x => x.Order).ToList();
        }
    }
}
