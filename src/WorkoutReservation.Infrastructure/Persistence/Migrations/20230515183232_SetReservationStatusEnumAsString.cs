using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class SetReservationStatusEnumAsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReservationStatus",
                schema: "WorkoutReservation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql(
                "UPDATE [WorkoutReservation].[WorkoutReservation].[Reservations] SET ReservationStatus = 'Reserved' WHERE ReservationStatus = '1'");
            
            migrationBuilder.Sql(
                "UPDATE [WorkoutReservation].[WorkoutReservation].[Reservations] SET ReservationStatus = 'Cancelled' WHERE ReservationStatus = '2'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatus",
                schema: "WorkoutReservation",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
            
            migrationBuilder.Sql(
                "UPDATE [WorkoutReservation].[WorkoutReservation].[Reservations] SET ReservationStatus = 1 WHERE ReservationStatus = 'Reserved'");
            
            migrationBuilder.Sql(
                "UPDATE [WorkoutReservation].[WorkoutReservation].[Reservations] SET ReservationStatus = 2 WHERE ReservationStatus = 'Cancelled'");
        }
    }
}
