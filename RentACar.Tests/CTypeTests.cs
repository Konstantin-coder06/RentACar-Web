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
    public class CTypeTests
    {
        private Mock<IRepository<CType>> mockRepository;
        private CTypeService cTypeService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<CType>>();
            cTypeService = new CTypeService(mockRepository.Object);
        }

        [Test]
        public async Task AddCTypeTest()
        {

            var cType = new CType { Id = 1, Name = "family-frendly sedan"};
            await cTypeService.Add(cType);
            mockRepository.Verify(r => r.Add(cType), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var cType = new CType { Id = 1 };
            cTypeService.Update(cType);
            mockRepository.Verify(r => r.Update(cType), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await cTypeService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedTypes()
        {
            var expectedTypes = new List<CType>
        {
            new CType { Id = 1,  Name = "Pickup" },
            new CType { Id = 2,  Name = "Sedan"}
        }.AsQueryable();

            Expression<Func<CType, object>>[] filters = { c => c.SeatCapacity };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<CType, object>>[]>())).Returns(expectedTypes);
            var result = cTypeService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedTypes, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.Name == "Pickup" ));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.Name == "Sedan" ));

        }
        [Test]
        public async Task GetAll_ReturnsAllTypes()
        {
            var types = new List<CType> { new CType { Id = 1 }, new CType { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(types);
            var result = await cTypeService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredTypes()
        {
            var types = new List<CType>
            {
                new CType { Id = 1, Name="Sedan" },
                new CType { Id = 2, Name="Hatchback" },
                new CType { Id = 3, Name="Hatchback" }
            };

            mockRepository.Setup(r => r.FindAll(c => c.Name == "Hatchback")).ReturnsAsync(types.Where(c => c.Name == "Hatchback"));

            var result = await cTypeService.FindAll(c => c.Name == "Hatchback");
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.Name == "Hatchback"));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedTypes()
        {
            var types = new List<CType> { new CType { Id = 2 }, new CType { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(types.OrderBy(c => c.Id));
            var result = await cTypeService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await cTypeService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.SeatCapacity==3)).ReturnsAsync(3);
            int count = await cTypeService.CountAsync(c => c.SeatCapacity==3);
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedTypes(int limit)
        {
            var types = new List<CType> { new CType { Id = 1 }, new CType { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(types);

            var result = await cTypeService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var cType = new CType { Id = 1, Name = "SUV" };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(cType);
            var result = await cTypeService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Name == "SUV");
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var cType = new CType { Id = 1 };
            cTypeService.Delete(cType);
            mockRepository.Verify(r => r.Delete(cType), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.Name == "SUV")).ReturnsAsync(true);
            bool result = await cTypeService.AnyAsync(c => c.Name == "SUV");
            Assert.IsTrue(result);
        }
    }
}
