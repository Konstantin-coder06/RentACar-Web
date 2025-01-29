using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ImageProblemSolved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "Dark Gray");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AdditionalMileageCharge", "Available", "Brand", "CategoryId", "Color", "Description", "EngineCapacity", "Gearbox", "MileageLimitForDay", "MileageLimitForWeek", "Model", "PricePerDay", "PricePerWeek", "Year" },
                values: new object[] { 22, 1.8, true, "Toyota", 3, "White", "Sporty and agile", 3.0, "Automatic", 300.0, 2000.0, "Supra", 220.0, 1100.0, 2022 });

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 22,
                column: "Url",
                value: "/images/ford-mustang-1.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 23,
                column: "Url",
                value: "/images/ford-mustang-2.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 24,
                column: "Url",
                value: "/images/ford-mustang-3.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 25,
                column: "Url",
                value: "/images/ford-mustang-4.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 26,
                column: "Url",
                value: "/images/ford-mustang-5.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 27,
                column: "Url",
                value: "/images/ford-mustang-6.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 28,
                column: "Url",
                value: "/images/ford-mustang-7.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 29,
                column: "Url",
                value: "/images/ford-mustang-8.avif");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 152,
                column: "CarId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 153,
                column: "CarId",
                value: 21);

            migrationBuilder.InsertData(
                table: "CarsFeatures",
                columns: new[] { "Id", "CarId", "FeatureId" },
                values: new object[,]
                {
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
                values: new object[] { 30, 22, 5 });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CarId", "Url" },
                values: new object[,]
                {
                    { 154, 22, "/images/toyota-supra-1.webp" },
                    { 155, 22, "/images/toyota-supra-2.webp" },
                    { 156, 22, "/images/toyota-supra-3.webp" },
                    { 157, 22, "/images/toyota-supra-4.webp" },
                    { 158, 22, "/images/toyota-supra-5.webp" },
                    { 159, 22, "/images/toyota-supra-6.webp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "Color",
                value: "Black");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 22,
                column: "Url",
                value: "/images/ford-mustang1.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 23,
                column: "Url",
                value: "/images/ford-mustang2.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 24,
                column: "Url",
                value: "/images/ford-mustang3.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 25,
                column: "Url",
                value: "/images/ford-mustang4.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 26,
                column: "Url",
                value: "/images/ford-mustang5.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 27,
                column: "Url",
                value: "/images/ford-mustang6.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 28,
                column: "Url",
                value: "/images/ford-mustang7.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 29,
                column: "Url",
                value: "/images/ford-mustang8.jpg");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 152,
                column: "CarId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 153,
                column: "CarId",
                value: 12);
        }
    }
}
