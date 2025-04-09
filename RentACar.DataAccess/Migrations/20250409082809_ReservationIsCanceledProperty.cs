using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReservationIsCanceledProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Reservations");
        }
    }
}
