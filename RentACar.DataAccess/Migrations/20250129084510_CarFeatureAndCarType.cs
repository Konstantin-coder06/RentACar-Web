using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CarFeatureAndCarType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 373, 21, 25 }
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
                    { 28, 20, 2 },
                    { 29, 21, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "CarsFeatures",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
