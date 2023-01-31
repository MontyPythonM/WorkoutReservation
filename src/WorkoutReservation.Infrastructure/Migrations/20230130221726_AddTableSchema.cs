using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddTableSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WorkoutReservation");

            migrationBuilder.RenameTable(
                name: "BaseWorkouts",
                newName: "BaseWorkouts",
                newSchema: "WorkoutReservation");
            
            migrationBuilder.RenameTable(
                name: "WorkoutTypeWorkoutTypeTag",
                newName: "WorkoutTypeWorkoutTypeTag",
                newSchema: "WorkoutReservation");

            migrationBuilder.RenameTable(
                name: "WorkoutTypeTags",
                newName: "WorkoutTypeTags",
                newSchema: "WorkoutReservation");

            migrationBuilder.RenameTable(
                name: "WorkoutTypes",
                newName: "WorkoutTypes",
                newSchema: "WorkoutReservation");

            migrationBuilder.RenameTable(
                name: "WorkoutTypeInstructors",
                newName: "WorkoutTypeInstructors",
                newSchema: "WorkoutReservation");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservations",
                newSchema: "WorkoutReservation");

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructors",
                newSchema: "WorkoutReservation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WorkoutTypeWorkoutTypeTag",
                schema: "WorkoutReservation",
                newName: "WorkoutTypeWorkoutTypeTag");

            migrationBuilder.RenameTable(
                name: "WorkoutTypeTags",
                schema: "WorkoutReservation",
                newName: "WorkoutTypeTags");

            migrationBuilder.RenameTable(
                name: "WorkoutTypes",
                schema: "WorkoutReservation",
                newName: "WorkoutTypes");

            migrationBuilder.RenameTable(
                name: "WorkoutTypeInstructors",
                schema: "WorkoutReservation",
                newName: "WorkoutTypeInstructors");

            migrationBuilder.RenameTable(
                name: "Reservations",
                schema: "WorkoutReservation",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "Instructors",
                schema: "WorkoutReservation",
                newName: "Instructors");

            migrationBuilder.RenameTable(
                name: "BaseWorkouts",
                schema: "WorkoutReservation",
                newName: "BaseWorkouts");
        }
    }
}
