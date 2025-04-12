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
    internal class ReportTest
    {
        private Mock<IRepository<Report>> mockRepository;
        private ReportService reportService;
        private Mock<ICustomerService> customerService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<Report>>();
            customerService=new Mock<ICustomerService>();
            reportService = new ReportService(mockRepository.Object,customerService.Object);
        }

        [Test]
        public async Task AddReportTest()
        {

            var report = new Report { Id = 1, Title="Other"};
            await reportService.Add(report);
            mockRepository.Verify(r => r.Add(report), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var report = new Report { Id = 1 };
            reportService.Update(report);
            mockRepository.Verify(r => r.Update(report), Times.Once());
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await reportService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedReports()
        {
            var expectedReports = new List<Report>
        {
            new Report { Id = 1,  Title="Others"},
            new Report { Id = 2,  Title= "Booking Inquiry"}
        };

            Expression<Func<Report, object>>[] filters = { c => c.Description };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Report, object>>[]>())).Returns(expectedReports);
            var result = reportService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedReports, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.Title == "Others"));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.Title == "Booking Inquiry"));

        }
        [Test]
        public async Task GetAll_ReturnsAllReports()
        {
            var reports = new List<Report> { new Report { Id = 1 }, new Report { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(reports);
            var result = await reportService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindAll_ReturnsFilteredReports()
        {
            var carCompanies = new List<Report>
            {
                new Report { Id = 1, Title="Others" },
                new Report { Id = 2, Title="Others" },
                new Report { Id = 3, Title="Booking Inquiry" }
            };

            mockRepository.Setup(r => r.FindAll(c => c.Title == "Others")).ReturnsAsync(carCompanies.Where(c => c.Title == "Others"));

            var result = await reportService.FindAll(c => c.Title== "Others");
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.Title == "Others"));
        }


        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedReports()
        {
            var reports = new List<Report> { new Report { Id = 2 }, new Report { Id = 1 } };

            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(reports.OrderBy(c => c.Id));
            var result = await reportService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        {
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await reportService.Count();
            Assert.That(count.Equals(10));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {

            mockRepository.Setup(r => r.CountAsync(c => c.Title=="Booking Inquiry")).ReturnsAsync(3);
            int count = await reportService.CountAsync(c => c.Title == "Booking Inquiry");
            Assert.That(count.Equals(3));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedReports(int limit)
        {
            var reports = new List<Report> { new Report { Id = 1 }, new Report { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(reports);

            var result = await reportService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var report = new Report { Id = 1, Title = "Others" };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(report);
            var result = await reportService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Title == "Others");
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var Report = new Report { Id = 1 };
            reportService.Delete(Report);
            mockRepository.Verify(r => r.Delete(Report), Times.Once());
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            mockRepository.Setup(r => r.AnyAsync(c => c.Title == "Others")).ReturnsAsync(true);
            bool result = await reportService.AnyAsync(c => c.Title == "Others");
            Assert.IsTrue(result);
        }
    }
}
