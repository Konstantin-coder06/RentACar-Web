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
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedReservations()
        {
            var expectedReservations = new List<Reservation>
        {
            new Reservation { Id = 1, StartDate =DateTime.Now.Date, EndDate=DateTime.Now.Date.AddDays(3) },
            new Reservation { Id = 2,  StartDate =DateTime.Now.Date.AddDays(2), EndDate=DateTime.Now.Date.AddDays(5)  }
        }.AsQueryable();

            Expression<Func<Reservation, object>>[] filters = { c => c.Customer };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Reservation, object>>[]>())).Returns(expectedReservations);
            var result = reservationService.AllWithInclude(filters);

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
        [Test]
        public async Task GetAllByOrderByCreateTime_ReturnsOrderedReservations()
        {
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CreateTime = DateTime.Now.Date.AddHours(-1) },
                new Reservation { Id = 2, CreateTime = DateTime.Now.Date.AddHours(-2) }
            };
            mockRepository.Setup(r => r.GetAllOrderBy(x => x.CreateTime)).ReturnsAsync(reservations.OrderBy(x => x.CreateTime));
            var result = await reservationService.GetAllByOrderByCreateTime();
            Assert.AreEqual(2, result.First().Id); 
            mockRepository.Verify(r => r.GetAllOrderBy(x => x.CreateTime), Times.Once());
        }

        [Test]
        public async Task HasOverlappingReservation_ReturnsTrueWhenOverlapExists()
        {
            var carId = 1;
            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date.AddDays(1);
            mockRepository.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(true);
            var result = await reservationService.HasOverlappingReservation(carId, startDate, endDate);
            Assert.IsTrue(result);
            mockRepository.Verify(r => r.AnyAsync(It.Is<Expression<Func<Reservation, bool>>>(expr =>
                expr.Compile()(new Reservation { CarId = carId, StartDate = startDate.AddHours(-1), EndDate = endDate.AddHours(1) }))), Times.Once());
        }
        [Test]
        public async Task FindAllForLast24Hours_ReturnsReservationsWithin24Hours()
        {
            var now = DateTime.Now.Date;
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CreateTime = now.AddHours(-12) },
                new Reservation { Id = 2, CreateTime = now.AddHours(-25) }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservations.Where(x => x.CreateTime >= now.AddDays(-1)));
            var result = await reservationService.FindAllForLast24Hours();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [Test]
        public async Task FindAllForLast24HoursBefore24Hours_ReturnsReservationsInRange()
        {
            var now = DateTime.Now.Date;
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CreateTime = now.AddHours(-36) },
                new Reservation { Id = 2, CreateTime = now.AddHours(-12) }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservations.Where(x => x.CreateTime >= now.AddDays(-2) && x.CreateTime <= now.AddDays(-1)));
            var result = await reservationService.FindAllForLast24HoursBefore24Hours();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [Test]
        public async Task FindAllForLastWeek_ReturnsReservationsInLastWeek()
        {
            var now = DateTime.Now.Date;
            var startOfLastWeek = now.AddDays(-6).Date; 
            var endOfLastWeek = startOfLastWeek.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CreateTime = now.AddDays(-5) },
                new Reservation { Id = 2, CreateTime = now.AddDays(-10) } 
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservations.Where(x => x.CreateTime >= startOfLastWeek && x.CreateTime <= endOfLastWeek));
            var result = await reservationService.FindAllForLastWeek();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [Test]
        public async Task FindAllForWeekBeforeLast_ReturnsReservationsInWeekBeforeLast()
        {
            var now = DateTime.Now.Date;
            var startOfWeekBeforeLast = now.AddDays(-13).Date;
            var endOfWeekBeforeLast = startOfWeekBeforeLast.AddDays(6).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CreateTime = now.AddDays(-10) },
                new Reservation { Id = 2, CreateTime = now.AddDays(-5) }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservations.Where(x => x.CreateTime >= startOfWeekBeforeLast && x.CreateTime <= endOfWeekBeforeLast));
            var result = await reservationService.FindAllForWeekBeforeLast();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [Test]
        public async Task FindAllForLastMonth_ReturnsReservationsInLastMonth()
        {

            var now = DateTime.UtcNow;
            var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfLastMonth = firstDayOfCurrentMonth.AddMonths(-1);
            var endOfLastMonth = firstDayOfCurrentMonth.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CreateTime = firstDayOfCurrentMonth.AddDays(-10) }, 
                new Reservation { Id = 2, CreateTime = startOfLastMonth.AddMonths(-1) },       
                new Reservation { Id = 3, CreateTime = now }                                   
            };

            mockRepository
                .Setup(r => r.FindAll(It.Is<Expression<Func<Reservation, bool>>>(expr =>
                    expr.Compile()(new Reservation { CreateTime = firstDayOfCurrentMonth.AddDays(-10) }) && 
                    !expr.Compile()(new Reservation { CreateTime = startOfLastMonth.AddMonths(-1) }) &&     
                    !expr.Compile()(new Reservation { CreateTime = now })))).ReturnsAsync(reservations.Where(x => x.CreateTime >= startOfLastMonth && x.CreateTime <= endOfLastMonth));

            var result = await reservationService.FindAllForLastMonth();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
            mockRepository.Verify(r => r.FindAll(It.IsAny<Expression<Func<Reservation, bool>>>()), Times.Once());
        }

        [Test]
        public async Task FindAllForPreviousMonth_ReturnsReservationsInPreviousMonth()
        {

            var now = DateTime.UtcNow; 
            var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfMonthBeforeLast = firstDayOfCurrentMonth.AddMonths(-2);
            var endOfMonthBeforeLast = firstDayOfCurrentMonth.AddMonths(-1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CreateTime = startOfMonthBeforeLast.AddDays(10) },    
                new Reservation { Id = 2, CreateTime = firstDayOfCurrentMonth.AddDays(-10) },  
                new Reservation { Id = 3, CreateTime = now }                                   
            };

            mockRepository
                .Setup(r => r.FindAll(It.Is<Expression<Func<Reservation, bool>>>(expr =>
                    expr.Compile()(new Reservation { CreateTime = startOfMonthBeforeLast.AddDays(10) }) && 
                    !expr.Compile()(new Reservation { CreateTime = firstDayOfCurrentMonth.AddDays(-10) }) && 
                    !expr.Compile()(new Reservation { CreateTime = now })))).ReturnsAsync(reservations.Where(x => x.CreateTime >= startOfMonthBeforeLast && x.CreateTime <= endOfMonthBeforeLast));
            var result = await reservationService.FindAllForPreviousMonth();

           
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
            mockRepository.Verify(r => r.FindAll(It.IsAny<Expression<Func<Reservation, bool>>>()), Times.Once());
        }
        [Test]
        public async Task TotalPriceForOnePeriodOfTime_ReturnsSumOfTotalPrices()
        {
            var reservations = new List<Reservation>
            {
                new Reservation { TotalPrice = 100 },
                new Reservation { TotalPrice = 200 }
            };
            var result = await reservationService.TotalPriceForOnePeriodOfTime(reservations);
            Assert.AreEqual(300, result);
        }
        [Test]
        public async Task GetAllReservationsByStartDate_ReturnsReservationsAfterStartDate()
        {
            var startDate = DateTime.Now.Date.AddDays(-1);
            var reservations = new List<Reservation> { new Reservation { Id = 1, CreateTime = startDate.AddHours(1) } };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Reservation, bool>>>())).ReturnsAsync(reservations);
            var result = await reservationService.GetAllReservationsByStartDate(startDate);
            Assert.AreEqual(1, result.Count());
        }
    }
}
