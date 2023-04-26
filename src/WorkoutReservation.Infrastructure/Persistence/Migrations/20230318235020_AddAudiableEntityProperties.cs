using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddAudiableEntityProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseWorkouts_Instructors_InstructorId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseWorkouts_WorkoutTypes_WorkoutTypeId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts");

            migrationBuilder.RenameColumn(
                name: "AccountCreationDate",
                schema: "WorkoutReservation.Permissions",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                schema: "WorkoutReservation",
                table: "Reservations",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                schema: "WorkoutReservation",
                table: "Reservations",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "WorkoutTypeTags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "WorkoutReservation",
                table: "WorkoutTypeTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WorkoutReservation",
                table: "WorkoutTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "WorkoutReservation",
                table: "WorkoutTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "WorkoutTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "WorkoutReservation",
                table: "WorkoutTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WorkoutReservation.Permissions",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "WorkoutReservation.Permissions",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "WorkoutReservation.Permissions",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WorkoutReservation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "WorkoutReservation",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "WorkoutReservation",
                table: "Instructors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "WorkoutReservation",
                table: "Instructors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutTypeId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWorkouts_Instructors_InstructorId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                column: "InstructorId",
                principalSchema: "WorkoutReservation",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWorkouts_WorkoutTypes_WorkoutTypeId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                column: "WorkoutTypeId",
                principalSchema: "WorkoutReservation",
                principalTable: "WorkoutTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseWorkouts_Instructors_InstructorId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseWorkouts_WorkoutTypes_WorkoutTypeId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "WorkoutTypeTags");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "WorkoutReservation",
                table: "WorkoutTypeTags");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WorkoutReservation",
                table: "WorkoutTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "WorkoutReservation",
                table: "WorkoutTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "WorkoutTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "WorkoutReservation",
                table: "WorkoutTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WorkoutReservation.Permissions",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "WorkoutReservation.Permissions",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "WorkoutReservation.Permissions",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WorkoutReservation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "WorkoutReservation",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "WorkoutReservation",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "WorkoutReservation",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "WorkoutReservation",
                table: "Instructors");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "WorkoutReservation.Permissions",
                table: "Users",
                newName: "AccountCreationDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                schema: "WorkoutReservation",
                table: "Reservations",
                newName: "LastModificationDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "WorkoutReservation",
                table: "Reservations",
                newName: "CreationDate");

            migrationBuilder.AlterColumn<int>(
                name: "WorkoutTypeId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWorkouts_Instructors_InstructorId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                column: "InstructorId",
                principalSchema: "WorkoutReservation",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWorkouts_WorkoutTypes_WorkoutTypeId",
                schema: "WorkoutReservation",
                table: "BaseWorkouts",
                column: "WorkoutTypeId",
                principalSchema: "WorkoutReservation",
                principalTable: "WorkoutTypes",
                principalColumn: "Id");
        }
    }
}
