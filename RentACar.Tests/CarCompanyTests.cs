﻿using Microsoft.AspNetCore.Identity;
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
    public class CarCompanyTests
    {
        private Mock<IRepository<CarCompany>> mockRepository;
        private CarCompanyService carCompanyService;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<CarCompany>>();
            var store = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            carCompanyService = new CarCompanyService(mockRepository.Object,_mockUserManager.Object);
        }

        [Test]
        public async Task AddCarCompanyTest()
        {

            var carCompany = new CarCompany { Id = 1, Name = "NASHATA FIRMA", Description = "Naj-dobrata firma" };
            await carCompanyService.Add(carCompany);
            mockRepository.Verify(r => r.Add(carCompany), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var carCompany = new CarCompany { Id = 1 };
            carCompanyService.Update(carCompany);
            mockRepository.Verify(r => r.Update(carCompany), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await carCompanyService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedCarCompanies()
        {
            var expectedCarCompanies = new List<CarCompany>
            {
                new CarCompany { Id = 1, Name = "NASHATA FIRMA", Description = "Naj-dobrata firma" },
                new CarCompany { Id = 2, Name = "Kastomoni", Description = "Rejem dUrva" }
            }.AsQueryable(); 

            Expression<Func<CarCompany, object>>[] filters = { c => c.Description };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<CarCompany, object>>[]>())).Returns(expectedCarCompanies);

          
            var result = carCompanyService.AllWithInclude(filters).ToList(); 

            
            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedCarCompanies.ToList(), result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.Name == "NASHATA FIRMA" && c.Description == "Naj-dobrata firma"));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.Name == "Kastomoni" && c.Description == "Rejem dUrva"));
        }
        [Test]
        public async Task GetAll_ReturnsAllCarCompanies()
        {
            var carCompanies = new List<CarCompany> { new CarCompany { Id = 1 }, new CarCompany { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(carCompanies);
            var result = await carCompanyService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredCarCompanies()
        {
            var carCompanies = new List<CarCompany>
            {
                new CarCompany { Id = 1, Name="Arsenal AD" },
                new CarCompany { Id = 2, Name="M+S Hidravlika" },
                new CarCompany { Id = 3, Name="Arsenal AD" }
            };

            mockRepository.Setup(r => r.FindAll(c => c.Name == "Arsenal AD")).ReturnsAsync(carCompanies.Where(c => c.Name == "Arsenal AD"));

            var result = await carCompanyService.FindAll(c => c.Name == "Arsenal AD");
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.Name == "Arsenal AD"));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedCarCompanies()
        {
            var carCompanies = new List<CarCompany> { new CarCompany { Id = 2 }, new CarCompany { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(carCompanies.OrderBy(c => c.Id));
            var result = await carCompanyService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await carCompanyService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.Address=="ul. \"Hadji Dimitur\"")).ReturnsAsync(3);
            int count = await carCompanyService.CountAsync(c => c.Address == "ul. \"Hadji Dimitur\"");
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedCarCompanies(int limit)
        {
            var carCompanies = new List<CarCompany> { new CarCompany { Id = 1 }, new CarCompany { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(carCompanies);

            var result = await carCompanyService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var carCompany = new CarCompany { Id = 1, Name="CBS" };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(carCompany);
            var result = await carCompanyService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Name=="CBS");
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var carCompany = new CarCompany { Id = 1 };
            carCompanyService.Delete(carCompany);
            mockRepository.Verify(r => r.Delete(carCompany), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.Name == "Peshovata firma")).ReturnsAsync(true);
            bool result = await carCompanyService.AnyAsync(c => c.Name=="Peshovata firma");
            Assert.IsTrue(result);
        }
        [Test]
        public async Task GetByUserId_ReturnsCarCompanyWhenFound()
        {
            var userId = "user123";
            var carCompany = new CarCompany { Id = 1, Name = "NASHATA FIRMA", UserId = userId };

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { UserId = userId })
            ))).ReturnsAsync(carCompany);

            var result = await carCompanyService.GetByUserId(userId);

            Assert.IsNotNull(result, "Expected car company to be found");
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("NASHATA FIRMA"));
            Assert.That(result.UserId, Is.EqualTo(userId));
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetByUserId_ReturnsNullWhenNotFound()
        {
            var userId = "user456";

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { UserId = userId })
            ))).ReturnsAsync((CarCompany)null);

            var result = await carCompanyService.GetByUserId(userId);

            Assert.IsNull(result, "Expected no car company to be found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetById_ReturnsCarCompanyWhenFound()
        {
            var id = 1;
            var carCompany = new CarCompany { Id = id, Name = "Kastomoni" };

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync(carCompany);

            var result = await carCompanyService.GetById(id);

            Assert.IsNotNull(result, "Expected car company to be found");
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo("Kastomoni"));
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetById_ReturnsNullWhenNotFound()
        {
            var id = 2;

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync((CarCompany)null);

            var result = await carCompanyService.GetById(id);

            Assert.IsNull(result, "Expected no car company to be found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetNameById_ReturnsNameWhenCompanyFound()
        {
            var id = 1;
            var carCompany = new CarCompany { Id = id, Name = "Arsenal AD" };

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync(carCompany);

            var result = await carCompanyService.GetNameById(id);

            Assert.That(result, Is.EqualTo("Arsenal AD"), "Expected correct company name");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public void GetNameById_ThrowsNullReferenceExceptionWhenCompanyNotFound()
        {
            var id = 3;

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync((CarCompany)null);

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await carCompanyService.GetNameById(id),
                "Expected NullReferenceException when company not found");

            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetAddressById_ReturnsAddressWhenCompanyFound()
        {
            var id = 1;
            var carCompany = new CarCompany { Id = id, Address = "ul. \"Hadji Dimitur\"" };

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync(carCompany);

            var result = await carCompanyService.GetAddressById(id);

            Assert.That(result, Is.EqualTo("ul. \"Hadji Dimitur\""), "Expected correct company address");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public void GetAddressById_ThrowsNullReferenceExceptionWhenCompanyNotFound()
        {
            var id = 4;

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync((CarCompany)null);

            Assert.ThrowsAsync<NullReferenceException>(async () =>
                await carCompanyService.GetAddressById(id),
                "Expected NullReferenceException when company not found");

            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetCompanyEmail_ReturnsEmailWhenCompanyAndUserFound()
        {
            var id = 1;
            var userId = "user789";
            var carCompany = new CarCompany { Id = id, UserId = userId };
            var identityUser = new IdentityUser { Id = userId, Email = "company@example.com" };

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync(carCompany);
            _mockUserManager.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(identityUser);

            var result = await carCompanyService.GetCompanyEmail(id);

            Assert.That(result, Is.EqualTo("company@example.com"), "Expected correct user email");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
            _mockUserManager.Verify(u => u.FindByIdAsync(userId), Times.Once());
        }

      
        [Test]
        public async Task GetCompanyEmail_ReturnsNullWhenUserNotFound()
        {
            var id = 1;
            var userId = "user101";
            var carCompany = new CarCompany { Id = id, UserId = userId };

            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<CarCompany, bool>>>(expr =>
                expr.Compile()(new CarCompany { Id = id })
            ))).ReturnsAsync(carCompany);
            _mockUserManager.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);

            var result = await carCompanyService.GetCompanyEmail(id);

            Assert.IsNull(result, "Expected null when user not found");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<CarCompany, bool>>>()), Times.Once());
            _mockUserManager.Verify(u => u.FindByIdAsync(userId), Times.Once());
        }
    }
}

