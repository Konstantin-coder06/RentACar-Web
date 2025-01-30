using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditTableCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Cars",
                newName: "ClassOfCarId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                newName: "IX_Cars_ClassOfCarId");

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

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClassOfCarId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClassOfCarId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClassOfCarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClassOfCarId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                column: "ClassOfCarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                column: "ClassOfCarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14,
                column: "ClassOfCarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                column: "ClassOfCarId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17,
                column: "ClassOfCarId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19,
                column: "ClassOfCarId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20,
                column: "ClassOfCarId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 21,
                column: "ClassOfCarId",
                value: 4);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ClassOfCars_ClassOfCarId",
                table: "Cars",
                column: "ClassOfCarId",
                principalTable: "ClassOfCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ClassOfCars_ClassOfCarId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "ClassOfCars");

            migrationBuilder.RenameColumn(
                name: "ClassOfCarId",
                table: "Cars",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ClassOfCarId",
                table: "Cars",
                newName: "IX_Cars_CategoryId");

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

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14,
                column: "CategoryId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                column: "CategoryId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17,
                column: "CategoryId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19,
                column: "CategoryId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20,
                column: "CategoryId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 21,
                column: "CategoryId",
                value: 6);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
