using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class ModifyPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "GetAllReservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "GetSomeoneReservations");
        }
    }
}
