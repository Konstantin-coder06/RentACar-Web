using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriveTrain",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HorsePower",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "OneToHundred",
                table: "Cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TopSpeed",
                table: "Cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Front", 1.6000000000000001, 122, 10.800000000000001, 185.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Front", 1.5, 205, 7.2999999999999998, 170.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 2.2999999999999998, 317, 5.7999999999999998, 250.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "Model", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 2.0, 190, "420", 7.0999999999999996, 240.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 258, 6.2000000000000002, 250.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 570, 3.7999999999999998, 327.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 640, 3.0, 310.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 3.0, 450, 3.7999999999999998, 304.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 1020, 2.1000000000000001, 322.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 720, 2.8999999999999999, 340.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AdditionalMileageCharge", "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { 2.0, "Rear", 6.7999999999999998, 571, 5.4000000000000004, 250.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 6.0, 659, 3.7000000000000002, 335.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 720, 2.8999999999999999, 341.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 550, 4.5, 291.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 3.5, 415, 6.7999999999999998, 210.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 730, 3.2000000000000002, 322.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 3.0, 340, 5.9000000000000004, 250.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 510, 3.7000000000000002, 250.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 182, 9.9000000000000004, 210.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 845, 2.7000000000000002, 210.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DriveTrain", "EngineCapacity", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Full Wheels", 3.0, 360, 6.9000000000000004, 209.0 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DriveTrain", "HorsePower", "OneToHundred", "TopSpeed" },
                values: new object[] { "Rear", 340, 4.4000000000000004, 262.0 });

            migrationBuilder.UpdateData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 28,
                column: "TypeId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 22,
                column: "Url",
                value: "/images/ford-mustang-1.webp");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 23,
                column: "Url",
                value: "/images/ford-mustang-2.webp");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 24,
                column: "Url",
                value: "/images/ford-mustang-3.webp");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 25,
                column: "Url",
                value: "/images/ford-mustang-4.webp");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 26,
                column: "Url",
                value: "/images/ford-mustang-5.webp");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 27,
                column: "Url",
                value: "/images/ford-mustang-6.webp");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 28,
                column: "Url",
                value: "/images/ford-mustang-7.webp");

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 29,
                column: "Url",
                value: "/images/ford-mustang-8.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriveTrain",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "HorsePower",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "OneToHundred",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TopSpeed",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "EngineCapacity",
                value: 1.8);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "EngineCapacity",
                value: 2.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "EngineCapacity",
                value: 5.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EngineCapacity", "Model" },
                values: new object[] { 0.0, "420i" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "EngineCapacity",
                value: 4.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AdditionalMileageCharge", "EngineCapacity" },
                values: new object[] { 3.0, 6.7000000000000002 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                column: "EngineCapacity",
                value: 4.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                column: "EngineCapacity",
                value: 3.3999999999999999);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17,
                column: "EngineCapacity",
                value: 4.0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 21,
                column: "EngineCapacity",
                value: 4.4000000000000004);

            migrationBuilder.UpdateData(
                table: "CarsTypes",
                keyColumn: "Id",
                keyValue: 28,
                column: "TypeId",
                value: 2);

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
        }
    }
}
