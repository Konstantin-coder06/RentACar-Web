using Microsoft.AspNetCore.Identity;
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
    public class CustomerTests
    {
        private Mock<IRepository<Customer>> mockRepository;
        private CustomerService customerService;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<Customer>>();
            var store = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            customerService = new CustomerService(mockRepository.Object,_mockUserManager.Object);
        }

        [Test]
        public async Task AddCustomerTest()
        {

            var customer = new Customer { Id = 1, Name = "Ivan"};
            await customerService.Add(customer);
            mockRepository.Verify(r => r.Add(customer), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var customer = new Customer { Id = 1 };
            customerService.Update(customer);
            mockRepository.Verify(r => r.Update(customer), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await customerService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedCustomers()
        {
            var expectedCustomers = new List<Customer>
        {
            new Customer { Id = 1,  Name = "Ivan"},
            new Customer { Id = 2,  Name = "Ivanka"}
        }.AsQueryable();

            Expression<Func<Customer, object>>[] filters = { c => c.Name };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Customer, object>>[]>())).Returns(expectedCustomers);
            var result = customerService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedCustomers, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.Name == "Ivan" ));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.Name == "Ivanka" ));

        }
        [Test]
        public async Task GetAll_ReturnsAllCustomers()
        {
            var customers = new List<Customer> { new Customer { Id = 1 }, new Customer { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(customers);
            var result = await customerService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name="Konstantin" },
                new Customer { Id = 2, Name="Mariika" },
                new Customer { Id = 3, Name="Konstantin" }
            };

            mockRepository.Setup(r => r.FindAll(c => c.Name == "Konstantin")).ReturnsAsync(customers.Where(c => c.Name == "Konstantin"));

            var result = await customerService.FindAll(c => c.Name == "Konstantin");
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.Name == "Konstantin"));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedCustomers()
        {
            var customers = new List<Customer> { new Customer { Id = 2 }, new Customer { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(customers.OrderBy(c => c.Id));
            var result = await customerService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await customerService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.Name=="Toshko")).ReturnsAsync(3);
            int count = await customerService.CountAsync(c => c.Name=="Toshko");
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedCustomers(int limit)
        {
            var customers = new List<Customer> { new Customer { Id = 1 }, new Customer { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(customers);

            var result = await customerService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var customer = new Customer { Id = 1, Name = "Ivan" };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(customer);
            var result = await customerService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Name == "Ivan");
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var Customer = new Customer { Id = 1 };
            customerService.Delete(Customer);
            mockRepository.Verify(r => r.Delete(Customer), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.Name == "Peshka")).ReturnsAsync(true);
            bool result = await customerService.AnyAsync(c => c.Name == "Peshka");
            Assert.IsTrue(result);
        }
    }
}
