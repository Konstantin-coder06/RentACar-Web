using Moq;
using RentACar.Core.IServices;
using RentACar.Core.Services;
using RentACar.DataAccess.IRepository;
using RentACar.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Tests
{
    public class CarTests
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
        public void AllWithInclude_CallsRepositoryAllWithInclude_ReturnsExpectedCars()
        {
            var expectedCars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota", Model = "Camry" },
                new Car { Id = 2, Brand = "Honda", Model = "Civic" }
            }.AsQueryable();

            Expression<Func<Car, object>>[] filters = { c => c.ClassOfCar };
            mockRepository.Setup(r => r.AllWithInclude(It.IsAny<Expression<Func<Car, object>>[]>())).Returns(expectedCars);
            var result = carService.AllWithInclude(filters);

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
            mockRepository.Setup(r => r.CountAsync(x => x.Pending == true && x.Available == true)).ReturnsAsync(5);
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
                new Car { Id = 1, Pending = true, Available = true },
                new Car { Id = 2, Pending = true, Available = true }
            };
            mockRepository.Setup(r => r.FindAll(x => x.Pending == true && x.Available == true)).ReturnsAsync(pendingCars);
            var result = await carService.FindAllPendingCars();
            Assert.AreEqual(2, result.Count(), "Expected 2 pending cars");
            Assert.IsTrue(result.All(c => c.Pending), "All cars should be pending");
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
            mockRepository.Setup(r => r.FindOne(x=>x.Id==1)).ReturnsAsync(car1);
            mockRepository.Setup(r => r.FindOne(x=>x.Id==2)).ReturnsAsync(car2);
            var carIdsWithCounts = new List<(int CarId, int Count)>
            {
                (1, 2), 
                (2, 1)
            };
            var result = await carService.GetTop10ReservedCars(carIdsWithCounts);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo(("Toyota", "Camry", 2)));
            Assert.That(result[1], Is.EqualTo(("Honda", "Civic", 1)));
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
                 new Car { Id = 1, Pending = true, Available = true, CarCompanyId = 1, CreatedAt = DateTime.Now.AddDays(-1) },
                 new Car { Id = 2, Pending = true, Available = true, CarCompanyId = 1, CreatedAt = DateTime.Now }
             };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Car, bool>>>())).ReturnsAsync((Expression<Func<Car, bool>> predicate) => cars.Where(predicate.Compile()).ToList());
            var result = await carService.GetAllPendingCompanyCars(companyId);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [Test]
        public async Task PendingCompanyCarsCount_ReturnsCountOfPendingCompanyCars()
        {
            var companyId = 1;
            mockRepository.Setup(r => r.CountAsync(x => x.CarCompanyId == companyId && x.Pending == true && x.Available == true)).ReturnsAsync(3);
            var count = await carService.PendingCompanyCarsCount(companyId);
            Assert.That(count, Is.EqualTo(3), "Expected 3 pending cars for the company");
        }

        [Test]
        public async Task FindAllPendingCarsForAdmin_ReturnsLimitedPendingCars()
        {
            var pendingCars = new List<Car>
            {
                 new Car { Id = 1, Pending = true, Available = true },
                 new Car { Id = 2, Pending = true, Available = true }
            };
            mockRepository.Setup(r => r.FindAllLimited(x => x.Pending == true, 8)).ReturnsAsync(pendingCars);
            var result = await carService.FindAllPendingCarsForAdmin();
            Assert.That(result.Count(), Is.EqualTo(2), "Expected 2 pending cars");
            Assert.IsTrue(result.All(c => c.Pending), "All cars should be pending");
            mockRepository.Verify(r => r.FindAllLimited(x => x.Pending == true, 8), Times.Once());
        }

        [Test]
        public async Task GetPricePerDayByCarId_ReturnsPriceWhenCarFound()
        {
            var carId = 1;
            var car = new Car { Id = carId, PricePerDay = 50.0 };
            mockRepository.Setup(r => r.FindOne(x => x.Id == carId)).ReturnsAsync(car);
            var result = await carService.GetPricePerDayByCarId(carId);
            Assert.That(result, Is.EqualTo(50.0), "Expected price per day to be 50.0");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<Car, bool>>>()), Times.Once());
        }

        [Test]
        public void GetPricePerDayByCarId_ThrowsExceptionWhenCarNotFound()
        {
            var carId = 2;
            mockRepository.Setup(r => r.FindOne(x => x.Id == carId)).ReturnsAsync((Car)null);
            var ex = Assert.ThrowsAsync<Exception>(async () => await carService.GetPricePerDayByCarId(carId));
            Assert.That(ex.Message, Is.EqualTo("The car is not located!"));
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<Car, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetCarsBySearchBrandAndModel_ReturnsMatchingCars()
        {
            var terms = new[] { "toy", "cam" };
            var cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota", Model = "Camry" },
                new Car { Id = 2, Brand = "Honda", Model = "Civic" }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Car, bool>>>())).ReturnsAsync(cars.Where(x => terms.Any(t => x.Brand.ToLower().Contains(t) || x.Model.ToLower().Contains(t))));
            var result = await carService.GetCarsBySearchBrandAndModel(terms);
            Assert.That(result.Count(), Is.EqualTo(1), "Expected 1 matching car");
            Assert.That(result.First().Brand, Is.EqualTo("Toyota"));
            Assert.That(result.First().Model, Is.EqualTo("Camry"));
        }

        [Test]
        public async Task GetCarsBySearchBrandAndModel_ReturnsAllCarsWhenTermsEmpty()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota" },
                new Car { Id = 2, Brand = "Honda" }
            };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(cars);
            var result = await carService.GetCarsBySearchBrandAndModel(null);
            Assert.That(result.Count(), Is.EqualTo(2), "Expected all cars when terms are null");
        }

        [Test]
        public async Task GetAllNotReservationedCarsForOnePeriod_ReturnsNonReservedNonPendingCars()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Pending = false },
                new Car { Id = 2, Pending = true },
                new Car { Id = 3, Pending = false }
            };
            var reservations = new List<Reservation>
            {
                new Reservation { CarId = 3 }
            };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(cars);
            var result = await carService.GetAllNotReservationedCarsForOnePeriod(cars, reservations);
            Assert.That(result.Count(), Is.EqualTo(1), "Expected 1 non-reserved, non-pending car");
            Assert.That(result.First().Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetFilteredCarsAsync_ReturnsFilteredCars()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, PricePerDay = 50, ClassOfCarId = 1, Brand = "Toyota", Color = "Red", DriveTrain = "FWD", Pending = false },
                new Car { Id = 2, PricePerDay = 150, ClassOfCarId = 2, Brand = "Honda", Color = "Blue", DriveTrain = "AWD", Pending = true }
            };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(cars);
            var result = await carService.GetFilteredCarsAsync(
                minPrice: 40,
                maxPrice: 100,
                selectedClassIds: new List<int> { 1 },
                selectedBrands: new List<string> { "Toyota" },
                selectedColors: new List<string> { "Red" },
                selectedDriveTrains: new List<string> { "FWD" }
            );
            Assert.That(result.Count(), Is.EqualTo(1), "Expected 1 filtered car");
            Assert.That(result.First().Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllBrandsDistinct_ReturnsDistinctBrands()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota" },
                new Car { Id = 2, Brand = "Toyota" },
                new Car { Id = 3, Brand = "Honda" }
            };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(cars);
            var result = await carService.GetAllBrandsDistinct();
            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 distinct brands");
            Assert.IsTrue(result.Contains("Toyota"));
            Assert.IsTrue(result.Contains("Honda"));
        }

        [Test]
        public async Task GetAllColorsDistinct_ReturnsDistinctColors()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Color = "Red" },
                new Car { Id = 2, Color = "Red" },
                new Car { Id = 3, Color = "Blue" }
            };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(cars);
            var result = await carService.GetAllColorsDistinct();
            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 distinct colors");
            Assert.IsTrue(result.Contains("Red"));
            Assert.IsTrue(result.Contains("Blue"));
        }

        [Test]
        public async Task GetAllDriveTrainsDistinct_ReturnsDistinctDriveTrains()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, DriveTrain = "FWD" },
                new Car { Id = 2, DriveTrain = "FWD" },
                new Car { Id = 3, DriveTrain = "AWD" }
            };
            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(cars);
            var result = await carService.GetAllDriveTrainsDistinct();
            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 distinct drive trains");
            Assert.IsTrue(result.Contains("FWD"));
            Assert.IsTrue(result.Contains("AWD"));
        }

        [Test]
        public async Task GetCompanyIdByCarId_ReturnsCompanyIdWhenCarFound()
        {
            var carId = 1;
            var car = new Car { Id = carId, CarCompanyId = 10 };
            mockRepository.Setup(r => r.FindOne(x => x.Id == carId)).ReturnsAsync(car);
            var result = await carService.GetCompanyIdByCarId(carId);
            Assert.That(result, Is.EqualTo(10), "Expected company ID to be 10");
            mockRepository.Verify(r => r.FindOne(It.IsAny<Expression<Func<Car, bool>>>()), Times.Once());
        }

        [Test]
        public async Task GetFilteredCarsOfCompanyAsync_ReturnsFilteredCompanyCars()
        {
            var companyId = 1;
            var cars = new List<Car>
            {
                new Car { Id = 1, CarCompanyId = 1, PricePerDay = 60, ClassOfCarId = 1, Brand = "Toyota", Color = "Red", DriveTrain = "FWD", Pending = false },
                new Car { Id = 2, CarCompanyId = 1, PricePerDay = 200, ClassOfCarId = 2, Brand = "Honda", Color = "Blue", DriveTrain = "AWD", Pending = false }
            };
            mockRepository.Setup(r => r.FindAll(x => x.CarCompanyId == companyId)).ReturnsAsync(cars);
            var result = await carService.GetFilteredCarsOfCompanyAsync(
                companyId: companyId,
                minPrice: 50,
                maxPrice: 100,
                selectedClassIds: new List<int> { 1 },
                selectedBrands: new List<string> { "Toyota" },
                selectedColors: new List<string> { "Red" },
                selectedDriveTrains: new List<string> { "FWD" }
            );
            Assert.That(result.Count(), Is.EqualTo(1), "Expected 1 filtered car");
            Assert.That(result.First().Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GetCarsOfCompanyBySearchBrandAndModel_ReturnsMatchingCars()
        {
            var companyId = 1;
            var terms = new[] { "toy", "cam" };
            var cars = new List<Car>
            {
                new Car { Id = 1, CarCompanyId = 1, Brand = "Toyota", Model = "Camry" },
                new Car { Id = 2, CarCompanyId = 1, Brand = "Honda", Model = "Civic" }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Car, bool>>>())).ReturnsAsync(cars.Where(x => x.CarCompanyId == companyId && terms.Any(t => x.Brand.ToLower().Contains(t) || x.Model.ToLower().Contains(t))));
            var result = await carService.GetCarsOfCompanyBySearchBrandAndModel(companyId, terms);
            Assert.That(result.Count(), Is.EqualTo(1), "Expected 1 matching car");
            Assert.That(result.First().Brand, Is.EqualTo("Toyota"));
            Assert.That(result.First().Model, Is.EqualTo("Camry"));
        }

        [Test]
        public async Task GetAllCarsIdByCompanyId_ReturnsCarIds()
        {
            var companyId = 1;
            var cars = new List<Car>
            {
                new Car { Id = 1, CarCompanyId = 1 },
                new Car { Id = 2, CarCompanyId = 1 },
                new Car { Id = 3, CarCompanyId = 2 }
            };
            mockRepository.Setup(r => r.FindAll(x => x.CarCompanyId == companyId)).ReturnsAsync(cars.Where(x => x.CarCompanyId == companyId));
            var result = await carService.GetAllCarsIdByCompanyId(companyId);
            Assert.That(result.Count, Is.EqualTo(2), "Expected 2 car IDs");
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(2));
        }

        [Test]
        public async Task SearchCarsByBrandOrModel_ReturnsMatchingCars()
        {
            var companyId = 1;
            var terms = new[] { "toy", "cam" };
            var cars = new List<Car>
            {
                new Car { Id = 1, CarCompanyId = 1, Brand = "Toyota", Model = "Camry" },
                new Car { Id = 2, CarCompanyId = 1, Brand = "Honda", Model = "Civic" }
            };
            mockRepository.Setup(r => r.FindAll(It.IsAny<Expression<Func<Car, bool>>>())).ReturnsAsync(cars.Where(x => x.CarCompanyId == companyId && terms.All(t => x.Brand.ToLower().Contains(t) || x.Model.ToLower().Contains(t))));
            var result = await carService.SearchCarsByBrandOrModel(companyId, terms);
            Assert.That(result.Count(), Is.EqualTo(1), "Expected 1 matching car");
            Assert.That(result.First().Brand, Is.EqualTo("Toyota"));
            Assert.That(result.First().Model, Is.EqualTo("Camry"));
        }

        [Test]
        public void TotalPriceOfCar_ReturnsCorrectPrice()
        {
            var price = 50.0;
            var days = 3;
            var result = carService.TotalPriceOfCar(price, days);
            Assert.That(result, Is.EqualTo(150.0), "Expected total price to be 150.0");
        }

        [Test]
        public void PriceOfTaxes_ReturnsCorrectTax()
        {
            var price = 100.0;
            var result = carService.PriceOfTaxes(price);
            Assert.That(result, Is.EqualTo(9.0), "Expected tax to be 9.0");
        }

        [Test]
        public void Difference_ReturnsCorrectDifference()
        {
            var price = 100.0;
            var second = 40.0;
            var result = carService.Difference(price, second);
            Assert.That(result, Is.EqualTo(60.0), "Expected difference to be 60.0");
        }

        [Test]
        public async Task FilterCarsByCategories_ReturnsFilteredCars()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, ClassOfCarId = 1 },
                new Car { Id = 2, ClassOfCarId = 2 },
                new Car { Id = 3, ClassOfCarId = 1 }
            };
            var categoriesIds = new List<int> { 1 };
            var result = await carService.FilterCarsByCategories(cars, categoriesIds);
            Assert.That(result.Count(), Is.EqualTo(2), "Expected 2 cars in category 1");
            Assert.IsTrue(result.All(c => c.ClassOfCarId == 1));
        }

        [Test]
        public async Task OrderByPrice_ReturnsCarsOrderedByPrice()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, PricePerDay = 100 },
                new Car { Id = 2, PricePerDay = 50 }
            };
            var result = await carService.OrderByPrice(cars);
            Assert.That(result.First().PricePerDay, Is.EqualTo(50), "Expected lowest price first");
        }

        [Test]
        public async Task OrderByDescendingPrice_ReturnsCarsOrderedByDescendingPrice()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, PricePerDay = 100 },
                new Car { Id = 2, PricePerDay = 50 }
            };
            var result = await carService.OrderByDescendingPrice(cars);
            Assert.That(result.First().PricePerDay, Is.EqualTo(100), "Expected highest price first");
        }

        [Test]
        public async Task OrderByBrand_ReturnsCarsOrderedByBrand()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Honda" },
                new Car { Id = 2, Brand = "Toyota" }
            };
            var result = await carService.OrderByBrand(cars);
            Assert.That(result.First().Brand, Is.EqualTo("Honda"), "Expected Honda first");
        }

        [Test]
        public async Task OrderByDescendingBrand_ReturnsCarsOrderedByDescendingBrand()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Honda" },
                new Car { Id = 2, Brand = "Toyota" }
            };
            var result = await carService.OrderByDescendingBrand(cars);
            Assert.That(result.First().Brand, Is.EqualTo("Toyota"), "Expected Toyota first");
        }

        [Test]
        public async Task OrderByYear_ReturnsCarsOrderedByYear()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Year = 2020 },
                new Car { Id = 2, Year = 2022 }
            };
            var result = await carService.OrderByYear(cars);
            Assert.That(result.First().Year, Is.EqualTo(2020), "Expected oldest year first");
        }

        [Test]
        public async Task OrderByDescendingYear_ReturnsCarsOrderedByDescendingYear()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1, Year = 2020 },
                new Car { Id = 2, Year = 2022 }
            };
            var result = await carService.OrderByDescendingYear(cars);
            Assert.That(result.First().Year, Is.EqualTo(2022), "Expected newest year first");
        }
    }
}