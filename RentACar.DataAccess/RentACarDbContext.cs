using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DataAccess
{
    public class RentACarDbContext:IdentityDbContext<IdentityUser>
    {
        public RentACarDbContext(DbContextOptions<RentACarDbContext> options) : base(options) { }   
        public DbSet<Car> Cars { get; set; }
        public DbSet<CType> Types { get; set; }
        public DbSet<CarType> CarsTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CarFeature> CarsFeatures { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer>Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
new Category { Id = 1, Name = "Economy" },
new Category { Id = 2, Name = "Luxury" },
new Category { Id = 3, Name = "Sport" },
new Category { Id = 4, Name = "German Cars" },
new Category { Id = 5, Name = "Electric" },
new Category { Id = 6, Name = "SUV" },
new Category { Id = 7, Name = "Muscle Cars" },
new Category { Id = 8, Name = "JDM Cars" },
new Category { Id = 9, Name = "Compact" },
new Category { Id = 10, Name = "Classic" }
);
            modelBuilder.Entity<Car>().HasData(
          new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Gearbox = "Automatic", Year = 2021, PricePerDay = 40, PricePerWeek = 250, MileageLimitForDay = 150, MileageLimitForWeek = 1000, AdditionalMileageCharge = 0.2, EngineCapacity = 1.8, Color = "White", Available = true, Description = "Compact and fuel-efficient", CategoryId = 9 },
          new Car { Id = 2, Brand = "Honda", Model = "Civic", Gearbox = "Manual", Year = 2022, PricePerDay = 50, PricePerWeek = 300, MileageLimitForDay = 200, MileageLimitForWeek = 1200, AdditionalMileageCharge = 0.25, EngineCapacity = 2.0, Color = "Black", Available = true, Description = "Sporty and reliable", CategoryId = 3 },
          new Car { Id = 3, Brand = "Ford", Model = "Mustang", Gearbox = "Automatic", Year = 2018, PricePerDay = 85, PricePerWeek = 510, MileageLimitForDay = 250, MileageLimitForWeek = 1500, AdditionalMileageCharge = 0.22, EngineCapacity = 5.2, Color = "Blue", Available = true, Description = "Comfortable and stylish", CategoryId = 7 },
          new Car { Id = 4, Brand = "BMW", Model = "420i", Gearbox = "Automatic", Year = 2023, PricePerDay = 100, PricePerWeek = 600, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 0.5, EngineCapacity = 0.0, Color = "Gray", Available = true, Description = "Luxury and performance", CategoryId = 2 },
          new Car { Id = 5, Brand = "Mercedes-Benz", Model = "C-Class", Gearbox = "Automatic", Year = 2023, PricePerDay = 110, PricePerWeek = 650, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 0.55, EngineCapacity = 2.1, Color = "Silver", Available = true, Description = "Premium luxury", CategoryId = 2 },
          new Car { Id = 6, Brand = "Audi", Model = "R8", Gearbox = "Automatic", Year = 2023, PricePerDay = 300, PricePerWeek = 1800, MileageLimitForDay = 400, MileageLimitForWeek = 2500, AdditionalMileageCharge = 1.0, EngineCapacity = 5.2, Color = "Red", Available = true, Description = "High-performance sports car", CategoryId = 3 },
          new Car { Id = 7, Brand = "Lamborghini", Model = "Huracan", Gearbox = "Automatic", Year = 2023, PricePerDay = 500, PricePerWeek = 3000, MileageLimitForDay = 400, MileageLimitForWeek = 3000, AdditionalMileageCharge = 1.5, EngineCapacity = 5.2, Color = "Yellow", Available = true, Description = "Exotic sports car", CategoryId = 3 },
          new Car { Id = 8, Brand = "Porsche", Model = "911 Turbo S", Gearbox = "Automatic", Year = 2023, PricePerDay = 400, PricePerWeek = 2400, MileageLimitForDay = 400, MileageLimitForWeek = 3000, AdditionalMileageCharge = 1.3, EngineCapacity = 3.8, Color = "Blue", Available = true, Description = "Luxury sports car", CategoryId = 2 },
          new Car { Id = 9, Brand = "Tesla", Model = "Model S Plaid", Gearbox = "Automatic", Year = 2023, PricePerDay = 350, PricePerWeek = 2100, MileageLimitForDay = 400, MileageLimitForWeek = 2500, AdditionalMileageCharge = 1.0, EngineCapacity = 0.0, Color = "Black", Available = true, Description = "Electric luxury sedan", CategoryId = 5 },
          new Car { Id = 10, Brand = "Ferrari", Model = "F8 Tributo", Gearbox = "Automatic", Year = 2023, PricePerDay = 600, PricePerWeek = 3600, MileageLimitForDay = 400, MileageLimitForWeek = 3000, AdditionalMileageCharge = 2.0, EngineCapacity = 3.9, Color = "Red", Available = true, Description = "Iconic Italian sports car", CategoryId = 3 },
          new Car { Id = 11, Brand = "Rolls-Royce", Model = "Phantom", Gearbox = "Automatic", Year = 2023, PricePerDay = 1000, PricePerWeek = 7000, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 3.0, EngineCapacity = 6.7, Color = "White", Available = true, Description = "Ultimate luxury car", CategoryId = 2 },
          new Car { Id = 12, Brand = "Bentley", Model = "Continental GT", Gearbox = "Automatic", Year = 2023, PricePerDay = 800, PricePerWeek = 5000, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 2.5, EngineCapacity = 6.0, Color = "Silver", Available = true, Description = "Grand luxury tourer", CategoryId = 2 },
          new Car { Id = 13, Brand = "McLaren", Model = "720S", Gearbox = "Automatic", Year = 2023, PricePerDay = 700, PricePerWeek = 4200, MileageLimitForDay = 400, MileageLimitForWeek = 3000, AdditionalMileageCharge = 2.0, EngineCapacity = 4.0, Color = "Orange", Available = true, Description = "Exquisite British engineering", CategoryId = 3 },
          new Car { Id = 14, Brand = "Lexus", Model = "RX 500h", Gearbox = "Automatic", Year = 2023, PricePerDay = 250, PricePerWeek = 1500, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 0.8, EngineCapacity = 2.4, Color = "Blue", Available = true, Description = "Luxury hybrid SUV", CategoryId = 6 },
          new Car { Id = 15, Brand = "Aston Martin", Model = "DB11", Gearbox = "Automatic", Year = 2023, PricePerDay = 600, PricePerWeek = 3600, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 2.0, EngineCapacity = 5.2, Color = "Green", Available = true, Description = "Luxury British grand tourer", CategoryId = 2 },
          new Car { Id = 16, Brand = "Lexus", Model = "LC 500", Gearbox = "Automatic", Year = 2023, PricePerDay = 500, PricePerWeek = 3000, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 1.8, EngineCapacity = 5.0, Color = "Black", Available = true, Description = "Sophisticated luxury coupe", CategoryId = 2 },
          new Car { Id = 17, Brand = "Mercedes-Benz", Model = "AMG GT", Gearbox = "Automatic", Year = 2023, PricePerDay = 700, PricePerWeek = 4200, MileageLimitForDay = 400, MileageLimitForWeek = 2500, AdditionalMileageCharge = 2.2, EngineCapacity = 4.0, Color = "Yellow", Available = true, Description = "German engineering excellence", CategoryId = 3 },
          new Car { Id = 18, Brand = "Audi", Model = "Q8", Gearbox = "Automatic", Year = 2023, PricePerDay = 300, PricePerWeek = 1800, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 1.0, EngineCapacity = 3.0, Color = "White", Available = true, Description = "Luxury SUV", CategoryId = 6 },
          new Car { Id = 19, Brand = "Toyota", Model = "Supra", Gearbox = "Automatic", Year = 2023, PricePerDay = 350, PricePerWeek = 2000, MileageLimitForDay = 300, MileageLimitForWeek = 2000, AdditionalMileageCharge = 1.5, EngineCapacity = 3.0, Color = "Red", Available = true, Description = "Sporty and agile", CategoryId = 3 },
          new Car { Id = 20, Brand = "Toyota", Model = "Camry", Gearbox = "Automatic", Year = 2021, PricePerDay = 50, PricePerWeek = 300, MileageLimitForDay = 200, MileageLimitForWeek = 1200, AdditionalMileageCharge = 0.25, EngineCapacity = 2.5, Color = "Blue", Available = true, Description = "Dependable sedan", CategoryId = 9 }
      );
            modelBuilder.Entity<Feature>().HasData(
      new Feature { Id = 1, NameOfFeatures = "Air Conditioning" },
      new Feature { Id = 2, NameOfFeatures = "Heated Seats" },
      new Feature { Id = 3, NameOfFeatures = "Bluetooth Connectivity" },
      new Feature { Id = 4, NameOfFeatures = "Navigation System" },
      new Feature { Id = 5, NameOfFeatures = "Cruise Control" },
      new Feature { Id = 6, NameOfFeatures = "Rearview Camera" },
      new Feature { Id = 7, NameOfFeatures = "Lane Departure Warning" },
      new Feature { Id = 8, NameOfFeatures = "Adaptive Headlights" },
      new Feature { Id = 9, NameOfFeatures = "Blind Spot Detection" },
      new Feature { Id = 10, NameOfFeatures = "Automatic Parking" },
      new Feature { Id = 11, NameOfFeatures = "Push-Button Start" },
      new Feature { Id = 12, NameOfFeatures = "Wireless Charging" },
      new Feature { Id = 13, NameOfFeatures = "Panoramic Sunroof" },
      new Feature { Id = 14, NameOfFeatures = "Power Adjustable Seats" },
      new Feature { Id = 15, NameOfFeatures = "Backup Sensors" },
      new Feature { Id = 16, NameOfFeatures = "Heated Steering Wheel" },
      new Feature { Id = 17, NameOfFeatures = "Keyless Entry" },
      new Feature { Id = 18, NameOfFeatures = "Surround-View Camera" },
      new Feature { Id = 19, NameOfFeatures = "Remote Start" },
      new Feature { Id = 20, NameOfFeatures = "Apple CarPlay" },
      new Feature { Id = 21, NameOfFeatures = "Android Auto" },
      new Feature { Id = 22, NameOfFeatures = "Rear Cross Traffic Alert" },
      new Feature { Id = 23, NameOfFeatures = "Adaptive Cruise Control" },
      new Feature { Id = 24, NameOfFeatures = "Heads-Up Display (HUD)" },
      new Feature { Id = 25, NameOfFeatures = "Ambient Lighting" },
      new Feature { Id = 26, NameOfFeatures = "Wireless Apple CarPlay" },
      new Feature { Id = 27, NameOfFeatures = "Power Tailgate" },
      new Feature { Id = 28, NameOfFeatures = "Automatic Emergency Braking" },
      new Feature { Id = 29, NameOfFeatures = "Traffic Sign Recognition" },
      new Feature { Id = 30, NameOfFeatures = "Rain-Sensing Wipers" },
      new Feature { Id = 31, NameOfFeatures = "Dual-Zone Climate Control" },
      new Feature { Id = 32, NameOfFeatures = "Power Folding Mirrors" },
      new Feature { Id = 33, NameOfFeatures = "Lane Keeping Assist" },
      new Feature { Id = 34, NameOfFeatures = "Electric Power Steering" },
      new Feature { Id = 35, NameOfFeatures = "Turbocharged Engine" },
      new Feature { Id = 36, NameOfFeatures = "Carbon Fiber Trim" },
      new Feature { Id = 37, NameOfFeatures = "Active Noise Cancellation" },
      new Feature { Id = 38, NameOfFeatures = "All-Wheel Drive" },
      new Feature { Id = 39, NameOfFeatures = "Front Parking Sensors" },
      new Feature { Id = 40, NameOfFeatures = "Sport Mode" },
      new Feature { Id = 41, NameOfFeatures = "Wireless Smartphone Charging" },
      new Feature { Id = 42, NameOfFeatures = "Massaging Seats" },
      new Feature { Id = 43, NameOfFeatures = "Self-Healing Paint" },
      new Feature { Id = 44, NameOfFeatures = "Night Vision" },
      new Feature { Id = 45, NameOfFeatures = "Active Rear Spoiler" },
      new Feature { Id = 46, NameOfFeatures = "Heads-Up Display with Navigation" },
      new Feature { Id = 47, NameOfFeatures = "Wireless Internet Access" },
      new Feature { Id = 48, NameOfFeatures = "Active Suspension" },
      new Feature { Id = 49, NameOfFeatures = "Power Adjustable Steering Wheel" },
      new Feature { Id = 50, NameOfFeatures = "Touchscreen Control Panel" }
  );

            modelBuilder.Entity<Image>().HasData(
                new Image { Id = 1,Url="/images/toyota-corolla1.jpg", CarId=1 },
                new Image { Id = 2, Url = "/images/toyota-corolla2.jpg", CarId = 1 },
                new Image { Id = 3, Url = "/images/toyota-corolla3.jpg", CarId = 1 },
                new Image { Id = 4, Url = "/images/toyota-corolla4.jpg", CarId = 1 },
                new Image { Id = 5, Url = "/images/honda-civic1.png", CarId = 2 },
                new Image { Id = 6, Url = "/images/honda-civic2.png", CarId = 2 },
                new Image { Id = 7, Url = "/images/honda-civic3.png", CarId = 2 },
                new Image { Id = 8, Url = "/images/honda-civic4.png", CarId = 2 },
                new Image { Id = 9, Url = "/images/bmw-420i1.png", CarId = 3 },
                new Image { Id = 10, Url = "/images/bmw-420i2.png", CarId = 3 },
                new Image { Id = 11, Url = "/images/bmw-420i7.png", CarId = 3 },
                new Image { Id = 12, Url = "/images/bmw-420i3.png", CarId = 3 },
                new Image { Id = 13, Url = "/images/bmw-420i4.png", CarId = 3 },
                new Image { Id = 14, Url = "/images/bmw-420i5.png", CarId = 3 },
                new Image { Id = 15, Url = "/images/bmw-420i6.png", CarId = 3 },
                new Image { Id = 16, Url = "/images/c300-1.webp", CarId = 4 },
                new Image { Id = 17, Url = "/images/c300-2.webp", CarId = 4 },
                new Image { Id = 18, Url = "/images/c300-3.webp", CarId = 4 },
                new Image { Id = 19, Url = "/images/c300-4.webp", CarId = 4 },
                new Image { Id = 20, Url = "/images/c300-5.webp", CarId = 4 },
                new Image { Id = 21, Url = "/images/c300-6.webp", CarId = 4 }

                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
