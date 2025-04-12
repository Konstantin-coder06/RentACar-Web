using CloudinaryDotNet;
using Moq;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Tests
{
    public class ImageTests
    {
        private Mock<IRepository<Image>> mockRepository;
        private ImageService imageService;
        private Mock<ICloudinaryService> cloudinaryService;
        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<Image>>();
            cloudinaryService = new Mock<ICloudinaryService>();
            imageService = new ImageService(mockRepository.Object,cloudinaryService.Object);
        }

        [Test]
        public async Task AddImageTest()
        {

            var image = new Image { Id = 1 };
            await imageService.Add(image);
            mockRepository.Verify(r => r.Add(image), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var image = new Image { Id = 1 };
            imageService.Update(image);
            mockRepository.Verify(r => r.Update(image), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await imageService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedImage()
        {
            var expectedImages = new List<Image>
        {
            new Image { Id = 1,  CarId=4},
            new Image { Id = 2,  CarId=2}
        }.AsQueryable();

            Expression<Func<Image, object>>[] filters = { c => c.CarId };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Image, object>>[]>())).Returns(expectedImages);
            var result = imageService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedImages, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.CarId == 4));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.CarId == 2));

        }
        [Test]
        public async Task GetAll_ReturnsAllImages()
        {
            var images = new List<Image> { new Image { Id = 1 }, new Image { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(images);
            var result = await imageService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredImages()
        {
            var images = new List<Image>
            {
                new Image { Id = 1, CarId=2 },
                new Image { Id = 2, CarId=1 },
                new Image { Id = 3, CarId=2 }
            };

            mockRepository.Setup(r => r.FindAll(c => c.CarId == 2)).ReturnsAsync(images.Where(c => c.CarId == 2));

            var result = await imageService.FindAll(c => c.CarId == 2);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.CarId == 2));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedImages()
        {
            var images = new List<Image> { new Image { Id = 2 }, new Image { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(images.OrderBy(c => c.Id));
            var result = await imageService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await imageService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.CarId == 3)).ReturnsAsync(3);
            int count = await imageService.CountAsync(c => c.CarId == 3);
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedImages(int limit)
        {
            var images = new List<Image> { new Image { Id = 1 }, new Image { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(images);

            var result = await imageService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var image = new Image { Id = 1, CarId = 1 };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(image);
            var result = await imageService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.CarId == 1);
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var Image = new Image { Id = 1 };
            imageService.Delete(Image);
            mockRepository.Verify(r => r.Delete(Image), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.CarId == 3)).ReturnsAsync(true);
            bool result = await imageService.AnyAsync(c => c.CarId == 3);
            Assert.IsTrue(result);
        }
    }
}
