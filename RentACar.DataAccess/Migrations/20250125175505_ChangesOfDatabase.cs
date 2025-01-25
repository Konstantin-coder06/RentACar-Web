using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangesOfDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Economy" },
                    { 2, "Luxury" },
                    { 3, "Sport" },
                    { 4, "German Cars" },
                    { 5, "Electric" },
                    { 6, "SUV" },
                    { 7, "Muscle Cars" },
                    { 8, "JDM Cars" },
                    { 9, "Compact" },
                    { 10, "Classic" }
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
                table: "Cars",
                columns: new[] { "Id", "AdditionalMileageCharge", "Available", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "Gearbox", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[,]
                {
                    { 1, 0.20000000000000001, true, "Toyota", 9, "White", "Compact and fuel-efficient", 1.8, "Automatic", 150.0, 1000.0, "Corolla", 40.0, 250.0, 2021 },
                    { 2, 0.25, true, "Honda", 3, "Black", "Sporty and reliable", 2.0, "Manual", 200.0, 1200.0, "Civic", 50.0, 300.0, 2022 },
                    { 3, 0.22, true, "Ford", 7, "Blue", "Comfortable and stylish", 5.2000000000000002, "Automatic", 250.0, 1500.0, "Mustang", 85.0, 510.0, 2018 },
                    { 4, 0.5, true, "BMW", 2, "Gray", "Luxury and performance", 0.0, "Automatic", 300.0, 2000.0, "420i", 100.0, 600.0, 2023 },
                    { 5, 0.55000000000000004, true, "Mercedes-Benz", 2, "Silver", "Premium luxury", 2.1000000000000001, "Automatic", 300.0, 2000.0, "C-Class", 110.0, 650.0, 2023 },
                    { 6, 1.0, true, "Audi", 3, "Red", "High-performance sports car", 5.2000000000000002, "Automatic", 400.0, 2500.0, "R8", 300.0, 1800.0, 2023 },
                    { 7, 1.5, true, "Lamborghini", 3, "Yellow", "Exotic sports car", 5.2000000000000002, "Automatic", 400.0, 3000.0, "Huracan", 500.0, 3000.0, 2023 },
                    { 8, 1.3, true, "Porsche", 2, "Blue", "Luxury sports car", 3.7999999999999998, "Automatic", 400.0, 3000.0, "911 Turbo S", 400.0, 2400.0, 2023 },
                    { 9, 1.0, true, "Tesla", 5, "Black", "Electric luxury sedan", 0.0, "Automatic", 400.0, 2500.0, "Model S Plaid", 350.0, 2100.0, 2023 },
                    { 10, 2.0, true, "Ferrari", 3, "Red", "Iconic Italian sports car", 3.8999999999999999, "Automatic", 400.0, 3000.0, "F8 Tributo", 600.0, 3600.0, 2023 },
                    { 11, 3.0, true, "Rolls-Royce", 2, "White", "Ultimate luxury car", 6.7000000000000002, "Automatic", 300.0, 2000.0, "Phantom", 1000.0, 7000.0, 2023 },
                    { 12, 2.5, true, "Bentley", 2, "Silver", "Grand luxury tourer", 6.0, "Automatic", 300.0, 2000.0, "Continental GT", 800.0, 5000.0, 2023 },
                    { 13, 2.0, true, "McLaren", 3, "Orange", "Exquisite British engineering", 4.0, "Automatic", 400.0, 3000.0, "720S", 700.0, 4200.0, 2023 },
                    { 14, 0.80000000000000004, true, "Lexus", 6, "Blue", "Luxury hybrid SUV", 2.3999999999999999, "Automatic", 300.0, 2000.0, "RX 500h", 250.0, 1500.0, 2023 },
                    { 15, 2.0, true, "Aston Martin", 2, "Green", "Luxury British grand tourer", 5.2000000000000002, "Automatic", 300.0, 2000.0, "DB11", 600.0, 3600.0, 2023 },
                    { 16, 1.8, true, "Lexus", 2, "Black", "Sophisticated luxury coupe", 5.0, "Automatic", 300.0, 2000.0, "LC 500", 500.0, 3000.0, 2023 },
                    { 17, 2.2000000000000002, true, "Mercedes-Benz", 3, "Yellow", "German engineering excellence", 4.0, "Automatic", 400.0, 2500.0, "AMG GT", 700.0, 4200.0, 2023 },
                    { 18, 1.0, true, "Audi", 6, "White", "Luxury SUV", 3.0, "Automatic", 300.0, 2000.0, "Q8", 300.0, 1800.0, 2023 },
                    { 19, 1.5, true, "Toyota", 3, "Red", "Sporty and agile", 3.0, "Automatic", 300.0, 2000.0, "Supra", 350.0, 2000.0, 2023 },
                    { 20, 0.25, true, "Toyota", 9, "Blue", "Dependable sedan", 2.5, "Automatic", 200.0, 1200.0, "Camry", 50.0, 300.0, 2021 }
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
                    { 5, 2, "/images/honda-civic1.jpg" },
                    { 6, 2, "/images/honda-civic2.jpg" },
                    { 7, 2, "/images/honda-civic3.jpg" },
                    { 8, 2, "/images/honda-civic4.jpg" },
                    { 9, 3, "/images/bmw-420i1.jpg" },
                    { 10, 3, "/images/bmw-420i2.jpg" },
                    { 11, 3, "/images/bmw-420i7.jpg" },
                    { 12, 3, "/images/bmw-420i3.jpg" },
                    { 13, 3, "/images/bmw-420i4.jpg" },
                    { 14, 3, "/images/bmw-420i5.jpg" },
                    { 15, 3, "/images/bmw-420i6.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CarId",
                table: "Images",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
