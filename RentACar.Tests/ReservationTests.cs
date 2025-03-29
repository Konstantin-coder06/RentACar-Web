using Microsoft.AspNetCore.Cors.Infrastructure;
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
    public class ReservationTests
    {
        private Mock<IRepository<Reservation>> mockRepository;
        private ReservationService reservationService;
        private Mock<ICustomerService> mockCustomerService;
        private Mock<ICarService> mockCarService;
        private Mock<IImageService> mockImageService;
        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<Reservation>>();
            mockCustomerService = new Mock<ICustomerService>();
            mockCarService = new Mock<ICarService>();
            mockImageService = new Mock<IImageService>();

            reservationService = new ReservationService(mockRepository.Object,mockCustomerService.Object,mockCarService.Object,mockImageService.Object);
        }

        [Test]
        public async Task AddReservationTest()
        {

            var reservation = new Reservation { Id = 1, StartDate = DateTime.Now, EndDate=DateTime.Now.AddDays(3) };
            await reservationService.Add(reservation);
            mockRepository.Verify(r => r.Add(reservation), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var reservation = new Reservation { Id = 1 };
            reservationService.Update(reservation);
            mockRepository.Verify(r => r.Update(reservation), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await reservationService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public async Task AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedReservations()
        {
            var expectedReservations = new List<Reservation>
        {
            new Reservation { Id = 1, StartDate =DateTime.Now.Date, EndDate=DateTime.Now.Date.AddDays(3) },
            new Reservation { Id = 2,  StartDate =DateTime.Now.Date.AddDays(2), EndDate=DateTime.Now.Date.AddDays(5)  }
        };

            Expression<Func<Reservation, object>>[] filters = { c => c.Customer };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Reservation, object>>[]>())).ReturnsAsync(expectedReservations);
            var result = await reservationService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedReservations, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.StartDate == DateTime.Now.Date && c.EndDate == DateTime.Now.Date.AddDays(3)));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.StartDate == DateTime.Now.Date.AddDays(2) && c.EndDate == DateTime.Now.Date.AddDays(5)));

        }
        [Test]
        public async Task GetAll_ReturnsAllReservations()
        {
            var reservations= new List<Reservation> { new Reservation { Id = 1 }, new Reservation { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(reservations);
            var result = await reservationService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [TestCase(-5)]
        public async Task FindAll_ReturnsFilteredReservations(int days)
        {
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, StartDate = DateTime.Now.Date },
                new Reservation { Id = 2, StartDate = DateTime.Now.Date.AddDays(days) },
                new Reservation { Id = 3, StartDate = DateTime.Now.Date }
            };

            mockRepository.Setup(r => r.FindAll(c => c.StartDate ==DateTime.Now.Date)).ReturnsAsync(reservations.Where(c => c.StartDate == DateTime.Now.Date));

            var result = await reservationService.FindAll(c => c.StartDate ==DateTime.Now.Date);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.StartDate==DateTime.Now.Date));
        }
       
       
        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedReservations()
        {
            var reservations = new List<Reservation> { new Reservation { Id = 2 }, new Reservation { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(reservations.OrderBy(c => c.Id));
            var result = await reservationService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await reservationService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.StartDate ==DateTime.Now.Date)).ReturnsAsync(3);
            int count = await reservationService.CountAsync(c => c.StartDate==DateTime.Now.Date);
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedReservations(int limit)
        {
            var reservations = new List<Reservation> { new Reservation { Id = 1 }, new Reservation { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(reservations);

            var result = await reservationService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var reservation = new Reservation { Id = 1, StartDate = DateTime.Now.Date };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(reservation);
            var result = await reservationService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.StartDate.Equals(DateTime.Now.Date));
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var reservation = new Reservation { Id = 1 };
            reservationService.Delete(reservation);
            mockRepository.Verify(r => r.Delete(reservation), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c=> c.StartDate ==DateTime.Now.Date)).ReturnsAsync(true);
            bool result = await reservationService.AnyAsync(c => c.StartDate == DateTime.Now.Date);
            Assert.IsTrue(result);
        }
    }
}
