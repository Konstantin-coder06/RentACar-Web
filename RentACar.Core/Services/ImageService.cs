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
    public class ImageService:IImageService
    {
        IRepository<Image> repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        CloudinaryService cloudinaryService;
        public ImageService(IRepository<Image> _repository,CloudinaryService cloudinaryService,  IWebHostEnvironment webHostEnvironment)
        {
            this.repository = _repository;
            _webHostEnvironment = webHostEnvironment;
            this.cloudinaryService = cloudinaryService;
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
            return repository.FindAll(x => x.CarId == carId).OrderBy(x=>x.Order).ToList();
        }

        public async Task ProcessImages(List<IFormFile> images, int carId,string imageOrder)
        {
            if (images == null || !images.Any())
                return;

            List<int> orderIndices = new List<int>();
            if (!string.IsNullOrEmpty(imageOrder))
            {
                orderIndices = imageOrder.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(int.Parse).ToList();
            }

            // Validate orderIndices matches the number of images
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
                        repository.Add(new Image
                        {
                            Url = imageUrl,
                            CarId = carId,
                            Order = order
                        });
                        order++;
                    }
                }
            }
            repository.Save();
        }
        public void Save()
        {
          repository.Save();
        }

        public void Update(Image entity)
        {
           repository.Update(entity);
        }

        public Image FindByid(int id)
        {
            return repository.FindOne(x => x.Id == id);
        }

        public Image ImageByCarId(int carid)
        {
            return repository.FindOne(x=>x.CarId == carid && x.Order==1);
        }
    }
}
