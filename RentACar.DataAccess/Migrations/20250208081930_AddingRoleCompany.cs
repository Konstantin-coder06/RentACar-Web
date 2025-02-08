using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingRoleCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassOfCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassOfCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfFeatures = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarCompanies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gearbox = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<double>(type: "float", nullable: false),
                    PricePerWeek = table.Column<double>(type: "float", nullable: false),
                    MileageLimitForDay = table.Column<double>(type: "float", nullable: false),
                    MileageLimitForWeek = table.Column<double>(type: "float", nullable: false),
                    AdditionalMileageCharge = table.Column<double>(type: "float", nullable: false),
                    EngineCapacity = table.Column<double>(type: "float", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriveTrain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorsePower = table.Column<int>(type: "int", nullable: false),
                    ZeroToHundred = table.Column<double>(type: "float", nullable: false),
                    TopSpeed = table.Column<double>(type: "float", nullable: false),
                    ClassOfCarId = table.Column<int>(type: "int", nullable: false),
                    CarCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarCompanies_CarCompanyId",
                        column: x => x.CarCompanyId,
                        principalTable: "CarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_ClassOfCars_ClassOfCarId",
                        column: x => x.ClassOfCarId,
                        principalTable: "ClassOfCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarsFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarsFeatures_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarsFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarsTypes_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarsTypes_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSelfPick = table.Column<bool>(type: "bit", nullable: false),
                    PaidDeliveryPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReturnBackAtSamePlace = table.Column<bool>(type: "bit", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "Address", "City", "Country", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Sheikh Zayed Road, Dubai", "Dubai", "UAE", "Luxury and sports car rentals in Dubai.", "Dubai Luxury Cars", null },
                    { 2, "Al Barsha, Dubai", "Dubai", "UAE", "Affordable and premium car rental services.", "Speedy Drive Car Rental", null },
                    { 3, "Business Bay, Dubai", "Dubai", "UAE", "Exclusive supercar rentals for special occasions.", "Prestige Exotic Car Rental", null },
                    { 4, "Deira, Dubai", "Dubai", "UAE", "Budget-friendly car rental options.", "Quick Lease Car Rental", null },
                    { 5, "Downtown Dubai", "Dubai", "UAE", "Wide selection of vehicles from economy to luxury.", "OneClickDrive", null }
                });

            migrationBuilder.InsertData(
                table: "ClassOfCars",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Economy" },
                    { 2, "Luxury" },
                    { 3, "Sport" },
                    { 4, "Business" },
                    { 5, "Electric" },
                    { 6, "Standard" },
                    { 7, "Retro" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "NameOfFeatures" },
                values: new object[,]
                {
                    { 1, "Air Conditioning" },
                    { 2, "Heated Seats" },
                    { 3, "Bluetooth Connectivity" },
                    { 4, "Navigation System" },
                    { 5, "Cruise Control" },
                    { 6, "Rearview Camera" },
                    { 7, "Lane Departure Warning" },
                    { 8, "Adaptive Headlights" },
                    { 9, "Blind Spot Detection" },
                    { 10, "Automatic Parking" },
                    { 11, "Push-Button Start" },
                    { 12, "Wireless Charging" },
                    { 13, "Panoramic Sunroof" },
                    { 14, "Power Adjustable Seats" },
                    { 15, "Backup Sensors" },
                    { 16, "Heated Steering Wheel" },
                    { 17, "Keyless Entry" },
                    { 18, "Surround-View Camera" },
                    { 19, "Remote Start" },
                    { 20, "Apple CarPlay" },
                    { 21, "Android Auto" },
                    { 22, "Rear Cross Traffic Alert" },
                    { 23, "Adaptive Cruise Control" },
                    { 24, "Heads-Up Display (HUD)" },
                    { 25, "Ambient Lighting" },
                    { 26, "Wireless Apple CarPlay" },
                    { 27, "Power Tailgate" },
                    { 28, "Automatic Emergency Braking" },
                    { 29, "Traffic Sign Recognition" },
                    { 30, "Rain-Sensing Wipers" },
                    { 31, "Dual-Zone Climate Control" },
                    { 32, "Power Folding Mirrors" },
                    { 33, "Lane Keeping Assist" },
                    { 34, "Electric Power Steering" },
                    { 35, "Turbocharged Engine" },
                    { 36, "Carbon Fiber Trim" },
                    { 37, "Active Noise Cancellation" },
                    { 38, "All-Wheel Drive" },
                    { 39, "Front Parking Sensors" },
                    { 40, "Sport Mode" },
                    { 41, "Wireless Smartphone Charging" },
                    { 42, "Massaging Seats" },
                    { 43, "Self-Healing Paint" },
                    { 44, "Night Vision" },
                    { 45, "Active Rear Spoiler" },
                    { 46, "Heads-Up Display with Navigation" },
                    { 47, "Wireless Internet Access" },
                    { 48, "Active Suspension" },
                    { 49, "Power Adjustable Steering Wheel" },
                    { 50, "Touchscreen Control Panel" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name", "SeatCapacity" },
                values: new object[,]
                {
                    { 1, "Sedan", 4 },
                    { 2, "SUV", 5 },
                    { 3, "SUV", 7 },
                    { 4, "Hatchback", 4 },
                    { 5, "Coupe", 2 },
                    { 6, "Grand Coupe", 4 },
                    { 7, "Convertible", 2 },
                    { 8, "Convertible", 4 },
                    { 9, "Minivan", 7 },
                    { 10, "Pickup", 2 },
                    { 11, "Pickup", 4 },
                    { 12, "Wagon", 5 }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AdditionalMileageCharge", "Available", "Brand", "CarCompanyId", "ClassOfCarId", "Color", "Description", "DriveTrain", "EngineCapacity", "Gearbox", "HorsePower", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "TopSpeed", "Year", "ZeroToHundred" },
                values: new object[,]
                {
                    { 1, 0.20000000000000001, true, "Toyota", 5, 1, "White", "Compact and fuel-efficient", "Front", 1.6000000000000001, "Automatic", 122, 150.0, 1000.0, "Corolla", 80.0, 450.0, 185.0, 2021, 10.800000000000001 },
                    { 2, 0.25, true, "Honda", 5, 6, "Black", "Sporty and reliable", "Front", 1.5, "Manual", 205, 200.0, 1200.0, "Civic", 140.0, 600.0, 170.0, 2022, 7.2999999999999998 },
                    { 3, 0.22, true, "Ford", 1, 3, "Dark Gray", "Comfortable and stylish", "Rear", 2.2999999999999998, "Automatic", 317, 250.0, 1500.0, "Mustang", 200.0, 910.0, 250.0, 2020, 5.7999999999999998 },
                    { 4, 0.5, true, "BMW", 1, 2, "Gray", "Luxury and performance", "Rear", 2.0, "Automatic", 190, 300.0, 2000.0, "420", 250.0, 1000.0, 240.0, 2022, 7.0999999999999996 },
                    { 5, 0.55000000000000004, true, "Mercedes-Benz", 1, 2, "Red", "Premium luxury", "Rear", 3.0, "Automatic", 258, 300.0, 2000.0, "C-Class", 210.0, 850.0, 250.0, 2021, 6.2000000000000002 },
                    { 6, 1.0, true, "Audi", 2, 3, "Green", "High-performance sports car", "Rear", 5.2000000000000002, "Automatic", 570, 400.0, 2500.0, "R8 Spyder", 750.0, 6400.0, 327.0, 2021, 3.7999999999999998 },
                    { 7, 1.5, true, "Lamborghini", 2, 3, "Black", "Exotic sports car", "Rear", 5.2000000000000002, "Automatic", 640, 400.0, 3000.0, "Huracan", 1700.0, 9000.0, 310.0, 2023, 3.0 },
                    { 8, 1.3, true, "Porsche", 1, 3, "Silver", "Luxury sports car", "Full Wheels", 3.0, "Automatic", 450, 400.0, 3000.0, "911 GT3", 1400.0, 8400.0, 304.0, 2023, 3.7999999999999998 },
                    { 9, 1.0, true, "Tesla", 3, 5, "White", "Electric luxury sedan", "Rear", 0.0, "Automatic", 1020, 400.0, 2500.0, "Model S Plaid", 350.0, 2100.0, 322.0, 2023, 2.1000000000000001 },
                    { 10, 2.0, true, "Ferrari", 3, 3, "Red", "Iconic Italian sports car", "Rear", 3.8999999999999999, "Automatic", 720, 400.0, 3000.0, "F8 Spider", 2300.0, 10000.0, 340.0, 2022, 2.8999999999999999 },
                    { 11, 2.0, true, "Rolls-Royce", 3, 4, "Black", "Ultimate luxury car", "Rear", 6.7999999999999998, "Automatic", 571, 300.0, 2000.0, "Phantom", 1000.0, 7000.0, 250.0, 2021, 5.4000000000000004 },
                    { 12, 2.5, true, "Bentley", 1, 4, "Black", "Grand luxury tourer", "Full Wheels", 6.0, "Automatic", 659, 300.0, 2000.0, "Continental GT-GTC", 1300.0, 7600.0, 335.0, 2023, 3.7000000000000002 },
                    { 13, 2.0, true, "McLaren", 2, 3, "Blue", "Exquisite British engineering", "Rear", 4.0, "Automatic", 720, 400.0, 3000.0, "720S", 1700.0, 8000.0, 341.0, 2023, 2.8999999999999999 },
                    { 14, 2.0, true, "Aston Martin", 2, 4, "Black", "Luxury British SUV", "Full Wheels", 4.0, "Automatic", 550, 300.0, 2000.0, "DBX", 600.0, 3600.0, 291.0, 2023, 4.5 },
                    { 15, 1.8, true, "Lexus", 4, 2, "Silver", "Sophisticated luxury SUV", "Full Wheels", 3.5, "Automatic", 415, 300.0, 2000.0, "LX", 500.0, 3000.0, 210.0, 2023, 6.7999999999999998 },
                    { 16, 2.2000000000000002, true, "Mercedes-Benz", 2, 3, "Orange", "German engineering excellence", "Rear", 4.0, "Automatic", 730, 400.0, 2500.0, "AMG GT", 2500.0, 1100.0, 322.0, 2022, 3.2000000000000002 },
                    { 17, 1.0, true, "Audi", 5, 4, "Orange", "Luxury SUV", "Full Wheels", 3.0, "Automatic", 340, 300.0, 2000.0, "Q8", 400.0, 2300.0, 250.0, 2021, 5.9000000000000004 },
                    { 18, 1.5, true, "BMW", 2, 3, "Yellow", "Sporty and agile", "Full Wheels", 3.0, "Automatic", 510, 300.0, 2000.0, "M4", 700.0, 4000.0, 250.0, 2023, 3.7000000000000002 },
                    { 19, 0.25, true, "Toyota", 2, 6, "Red", "Dependable sedan", "Rear", 2.5, "Automatic", 182, 200.0, 1200.0, "Camry", 130.0, 670.0, 210.0, 2021, 9.9000000000000004 },
                    { 20, 1.8, true, "Tesla", 3, 5, "Black", "Sophisticated luxury SUV", "Full Wheels", 0.0, "Automatic", 845, 300.0, 2000.0, "Cybertruck", 2000.0, 12000.0, 210.0, 2024, 2.7000000000000002 },
                    { 21, 1.8, true, "Land Rover", 2, 4, "Black", "Sophisticated luxury SUV", "Full Wheels", 3.0, "Automatic", 360, 300.0, 2000.0, "Range Rover", 1200.0, 7000.0, 209.0, 2023, 6.9000000000000004 },
                    { 22, 1.8, true, "Toyota", 1, 3, "White", "Sporty and agile", "Rear", 3.0, "Automatic", 340, 300.0, 2000.0, "Supra", 220.0, 1100.0, 262.0, 2022, 4.4000000000000004 }
                });

            migrationBuilder.InsertData(
                table: "CarsFeatures",
                columns: new[] { "Id", "CarId", "FeatureId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 3 },
                    { 3, 1, 5 },
                    { 4, 1, 6 },
                    { 5, 1, 7 },
                    { 6, 1, 23 },
                    { 7, 2, 1 },
                    { 8, 2, 3 },
                    { 9, 2, 5 },
                    { 10, 2, 6 },
                    { 11, 2, 7 },
                    { 12, 2, 9 },
                    { 13, 2, 17 },
                    { 14, 2, 19 },
                    { 15, 2, 20 },
                    { 16, 2, 21 },
                    { 17, 2, 23 },
                    { 18, 2, 28 },
                    { 19, 2, 30 },
                    { 20, 2, 31 },
                    { 21, 2, 33 },
                    { 22, 2, 38 },
                    { 23, 3, 1 },
                    { 24, 3, 3 },
                    { 25, 3, 5 },
                    { 26, 3, 6 },
                    { 27, 3, 7 },
                    { 28, 3, 9 },
                    { 29, 3, 17 },
                    { 30, 3, 19 },
                    { 31, 3, 20 },
                    { 32, 3, 21 },
                    { 33, 3, 28 },
                    { 34, 3, 33 },
                    { 35, 3, 40 },
                    { 36, 3, 42 },
                    { 37, 3, 45 },
                    { 38, 3, 46 },
                    { 39, 4, 1 },
                    { 40, 4, 3 },
                    { 41, 4, 5 },
                    { 42, 4, 6 },
                    { 43, 4, 7 },
                    { 44, 4, 9 },
                    { 45, 4, 17 },
                    { 46, 4, 19 },
                    { 47, 4, 20 },
                    { 48, 4, 21 },
                    { 49, 4, 23 },
                    { 50, 4, 28 },
                    { 51, 4, 30 },
                    { 52, 4, 31 },
                    { 53, 4, 33 },
                    { 54, 4, 34 },
                    { 55, 4, 36 },
                    { 56, 4, 40 },
                    { 57, 4, 41 },
                    { 58, 4, 45 },
                    { 59, 4, 47 },
                    { 60, 4, 33 },
                    { 61, 4, 34 },
                    { 62, 5, 1 },
                    { 63, 5, 3 },
                    { 64, 5, 5 },
                    { 65, 5, 6 },
                    { 66, 5, 7 },
                    { 67, 5, 9 },
                    { 68, 5, 17 },
                    { 69, 5, 19 },
                    { 70, 5, 20 },
                    { 71, 5, 21 },
                    { 72, 5, 23 },
                    { 73, 5, 28 },
                    { 74, 5, 30 },
                    { 75, 5, 31 },
                    { 76, 5, 33 },
                    { 77, 5, 34 },
                    { 78, 5, 40 },
                    { 79, 5, 41 },
                    { 80, 5, 45 },
                    { 81, 5, 47 },
                    { 82, 6, 1 },
                    { 83, 6, 3 },
                    { 84, 6, 5 },
                    { 85, 6, 6 },
                    { 86, 6, 7 },
                    { 87, 6, 9 },
                    { 88, 6, 17 },
                    { 89, 6, 19 },
                    { 90, 6, 20 },
                    { 91, 6, 21 },
                    { 92, 6, 23 },
                    { 93, 6, 28 },
                    { 94, 6, 30 },
                    { 95, 6, 31 },
                    { 96, 6, 33 },
                    { 97, 6, 34 },
                    { 98, 6, 40 },
                    { 99, 6, 41 },
                    { 100, 6, 45 },
                    { 101, 6, 47 },
                    { 102, 7, 1 },
                    { 103, 7, 3 },
                    { 104, 7, 5 },
                    { 105, 7, 6 },
                    { 106, 7, 7 },
                    { 107, 7, 9 },
                    { 108, 7, 17 },
                    { 109, 7, 19 },
                    { 110, 7, 20 },
                    { 111, 7, 23 },
                    { 112, 7, 28 },
                    { 113, 7, 30 },
                    { 114, 7, 31 },
                    { 115, 7, 33 },
                    { 116, 7, 40 },
                    { 117, 7, 41 },
                    { 118, 7, 45 },
                    { 119, 7, 47 },
                    { 120, 8, 1 },
                    { 121, 8, 3 },
                    { 122, 8, 5 },
                    { 123, 8, 6 },
                    { 124, 8, 7 },
                    { 125, 8, 17 },
                    { 126, 8, 20 },
                    { 127, 8, 21 },
                    { 128, 8, 23 },
                    { 129, 8, 28 },
                    { 130, 8, 40 },
                    { 131, 8, 41 },
                    { 132, 8, 45 },
                    { 133, 8, 36 },
                    { 134, 8, 48 },
                    { 135, 9, 1 },
                    { 136, 9, 3 },
                    { 137, 9, 5 },
                    { 138, 9, 6 },
                    { 139, 9, 7 },
                    { 140, 9, 9 },
                    { 141, 9, 17 },
                    { 142, 9, 19 },
                    { 143, 9, 20 },
                    { 144, 9, 21 },
                    { 145, 9, 23 },
                    { 146, 9, 28 },
                    { 147, 9, 30 },
                    { 148, 9, 31 },
                    { 149, 9, 34 },
                    { 150, 9, 40 },
                    { 151, 9, 41 },
                    { 152, 9, 47 },
                    { 153, 9, 48 },
                    { 154, 10, 1 },
                    { 155, 10, 3 },
                    { 156, 10, 5 },
                    { 157, 10, 6 },
                    { 158, 10, 7 },
                    { 159, 10, 9 },
                    { 160, 10, 17 },
                    { 161, 10, 19 },
                    { 162, 10, 20 },
                    { 163, 10, 23 },
                    { 164, 10, 28 },
                    { 165, 10, 30 },
                    { 166, 10, 40 },
                    { 167, 10, 41 },
                    { 168, 10, 45 },
                    { 169, 10, 36 },
                    { 170, 10, 48 },
                    { 171, 11, 1 },
                    { 172, 11, 3 },
                    { 173, 11, 5 },
                    { 174, 11, 6 },
                    { 175, 11, 7 },
                    { 176, 11, 9 },
                    { 177, 11, 17 },
                    { 178, 11, 19 },
                    { 179, 11, 20 },
                    { 180, 11, 21 },
                    { 181, 11, 23 },
                    { 182, 11, 28 },
                    { 183, 11, 30 },
                    { 184, 11, 31 },
                    { 185, 11, 34 },
                    { 186, 11, 40 },
                    { 187, 11, 41 },
                    { 188, 11, 45 },
                    { 189, 11, 47 },
                    { 190, 11, 48 },
                    { 191, 12, 1 },
                    { 192, 12, 3 },
                    { 193, 12, 5 },
                    { 194, 12, 6 },
                    { 195, 12, 7 },
                    { 196, 12, 9 },
                    { 197, 12, 17 },
                    { 198, 12, 19 },
                    { 199, 12, 20 },
                    { 200, 12, 21 },
                    { 201, 12, 23 },
                    { 202, 12, 28 },
                    { 203, 12, 30 },
                    { 204, 12, 31 },
                    { 205, 12, 34 },
                    { 206, 12, 40 },
                    { 207, 12, 41 },
                    { 208, 12, 45 },
                    { 209, 12, 47 },
                    { 210, 12, 48 },
                    { 211, 13, 1 },
                    { 212, 13, 3 },
                    { 213, 13, 5 },
                    { 214, 13, 6 },
                    { 215, 13, 7 },
                    { 216, 13, 9 },
                    { 217, 13, 17 },
                    { 218, 13, 19 },
                    { 219, 13, 20 },
                    { 220, 13, 21 },
                    { 221, 13, 23 },
                    { 222, 13, 28 },
                    { 223, 13, 30 },
                    { 224, 13, 31 },
                    { 225, 13, 34 },
                    { 226, 13, 40 },
                    { 227, 13, 41 },
                    { 228, 13, 45 },
                    { 229, 13, 47 },
                    { 230, 13, 48 },
                    { 231, 14, 1 },
                    { 232, 14, 3 },
                    { 233, 14, 5 },
                    { 234, 14, 6 },
                    { 235, 14, 7 },
                    { 236, 14, 9 },
                    { 237, 14, 17 },
                    { 238, 14, 19 },
                    { 239, 14, 20 },
                    { 240, 14, 21 },
                    { 241, 14, 23 },
                    { 242, 14, 28 },
                    { 243, 14, 30 },
                    { 244, 14, 31 },
                    { 245, 14, 34 },
                    { 246, 14, 40 },
                    { 247, 14, 41 },
                    { 248, 14, 45 },
                    { 249, 14, 47 },
                    { 250, 14, 48 },
                    { 251, 14, 36 },
                    { 252, 14, 46 },
                    { 253, 14, 42 },
                    { 254, 14, 43 },
                    { 255, 14, 44 },
                    { 256, 14, 49 },
                    { 257, 14, 50 },
                    { 258, 15, 1 },
                    { 259, 15, 5 },
                    { 260, 15, 6 },
                    { 261, 15, 7 },
                    { 262, 15, 9 },
                    { 263, 15, 17 },
                    { 264, 15, 19 },
                    { 265, 15, 23 },
                    { 266, 15, 30 },
                    { 267, 15, 40 },
                    { 268, 16, 1 },
                    { 269, 16, 3 },
                    { 270, 16, 5 },
                    { 271, 16, 6 },
                    { 272, 16, 7 },
                    { 273, 16, 9 },
                    { 274, 16, 17 },
                    { 275, 16, 19 },
                    { 276, 16, 20 },
                    { 277, 16, 21 },
                    { 278, 16, 23 },
                    { 279, 16, 28 },
                    { 280, 16, 30 },
                    { 281, 16, 31 },
                    { 282, 16, 34 },
                    { 283, 16, 40 },
                    { 284, 16, 41 },
                    { 285, 16, 45 },
                    { 286, 16, 47 },
                    { 287, 16, 48 },
                    { 288, 16, 36 },
                    { 289, 16, 46 },
                    { 290, 16, 42 },
                    { 291, 16, 43 },
                    { 292, 16, 44 },
                    { 293, 16, 49 },
                    { 294, 16, 50 },
                    { 295, 17, 1 },
                    { 296, 17, 3 },
                    { 297, 17, 5 },
                    { 298, 17, 6 },
                    { 299, 17, 7 },
                    { 300, 17, 9 },
                    { 301, 17, 17 },
                    { 302, 17, 19 },
                    { 303, 17, 20 },
                    { 304, 17, 21 },
                    { 305, 17, 23 },
                    { 306, 17, 28 },
                    { 307, 17, 30 },
                    { 308, 17, 31 },
                    { 309, 17, 34 },
                    { 310, 17, 40 },
                    { 311, 17, 41 },
                    { 312, 18, 1 },
                    { 313, 18, 3 },
                    { 314, 18, 5 },
                    { 315, 18, 6 },
                    { 316, 18, 7 },
                    { 317, 18, 9 },
                    { 318, 18, 17 },
                    { 319, 18, 19 },
                    { 320, 18, 20 },
                    { 321, 18, 21 },
                    { 322, 18, 23 },
                    { 323, 18, 28 },
                    { 324, 18, 30 },
                    { 325, 18, 31 },
                    { 326, 18, 34 },
                    { 327, 18, 40 },
                    { 328, 18, 41 },
                    { 329, 18, 45 },
                    { 330, 18, 47 },
                    { 331, 19, 1 },
                    { 332, 19, 5 },
                    { 333, 19, 6 },
                    { 334, 19, 7 },
                    { 335, 19, 9 },
                    { 336, 19, 17 },
                    { 337, 20, 1 },
                    { 338, 20, 5 },
                    { 339, 20, 6 },
                    { 340, 20, 7 },
                    { 341, 20, 9 },
                    { 342, 20, 17 },
                    { 343, 20, 19 },
                    { 344, 20, 23 },
                    { 345, 20, 30 },
                    { 346, 21, 1 },
                    { 347, 21, 3 },
                    { 348, 21, 5 },
                    { 349, 21, 6 },
                    { 350, 21, 7 },
                    { 351, 21, 9 },
                    { 352, 21, 17 },
                    { 353, 21, 19 },
                    { 354, 21, 20 },
                    { 355, 21, 21 },
                    { 356, 21, 23 },
                    { 357, 21, 28 },
                    { 358, 21, 30 },
                    { 359, 21, 31 },
                    { 360, 21, 34 },
                    { 361, 21, 40 },
                    { 362, 21, 41 },
                    { 363, 21, 45 },
                    { 364, 21, 47 },
                    { 365, 21, 48 },
                    { 366, 21, 36 },
                    { 367, 21, 46 },
                    { 368, 21, 42 },
                    { 369, 21, 43 },
                    { 370, 21, 44 },
                    { 371, 21, 49 },
                    { 372, 21, 50 },
                    { 373, 21, 25 },
                    { 374, 22, 1 },
                    { 375, 22, 3 },
                    { 376, 22, 5 },
                    { 377, 22, 6 },
                    { 378, 22, 7 },
                    { 379, 22, 9 },
                    { 380, 22, 17 },
                    { 381, 22, 19 },
                    { 382, 22, 23 },
                    { 383, 22, 28 }
                });

            migrationBuilder.InsertData(
                table: "CarsTypes",
                columns: new[] { "Id", "CarId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 6 },
                    { 4, 3, 8 },
                    { 5, 4, 6 },
                    { 6, 4, 8 },
                    { 7, 5, 6 },
                    { 8, 5, 8 },
                    { 9, 6, 5 },
                    { 10, 6, 7 },
                    { 11, 7, 5 },
                    { 12, 7, 7 },
                    { 13, 8, 5 },
                    { 14, 9, 1 },
                    { 15, 10, 5 },
                    { 16, 10, 7 },
                    { 17, 11, 1 },
                    { 18, 12, 6 },
                    { 19, 12, 8 },
                    { 20, 13, 5 },
                    { 21, 14, 2 },
                    { 22, 15, 3 },
                    { 23, 16, 5 },
                    { 24, 17, 2 },
                    { 25, 18, 6 },
                    { 26, 18, 8 },
                    { 27, 19, 1 },
                    { 28, 20, 11 },
                    { 29, 21, 2 },
                    { 30, 22, 5 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CarId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "/images/toyota-corolla1.jpg" },
                    { 2, 1, "/images/toyota-corolla2.jpg" },
                    { 3, 1, "/images/toyota-corolla3.jpg" },
                    { 4, 1, "/images/toyota-corolla4.jpg" },
                    { 5, 2, "/images/honda-civic1.png" },
                    { 6, 2, "/images/honda-civic2.png" },
                    { 7, 2, "/images/honda-civic3.png" },
                    { 8, 2, "/images/honda-civic4.png" },
                    { 9, 4, "/images/bmw-420i1.png" },
                    { 10, 4, "/images/bmw-420i2.png" },
                    { 11, 4, "/images/bmw-420i7.png" },
                    { 12, 4, "/images/bmw-420i3.png" },
                    { 13, 4, "/images/bmw-420i4.png" },
                    { 14, 4, "/images/bmw-420i5.png" },
                    { 15, 4, "/images/bmw-420i6.png" },
                    { 16, 5, "/images/c300-1.webp" },
                    { 17, 5, "/images/c300-2.webp" },
                    { 18, 5, "/images/c300-3.webp" },
                    { 19, 5, "/images/c300-4.webp" },
                    { 20, 5, "/images/c300-5.webp" },
                    { 21, 5, "/images/c300-6.webp" },
                    { 22, 3, "/images/ford-mustang-1.webp" },
                    { 23, 3, "/images/ford-mustang-2.webp" },
                    { 24, 3, "/images/ford-mustang-3.webp" },
                    { 25, 3, "/images/ford-mustang-4.webp" },
                    { 26, 3, "/images/ford-mustang-5.webp" },
                    { 27, 3, "/images/ford-mustang-6.webp" },
                    { 28, 3, "/images/ford-mustang-7.webp" },
                    { 29, 3, "/images/ford-mustang-8.webp" },
                    { 30, 6, "/images/audi-r8-1.jpg" },
                    { 31, 6, "/images/audi-r8-2.jpg" },
                    { 32, 6, "/images/audi-r8-3.jpg" },
                    { 33, 6, "/images/audi-r8-4.jpg" },
                    { 34, 6, "/images/audi-r8-5.jpg" },
                    { 35, 6, "/images/audi-r8-6.jpg" },
                    { 36, 6, "/images/audi-r8-7.jpg" },
                    { 37, 6, "/images/audi-r8-8.jpg" },
                    { 38, 7, "/images/lambo-hur-1.jpg" },
                    { 39, 7, "/images/lambo-hur-2.jpg" },
                    { 40, 7, "/images/lambo-hur-3.jpg" },
                    { 41, 7, "/images/lambo-hur-4.jpg" },
                    { 42, 7, "/images/lambo-hur-5.jpg" },
                    { 43, 8, "/images/porsche-gt3-1.webp" },
                    { 44, 8, "/images/porsche-gt3-2.webp" },
                    { 45, 8, "/images/porsche-gt3-3.webp" },
                    { 46, 8, "/images/porsche-gt3-4.webp" },
                    { 47, 8, "/images/porsche-gt3-5.webp" },
                    { 48, 8, "/images/porsche-gt3-6.webp" },
                    { 49, 8, "/images/porsche-gt3-7.webp" },
                    { 50, 8, "/images/porsche-gt3-8.webp" },
                    { 51, 9, "/images/tesla-s-1.webp" },
                    { 52, 9, "/images/tesla-s-2.webp" },
                    { 53, 9, "/images/tesla-s-3.webp" },
                    { 54, 9, "/images/tesla-s-4.webp" },
                    { 55, 9, "/images/tesla-s-5.webp" },
                    { 56, 9, "/images/tesla-s-6.webp" },
                    { 57, 10, "/images/ferrari-f-1.webp" },
                    { 58, 10, "/images/ferrari-f-2.webp" },
                    { 59, 10, "/images/ferrari-f-3.webp" },
                    { 60, 10, "/images/ferrari-f-4.webp" },
                    { 61, 10, "/images/ferrari-f-5.webp" },
                    { 62, 10, "/images/ferrari-f-6.webp" },
                    { 63, 10, "/images/ferrari-f-7.webp" },
                    { 64, 10, "/images/ferrari-f-8.webp" },
                    { 65, 11, "/images/phanthom-1.webp" },
                    { 66, 11, "/images/phanthom-2.webp" },
                    { 67, 11, "/images/phanthom-3.webp" },
                    { 68, 11, "/images/phanthom-4.webp" },
                    { 69, 11, "/images/phanthom-5.webp" },
                    { 70, 11, "/images/phanthom-6.webp" },
                    { 71, 11, "/images/phanthom-7.webp" },
                    { 72, 11, "/images/phanthom-8.webp" },
                    { 73, 11, "/images/phanthom-9.webp" },
                    { 74, 11, "/images/phanthom-10.webp" },
                    { 75, 11, "/images/phanthom-11.webp" },
                    { 76, 12, "/images/bentley-continental-gt-1.webp" },
                    { 77, 12, "/images/bentley-continental-gt-2.webp" },
                    { 78, 12, "/images/bentley-continental-gt-3.webp" },
                    { 79, 12, "/images/bentley-continental-gt-4.webp" },
                    { 80, 12, "/images/bentley-continental-gt-5.webp" },
                    { 81, 12, "/images/bentley-continental-gt-6.webp" },
                    { 82, 12, "/images/bentley-continental-gt-7.webp" },
                    { 83, 12, "/images/bentley-continental-gt-8.webp" },
                    { 84, 12, "/images/bentley-continental-gt-9.webp" },
                    { 85, 12, "/images/bentley-continental-gt-10.webp" },
                    { 86, 12, "/images/bentley-continental-gt-11.webp" },
                    { 87, 12, "/images/bentley-continental-gt-12.webp" },
                    { 88, 13, "/images/mc-720s-1.webp" },
                    { 89, 13, "/images/mc-720s-2.webp" },
                    { 90, 13, "/images/mc-720s-3.webp" },
                    { 91, 13, "/images/mc-720s-4.webp" },
                    { 92, 13, "/images/mc-720s-5.webp" },
                    { 93, 13, "/images/mc-720s-6.webp" },
                    { 94, 13, "/images/mc-720s-7.webp" },
                    { 95, 14, "/images/aston-dbx-1.webp" },
                    { 96, 14, "/images/aston-dbx-2.webp" },
                    { 97, 14, "/images/aston-dbx-3.webp" },
                    { 98, 14, "/images/aston-dbx-4.webp" },
                    { 99, 14, "/images/aston-dbx-5.webp" },
                    { 100, 14, "/images/aston-dbx-6.webp" },
                    { 101, 14, "/images/aston-dbx-7.webp" },
                    { 102, 14, "/images/aston-dbx-8.webp" },
                    { 103, 15, "/images/lexus-lx-1.webp" },
                    { 104, 15, "/images/lexus-lx-2.webp" },
                    { 105, 15, "/images/lexus-lx-3.webp" },
                    { 106, 15, "/images/lexus-lx-4.webp" },
                    { 107, 15, "/images/lexus-lx-5.webp" },
                    { 108, 15, "/images/lexus-lx-6.webp" },
                    { 109, 15, "/images/lexus-lx-7.webp" },
                    { 110, 16, "/images/amg-gt-1.webp" },
                    { 111, 16, "/images/amg-gt-2.webp" },
                    { 112, 16, "/images/amg-gt-3.webp" },
                    { 113, 16, "/images/amg-gt-4.webp" },
                    { 114, 16, "/images/amg-gt-5.webp" },
                    { 115, 16, "/images/amg-gt-6.webp" },
                    { 116, 16, "/images/amg-gt-7.webp" },
                    { 117, 16, "/images/amg-gt-8.webp" },
                    { 118, 17, "/images/audi-q8-1.webp" },
                    { 119, 17, "/images/audi-q8-2.webp" },
                    { 120, 17, "/images/audi-q8-3.webp" },
                    { 121, 17, "/images/audi-q8-4.webp" },
                    { 122, 17, "/images/audi-q8-5.webp" },
                    { 123, 17, "/images/audi-q8-6.webp" },
                    { 124, 17, "/images/audi-q8-7.webp" },
                    { 125, 17, "/images/audi-q8-8.webp" },
                    { 126, 17, "/images/audi-q8-9.webp" },
                    { 127, 18, "/images/bmw-m4-1.webp" },
                    { 128, 18, "/images/bmw-m4-2.webp" },
                    { 129, 18, "/images/bmw-m4-3.webp" },
                    { 130, 18, "/images/bmw-m4-4.webp" },
                    { 131, 18, "/images/bmw-m4-5.webp" },
                    { 132, 18, "/images/bmw-m4-6.webp" },
                    { 133, 19, "/images/camry-1.webp" },
                    { 134, 19, "/images/camry-2.webp" },
                    { 135, 19, "/images/camry-3.webp" },
                    { 136, 19, "/images/camry-4.webp" },
                    { 137, 19, "/images/camry-5.webp" },
                    { 138, 19, "/images/camry-6.webp" },
                    { 139, 19, "/images/camry-7.webp" },
                    { 140, 20, "/images/cybertruck-1.webp" },
                    { 141, 20, "/images/cybertruck-2.webp" },
                    { 142, 20, "/images/cybertruck-3.webp" },
                    { 143, 20, "/images/cybertruck-4.webp" },
                    { 144, 20, "/images/cybertruck-5.webp" },
                    { 145, 21, "/images/rover-1.webp" },
                    { 146, 21, "/images/rover-2.webp" },
                    { 147, 21, "/images/rover-3.webp" },
                    { 148, 21, "/images/rover-4.webp" },
                    { 149, 21, "/images/rover-5.webp" },
                    { 150, 21, "/images/rover-6.webp" },
                    { 151, 21, "/images/rover-7.webp" },
                    { 152, 21, "/images/rover-8.webp" },
                    { 153, 21, "/images/rover-9.webp" },
                    { 154, 22, "/images/toyota-supra-1.webp" },
                    { 155, 22, "/images/toyota-supra-2.webp" },
                    { 156, 22, "/images/toyota-supra-3.webp" },
                    { 157, 22, "/images/toyota-supra-4.webp" },
                    { 158, 22, "/images/toyota-supra-5.webp" },
                    { 159, 22, "/images/toyota-supra-6.webp" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CarCompanies_UserId",
                table: "CarCompanies",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarCompanyId",
                table: "Cars",
                column: "CarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClassOfCarId",
                table: "Cars",
                column: "ClassOfCarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsFeatures_CarId",
                table: "CarsFeatures",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsFeatures_FeatureId",
                table: "CarsFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsTypes_CarId",
                table: "CarsTypes",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsTypes_TypeId",
                table: "CarsTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_CarId",
                table: "Images",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarsFeatures");

            migrationBuilder.DropTable(
                name: "CarsTypes");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CarCompanies");

            migrationBuilder.DropTable(
                name: "ClassOfCars");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
