using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EveryInformationTo15042025 : Migration
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
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    CarCompanyId = table.Column<int>(type: "int", nullable: false),
                    Pending = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CTypeId = table.Column<int>(type: "int", nullable: false),
                    IsConvertable = table.Column<bool>(type: "bit", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Cars_Types_CTypeId",
                        column: x => x.CTypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
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
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
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
                    PaidDeliveryPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReturnBackAtSamePlace = table.Column<bool>(type: "bit", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14d0633b-3047-4eb2-b6cd-9c2565ffbb40", null, "Company", "COMPANY" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "14818507-0d14-48c7-bc15-f81abe0a1dc2", 0, "242e77d8-b884-4d90-a75c-668cbe8b1bb0", "IdentityUser", "oneclickonedrive@gmail.com", false, true, null, "ONECLICKONEDRIVE@GMAIL.COM", "ONECLICKONEDRIVE@GMAIL.COM", "AQAAAAIAAYagAAAAEAmk5gKltaxEzJqrLc3/SZ/5i9zEHz0rtSL5ScgFfTW26u2z3snjJlYI/q42MlZrtw==", null, false, "7BG3CAZ2A2RCPSY4ENWH5GK4GOL2SMIT", false, "oneclickonedrive@gmail.com" },
                    { "2575ca69-1258-4767-ac40-4710bb24b67d", 0, "cde74caf-bbcb-462b-9bcd-38dc4c5ad875", "IdentityUser", "anditobg@gmail.com", false, true, null, "ANDITOBG@GMAIL.COM", "ANDITOBG@GMAIL.COM", "AQAAAAIAAYagAAAAEAlsa2MMdRhyMUgKFLda83ZC6kV+F5vEVWRtGqeW8TTqMmKp/ILuiqU9iETHMiJ4KQ==", null, false, "Y2GAED2SIOTPY5W6TPOSTCYGTFFTLY6S", false, "anditobg@gmail.com" },
                    { "6e993bea-9377-4d0b-abfc-637ed7a8b69e", 0, "e5877398-35da-4428-800e-85d4bf71349b", "IdentityUser", "dubailuxury@company.abv.bg", false, true, null, "DUBAILUXURY@COMPANY.ABV.BG", "DUBAILUXURY@COMPANY.ABV.BG", "AQAAAAIAAYagAAAAELh5jOnWpFjlMDzPsbO32i/BDgnvHO75gAS+8msNrKzhlC6wKABJ+cuw6yoO9ca1yQ==", null, false, "HXDEUYUVH6YZUBTKYTEVTN4UVV2552WA", false, "dubailuxury@company.abv.bg" },
                    { "8679b5d4-0473-4fc3-bbb2-dd0d128f3a53", 0, "54ac1e5f-79eb-4db3-a58e-fd35d0f3e75a", "IdentityUser", "dimiturstoqnov@gmail.com", false, true, null, "DIMITURSTOQNOV@GMAIL.COM", "DIMITURSTOQNOV@GMAIL.COM", "AQAAAAIAAYagAAAAEFFYgk6mKgRJjTqwAg1ppB5J6xKjUXXvwC4tk+47+wWSuvkWkxTJpUwhqox6n78sjw==", null, false, "OAYYM3H6DKRDLTFWARJWRS6D65RAUYSP", false, "dimiturstoqnov@gmail.com" },
                    { "9ec0d6cb-0cd4-4b55-87b2-27470215a711", 0, "42c2e9a7-4a46-4110-a722-5a42179eafa8", "IdentityUser", "mariageorgieva@gmail.com", false, true, null, "MARIAGEORGIEVA@GMAIL.COM", "MARIAGEORGIEVA@GMAIL.COM", "AQAAAAIAAYagAAAAEKzggjgHFWU1AjIaDj4ddYsYcsRh+37xmxqZAPNX+tsXMC61ESdhvCOnk7saTtcZcQ==", null, false, "6CTCZTEYEZPBPUYKAPXUSOGQY6QRYAGT", false, "mariageorgieva@gmail.com" },
                    { "bfd29fba-8052-48d3-87c3-ffad67faca1a", 0, "4d2abaac-c77f-4622-bca3-bdcbad71ea5f", "IdentityUser", "kurtach@gmail.com", false, true, null, "KURTACH@GMAIL.COM", "KURTACH@GMAIL.COM", "AQAAAAIAAYagAAAAEGP36dPC/pR6jQBDAJUS22VN9Vt1FPysu6tVvz3JeFN6mNQYJhpnZnje+X2+ZUCC/Q==", null, false, "5ZBAZEYHK2P2RTN6DIWMI5CKUFMM2YZM", false, "kurtach@gmail.com" },
                    { "ca105f54-8e1b-4bac-8692-a5a3af6c0124", 0, "30564eca-4c17-460c-a251-5376d4b0a210", "IdentityUser", "peshkan@gmail.com", false, true, null, "PESHKAN@GMAIL.COM", "PESHKAN@GMAIL.COM", "AQAAAAIAAYagAAAAEMKinXBIkpTRVoJmF7xrGHl64a0l5qBElqX/XOcF4UEjS1dUdAiffEVgFTr1PK13Lg==", null, false, "FQRXCURNZUDMAKPZMNGSG5JC4VQSPIQ2", false, "peshkan@gmail.com" },
                    { "d37497a5-b222-4e3e-8a6f-34ec9ffe3b16", 0, "856e739e-fb2e-4db0-bdab-9a39c302b35a", "IdentityUser", "rosen@gmail.com", false, true, null, "ROSEN@GMAIL.COM", "ROSEN@GMAIL.COM", "AQAAAAIAAYagAAAAEHQP9dkOMANIVw1qhjjVV2KmU2wEOg8iEV8cmxMxL/rqkbx6GV5M23XxqhHg7+wbEA==", null, false, "Z2GIRNMTF74UIWIUFILJJDUBS24RL7PN", false, "rosen@gmail.com" },
                    { "ec246a6e-5cb7-48a0-983d-020788e31ad9", 0, "6392a7ba-060b-41c5-a05d-0299d4872b21", "IdentityUser", "stoqnkata@gmail.com", false, true, null, "STOQNKATA@GMAIL.COM", "STOQNKATA@GMAIL.COM", "AQAAAAIAAYagAAAAEN1dPbAD1m2UbzxGUKqltYuf92pAELZeMzJMku8qkZPPPZ5aQHTCH856+R1xxW2jqw==", null, false, "TCVOTCVJHJ7OSF65N6NEI2W5GINUFESQ", false, "stoqnkata@gmail.com" },
                    { "ecba9c4c-dd9e-4b92-b0c9-30102d68081a", 0, "dfc787a4-624c-4f94-8a63-dd16a2a08c83", "IdentityUser", "yangodrive@gmail.com", false, true, null, "YANGODRIVE@GMAIL.COM", "YANGODRIVE@GMAIL.COM", "AQAAAAIAAYagAAAAEEyhX5CogWLvxIG1ZK1wxeXiWys+8YLIas0QdI5tadjCPJWx1zk9t/WQhboFgGW1Og==", null, false, "SL7X4VI5EQSGGZOHDRSLYEXJ2ZMWR6PY", false, "yangodrive@gmail.com" },
                    { "f1a432b0-510c-46c1-bb51-696cb688db06", 0, "b3aed364-9454-45b2-9b05-c75a289e0c20", "IdentityUser", "konstantinmitkov@gmail.com", false, true, null, "KONSTANTINMITKOV@GMAIL.COM", "KONSTANTINMITKOV@GMAIL.COM", "AQAAAAIAAYagAAAAENryZeTkwk/Q2dXZoWzdyN6QR3QD+rZVwkch7zeQyMijQKEad0reMYWjMk0rd52mGA==", null, false, "GITLLRXGQHTB23FNZBZQYH7VPURHEVZB", false, "konstantinmitkov@gmail.com" }
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
                    { 50, "Touchscreen Control Panel" },
                    { 51, "Auto-Dimming Rearview Mirror" }
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
                    { 5, "Hatchback", 2 },
                    { 6, "Coupe", 2 },
                    { 7, "Coupe", 4 },
                    { 8, "Grand Coupe", 4 },
                    { 9, "Minivan", 7 },
                    { 10, "Pickup", 2 },
                    { 11, "Pickup", 4 },
                    { 12, "Wagon", 5 },
                    { 14, "Hatchback", 5 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "14d0633b-3047-4eb2-b6cd-9c2565ffbb40", "14818507-0d14-48c7-bc15-f81abe0a1dc2" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "2575ca69-1258-4767-ac40-4710bb24b67d" },
                    { "14d0633b-3047-4eb2-b6cd-9c2565ffbb40", "6e993bea-9377-4d0b-abfc-637ed7a8b69e" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "8679b5d4-0473-4fc3-bbb2-dd0d128f3a53" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "9ec0d6cb-0cd4-4b55-87b2-27470215a711" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "bfd29fba-8052-48d3-87c3-ffad67faca1a" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "ca105f54-8e1b-4bac-8692-a5a3af6c0124" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "d37497a5-b222-4e3e-8a6f-34ec9ffe3b16" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "ec246a6e-5cb7-48a0-983d-020788e31ad9" },
                    { "14d0633b-3047-4eb2-b6cd-9c2565ffbb40", "ecba9c4c-dd9e-4b92-b0c9-30102d68081a" },
                    { "8c796f68-1ca7-41ae-9f17-465876acab20", "f1a432b0-510c-46c1-bb51-696cb688db06" }
                });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "Address", "City", "Country", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Sheikh Zayed Road, Dubai", "Dubai", "UAE", "Luxury and sports car rentals in Dubai.", "Dubai Luxury Cars", "6e993bea-9377-4d0b-abfc-637ed7a8b69e" },
                    { 3, "Sheikh Zayed Road, Dubai", "Dubai", "UAE", "Ultimate luxury cars", "Yango Drive", "ecba9c4c-dd9e-4b92-b0c9-30102d68081a" },
                    { 4, "Sheikh Zayed Road, Dubai", "Dubai", "UAE", "Ultimate luxury", "One click one drive", "14818507-0d14-48c7-bc15-f81abe0a1dc2" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BirthDay", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maria", "9ec0d6cb-0cd4-4b55-87b2-27470215a711" },
                    { 2, new DateTime(1988, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Konstantin", "f1a432b0-510c-46c1-bb51-696cb688db06" },
                    { 3, new DateTime(1990, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rosen", "d37497a5-b222-4e3e-8a6f-34ec9ffe3b16" },
                    { 4, new DateTime(2000, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andrian", "2575ca69-1258-4767-ac40-4710bb24b67d" },
                    { 5, new DateTime(1992, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dimitur", "8679b5d4-0473-4fc3-bbb2-dd0d128f3a53" },
                    { 6, new DateTime(1985, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stoqn", "ec246a6e-5cb7-48a0-983d-020788e31ad9" },
                    { 7, new DateTime(1998, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kurtacha", "bfd29fba-8052-48d3-87c3-ffad67faca1a" },
                    { 8, new DateTime(2005, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pesho", "ca105f54-8e1b-4bac-8692-a5a3af6c0124" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AdditionalMileageCharge", "Available", "Brand", "CTypeId", "CarCompanyId", "ClassOfCarId", "Color", "CreatedAt", "Description", "DriveTrain", "EngineCapacity", "Gearbox", "HorsePower", "IsConvertable", "MileageLimitForDay", "MileageLimitForWeek", "Model", "Pending", "PricePerDay", "PricePerWeek", "TopSpeed", "Year", "ZeroToHundred" },
                values: new object[,]
                {
                    { 1, 0.20000000000000001, true, "Toyota", 1, 1, 1, "White", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Compact and fuel-efficient", "Front", 1.6000000000000001, "Automatic", 122, false, 150.0, 1000.0, "Corolla", false, 80.0, 450.0, 185.0, 2021, 10.800000000000001 },
                    { 2, 0.25, true, "Honda", 1, 1, 6, "Black", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Sporty and reliable", "Front", 1.5, "Manual", 205, false, 200.0, 1200.0, "Civic", false, 140.0, 600.0, 170.0, 2022, 7.2999999999999998 },
                    { 3, 0.22, true, "Ford", 6, 1, 3, "Dark Gray", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Comfortable and stylish", "Rear", 2.2999999999999998, "Automatic", 317, true, 250.0, 1500.0, "Mustang", false, 200.0, 910.0, 250.0, 2020, 5.7999999999999998 },
                    { 4, 0.5, true, "BMW", 6, 1, 2, "Gray", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Luxury and performance", "Rear", 2.0, "Automatic", 190, true, 300.0, 2000.0, "420", false, 250.0, 1000.0, 240.0, 2022, 7.0999999999999996 },
                    { 5, 0.55000000000000004, true, "Mercedes-Benz", 6, 1, 2, "Red", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Premium luxury", "Rear", 3.0, "Automatic", 258, true, 300.0, 2000.0, "C-Class", false, 210.0, 850.0, 250.0, 2021, 6.2000000000000002 },
                    { 6, 1.0, true, "Audi", 5, 1, 3, "Green", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "High-performance sports car", "Rear", 5.2000000000000002, "Automatic", 570, true, 400.0, 2500.0, "R8 Spyder", false, 750.0, 6400.0, 327.0, 2021, 3.7999999999999998 },
                    { 7, 1.5, true, "Lamborghini", 5, 1, 3, "Black", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Exotic sports car", "Rear", 5.2000000000000002, "Automatic", 640, true, 400.0, 3000.0, "Huracan", false, 1700.0, 9000.0, 310.0, 2023, 3.0 },
                    { 8, 1.3, true, "Porsche", 5, 1, 3, "Silver", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Luxury sports car", "Full Wheels", 3.0, "Automatic", 450, false, 400.0, 3000.0, "911 GT3", false, 1400.0, 8400.0, 304.0, 2023, 3.7999999999999998 },
                    { 9, 1.0, true, "Tesla", 1, 1, 5, "White", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Electric luxury sedan", "Rear", 0.0, "Automatic", 1020, false, 400.0, 2500.0, "Model S Plaid", false, 350.0, 2100.0, 322.0, 2023, 2.1000000000000001 },
                    { 10, 2.0, true, "Ferrari", 5, 1, 3, "Red", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Iconic Italian sports car", "Rear", 3.8999999999999999, "Automatic", 720, true, 400.0, 3000.0, "F8 Spider", false, 2300.0, 10000.0, 340.0, 2022, 2.8999999999999999 },
                    { 11, 2.0, true, "Rolls-Royce", 1, 1, 4, "Black", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Ultimate luxury car", "Rear", 6.7999999999999998, "Automatic", 571, false, 300.0, 2000.0, "Phantom", false, 1000.0, 7000.0, 250.0, 2021, 5.4000000000000004 },
                    { 12, 2.5, true, "Bentley", 6, 1, 4, "Black", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Grand luxury tourer", "Full Wheels", 6.0, "Automatic", 659, true, 300.0, 2000.0, "Continental GT-GTC", false, 1300.0, 7600.0, 335.0, 2023, 3.7000000000000002 },
                    { 13, 2.0, true, "McLaren", 5, 1, 3, "Blue", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Exquisite British engineering", "Rear", 4.0, "Automatic", 720, false, 400.0, 3000.0, "720S", false, 1700.0, 8000.0, 341.0, 2023, 2.8999999999999999 },
                    { 14, 2.0, true, "Aston Martin", 2, 1, 4, "Black", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Luxury British SUV", "Full Wheels", 4.0, "Automatic", 550, false, 300.0, 2000.0, "DBX", false, 600.0, 3600.0, 291.0, 2023, 4.5 },
                    { 15, 1.8, true, "Lexus", 3, 1, 2, "Silver", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Sophisticated luxury SUV", "Full Wheels", 3.5, "Automatic", 415, false, 300.0, 2000.0, "LX", false, 500.0, 3000.0, 210.0, 2023, 6.7999999999999998 },
                    { 16, 2.2000000000000002, true, "Mercedes-Benz", 5, 1, 3, "Orange", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "German engineering excellence", "Rear", 4.0, "Automatic", 730, false, 400.0, 2500.0, "AMG GT", false, 2500.0, 1100.0, 322.0, 2022, 3.2000000000000002 },
                    { 17, 1.0, true, "Audi", 2, 1, 4, "Orange", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Luxury SUV", "Full Wheels", 3.0, "Automatic", 340, false, 300.0, 2000.0, "Q8", false, 400.0, 2300.0, 250.0, 2021, 5.9000000000000004 },
                    { 18, 1.5, true, "BMW", 6, 1, 3, "Yellow", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Sporty and agile", "Full Wheels", 3.0, "Automatic", 510, true, 300.0, 2000.0, "M4", false, 700.0, 4000.0, 250.0, 2023, 3.7000000000000002 },
                    { 19, 0.25, true, "Toyota", 1, 1, 6, "Red", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Dependable sedan", "Rear", 2.5, "Automatic", 182, false, 200.0, 1200.0, "Camry", false, 130.0, 670.0, 210.0, 2021, 9.9000000000000004 },
                    { 20, 1.8, true, "Tesla", 11, 1, 5, "Black", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Sophisticated luxury SUV", "Full Wheels", 0.0, "Automatic", 845, false, 300.0, 2000.0, "Cybertruck", false, 2000.0, 12000.0, 210.0, 2024, 2.7000000000000002 },
                    { 21, 1.8, true, "Land Rover", 2, 1, 4, "Black", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Sophisticated luxury SUV", "Full Wheels", 3.0, "Automatic", 360, false, 300.0, 2000.0, "Range Rover", false, 1200.0, 7000.0, 209.0, 2023, 6.9000000000000004 },
                    { 22, 1.8, true, "Toyota", 5, 1, 3, "White", new DateTime(2025, 1, 14, 14, 30, 45, 0, DateTimeKind.Unspecified), "Sporty and agile", "Rear", 3.0, "Automatic", 340, false, 300.0, 2000.0, "Supra", false, 220.0, 1100.0, 262.0, 2022, 4.4000000000000004 },
                    { 29, 2.2999999999999998, true, "BMW", 6, 1, 3, "Grey", new DateTime(2025, 2, 3, 15, 34, 43, 0, DateTimeKind.Unspecified), "Sporty and luxury", "Full wheels", 3.0, "Automatic", 340, true, 200.0, 1500.0, "840", false, 700.0, 4400.0, 250.0, 2023, 3.8999999999999999 },
                    { 30, 2.2999999999999998, true, "Mercedes-Benz", 1, 4, 2, "Silver", new DateTime(2025, 2, 3, 15, 47, 23, 0, DateTimeKind.Unspecified), "Ultimate luxury", "Full wheels", 3.0, "Automatic", 435, false, 250.0, 1500.0, "S550", false, 700.0, 4400.0, 250.0, 2023, 4.9000000000000004 },
                    { 31, 2.2999999999999998, true, "Aston Martin", 5, 3, 3, "Black", new DateTime(2025, 2, 3, 16, 23, 32, 0, DateTimeKind.Unspecified), "Sporty and luxury", "Rear", 4.0, "Automatic", 510, false, 250.0, 1500.0, "Vantage", false, 1200.0, 7000.0, 314.0, 2023, 3.6000000000000001 },
                    { 42, 2.2999999999999998, true, "Mini", 4, 3, 6, "Blue", new DateTime(2025, 2, 4, 9, 23, 45, 0, DateTimeKind.Unspecified), "Small and sporty", "Front", 1.7, "Automatic", 208, true, 250.0, 1400.0, "Cooper", false, 250.0, 1575.0, 240.0, 2023, 6.0999999999999996 },
                    { 43, 2.5, true, "Mercedes-Benz", 3, 3, 4, "Black", new DateTime(2025, 2, 4, 9, 26, 43, 0, DateTimeKind.Unspecified), "Ultimate luxury", "Full wheels", 4.0, "Automatic", 558, false, 300.0, 2400.0, "Maybach", true, 1600.0, 10200.0, 250.0, 2024, 4.9000000000000004 },
                    { 44, 2.0, true, "Porsche", 2, 3, 2, "Silver", new DateTime(2025, 2, 4, 9, 32, 21, 0, DateTimeKind.Unspecified), "Sporty and luxury SUV", "Full wheels", 3.0, "Automatic", 340, false, 300.0, 2400.0, "Cayenne", true, 450.0, 2835.0, 245.0, 2022, 6.2000000000000002 },
                    { 48, 2.2999999999999998, true, "Mercedes-Benz", 6, 3, 3, "Silver", new DateTime(2025, 2, 4, 9, 43, 11, 0, DateTimeKind.Unspecified), "Convertable luxury car", "Full wheels", 4.0, "Automatic", 585, true, 300.0, 2400.0, "SL", true, 800.0, 5040.0, 315.0, 2022, 3.6000000000000001 },
                    { 49, 1.6000000000000001, true, "BMW", 1, 3, 5, "Black", new DateTime(2025, 2, 4, 9, 46, 26, 0, DateTimeKind.Unspecified), "German Masterpiece", "Rear", 0.0, "Automatic", 184, false, 275.0, 2000.0, "520i", false, 300.0, 1890.0, 235.0, 2024, 7.7999999999999998 },
                    { 50, 2.6000000000000001, true, "BMW", 2, 3, 4, "Grey", new DateTime(2025, 2, 4, 9, 51, 32, 0, DateTimeKind.Unspecified), "Home on 4 wheels", "Full wheels", 3.0, "Automatic", 555, false, 300.0, 2500.0, "X5", true, 320.0, 2016.0, 280.0, 2023, 6.5 },
                    { 51, 1.3, true, "Tesla", 5, 3, 5, "Yellow", new DateTime(2025, 2, 4, 10, 32, 53, 0, DateTimeKind.Unspecified), "3 TONS MICROWAVE", "Full wheels", 0.0, "Automatic", 345, false, 350.0, 2500.0, "Model Y", false, 210.0, 1323.0, 217.0, 2022, 5.0999999999999996 },
                    { 53, 2.0, true, "Audi", 6, 3, 2, "Black", new DateTime(2025, 2, 4, 11, 12, 46, 0, DateTimeKind.Unspecified), "Sporty and luxury", "Full wheels", 2.0, "Automatic", 245, false, 275.0, 1300.0, "A7", true, 250.0, 1400.0, 250.0, 2021, 6.2000000000000002 },
                    { 54, 2.2999999999999998, true, "Porsche", 5, 3, 3, "Black", new DateTime(2025, 2, 6, 12, 12, 54, 0, DateTimeKind.Unspecified), "Sporty and luxury", "Rear", 4.0, "Automatic", 500, false, 200.0, 1500.0, "Cayman", true, 600.0, 3200.0, 314.0, 2023, 3.6000000000000001 },
                    { 55, 2.0, true, "Audi", 2, 3, 1, "Grey", new DateTime(2025, 2, 7, 17, 51, 13, 0, DateTimeKind.Unspecified), "Small SUV", "Front", 2.0, "Automatic", 186, false, 200.0, 1500.0, "Q3", true, 250.0, 1400.0, 200.0, 2023, 8.8000000000000007 },
                    { 56, 2.2000000000000002, false, "Kia", 2, 3, 6, "Blue", new DateTime(2025, 2, 10, 12, 14, 23, 0, DateTimeKind.Unspecified), "One Big SUV", "Full wheels", 3.7999999999999998, "Automatic", 295, false, 250.0, 1500.0, "Telluride", true, 170.0, 1071.0, 170.0, 2023, 6.5999999999999996 },
                    { 57, 2.1000000000000001, true, "Kia", 5, 3, 6, "Dark Grey", new DateTime(2025, 2, 11, 14, 56, 54, 0, DateTimeKind.Unspecified), "it is still a kia", "Full wheels", 2.0, "Automatic", 197, false, 300.0, 2200.0, "Stinger", true, 110.0, 693.0, 224.0, 2021, 8.0 },
                    { 60, 2.0, true, "BMW", 2, 3, 4, "Grey", new DateTime(2025, 4, 2, 6, 39, 48, 0, DateTimeKind.Unspecified), "THE ULTIMATE DRIVING MACHINE", "Full wheels", 4.4000000000000004, "Automatic", 625, false, 200.0, 1500.0, "X6M", false, 350.0, 2205.0, 250.0, 2021, 3.7000000000000002 },
                    { 61, 1.6000000000000001, true, "Volkswagen", 2, 3, 1, "White", new DateTime(2025, 4, 2, 9, 44, 31, 0, DateTimeKind.Unspecified), "Small SUV but practical", "Full wheels", 1.5, "Automatic", 150, false, 300.0, 2200.0, "T-ROC Life", true, 60.0, 378.0, 200.0, 2024, 8.4000000000000004 },
                    { 62, 2.2999999999999998, true, "Cadillac", 3, 1, 4, "Black", new DateTime(2025, 4, 13, 22, 26, 10, 0, DateTimeKind.Unspecified), "THE MAFIA", "Full wheels", 6.2000000000000002, "Automatic", 416, false, 300.0, 1600.0, "Escalade", true, 375.0, 2362.0, 200.0, 2024, 6.5999999999999996 }
                });

            migrationBuilder.InsertData(
                table: "Report",
                columns: new[] { "Id", "CreateAt", "CustomerId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 15, 12, 32, 45, 0, DateTimeKind.Unspecified), 1, "Mnogo hubav", "Test" },
                    { 2, new DateTime(2025, 2, 19, 12, 13, 43, 0, DateTimeKind.Unspecified).AddTicks(2100), 7, "LUDDADAA Po TEBB ", "Az sum nikola" },
                    { 3, new DateTime(2025, 2, 19, 12, 28, 26, 499, DateTimeKind.Unspecified).AddTicks(2463), 1, "test", "test" },
                    { 4, new DateTime(2025, 3, 10, 15, 14, 22, 452, DateTimeKind.Unspecified).AddTicks(1300), 1, "TEST TEST TEST", "Other" },
                    { 5, new DateTime(2025, 4, 12, 14, 40, 41, 348, DateTimeKind.Unspecified).AddTicks(8780), 2, "I have to test this if it works. Please everyone hope to work", "Booking Inquiry" }
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
                    { 383, 22, 28 },
                    { 385, 60, 2 },
                    { 386, 60, 3 },
                    { 387, 60, 4 },
                    { 388, 60, 5 },
                    { 389, 60, 6 },
                    { 390, 60, 7 },
                    { 392, 60, 9 },
                    { 393, 60, 12 },
                    { 394, 60, 14 },
                    { 395, 60, 15 },
                    { 396, 60, 17 },
                    { 397, 60, 18 },
                    { 398, 60, 20 },
                    { 399, 60, 21 },
                    { 400, 60, 22 },
                    { 402, 60, 24 },
                    { 404, 60, 26 },
                    { 405, 60, 28 },
                    { 406, 60, 29 },
                    { 407, 60, 30 },
                    { 408, 60, 31 },
                    { 409, 60, 32 },
                    { 410, 60, 33 },
                    { 411, 60, 34 },
                    { 412, 60, 35 },
                    { 413, 60, 36 },
                    { 415, 60, 39 },
                    { 416, 60, 40 },
                    { 417, 60, 41 },
                    { 418, 60, 46 },
                    { 419, 60, 47 },
                    { 420, 60, 49 },
                    { 421, 60, 50 },
                    { 422, 60, 23 },
                    { 423, 60, 8 },
                    { 424, 60, 1 },
                    { 425, 60, 38 },
                    { 426, 60, 25 },
                    { 427, 61, 1 },
                    { 428, 61, 3 },
                    { 429, 61, 4 },
                    { 430, 61, 15 },
                    { 431, 61, 20 },
                    { 432, 61, 21 },
                    { 433, 61, 30 },
                    { 434, 61, 38 },
                    { 435, 61, 50 },
                    { 438, 10, 12 },
                    { 439, 62, 1 },
                    { 440, 62, 2 },
                    { 441, 62, 3 },
                    { 442, 62, 4 },
                    { 443, 62, 5 },
                    { 444, 62, 7 },
                    { 445, 62, 8 },
                    { 446, 62, 9 },
                    { 447, 62, 11 },
                    { 448, 62, 15 },
                    { 449, 62, 16 },
                    { 450, 62, 23 },
                    { 451, 62, 24 },
                    { 452, 62, 28 },
                    { 453, 62, 31 },
                    { 454, 62, 33 },
                    { 455, 62, 34 },
                    { 456, 62, 35 },
                    { 457, 62, 38 },
                    { 458, 62, 39 },
                    { 459, 62, 40 },
                    { 460, 62, 42 },
                    { 461, 62, 44 },
                    { 462, 62, 48 },
                    { 463, 62, 50 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CarId", "Order", "Url" },
                values: new object[,]
                {
                    { 1, 1, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624215/toyota-corolla1_b4potk.jpg" },
                    { 2, 1, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624216/toyota-corolla2_n7shyh.jpg" },
                    { 3, 1, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624216/toyota-corolla3_dho7dm.jpg" },
                    { 4, 1, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624217/toyota-corolla4_cfo28b.jpg" },
                    { 5, 2, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624172/honda-civic1_rnvnry.png" },
                    { 6, 2, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624173/honda-civic2_gvys4w.png" },
                    { 7, 2, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624174/honda-civic3_jltzua.png" },
                    { 8, 2, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624175/honda-civic4_frqcl3.png" },
                    { 9, 4, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624119/bmw-420i1_nxmy5j.png" },
                    { 10, 4, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624124/bmw-420i7_y9o1y9.png" },
                    { 11, 4, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624120/bmw-420i2_iokhet.png" },
                    { 12, 4, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624121/bmw-420i3_ldnjlf.png" },
                    { 13, 4, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624122/bmw-420i4_ridevu.png" },
                    { 14, 4, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624122/bmw-420i5_q10b3d.png" },
                    { 15, 4, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624123/bmw-420i6_wtzvrn.png" },
                    { 16, 5, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624129/c300-1_xa9fzu.webp" },
                    { 17, 5, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624129/c300-2_szhfxp.webp" },
                    { 18, 5, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624130/c300-3_rdbinb.webp" },
                    { 19, 5, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624131/c300-4_bgrzgs.webp" },
                    { 20, 5, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624132/c300-5_dve7qn.webp" },
                    { 21, 5, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624132/c300-6_h9enpn.webp" },
                    { 22, 3, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624147/ford-mustang-1_bvjyjo.webp" },
                    { 23, 3, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624148/ford-mustang-2_tbc9od.webp" },
                    { 24, 3, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624149/ford-mustang-3_wsj9bd.webp" },
                    { 25, 3, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624149/ford-mustang-4_abjtqh.webp" },
                    { 26, 3, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624150/ford-mustang-5_d2g57e.webp" },
                    { 27, 3, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624151/ford-mustang-6_oi7yjf.webp" },
                    { 28, 3, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624171/ford-mustang-7_gfhjq4.webp" },
                    { 29, 3, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624171/ford-mustang-8_l8owjg.webp" },
                    { 30, 6, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624101/audi-r8-1_izlqmr.jpg" },
                    { 31, 6, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624102/audi-r8-2_bwgt3m.jpg" },
                    { 32, 6, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624105/audi-r8-7_qcjsl4.jpg" },
                    { 33, 6, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624104/audi-r8-5_c6hugv.jpg" },
                    { 34, 6, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624105/audi-r8-6_wtw31c.jpg" },
                    { 35, 6, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624103/audi-r8-4_wtfr0f.jpg" },
                    { 36, 6, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624102/audi-r8-3_hrsi4g.jpg" },
                    { 37, 6, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624106/audi-r8-8_uixxgi.jpg" },
                    { 38, 7, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624175/lambo-hur-1_il42yl.jpg" },
                    { 39, 7, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624176/lambo-hur-2_odjisi.jpg" },
                    { 40, 7, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624177/lambo-hur-3_yyl1xk.jpg" },
                    { 41, 7, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624178/lambo-hur-4_errnh3.jpg" },
                    { 42, 7, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624178/lambo-hur-5_lako1f.jpg" },
                    { 43, 8, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624198/porsche-gt3-1_aw1eyn.webp" },
                    { 44, 8, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624199/porsche-gt3-2_viiwrz.webp" },
                    { 45, 8, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624200/porsche-gt3-4_ozoddo.webp" },
                    { 46, 8, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624201/porsche-gt3-5_r8rh4a.webp" },
                    { 47, 8, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624200/porsche-gt3-3_ryxvfh.webp" },
                    { 48, 8, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624202/porsche-gt3-6_axhlgo.webp" },
                    { 49, 8, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624203/porsche-gt3-7_qrihj8.webp" },
                    { 50, 8, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624203/porsche-gt3-8_jxqgaa.webp" },
                    { 51, 9, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624210/tesla-s-1_lpjunc.webp" },
                    { 52, 9, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624211/tesla-s-2_f3xqwp.webp" },
                    { 53, 9, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624212/tesla-s-3_thbj4a.webp" },
                    { 54, 9, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624213/tesla-s-4_gpgpcz.webp" },
                    { 55, 9, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624213/tesla-s-5_zwunpd.webp" },
                    { 56, 9, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624214/tesla-s-6_q9yrxu.webp" },
                    { 57, 10, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624141/ferrari-f-1_adyn0z.webp" },
                    { 58, 10, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624142/ferrari-f-2_ccxpeg.webp" },
                    { 59, 10, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624143/ferrari-f-3_orloyj.webp" },
                    { 60, 10, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624144/ferrari-f-4_bdztgj.webp" },
                    { 61, 10, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624144/ferrari-f-5_s7ru1h.webp" },
                    { 62, 10, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624145/ferrari-f-6_taupu5.webp" },
                    { 63, 10, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624146/ferrari-f-7_kzoecy.webp" },
                    { 64, 10, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624146/ferrari-f-8_dn9g53.webp" },
                    { 65, 11, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624190/phanthom-1_uh4lhj.webp" },
                    { 66, 11, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624191/phanthom-2_ruvpjz.webp" },
                    { 67, 11, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624192/phanthom-3_clwtzg.webp" },
                    { 68, 11, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624192/phanthom-4_fso0mv.webp" },
                    { 69, 11, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624193/phanthom-5_htnvgs.webp" },
                    { 70, 11, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624194/phanthom-6_e6u4tc.webp" },
                    { 71, 11, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624195/phanthom-7_pnuqus.webp" },
                    { 72, 11, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624195/phanthom-8_g2jcvh.webp" },
                    { 73, 11, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624196/phanthom-9_eydedv.webp" },
                    { 74, 11, 10, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624197/phanthom-10_tac7j7.webp" },
                    { 75, 11, 11, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624197/phanthom-11_mmhpah.webp" },
                    { 76, 12, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624110/bentley-continental-gt-1_jtijlv.webp" },
                    { 77, 12, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624111/bentley-continental-gt-2_gkaxkc.webp" },
                    { 78, 12, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624111/bentley-continental-gt-3_hgzvhk.webp" },
                    { 79, 12, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624112/bentley-continental-gt-4_gksbaw.webp" },
                    { 80, 12, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624113/bentley-continental-gt-5_ansuex.webp" },
                    { 81, 12, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624114/bentley-continental-gt-6_dmhczo.webp" },
                    { 82, 12, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624115/bentley-continental-gt-7_wa6hzb.webp" },
                    { 83, 12, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624115/bentley-continental-gt-8_zfce2y.webp" },
                    { 84, 12, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624116/bentley-continental-gt-9_ailjxc.webp" },
                    { 85, 12, 10, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624117/bentley-continental-gt-10_zuafdn.webp" },
                    { 86, 12, 11, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624118/bentley-continental-gt-11_dpgk0n.webp" },
                    { 87, 12, 12, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624119/bentley-continental-gt-12_qix8df.webp" },
                    { 88, 13, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624184/mc-720s-1_gfpc3h.webp" },
                    { 89, 13, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624185/mc-720s-2_chjno0.webp" },
                    { 90, 13, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624186/mc-720s-3_yyid0a.webp" },
                    { 91, 13, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624187/mc-720s-4_byfzl3.webp" },
                    { 92, 13, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624187/mc-720s-5_xfm1ak.webp" },
                    { 93, 13, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624188/mc-720s-6_jchqap.webp" },
                    { 94, 13, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624189/mc-720s-7_qhapko.webp" },
                    { 95, 14, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624088/aston-dbx-1_fxbd0z.webp" },
                    { 96, 14, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624089/aston-dbx-2_pokk3x.webp" },
                    { 97, 14, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624090/aston-dbx-3_hr8new.webp" },
                    { 98, 14, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624091/aston-dbx-4_mdqqwi.webp" },
                    { 99, 14, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624091/aston-dbx-5_sjbfzg.webp" },
                    { 100, 14, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624092/aston-dbx-6_n26der.webp" },
                    { 101, 14, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624093/aston-dbx-7_rde45u.webp" },
                    { 102, 14, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624093/aston-dbx-8_g56hgy.webp" },
                    { 103, 15, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624179/lexus-lx-1_oy7dob.webp" },
                    { 104, 15, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624180/lexus-lx-2_gbutlv.webp" },
                    { 105, 15, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624181/lexus-lx-3_qauteg.webp" },
                    { 106, 15, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624181/lexus-lx-4_o36dcg.webp" },
                    { 107, 15, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624182/lexus-lx-5_sltxmf.webp" },
                    { 108, 15, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624183/lexus-lx-6_tfpdn9.webp" },
                    { 109, 15, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624184/lexus-lx-7_sh3rbv.webp" },
                    { 110, 16, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624084/amg-gt-1_sarh5f.webp" },
                    { 111, 16, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624084/amg-gt-2_zkxqad.webp" },
                    { 112, 16, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624085/amg-gt-3_bsf98p.webp" },
                    { 113, 16, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624085/amg-gt-4_zjcdzb.webp" },
                    { 114, 16, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624086/amg-gt-5_qmk6z5.webp" },
                    { 115, 16, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624086/amg-gt-6_qeovuo.webp" },
                    { 116, 16, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624087/amg-gt-7_dzuweo.webp" },
                    { 117, 16, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624088/amg-gt-8_s7wcnc.webp" },
                    { 118, 17, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624094/audi-q8-1_iehvtl.webp" },
                    { 119, 17, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624095/audi-q8-2_k1zvqr.webp" },
                    { 120, 17, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624096/audi-q8-3_se4pci.webp" },
                    { 121, 17, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624096/audi-q8-4_lp8szg.webp" },
                    { 122, 17, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624097/audi-q8-5_rmh0xi.webp" },
                    { 123, 17, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624098/audi-q8-6_lhuxkd.webp" },
                    { 124, 17, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624098/audi-q8-7_mfgkde.webp" },
                    { 125, 17, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624099/audi-q8-8_uvjc4w.webp" },
                    { 126, 17, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624100/audi-q8-9_q48agf.webp" },
                    { 127, 18, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624124/bmw-m4-1_wrcccj.webp" },
                    { 128, 18, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624125/bmw-m4-2_zvh1qm.webp" },
                    { 129, 18, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624126/bmw-m4-3_tcnfuc.webp" },
                    { 130, 18, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624127/bmw-m4-4_u7ibmo.webp" },
                    { 131, 18, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624128/bmw-m4-5_u3rnbk.webp" },
                    { 132, 18, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624128/bmw-m4-6_gk7knn.webp" },
                    { 133, 19, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624133/camry-1_wo9lkk.webp" },
                    { 134, 19, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624134/camry-2_etxvu2.webp" },
                    { 135, 19, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624134/camry-3_lb0kqw.webp" },
                    { 136, 19, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624135/camry-4_c1fqal.webp" },
                    { 137, 19, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624136/camry-5_njv1ok.webp" },
                    { 138, 19, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624137/camry-6_sdwbt0.webp" },
                    { 139, 19, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624137/camry-7_jw8pk1.webp" },
                    { 140, 20, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624138/cybertruck-1_s3nz5x.webp" },
                    { 141, 20, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624139/cybertruck-2_jmlgm8.webp" },
                    { 142, 20, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624139/cybertruck-3_num8xy.webp" },
                    { 143, 20, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624140/cybertruck-4_moirmd.webp" },
                    { 144, 20, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624141/cybertruck-5_l3ldx9.webp" },
                    { 145, 21, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624204/rover-1_dbhrsn.webp" },
                    { 146, 21, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624205/rover-2_ewg7kg.webp" },
                    { 147, 21, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624205/rover-3_ocenlp.webp" },
                    { 148, 21, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624206/rover-4_cqy61j.webp" },
                    { 149, 21, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624207/rover-5_jpjdek.webp" },
                    { 150, 21, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624207/rover-6_ffmgs5.webp" },
                    { 151, 21, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624208/rover-7_y5nvgx.webp" },
                    { 152, 21, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624209/rover-8_coy3zd.webp" },
                    { 153, 21, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624210/rover-9_ygo8xm.webp" },
                    { 154, 22, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624218/toyota-supra-1_rlschs.webp" },
                    { 155, 22, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624218/toyota-supra-2_mgzqxi.webp" },
                    { 156, 22, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624219/toyota-supra-3_p5v7h4.webp" },
                    { 157, 22, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624220/toyota-supra-4_lqxnfq.webp" },
                    { 158, 22, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624221/toyota-supra-5_bdk5pw.webp" },
                    { 159, 22, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624221/toyota-supra-6_dmnvea.webp" },
                    { 168, 29, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624279/fb06c790-0966-45b8-bb33-3ff8daa27bae_m8-5_jdio3w.webp" },
                    { 169, 29, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624272/ae7739e8-ee7e-4737-9290-10106e1d319d_m8-7_vvbo9q.webp" },
                    { 170, 29, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624275/d7403c23-b060-487d-b53f-4b47ee6b6d88_m8-6_c1hgfv.webp" },
                    { 171, 29, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624274/d08ab2eb-cc07-4aa3-b750-88a1c7e550bb_m8-1_jge5ek.webp" },
                    { 172, 29, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624265/62b475ca-c950-4b19-a288-c8f76cb77c22_m8-2_rjjzp8.webp" },
                    { 173, 29, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624265/209c6ba6-4583-46f2-ac3e-2ffc8817e12f_m8-8_uldwdr.webp" },
                    { 174, 29, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624269/2875b307-22b1-40ec-82cf-8848e4e448a6_m8-3_p86pcj.webp" },
                    { 175, 29, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624260/6d818001-1a44-4ac1-84db-8d34b21d7781_m8-4_gn7kry.webp" },
                    { 176, 30, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624277/f4ba43d2-9b58-45ee-9124-e9ab3a691f27_s500-1_ptfknq.webp" },
                    { 177, 30, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624268/721d9f20-a18e-4f46-8a6b-ae3b938dc35b_s500-2_exazic.webp" },
                    { 178, 30, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624276/edb09ce5-3ab2-4f24-87fc-95dff9e23112_s500-3_psj78x.webp" },
                    { 179, 30, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624271/a55b5ab6-04df-4769-a630-2da7e8d33a2f_s500-4_gpnmkd.webp" },
                    { 180, 30, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624261/8bf7b19a-975d-402a-b122-ac549da2dd5a_s500-5_z43sy3.webp" },
                    { 181, 30, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624263/036bc54d-80b7-4807-999f-0a0d00151f19_s500-6_nmkeux.webp" },
                    { 182, 30, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624266/301b678b-9700-45f7-beb7-3ef13db1c22a_s500-7_jrezic.webp" },
                    { 183, 30, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624272/a80b8d53-ad3e-4b0c-aec0-ad6ef45852f9_s500-8_hj8qmk.webp" },
                    { 184, 30, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624270/06765e41-ebb4-430d-a456-1948eddfabce_s500-9_svlcnd.webp" },
                    { 185, 30, 10, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624268/956a0de4-fb58-423c-a686-4d03188dc37c_s500-10_aj2s6a.webp" },
                    { 186, 30, 11, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624259/4e8303a9-66de-4af4-8d6e-783e7bb05f04_s500-11_p54snz.webp" },
                    { 187, 31, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624263/21b1a87d-e69b-48c3-8402-3e352e73a766_aston-vantage-1_btxlye.webp" },
                    { 188, 31, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624275/d8fb37af-2075-4542-84e9-44d889dbea61_aston-vantage-2_fyntld.webp" },
                    { 189, 31, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624278/f5cc2d5b-cc0c-465a-86ed-e1f430e8cf9f_aston-vantage-6_s2c8mm.webp" },
                    { 190, 31, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624262/8d838486-948a-45af-8e19-37dbb896f6cf_aston-vantage-7_qws9fm.webp" },
                    { 191, 31, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624273/cef4c199-3cc0-4a1d-b687-34d21201e5db_aston-vantage-9_oalm5d.webp" },
                    { 192, 31, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624279/f79ea5a7-4aba-42d2-bb37-dbb62f1cc9e4_aston-vantage-3_mx9flo.webp" },
                    { 193, 31, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624258/0d216c7e-2714-479e-847b-57b85f9135b7_aston-vantage-8_s2s0pm.webp" },
                    { 194, 31, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624260/7ac23fbd-a22c-4a01-aeaa-575b6581db7d_aston-vantage-4_abh4x9.webp" },
                    { 195, 31, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739624267/405b032a-01da-4411-898e-6f2e2e98aa53_aston-vantage-5_cjmqdw.webp" },
                    { 196, 42, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628895/cars/Images/19f6420c-160d-429a-9d22-685b14ebd333.webp" },
                    { 197, 42, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628896/cars/Images/30454fee-73ab-47a1-832b-55943feb94e8.webp" },
                    { 198, 42, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628897/cars/Images/d5fde7b4-8205-4b20-b13c-57200cc74ac4.webp" },
                    { 199, 42, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628898/cars/Images/c41640e6-f634-47e6-b87f-331046d72ad5.webp" },
                    { 200, 42, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628899/cars/Images/18ed750a-f2d1-413f-8984-44c54eb5781f.webp" },
                    { 201, 42, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628899/cars/Images/6a8301dc-7f48-4c9d-841f-c1799c631ae0.webp" },
                    { 202, 42, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628900/cars/Images/41ecbee7-656e-4391-94d6-69359050810f.webp" },
                    { 203, 42, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1739628901/cars/Images/42af69bc-b2cc-47fe-8b22-a8fccb1f01f9.webp" },
                    { 204, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244955/cars/Images/b8ecd959-f42d-465b-90d2-c5873c10a902.webp" },
                    { 205, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244956/cars/Images/981c3909-bd3e-4cb9-9a58-16b2bc63f76f.webp" },
                    { 206, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244957/cars/Images/6812c0d9-2219-46b4-9623-57cc2be612f4.webp" },
                    { 207, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244957/cars/Images/8195f1e5-73db-44f6-9bd6-0716515472fe.webp" },
                    { 208, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244958/cars/Images/e7e710df-caed-47d8-aa19-565bc622a315.webp" },
                    { 209, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244959/cars/Images/ad3adf38-4290-4601-a654-182f3f4acfee.webp" },
                    { 210, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244959/cars/Images/b2b1a36c-4b28-43b8-bb0e-d8b0c64276db.webp" },
                    { 211, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244960/cars/Images/63c38baa-ecdf-419f-95ee-6136796a6783.webp" },
                    { 212, 43, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740244960/cars/Images/eef7ab1a-9d0d-42c9-8bd7-8676d27cd7c0.webp" },
                    { 213, 44, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740245470/cars/Images/643dfe36-8988-48f3-89b8-69a842f7eeb8.webp" },
                    { 214, 44, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740245471/cars/Images/2c8150e8-0b38-4ca7-8ea5-447e22e7c1f3.webp" },
                    { 215, 44, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740245472/cars/Images/4be19a48-5171-458d-af1b-3a6e16fcdc08.webp" },
                    { 216, 44, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740245472/cars/Images/66d5ca09-6f09-4d31-b5db-9020b0ab0f4f.webp" },
                    { 217, 44, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740245473/cars/Images/ab850725-3845-4c7d-98c8-e408eab744c5.webp" },
                    { 228, 48, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733624/cars/Images/d83998be-3cdb-47cb-9c71-e00b50766922.webp" },
                    { 229, 48, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733625/cars/Images/a7b757da-467c-48c6-8d5b-1bd6a086fdff.webp" },
                    { 230, 48, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733626/cars/Images/f05f5a8c-f0c4-4de8-a651-b0149efa2ad3.webp" },
                    { 231, 48, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733627/cars/Images/e2304303-2590-4bc6-bfc6-f73a01727f3f.webp" },
                    { 232, 48, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733627/cars/Images/e041a947-d333-4130-89b3-789da2c0e869.webp" },
                    { 233, 48, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733628/cars/Images/3787666c-6597-4b3a-aa6a-3420d58168f4.webp" },
                    { 234, 48, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733628/cars/Images/c4ed526e-2a6c-4283-acc7-fada941d546d.webp" },
                    { 235, 48, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733629/cars/Images/bef1c56b-74b1-4afc-821a-9b17f80b75d5.webp" },
                    { 236, 48, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733630/cars/Images/38d5cdb5-b9da-4f8b-a642-2d4ec2f1ba6e.webp" },
                    { 237, 48, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740733631/cars/Images/8dab0c22-8f4a-4e0c-a137-9e8ca94b9c36.webp" },
                    { 238, 49, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735257/cars/Images/cf96bf20-f297-4d6f-8025-08d48ff05005.webp" },
                    { 239, 49, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735258/cars/Images/a1a0fc6e-274e-4b21-9053-3791e0725ef8.webp" },
                    { 240, 49, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735259/cars/Images/d221cae8-06c4-4c6a-932a-d11a71130e23.webp" },
                    { 241, 49, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735259/cars/Images/638d301e-33a9-442f-a040-8bbf6a7a439f.webp" },
                    { 242, 49, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735260/cars/Images/83af8756-a511-43fa-b83f-265369ff8f6b.webp" },
                    { 243, 49, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735261/cars/Images/c87adfd2-d75b-44b9-8010-e456608fe51c.webp" },
                    { 244, 49, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735262/cars/Images/b9d72309-adc2-488c-870c-b5b7ff3af6b6.webp" },
                    { 245, 49, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735262/cars/Images/807086e3-1c63-4356-9ae8-19864f029a8d.webp" },
                    { 246, 49, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740735263/cars/Images/f0b7ac54-f417-4f12-b1c4-4693cc31cafa.webp" },
                    { 247, 50, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740736703/cars/Images/77ec370d-e163-40c8-8d34-cfbf333230e7.webp" },
                    { 248, 50, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740736704/cars/Images/c7a62841-68fd-48e8-a35e-4956d3f227e8.webp" },
                    { 249, 50, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740736705/cars/Images/d32232f6-5a31-4b64-9283-5462d52be802.webp" },
                    { 250, 50, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740736705/cars/Images/381852b3-c708-4004-8b8e-fd9800ff0a99.webp" },
                    { 251, 50, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740736706/cars/Images/d915d242-25c1-4435-9e60-576945253c0e.webp" },
                    { 252, 50, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740736707/cars/Images/e3d177d9-d88a-43cd-9e01-e438ea24ed75.webp" },
                    { 253, 50, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740736707/cars/Images/43c66b93-52ce-46df-ab21-90ab17bdfd99.webp" },
                    { 254, 51, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740737869/cars/Images/f2f948a0-dcfe-4e72-82b4-08e4484fd39d.webp" },
                    { 255, 51, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740737870/cars/Images/503df031-1383-4f3b-a61c-a6c0c9a335f6.webp" },
                    { 256, 51, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740737871/cars/Images/246b04cb-e3e9-4822-b860-de2b194d0f55.webp" },
                    { 257, 51, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740737872/cars/Images/11bb4295-76a6-4152-a24c-21051dc68346.webp" },
                    { 258, 51, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740737873/cars/Images/dc9bda3f-2e4c-4889-8552-b8d8cec1bd92.webp" },
                    { 259, 51, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740737873/cars/Images/61bd5605-ec36-4f0a-8f63-c58779dc6422.webp" },
                    { 260, 51, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740737874/cars/Images/3a2f6b36-b378-4a04-b257-aadec245b5e0.webp" },
                    { 266, 53, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740741941/cars/Images/890d6faa-7edd-461a-9575-183e7f0db9cb.jpg" },
                    { 267, 53, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740741942/cars/Images/063cac4b-c187-446d-ab99-00016f5a3cfc.webp" },
                    { 268, 53, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740741942/cars/Images/c0856400-5863-49e8-8180-169814b864f5.webp" },
                    { 269, 53, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740741943/cars/Images/69bfe3c0-e9ff-45bd-bba3-cc1d09ab8db0.webp" },
                    { 270, 53, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740741944/cars/Images/52969ed3-7af8-420e-bae7-914b9dbf14b0.webp" },
                    { 271, 53, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740741945/cars/Images/5edd4a6d-32a1-4e1e-9ca4-bdee4df9ed57.webp" },
                    { 272, 53, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740741946/cars/Images/9f01d256-0bb5-4722-a44c-872fb937b57c.webp" },
                    { 273, 54, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767513/cars/Images/0d590574-be75-4d19-a430-9b7e916c42d4.webp" },
                    { 274, 54, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767514/cars/Images/e5b6ea72-070a-4838-bfd3-5f94d88d943b.webp" },
                    { 275, 54, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767515/cars/Images/d0fd8b96-f2fe-4437-85b2-05db8c0bcd01.webp" },
                    { 276, 54, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767515/cars/Images/41e3ab63-b7ea-4143-918b-53b2cc2ce810.webp" },
                    { 277, 54, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767516/cars/Images/3a6aaf28-bf71-47a1-b2ce-7e8c32e7abdb.webp" },
                    { 278, 54, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767516/cars/Images/350ad644-485a-438c-af0c-6347eb08d8a5.webp" },
                    { 279, 54, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767517/cars/Images/271ac88f-b5eb-440b-9175-0c78351cabf2.webp" },
                    { 280, 54, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740767518/cars/Images/acbba22d-a0ba-4eed-aa07-d390eec5da91.webp" },
                    { 281, 55, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740769698/cars/Images/66affa34-1130-4486-b6d3-e289798e2efe.webp" },
                    { 282, 55, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740769699/cars/Images/a92f534c-50d0-4d41-a778-9f152b67ffa1.webp" },
                    { 283, 55, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740769700/cars/Images/de4374d4-4588-4349-8f40-a0bdce08c574.webp" },
                    { 284, 55, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740769701/cars/Images/3a110ceb-30b4-4e7a-b744-3ac898cc985c.webp" },
                    { 285, 55, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740769701/cars/Images/90c2c176-ffb1-444e-aff0-b058ed061b67.webp" },
                    { 286, 56, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770316/cars/Images/46cd213c-8185-44b4-9ae4-efd3a0f6105b.webp" },
                    { 287, 56, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770316/cars/Images/2f19ed1e-5fca-4072-bdfe-cee37cf3a242.webp" },
                    { 288, 56, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770317/cars/Images/661f9f36-0673-4c61-a7a8-09846dea64d9.webp" },
                    { 289, 56, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770318/cars/Images/4517191a-32d1-4ef6-86d9-9536437879c2.webp" },
                    { 290, 56, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770318/cars/Images/3a570439-6056-4fec-b5d4-265e25c2674c.webp" },
                    { 291, 56, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770319/cars/Images/2c377818-6a14-4ae2-a4a1-00c5e2991094.webp" },
                    { 292, 56, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770319/cars/Images/1b4b05e7-b6a9-4595-8920-aa0d4d8aa86a.webp" },
                    { 293, 56, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770320/cars/Images/faaa6311-9205-474b-b46e-904345572f13.webp" },
                    { 294, 56, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770321/cars/Images/bbf3cf5a-3545-42f6-b996-f423ce57486a.webp" },
                    { 295, 56, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770321/cars/Images/20cd0e2e-aa6f-4e61-a7f3-950296bf0a8d.webp" },
                    { 296, 56, 10, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740770322/cars/Images/99b3bf06-2a67-45d2-aa61-e644d5c616fd.webp" },
                    { 297, 57, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927744/cars/Images/1cfd9dbb-412b-42c3-a672-761026f5944c.webp" },
                    { 298, 57, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927745/cars/Images/037dbd9e-e300-4a9d-8caf-371e72aee63d.webp" },
                    { 299, 57, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927746/cars/Images/4ce94846-2c01-4cb9-af8c-fc201f68f2d9.webp" },
                    { 300, 57, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927746/cars/Images/6bc9541e-ed9f-43d6-96f8-2f8c6b06d40f.webp" },
                    { 301, 57, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927747/cars/Images/0dd509a5-2c32-4ca5-9026-586f88954857.webp" },
                    { 302, 57, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927748/cars/Images/3debbb94-7179-41e8-a4eb-9ebef712d2d9.webp" },
                    { 303, 57, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927748/cars/Images/83a93175-82c2-40e5-a626-de64dcb9da1b.webp" },
                    { 304, 57, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927749/cars/Images/4ef653d5-0ef8-42f8-9589-c6124410e8d2.webp" },
                    { 305, 57, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927750/cars/Images/26765354-bbb7-463e-b567-d62172291de7.webp" },
                    { 306, 57, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1740927751/cars/Images/2fa005b6-3072-4ecf-87d0-bc8431151332.webp" },
                    { 327, 60, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575978/cars/Images/c4e7326b-f54b-49ce-9d74-0071a32928b7.webp" },
                    { 328, 60, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575979/cars/Images/a205ca4f-165e-476c-babb-52635723f8dd.webp" },
                    { 329, 60, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575980/cars/Images/f1b07494-277b-4e17-abae-34b8426fe69d.webp" },
                    { 330, 60, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575980/cars/Images/00884e46-5af8-4cb4-9b26-b7fafef27b0e.webp" },
                    { 331, 60, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575981/cars/Images/86b00f26-cca0-4ed9-8052-8a0d179c142c.webp" },
                    { 332, 60, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575982/cars/Images/9f9e64de-5df1-42c5-86f3-c4cdd8537d28.webp" },
                    { 333, 60, 6, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575983/cars/Images/e15bb144-186c-49c0-9c65-51b22e42428c.webp" },
                    { 334, 60, 7, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575984/cars/Images/ac8363cb-0f94-44db-ab6e-2f91573b969d.webp" },
                    { 335, 60, 8, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575984/cars/Images/8c05d3b9-2f50-49be-81f3-b8620f8cb148.webp" },
                    { 336, 60, 9, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743575985/cars/Images/d17a482c-02c8-4cc6-ab32-ca1683f0edde.webp" },
                    { 337, 61, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743587063/cars/Images/d53f87c3-40a4-4ba6-886b-1a6b10fd4f73.webp" },
                    { 338, 61, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743587064/cars/Images/8d0c8326-713c-4590-a088-730da0bafcad.webp" },
                    { 339, 61, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743587065/cars/Images/fae101d9-932a-4e49-969a-3d9dcd2e45bc.webp" },
                    { 340, 61, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743587066/cars/Images/a8f7a015-d24b-46d4-b38c-23110ba0814c.webp" },
                    { 341, 61, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743587067/cars/Images/c3e04bd5-a144-4196-963a-800d60ebb8f5.webp" },
                    { 342, 61, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1743587068/cars/Images/3c30c3ba-3105-42d5-9ea8-84b8052d1153.webp" },
                    { 343, 62, 0, "https://res.cloudinary.com/dhibmotzx/image/upload/v1744583166/cars/Images/f0ade371-a95e-461a-b93e-25702ae0b49c.webp" },
                    { 344, 62, 1, "https://res.cloudinary.com/dhibmotzx/image/upload/v1744583167/cars/Images/e8339f34-722f-4dab-89db-f6b21a8f493d.webp" },
                    { 345, 62, 2, "https://res.cloudinary.com/dhibmotzx/image/upload/v1744583168/cars/Images/f458d1da-3ba0-4914-aec1-b5d4f1f1c1d2.webp" },
                    { 346, 62, 3, "https://res.cloudinary.com/dhibmotzx/image/upload/v1744583169/cars/Images/5429a81e-f652-4a26-a5f6-7b34a13facaf.webp" },
                    { 347, 62, 4, "https://res.cloudinary.com/dhibmotzx/image/upload/v1744583169/cars/Images/875e0028-6e2a-4a6a-ad47-abc4e9890bfa.webp" },
                    { 348, 62, 5, "https://res.cloudinary.com/dhibmotzx/image/upload/v1744583170/cars/Images/c51322a4-9555-4674-b39e-24938af0c96d.webp" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarId", "CreateTime", "CustomerId", "EndDate", "IsCanceled", "IsReturnBackAtSamePlace", "IsSelfPick", "PaidDeliveryPlace", "StartDate", "TotalPrice" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2025, 2, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 2, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), false, true, false, "Test s", new DateTime(2025, 2, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 216.0 },
                    { 5, 20, new DateTime(2025, 2, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 2, 19, 10, 0, 0, 0, DateTimeKind.Unspecified), false, true, false, "Test t", new DateTime(2025, 2, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), 5400.0 },
                    { 6, 22, new DateTime(2025, 2, 14, 13, 16, 18, 138, DateTimeKind.Unspecified), 1, new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2970.0 },
                    { 8, 5, new DateTime(2025, 2, 15, 8, 49, 13, 921, DateTimeKind.Unspecified), 1, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 567.0 },
                    { 11, 15, new DateTime(2025, 2, 15, 9, 2, 16, 740, DateTimeKind.Unspecified), 1, new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1350.0 },
                    { 12, 4, new DateTime(2025, 2, 14, 13, 5, 7, 29, DateTimeKind.Unspecified), 7, new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 675.0 },
                    { 13, 8, new DateTime(2025, 2, 25, 10, 58, 44, 381, DateTimeKind.Unspecified), 7, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3780.0 },
                    { 14, 42, new DateTime(2025, 2, 26, 10, 59, 48, 347, DateTimeKind.Unspecified), 7, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 675.0 },
                    { 15, 21, new DateTime(2025, 2, 26, 8, 0, 31, 391, DateTimeKind.Unspecified), 1, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3240.0 },
                    { 16, 4, new DateTime(2025, 3, 6, 12, 33, 3, 265, DateTimeKind.Unspecified), 7, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 675.0 },
                    { 17, 22, new DateTime(2025, 3, 13, 11, 56, 34, 581, DateTimeKind.Unspecified), 1, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 594.0 },
                    { 19, 8, new DateTime(2025, 3, 21, 12, 5, 7, 208, DateTimeKind.Unspecified), 1, new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3780.0 },
                    { 20, 10, new DateTime(2025, 3, 24, 9, 29, 59, 234, DateTimeKind.Unspecified), 1, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, false, null, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 6210.0 },
                    { 21, 22, new DateTime(2025, 3, 26, 19, 2, 58, 318, DateTimeKind.Unspecified), 7, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 594.0 },
                    { 22, 5, new DateTime(2025, 4, 5, 9, 1, 51, 994, DateTimeKind.Unspecified), 7, new DateTime(2025, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, false, "ul. \"DUBAISKA\"", new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 617.0 },
                    { 23, 5, new DateTime(2025, 4, 6, 9, 10, 9, 156, DateTimeKind.Unspecified), 2, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, false, "ul. \"DUBAISKA 2\"", new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 995.0 },
                    { 24, 5, new DateTime(2025, 4, 6, 9, 15, 7, 406, DateTimeKind.Unspecified), 2, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, false, "ul. \"DUBAISKA 2\"", new DateTime(2025, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 617.0 },
                    { 25, 9, new DateTime(2025, 4, 6, 10, 28, 6, 598, DateTimeKind.Unspecified), 2, new DateTime(2025, 5, 10, 0, 1, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 630.0 },
                    { 26, 42, new DateTime(2025, 4, 6, 18, 50, 55, 973, DateTimeKind.Unspecified), 4, new DateTime(2025, 4, 10, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 4, 7, 3, 0, 0, 0, DateTimeKind.Unspecified), 675.0 },
                    { 27, 22, new DateTime(2025, 4, 7, 18, 54, 0, 371, DateTimeKind.Unspecified), 4, new DateTime(2025, 4, 14, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 4, 11, 3, 0, 0, 0, DateTimeKind.Unspecified), 594.0 },
                    { 28, 49, new DateTime(2025, 4, 6, 19, 16, 33, 109, DateTimeKind.Unspecified), 1, new DateTime(2025, 4, 10, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 4, 7, 3, 0, 0, 0, DateTimeKind.Unspecified), 810.0 },
                    { 29, 60, new DateTime(2025, 4, 6, 19, 51, 10, 745, DateTimeKind.Unspecified), 2, new DateTime(2025, 4, 10, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, null, new DateTime(2025, 4, 7, 3, 0, 0, 0, DateTimeKind.Unspecified), 945.0 },
                    { 30, 51, new DateTime(2025, 4, 11, 9, 12, 47, 307, DateTimeKind.Unspecified), 1, new DateTime(2025, 4, 15, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, false, "Bluewaters Island, Dubai", new DateTime(2025, 4, 12, 3, 0, 0, 0, DateTimeKind.Unspecified), 617.0 },
                    { 33, 49, new DateTime(2025, 4, 13, 16, 50, 36, 478, DateTimeKind.Unspecified), 4, new DateTime(2025, 4, 17, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, false, null, new DateTime(2025, 4, 14, 3, 0, 0, 0, DateTimeKind.Unspecified), 810.0 },
                    { 34, 19, new DateTime(2025, 4, 13, 17, 2, 34, 244, DateTimeKind.Unspecified), 4, new DateTime(2025, 4, 20, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, false, null, new DateTime(2025, 4, 18, 3, 0, 0, 0, DateTimeKind.Unspecified), 259.0 },
                    { 36, 10, new DateTime(2025, 4, 14, 23, 24, 8, 614, DateTimeKind.Unspecified), 2, new DateTime(2025, 4, 21, 3, 0, 0, 0, DateTimeKind.Unspecified), false, false, false, null, new DateTime(2025, 4, 19, 3, 0, 0, 0, DateTimeKind.Unspecified), 4140.0 }
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
                name: "IX_Cars_CTypeId",
                table: "Cars",
                column: "CTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsFeatures_CarId",
                table: "CarsFeatures",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsFeatures_FeatureId",
                table: "CarsFeatures",
                column: "FeatureId");

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
                name: "IX_Report_CustomerId",
                table: "Report",
                column: "CustomerId");

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
                name: "Images");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CarCompanies");

            migrationBuilder.DropTable(
                name: "ClassOfCars");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
