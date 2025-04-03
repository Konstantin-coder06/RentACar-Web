using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeCTYPEIDNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Types_CTypeId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CTypeId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Hatchback");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "SeatCapacity" },
                values: new object[] { "Coupe", 2 });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name", "SeatCapacity" },
                values: new object[,]
                {
                    { 7, "Coupe", 4 },
                    { 8, "Grand Coupe", 4 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Types_CTypeId",
                table: "Cars",
                column: "CTypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Types_CTypeId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<int>(
                name: "CTypeId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Coupe");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "SeatCapacity" },
                values: new object[] { "Grand Coupe", 4 });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Types_CTypeId",
                table: "Cars",
                column: "CTypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }
    }
}
