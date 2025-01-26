using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AllImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PricePerDay", "PricePerWeek" },
                values: new object[] { 80.0, 450.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PricePerDay", "PricePerWeek" },
                values: new object[] { 140.0, 600.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "EngineCapacity", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "Black", 5.0, 200.0, 910.0, 2020 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 250.0, 1000.0, 2022 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "EngineCapacity", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "Red", 3.0, 210.0, 850.0, 2021 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "Green", "R8 Spyder", 750.0, 6400.0, 2021 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Color", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Black", 1700.0, 9000.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Silver", 4.0, "911 GT3", 1400.0, 8400.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9,
                column: "Color",
                value: "White");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "F8 Spider", 2300.0, 10000.0, 2022 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "Year" },
                values: new object[] { "Black", 2021 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Black", 4.0, "Continental GT-GTC", 1300.0, 7600.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Color", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Blue", 1700.0, 8000.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AdditionalMileageCharge", "Brand", "Color", "Description", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { 2.0, "Aston Martin", "Black", "Luxury British SUV", 4.0, "DBX", 600.0, 3600.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { 1.8, "Lexus", 6, "Silver", "Sophisticated luxury SUV", 3.3999999999999999, "LX", 500.0, 3000.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 2.2000000000000002, "Mercedes-Benz", 3, "Orange", "German engineering excellence", 4.0, 400.0, 2500.0, "AMG GT", 2500.0, 1100.0, 2022 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 1.0, "Audi", 6, "Orange", "Luxury SUV", 300.0, 2000.0, "Q8", 400.0, 2300.0, 2021 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { 1.5, "BMW", 3, "Yellow", "Sporty and agile", "M4", 700.0, 4000.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AdditionalMileageCharge", "CategoryId", "Description", "EngineCapacity", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 0.25, 9, "Dependable sedan", 2.5, 200.0, 1200.0, "Camry", 130.0, 670.0, 2021 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 1.8, "Tesla", 6, "Black", "Sophisticated luxury SUV", 0.0, 300.0, 2000.0, "Cybertruck", 2000.0, 12000.0, 2024 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AdditionalMileageCharge", "Available", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "Gearbox", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 21, 1.8, true, "Land Rover", 6, "Black", "Sophisticated luxury SUV", 4.4000000000000004, "Automatic", 300.0, 2000.0, "Range Rover", 1200.0, 7000.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                column: "CarId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                column: "CarId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                column: "CarId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                column: "CarId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                column: "CarId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 21,
                column: "CarId",
                value: 5);

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CarId", "Url" },
                values: new object[,]
                {
                    { 22, 3, "/images/ford-mustang1.jpg" },
                    { 23, 3, "/images/ford-mustang2.jpg" },
                    { 24, 3, "/images/ford-mustang3.jpg" },
                    { 25, 3, "/images/ford-mustang4.jpg" },
                    { 26, 3, "/images/ford-mustang5.jpg" },
                    { 27, 3, "/images/ford-mustang6.jpg" },
                    { 28, 3, "/images/ford-mustang7.jpg" },
                    { 29, 3, "/images/ford-mustang8.jpg" },
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
                    { 152, 12, "/images/rover-8.webp" },
                    { 153, 12, "/images/rover-9.webp" },
                    { 145, 21, "/images/rover-1.webp" },
                    { 146, 21, "/images/rover-2.webp" },
                    { 147, 21, "/images/rover-3.webp" },
                    { 148, 21, "/images/rover-4.webp" },
                    { 149, 21, "/images/rover-5.webp" },
                    { 150, 21, "/images/rover-6.webp" },
                    { 151, 21, "/images/rover-7.webp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PricePerDay", "PricePerWeek" },
                values: new object[] { 40.0, 250.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PricePerDay", "PricePerWeek" },
                values: new object[] { 50.0, 300.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Color", "EngineCapacity", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "Blue", 5.2000000000000002, 85.0, 510.0, 2018 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 100.0, 600.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Color", "EngineCapacity", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "Silver", 2.1000000000000001, 110.0, 650.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Color", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "Red", "R8", 300.0, 1800.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Color", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Yellow", 500.0, 3000.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Color", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Blue", 3.7999999999999998, "911 Turbo S", 400.0, 2400.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9,
                column: "Color",
                value: "Black");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { "F8 Tributo", 600.0, 3600.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Color", "Year" },
                values: new object[] { "White", 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Color", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Silver", 6.0, "Continental GT", 800.0, 5000.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Color", "PricePerDay", "PricePerWeek" },
                values: new object[] { "Orange", 700.0, 4200.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AdditionalMileageCharge", "Brand", "Color", "Description", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { 0.80000000000000004, "Lexus", "Blue", "Luxury hybrid SUV", 2.3999999999999999, "RX 500h", 250.0, 1500.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { 2.0, "Aston Martin", 2, "Green", "Luxury British grand tourer", 5.2000000000000002, "DB11", 600.0, 3600.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 1.8, "Lexus", 2, "Black", "Sophisticated luxury coupe", 5.0, 300.0, 2000.0, "LC 500", 500.0, 3000.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 2.2000000000000002, "Mercedes-Benz", 3, "Yellow", "German engineering excellence", 400.0, 2500.0, "AMG GT", 700.0, 4200.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "Model", "PricePerDay", "PricePerWeek" },
                values: new object[] { 1.0, "Audi", 6, "White", "Luxury SUV", "Q8", 300.0, 1800.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AdditionalMileageCharge", "CategoryId", "Description", "EngineCapacity", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 1.5, 3, "Sporty and agile", 3.0, 300.0, 2000.0, "Supra", 350.0, 2000.0, 2023 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AdditionalMileageCharge", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 0.25, "Toyota", 9, "Blue", "Dependable sedan", 2.5, 200.0, 1200.0, "Camry", 50.0, 300.0, 2021 });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15,
                column: "CarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20,
                column: "CarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 21,
                column: "CarId",
                value: 4);
        }
    }
}
