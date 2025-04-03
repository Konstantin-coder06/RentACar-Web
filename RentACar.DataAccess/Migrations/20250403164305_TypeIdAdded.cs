using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TypeIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CTypeId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConvertable",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CTypeId", "IsConvertable" },
                values: new object[] { null, false });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CTypeId",
                table: "Cars",
                column: "CTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Types_CTypeId",
                table: "Cars",
                column: "CTypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Types_CTypeId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsConvertable",
                table: "Cars");
        }
    }
}
