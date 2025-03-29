using Moq;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System.Linq.Expressions;
namespace RentACar.Tests
{
    public class Tests
    {
        private Mock<IRepository<Car>> mockRepository;
        private CarService carService;
        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<Car>>();
            carService = new CarService(mockRepository.Object);
        }

        [Test]
        public async Task AddCarTest()
        {
             
            var car = new Car { Id = 1, Brand = "TestBrand", Model = "TestModel" };
            await carService.Add(car);

            
            mockRepository.Verify(r => r.Add(car), Times.Once());
        }
        [Test]
        public async Task AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedCars()
        {
            var expectedCars = new List<Car>
        {
            new Car { Id = 1, Brand = "Toyota", Model = "Camry" },
            new Car { Id = 2, Brand = "Honda", Model = "Civic" }
        };

            Expression<Func<Car, object>>[] filters = { c => c.ClassOfCar };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Car, object>>[]>())).ReturnsAsync(expectedCars);
            var result = await carService.AllWithInclude(filters);

            mockRepository.Verify(r => r.AllWithInclude(filters), Times.Once());
            Assert.AreEqual(expectedCars, result);
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.Any(c => c.Id == 1 && c.Brand == "Toyota" && c.Model == "Camry"));
            Assert.IsTrue(result.Any(c => c.Id == 2 && c.Brand == "Honda" && c.Model == "Civic"));

        }
        
        [TestCase(1)]
        public async Task CountOfCarsWithCategoryTest(int categoryId)
        {

            var cars = new List<Car>
            {
                new Car { Id = 1, ClassOfCarId = 1 },
                new Car { Id = 2, ClassOfCarId = 1 },
                new Car { Id = 3, ClassOfCarId = 2 }
            };
            mockRepository.Setup(r => r.FindAll(x => x.ClassOfCarId == categoryId)).ReturnsAsync(cars.Where(x => x.ClassOfCarId == categoryId));
            int count = await carService.CountOfCarsWithCategory(categoryId); 
            Assert.That(count.Equals(2));
        }
        [Test]
        public void Delete_CallsRepositoryDelete()
        {
            var car = new Car { Id = 1 };
            carService.Delete(car);
            mockRepository.Verify(r => r.Delete(car), Times.Once());
        }
        [Test]
        public async Task FindAll_ReturnsFilteredCars()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota" },
                new Car { Id = 2, Brand = "Honda" },
                new Car { Id = 3, Brand = "Toyota" }
            };
           
            mockRepository.Setup(r => r.FindAll(c => c.Brand == "Toyota")).ReturnsAsync(cars.Where(c => c.Brand == "Toyota"));

            var result = await carService.FindAll(c => c.Brand == "Toyota");
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.Brand == "Toyota"));
        }
        [Test]
        public async Task AnyAsync_ReturnsTrueWhenCarsExist()
        {
            Expression<Func<Car, bool>> predicate = c => c.Brand == "Toyota";
            mockRepository.Setup(r => r.AnyAsync(predicate)).ReturnsAsync(true);
            bool result = await carService.AnyAsync(predicate);
            Assert.IsTrue(result);
        }
        [Test]
        public async Task FindOne_ReturnsCarWhenFound()
        {
            var car = new Car { Id = 1, Brand = "Toyota" };
            mockRepository.Setup(r => r.FindOne(c => c.Id == 1)).ReturnsAsync(car);
            var result = await carService.FindOne(c => c.Id == 1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
            Assert.That(result.Brand.Equals("Toyota"));
        }
        [Test]
        public async Task GetAll_ReturnsAllCars()
        {
            var cars = new List<Car> { new Car { Id = 1 }, new Car { Id = 2 } };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(cars);
            var result = await carService.GetAll();
            Assert.That(result.Count().Equals(2));
        }
        [TestCase(1)]
        public async Task MinPriceOfCarByCategory_ReturnsMinPrice(int categoryId)
        {
       
            var cars = new List<Car>
            {
                new Car { Id = 1, ClassOfCarId = 1, PricePerDay = 100 },
                new Car { Id = 2, ClassOfCarId = 1, PricePerDay = 80 },
                new Car { Id = 3, ClassOfCarId = 2, PricePerDay = 90 }
            };
            mockRepository.Setup(r => r.FindAll(x => x.ClassOfCarId == categoryId)).ReturnsAsync(cars.Where(x => x.ClassOfCarId == categoryId));
            double minPrice = await carService.MinPriceOfCarByCategory(categoryId);
            Assert.That(minPrice.Equals(80));
        }
        [Test]
        public async Task Save_CallsRepositorySave()
        {
            await carService.Save();
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [Test]
        public void Update_CallsRepositoryUpdate()
        {
            var car = new Car { Id = 1 };
            carService.Update(car);
            mockRepository.Verify(r => r.Update(car), Times.Once());
        }
        [Test]
        public async Task PendingCarsCount_ReturnsCountOfPendingCars()
        {
            mockRepository.Setup(r => r.CountAsync(x => x.Pending == true)).ReturnsAsync(5);
            int count = await carService.PendingCarsCount();
            Assert.That(count.Equals(5));
        }
        [Test]
        public async Task CountAsync_ReturnsCorrectCount()
        {
           
            mockRepository.Setup(r => r.CountAsync(c => c.Brand == "Toyota")).ReturnsAsync(3);
            int count = await carService.CountAsync(c => c.Brand == "Toyota");
            Assert.That(count.Equals(3));
        }
        [Test]
        public async Task GetAllOrderBy_ReturnsOrderedCars()
        {
            var cars = new List<Car> { new Car { Id = 2 }, new Car { Id = 1 } };
          
            mockRepository.Setup(r => r.GetAllOrderBy(c => c.Id)).ReturnsAsync(cars.OrderBy(c => c.Id));
            var result = await carService.GetAllOrderBy(c => c.Id);
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task FindAllPendingCars_ReturnsLimitedPendingCars()
        {
            var pendingCars = new List<Car>
            {
                new Car { Id = 1, Pending = true },
                new Car { Id = 2, Pending = true }
            };
            mockRepository.Setup(r => r.FindAllLimited(x => x.Pending == true, 8)).ReturnsAsync(pendingCars);
            var result = await carService.FindAllPendingCars();
            Assert.That(result.Count().Equals(2));
            Assert.IsTrue(result.All(c => c.Pending));
        }
        [Test]
        public async Task Count_ReturnsTotalCount()
        { 
            mockRepository.Setup(r => r.Count()).ReturnsAsync(10);
            int count = await carService.Count();
            Assert.That(count.Equals(10));
        }
        [TestCase(2)]
        public async Task FindAllLimited_ReturnsLimitedCars(int limit)
        {
            var cars = new List<Car> { new Car { Id = 1 }, new Car { Id = 2 } };
            mockRepository.Setup(r => r.FindAllLimited(c => true, limit)).ReturnsAsync(cars);

            var result = await carService.FindAllLimited(c => true, limit);
            Assert.That(result.Count().Equals(2));
        }
        [Test]
        public async Task GetTop10ReservedCars_ReturnsCorrectTuples()
        {
            var car1 = new Car { Id = 1, Brand = "Toyota", Model = "Camry" };
            var car2 = new Car { Id = 2, Brand = "Honda", Model = "Civic" };
            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<Car, bool>>>(p => p.Compile()(car1)))).ReturnsAsync(car1);
            mockRepository.Setup(r => r.FindOne(It.Is<Expression<Func<Car, bool>>>(p => p.Compile()(car2)))).ReturnsAsync(car2);
            List<int> carIds = new List<int> { 1, 2, 1 };

            var result = await carService.GetTop10ReservedCars(carIds);
            Assert.That(result.Count.Equals(3));
            Assert.That(result[0].Equals(("Toyota", "Camry", 2)));
            Assert.That(result[1].Equals(("Honda", "Civic", 1)));
            Assert.That(result[2].Equals(("Toyota", "Camry", 2)));
        }
        [TestCase(1)]
        public async Task GetAllCarsOfCompany_ReturnsCarsOfCompany(int companyId)
        {
         
           
            var cars = new List<Car>
            {
                new Car { Id = 1, CarCompanyId = 1 },
                new Car { Id = 2, CarCompanyId = 2 }
            };
            mockRepository.Setup(r => r.FindAll(x => x.CarCompanyId == companyId)).ReturnsAsync(cars.Where(x => x.CarCompanyId == companyId));

            var result = await carService.GetAllCarsOfCompany(companyId);
            Assert.That(result.Count().Equals(1));
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task GetAllNotReservetedAndNotPendingCars_ReturnsCorrectCars()
        {
          
            var cars = new List<Car>
            {
                new Car { Id = 1, Pending = false },
                new Car { Id = 2, Pending = true },
                new Car { Id = 3, Pending = false }
            };
            List<int> reservedCarIds = new List<int> { 3 };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Car, bool>>>())).ReturnsAsync((Expression<Func<Car, bool>> p) => cars.Where(p.Compile()));

            var result = await carService.GetAllNotReservetedAndNotPendingCars(reservedCarIds);
            Assert.That(result.Count().Equals(1));
            Assert.That(result.First().Id.Equals(1));
        }
        [Test]
        public async Task FindById_ReturnsCar()
        {
            var car = new Car { Id = 1 };
            mockRepository.Setup(r => r.FindOne(x => x.Id == 1)).ReturnsAsync(car);      
            var result = await carService.FindById(1);
            Assert.IsNotNull(result);
            Assert.That(result.Id.Equals(1));
        }
        [Test]
        public async Task SetCarUnavailable_SetsAvailableToFalseAndSaves()
        {
            var car = new Car { Id = 1, Available = true };
            mockRepository.Setup(r => r.FindOne(x => x.Id == 1)).ReturnsAsync(car);
            await carService.SetCarUnavailable(1);
            Assert.IsFalse(car.Available);
            mockRepository.Verify(r => r.Update(car), Times.Once());
            mockRepository.Verify(r => r.Save(), Times.Once());
        }
        [TestCase(1)]
        public async Task GetAllPendingCompanyCars_WithCompanyId_ReturnsOrderedCars(int companyId)
        {
            var cars = new List<Car>
            {
                 new Car { Id = 1, Pending = true, CarCompanyId = 1, CreatedAt = DateTime.Now.AddDays(-1) },
                 new Car { Id = 2, Pending = true, CarCompanyId = 1, CreatedAt = DateTime.Now }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Car, bool>>>())).ReturnsAsync((Expression<Func<Car, bool>> predicate) => cars.Where(predicate.Compile()).ToList());
            var result = await carService.GetAllPendingCompanyCars(companyId);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [TestCase(8)]
        public async Task GetAllPendingCompanyCars_WithoutCompanyId_ReturnsOrderedPendingCars(int limit)
        {      
            var pendingCars = new List<Car>
            {
                new Car { Id = 3, Pending = true, CreatedAt = DateTime.Now.AddDays(-2) },
                new Car { Id = 4, Pending = true, CreatedAt = DateTime.Now.AddDays(-1) }
            };
            mockRepository.Setup(r => r.FindAllLimited(x => x.Pending == true, limit)).ReturnsAsync(pendingCars);    
            var result = await carService.GetAllPendingCompanyCars(null);
            Assert.That(result.Count().Equals(0)); 
        }
    }
}