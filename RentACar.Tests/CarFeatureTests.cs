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
    public class CarFeatureTests
    {
        private Mock<IRepository<CarFeature>> mockRepository;
        private CarFeatureService carFeatureService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<CarFeature>>();
            carFeatureService = new CarFeatureService(mockRepository.Object);
        }

        [Test]
        public async Task AddCarFeatureTest()
        {

            var carFeature = new CarFeature { Id = 1};
            await carFeatureService.Add(carFeature);
            mockRepository.Verify(r => r.Add(carFeature), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var carFeature = new CarFeature { Id = 1 };
            carFeatureService.Update(carFeature);
            mockRepository.Verify(r => r.Update(carFeature), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await carFeatureService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedCarFeature()
        {
            var expectedCarFeatures = new List<CarFeature>
        {
            new CarFeature { Id = 1,  CarId=4},
            new CarFeature { Id = 2,  CarId=2}
        }.AsQueryable();

            Expression<Func<CarFeature, object>>[] filters = { c => c.CarId };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<CarFeature, object>>[]>())).Returns(expectedCarFeatures);
            var result =  carFeatureService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedCarFeatures, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.CarId==4));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.CarId==2));

        }
        [Test]
        public async Task GetAll_ReturnsAllCarFeatures()
        {
            var carFeatures = new List<CarFeature> { new CarFeature { Id = 1 }, new CarFeature { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(carFeatures);
            var result = await carFeatureService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredCarFeatures()
        {
            var carFeatures = new List<CarFeature>
            {
                new CarFeature { Id = 1, CarId=2 },
                new CarFeature { Id = 2, CarId=1 },
                new CarFeature { Id = 3, CarId=2 }
            };

            mockRepository.Setup(r => r.FindAll(c => c.CarId==2)).ReturnsAsync(carFeatures.Where(c => c.CarId==2));

            var result = await carFeatureService.FindAll(c => c.CarId==2);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.CarId==2));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedCarFeatures()
        {
            var carFeatures = new List<CarFeature> { new CarFeature { Id = 2 }, new CarFeature { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(carFeatures.OrderBy(c => c.Id));
            var result = await carFeatureService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await carFeatureService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.FeatureId==3)).ReturnsAsync(3);
            int count = await carFeatureService.CountAsync(c => c.FeatureId==3);
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedCarFeatures(int limit)
        {
            var carFeatures = new List<CarFeature> { new CarFeature { Id = 1 }, new CarFeature { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(carFeatures);

            var result = await carFeatureService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var carFeature = new CarFeature { Id = 1, CarId=1 };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(carFeature);
            var result = await carFeatureService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.CarId == 1);
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var carFeature = new CarFeature { Id = 1 };
            carFeatureService.Delete(carFeature);
            mockRepository.Verify(r => r.Delete(carFeature), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.FeatureId==3)).ReturnsAsync(true);
            bool result = await carFeatureService.AnyAsync(c => c.FeatureId==3);
            Assert.IsTrue(result);
        }
        [TestCase(1)]
        public async Task GetByCarIDAllFeatures_ReturnsFeatureIdsForGivenCarId(int carId)
        {
            var carFeatures = new List<CarFeature>
            {
                new CarFeature { CarId = carId, FeatureId = 10 },
                new CarFeature { CarId = carId, FeatureId = 20 },
                new CarFeature { CarId = 2, FeatureId = 30 } 
            };

            mockRepository.Setup(r => r.FindAll(x => x.CarId == carId)).ReturnsAsync(carFeatures.Where(x => x.CarId == carId));

            var expectedFeatureIds = new List<int> { 10, 20 };
            var result = await carFeatureService.GetByCarIDAllFeatures(carId);

           
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.That(result.OrderBy(x => x), Is.EqualTo(expectedFeatureIds.OrderBy(x => x))); 
            mockRepository.Verify(r => r.FindAll(x => x.CarId == carId), Times.Once());
        }
    }
}
