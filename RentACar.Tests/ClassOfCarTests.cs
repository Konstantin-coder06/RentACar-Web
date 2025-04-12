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
    public class ClassOfCarTests
    {
        private Mock<IRepository<ClassOfCar>> mockRepository;
        private ClassOfCarService classOfCarService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<ClassOfCar>>();
            classOfCarService = new ClassOfCarService(mockRepository.Object);
        }

        [Test]
        public async Task AddClassOfCarTest()
        {

            var classOfCar = new ClassOfCar { Id = 1, Name = "Sport"};
            await classOfCarService.Add(classOfCar);
            mockRepository.Verify(r => r.Add(classOfCar), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var classOfCar = new ClassOfCar { Id = 1 };
            classOfCarService.Update(classOfCar);
            mockRepository.Verify(r => r.Update(classOfCar), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await classOfCarService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedClassOfCars()
        {
            var expectedClassofCars = new List<ClassOfCar>
            {
                new ClassOfCar { Id = 1,  Name = "Business"},
                new ClassOfCar { Id = 2,  Name = "Sport"}
            }.AsQueryable();

            Expression<Func<ClassOfCar, object>>[] filters = { c => c.Name };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<ClassOfCar, object>>[]>())).Returns(expectedClassofCars);
            var result = classOfCarService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedClassofCars, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.Name == "Business" ));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.Name == "Sport" ));

        }
        [Test]
        public async Task GetAll_ReturnsAllClassOfCars()
        {
            var classOfCars = new List<ClassOfCar> { new ClassOfCar { Id = 1 }, new ClassOfCar { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(classOfCars);
            var result = await classOfCarService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredClassOfCars()
        {
            var classOfCars = new List<ClassOfCar>
            {
                new ClassOfCar { Id = 1, Name="Business" },
                new ClassOfCar { Id = 2, Name="Standart" },
                new ClassOfCar { Id = 3, Name="Business" }
            };

            mockRepository.Setup(r => r.FindAll(c => c.Name == "Business")).ReturnsAsync(classOfCars.Where(c => c.Name == "Business"));

            var result = await classOfCarService.FindAll(c => c.Name == "Business");
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.Name == "Business"));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedClassOfCars()
        {
            var classOfCars = new List<ClassOfCar> { new ClassOfCar { Id = 2 }, new ClassOfCar { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(classOfCars.OrderBy(c => c.Id));
            var result = await classOfCarService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await classOfCarService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.Name=="Standard")).ReturnsAsync(3);
            int count = await classOfCarService.CountAsync(c => c.Name == "Standard");
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedClassOfCars(int limit)
        {
            var classOfCars = new List<ClassOfCar> { new ClassOfCar { Id = 1 }, new ClassOfCar { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(classOfCars);

            var result = await classOfCarService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var classOfCar = new ClassOfCar { Id = 1, Name = "Standard" };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(classOfCar);
            var result = await classOfCarService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Name == "Standard");
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var classOfCar = new ClassOfCar { Id = 1 };
            classOfCarService.Delete(classOfCar);
            mockRepository.Verify(r => r.Delete(classOfCar), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.Name == "Sport")).ReturnsAsync(true);
            bool result = await classOfCarService.AnyAsync(c => c.Name == "Sport");
            Assert.IsTrue(result);
        }
    }
}
