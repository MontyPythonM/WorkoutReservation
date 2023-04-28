using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddDateOfBirthInUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipianNumber",
                table: "Workouts",
                newName: "MaxParticipiantNumber");

            migrationBuilder.RenameColumn(
                name: "CurrentParticipianNumber",
                table: "Workouts",
                newName: "CurrentParticipiantNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MaxParticipiantNumber",
                table: "Workouts",
                newName: "MaxParticipianNumber");

            migrationBuilder.RenameColumn(
                name: "CurrentParticipiantNumber",
                table: "Workouts",
                newName: "CurrentParticipianNumber");
        }
    }
}
