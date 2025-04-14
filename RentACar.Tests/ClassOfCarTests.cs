using Moq;
using RentACar.Core.Services;
using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;

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
            var classOfCar = new ClassOfCar { Id = 1, Name = "Sport" };
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
                new ClassOfCar { Id = 1, Name = "Business" },
                new ClassOfCar { Id = 2, Name = "Sport" }
            }.AsQueryable();

            Expression<Func<ClassOfCar, object>>[] filters = { c => c.Name };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<ClassOfCar, object>>[]>())).Returns(expectedClassofCars);
            var result = classOfCarService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedClassofCars, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.Name == "Business"));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.Name == "Sport"));
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
                new ClassOfCar { Id = 1, Name = "Business" },
                new ClassOfCar { Id = 2, Name = "Standard" },
                new ClassOfCar { Id = 3, Name = "Business" }
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
            mockRepository.Setup(r => r.CountAsync(c => c.Name == "Standard")).ReturnsAsync(3);
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

        [Test]
        public async Task GetClassOptionsAsync_ReturnsAllClassOfCars()
        {
            var classOfCars = new List<ClassOfCar>
            {
                new ClassOfCar { Id = 1, Name = "Economy" },
                new ClassOfCar { Id = 2, Name = "Luxury" }
            };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(classOfCars);
            var result = await classOfCarService.GetClassOptionsAsync();
            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 class options");
            Assert.IsTrue(result.Any(c => c.Name == "Economy"));
            Assert.IsTrue(result.Any(c => c.Name == "Luxury"));
            mockRepository.Verify(r => r.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetAllClassSelectedIds_ReturnsMatchingIds()
        {
            var selectedCategories = new List<string> { "Standard", "Luxury" };
            var classOfCars = new List<ClassOfCar>
            {
                new ClassOfCar { Id = 1, Name = "Standard" },
                new ClassOfCar { Id = 2, Name = "Luxury" },
                new ClassOfCar { Id = 3, Name = "Economy" }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<ClassOfCar, bool>>>())).ReturnsAsync(classOfCars.Where(c => selectedCategories.Contains(c.Name)));
            var result = await classOfCarService.GetAllClassSelectedIds(selectedCategories);
            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 matching IDs");
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(2));
            mockRepository.Verify(r => r.FindAll(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetAllClassSelectedIds_ReturnsEmptyListWhenNoMatches()
        {
            var selectedCategories = new List<string> { "Sport" };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<ClassOfCar, bool>>>())).ReturnsAsync(new List<ClassOfCar>());
            var result = await classOfCarService.GetAllClassSelectedIds(selectedCategories);
            Assert.That(result.Count, Is.EqualTo(0), "Expected empty list when no matches");
            mockRepository.Verify(r => r.FindAll(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task IsThereClassWithThisName_ReturnsTrueWhenClassExists()
        {
            var className = "Business";
            var classOfCar = new ClassOfCar { Id = 1, Name = "Business" };
            mockRepository.Setup(r => r.FindOne(x => x.Name.ToLower() == className.ToLower())).ReturnsAsync(classOfCar);
            var result = await classOfCarService.IsThereClassWithThisName(className);
            Assert.IsTrue(result, "Expected true when class exists");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task IsThereClassWithThisName_ReturnsFalseWhenClassNotFound()
        {
            var className = "Electric";
            mockRepository.Setup(r => r.FindOne(x => x.Name.ToLower() == className.ToLower())).ReturnsAsync((ClassOfCar)null);
            var result = await classOfCarService.IsThereClassWithThisName(className);
            Assert.IsFalse(result, "Expected false when class not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetClassOfCarById_ReturnsClassWhenFound()
        {
            var id = 1;
            var classOfCar = new ClassOfCar { Id = id, Name = "Standard" };
            mockRepository.Setup(r => r.FindOne(x => x.Id == id)).ReturnsAsync(classOfCar);
            var result = await classOfCarService.GetClassOfCarById(id);
            Assert.IsNotNull(result, "Expected class to be found");
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo("Standard"));
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetClassOfCarById_ReturnsNullWhenNotFound()
        {
            var id = 2;
            mockRepository.Setup(r => r.FindOne(x => x.Id == id)).ReturnsAsync((ClassOfCar)null);
            var result = await classOfCarService.GetClassOfCarById(id);
            Assert.IsNull(result, "Expected null when class not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetStandardId_ReturnsIdWhenStandardFound()
        {
            var classOfCar = new ClassOfCar { Id = 1, Name = "Standard" };
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Standard")).ReturnsAsync(classOfCar);
            var result = await classOfCarService.GetStandardId();
            Assert.That(result, Is.EqualTo(1), "Expected Standard ID to be 1");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public void GetStandardId_ThrowsNullReferenceExceptionWhenNotFound()
        {
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Standard")).ReturnsAsync((ClassOfCar)null);
            Assert.ThrowsAsync<NullReferenceException>(async () => await classOfCarService.GetStandardId(), "Expected NullReferenceException when Standard not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetLuxuryId_ReturnsIdWhenLuxuryFound()
        {
            var classOfCar = new ClassOfCar { Id = 2, Name = "Luxury" };
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Luxury")).ReturnsAsync(classOfCar);
            var result = await classOfCarService.GetLuxuryId();
            Assert.That(result, Is.EqualTo(2), "Expected Luxury ID to be 2");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public void GetLuxuryId_ThrowsNullReferenceExceptionWhenNotFound()
        {
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Luxury")).ReturnsAsync((ClassOfCar)null);
            Assert.ThrowsAsync<NullReferenceException>(async () => await classOfCarService.GetLuxuryId(), "Expected NullReferenceException when Luxury not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetEconomyId_ReturnsIdWhenEconomyFound()
        {
            var classOfCar = new ClassOfCar { Id = 3, Name = "Economy" };
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Economy")).ReturnsAsync(classOfCar);
            var result = await classOfCarService.GetEconomyId();
            Assert.That(result, Is.EqualTo(3), "Expected Economy ID to be 3");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public void GetEconomyId_ThrowsNullReferenceExceptionWhenNotFound()
        {
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Economy")).ReturnsAsync((ClassOfCar)null);
            Assert.ThrowsAsync<NullReferenceException>(async () => await classOfCarService.GetEconomyId(), "Expected NullReferenceException when Economy not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetBusinessId_ReturnsIdWhenBusinessFound()
        {
            var classOfCar = new ClassOfCar { Id = 4, Name = "Business" };
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Business")).ReturnsAsync(classOfCar);
            var result = await classOfCarService.GetBusinessId();
            Assert.That(result, Is.EqualTo(4), "Expected Business ID to be 4");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public void GetBusinessId_ThrowsNullReferenceExceptionWhenNotFound()
        {
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Business")).ReturnsAsync((ClassOfCar)null);
            Assert.ThrowsAsync<NullReferenceException>(async () => await classOfCarService.GetBusinessId(), "Expected NullReferenceException when Business not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetElectricId_ReturnsIdWhenElectricFound()
        {
            var classOfCar = new ClassOfCar { Id = 5, Name = "Electric" };
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Electric")).ReturnsAsync(classOfCar);
            var result = await classOfCarService.GetElectricId();
            Assert.That(result, Is.EqualTo(5), "Expected Electric ID to be 5");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public void GetElectricId_ThrowsNullReferenceExceptionWhenNotFound()
        {
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Electric")).ReturnsAsync((ClassOfCar)null);
            Assert.ThrowsAsync<NullReferenceException>(async () => await classOfCarService.GetElectricId(), "Expected NullReferenceException when Electric not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetSportId_ReturnsIdWhenSportFound()
        {
            var classOfCar = new ClassOfCar { Id = 6, Name = "Sport" };
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Sport")).ReturnsAsync(classOfCar);
            var result = await classOfCarService.GetSportId();
            Assert.That(result, Is.EqualTo(6), "Expected Sport ID to be 6");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }

        [Test]
        public void GetSportId_ThrowsNullReferenceExceptionWhenNotFound()
        {
            mockRepository.Setup(r => r.FindOne(x => x.Name == "Sport")).ReturnsAsync((ClassOfCar)null);
            Assert.ThrowsAsync<NullReferenceException>(async () => await classOfCarService.GetSportId(), "Expected NullReferenceException when Sport not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<ClassOfCar, bool>>>()), Times.Once());
        }
    }
}