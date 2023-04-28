using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class FixParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipiantNumber",
                table: "Workouts",
                newName: "MaxParticipantNumber");

            migrationBuilder.RenameColumn(
                name: "CurrentParticipiantNumber",
                table: "Workouts",
                newName: "CurrentParticipantNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipantNumber",
                table: "Workouts",
                newName: "MaxParticipiantNumber");

            migrationBuilder.RenameColumn(
                name: "CurrentParticipantNumber",
                table: "Workouts",
                newName: "CurrentParticipiantNumber");
        }
    }
}
