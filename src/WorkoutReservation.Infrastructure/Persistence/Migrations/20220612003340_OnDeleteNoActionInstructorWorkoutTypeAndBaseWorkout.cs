using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class OnDeleteNoActionInstructorWorkoutTypeAndBaseWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTypeInstructors_Instructors_InstructorId",
                table: "WorkoutTypeInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTypeInstructors_WorkoutTypes_WorkoutTypeId",
                table: "WorkoutTypeInstructors");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTypeInstructors_Instructors_InstructorId",
                table: "WorkoutTypeInstructors",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTypeInstructors_WorkoutTypes_WorkoutTypeId",
                table: "WorkoutTypeInstructors",
                column: "WorkoutTypeId",
                principalTable: "WorkoutTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTypeInstructors_Instructors_InstructorId",
                table: "WorkoutTypeInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTypeInstructors_WorkoutTypes_WorkoutTypeId",
                table: "WorkoutTypeInstructors");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTypeInstructors_Instructors_InstructorId",
                table: "WorkoutTypeInstructors",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTypeInstructors_WorkoutTypes_WorkoutTypeId",
                table: "WorkoutTypeInstructors",
                column: "WorkoutTypeId",
                principalTable: "WorkoutTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
