using Moq;
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
    public class FeatureTests
    {
        private Mock<IRepository<Feature>> mockRepository;
        private FeatureService featureService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<Feature>>();
            featureService = new FeatureService(mockRepository.Object);
        }

        [Test]
        public async Task AddFeatureTest()
        {

            var feature = new Feature { Id = 1, NameOfFeatures="Head-up display" };
            await featureService.Add(feature);
            mockRepository.Verify(r => r.Add(feature), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var feature = new Feature { Id = 1 };
            featureService.Update(feature);
            mockRepository.Verify(r => r.Update(feature), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await featureService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedFeatures()
        {
            var expectedFeatures = new List<Feature>
        {
            new Feature { Id = 1,  NameOfFeatures = "Distronic"},
            new Feature { Id = 2,  NameOfFeatures = "Sensor for Death Zone"}
        }.AsQueryable();

            Expression<Func<Feature, object>>[] filters = { c => c.NameOfFeatures };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Feature, object>>[]>())).Returns(expectedFeatures);
            var result = featureService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedFeatures, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.NameOfFeatures == "Distronic" ));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.NameOfFeatures == "Sensor for Death Zone" ));

        }
        [Test]
        public async Task GetAll_ReturnsAllFeatures()
        {
            var features = new List<Feature> { new Feature { Id = 1 }, new Feature { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(features);
            var result = await featureService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredFeatures()
        {
            var features = new List<Feature>
            {
                new Feature { Id = 1, NameOfFeatures="Adaptive Cruise Control" },
                new Feature { Id = 2, NameOfFeatures="Adaptive Cruise Control" },
                new Feature { Id = 3, NameOfFeatures="Heated Seats" }
            };

            mockRepository.Setup(r => r.FindAll(c => c.NameOfFeatures == "Adaptive Cruise Control")).ReturnsAsync(features.Where(c => c.NameOfFeatures == "Adaptive Cruise Control"));

            var result = await featureService.FindAll(c => c.NameOfFeatures == "Adaptive Cruise Control");
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.NameOfFeatures == "Adaptive Cruise Control"));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedFeatures()
        {
            var features = new List<Feature> { new Feature { Id = 2 }, new Feature { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(features.OrderBy(c => c.Id));
            var result = await featureService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await featureService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.NameOfFeatures=="Night Vision")).ReturnsAsync(3);
            int count = await featureService.CountAsync(c => c.NameOfFeatures=="Night Vision");
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedFeatures(int limit)
        {
            var features = new List<Feature> { new Feature { Id = 1 }, new Feature { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(features);

            var result = await featureService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var feature = new Feature { Id = 1, NameOfFeatures = "Night Vision" };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(feature);
            var result = await featureService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.NameOfFeatures == "Night Vision");
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var feature = new Feature { Id = 1 };
            featureService.Delete(feature);
            mockRepository.Verify(r => r.Delete(feature), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.NameOfFeatures == "Night Vision")).ReturnsAsync(true);
            bool result = await featureService.AnyAsync(c => c.NameOfFeatures == "Night Vision");
            Assert.IsTrue(result);
        }
    }
}
