using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddReservationNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                schema: "WorkoutReservation",
                table: "Reservations",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "Name",
                value: "UpdateReservation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                schema: "WorkoutReservation",
                table: "Reservations");

            migrationBuilder.UpdateData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "Name",
                value: "UpdateReservationStatus");
        }
    }
}
