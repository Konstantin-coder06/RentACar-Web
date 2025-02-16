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
            return repository.FindAll(x => x.CarId == carId).ToList();
        }

        public async Task ProcessImages(List<IFormFile> images, int carId)
        {
            // Use IWebHostEnvironment to get the web root path dynamically
            /*string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }*/

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    if (image.Length > 0)
                    {
                        var imageUrl = await cloudinaryService.UploadImageAsync(image);

                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            repository.Add(new Image
                            {
                                Url = imageUrl,  // Save Cloudinary URL
                                CarId = carId
                            });
                        }
                    }
                }
            }

            repository.Save(); // Assuming SaveAsync is implemented
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
    }
}
