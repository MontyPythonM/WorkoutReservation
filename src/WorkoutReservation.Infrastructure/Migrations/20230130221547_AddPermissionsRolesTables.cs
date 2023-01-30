using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddPermissionsRolesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Workouts_RealWorkoutId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Instructors_InstructorId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_WorkoutTypes_WorkoutTypeId",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.EnsureSchema(
                name: "WorkoutReservation.Permissions");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "WorkoutReservation.Permissions");

            migrationBuilder.RenameTable(
                name: "Workouts",
                newName: "BaseWorkouts");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_WorkoutTypeId",
                table: "BaseWorkouts",
                newName: "IX_BaseWorkouts_WorkoutTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_InstructorId",
                table: "BaseWorkouts",
                newName: "IX_BaseWorkouts_InstructorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseWorkouts",
                table: "BaseWorkouts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "WorkoutReservation.Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "WorkoutReservation.Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                schema: "WorkoutReservation.Permissions",
                columns: table => new
                {
                    ApplicationRoleId = table.Column<int>(type: "int", nullable: false),
                    ApplicationPermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.ApplicationRoleId, x.ApplicationPermissionId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_ApplicationPermissionId",
                        column: x => x.ApplicationPermissionId,
                        principalSchema: "WorkoutReservation.Permissions",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalSchema: "WorkoutReservation.Permissions",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                schema: "WorkoutReservation.Permissions",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.ApplicationUserId, x.ApplicationRoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalSchema: "WorkoutReservation.Permissions",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "WorkoutReservation.Permissions",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CreateInstructor" },
                    { 2, "UpdateInstructor" },
                    { 3, "DeleteInstructor" },
                    { 4, "CreateWorkoutType" },
                    { 5, "UpdateWorkoutType" },
                    { 6, "DeleteWorkoutType" },
                    { 7, "GetAllWorkoutTypeTags" },
                    { 8, "CreateWorkoutTypeTag" },
                    { 9, "UpdateWorkoutTypeTag" },
                    { 10, "DeleteWorkoutTypeTag" },
                    { 11, "GetRealWorkoutDetails" },
                    { 12, "CreateRealWorkout" },
                    { 13, "UpdateRealWorkout" },
                    { 14, "DeleteRealWorkout" },
                    { 15, "GetRepetitiveWorkouts" },
                    { 16, "CreateRepetitiveWorkout" },
                    { 17, "UpdateRepetitiveWorkout" },
                    { 18, "DeleteRepetitiveWorkout" },
                    { 19, "DeleteAllRepetitiveWorkouts" },
                    { 20, "GenerateNewUpcomingWeek" },
                    { 21, "OpenHangfireDashboard" },
                    { 22, "GetOwnReservations" },
                    { 23, "GetSomeoneReservations" },
                    { 24, "CreateReservation" },
                    { 25, "CancelReservation" },
                    { 26, "UpdateReservationStatus" },
                    { 27, "GetAllUsers" },
                    { 28, "SetUserRole" },
                    { 29, "DeleteUserAccount" },
                    { 30, "DeleteOwnAccount" }
                });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SystemAdministrator" },
                    { 2, "BusinessAdministrator" },
                    { 3, "Manager" },
                    { 4, "Member" }
                });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 },
                    { 24, 1 },
                    { 25, 1 },
                    { 26, 1 },
                    { 27, 1 },
                    { 28, 1 },
                    { 29, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 9, 2 },
                    { 10, 2 },
                    { 11, 2 },
                    { 12, 2 },
                    { 13, 2 }
                });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[,]
                {
                    { 14, 2 },
                    { 15, 2 },
                    { 16, 2 },
                    { 17, 2 },
                    { 18, 2 },
                    { 19, 2 },
                    { 21, 2 },
                    { 23, 2 },
                    { 26, 2 },
                    { 27, 2 },
                    { 28, 2 },
                    { 30, 2 },
                    { 7, 3 },
                    { 11, 3 },
                    { 12, 3 },
                    { 13, 3 },
                    { 14, 3 },
                    { 15, 3 },
                    { 22, 3 },
                    { 23, 3 },
                    { 24, 3 },
                    { 25, 3 },
                    { 26, 3 },
                    { 27, 3 },
                    { 30, 3 },
                    { 11, 4 },
                    { 22, 4 },
                    { 24, 4 },
                    { 25, 4 },
                    { 30, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "WorkoutReservation.Permissions",
                table: "Roles",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_ApplicationPermissionId",
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                column: "ApplicationPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_ApplicationRoleId",
                schema: "WorkoutReservation.Permissions",
                table: "UsersRoles",
                column: "ApplicationRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWorkouts_Instructors_InstructorId",
                table: "BaseWorkouts",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWorkouts_WorkoutTypes_WorkoutTypeId",
                table: "BaseWorkouts",
                column: "WorkoutTypeId",
                principalTable: "WorkoutTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BaseWorkouts_RealWorkoutId",
                table: "Reservations",
                column: "RealWorkoutId",
                principalTable: "BaseWorkouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseWorkouts_Instructors_InstructorId",
                table: "BaseWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseWorkouts_WorkoutTypes_WorkoutTypeId",
                table: "BaseWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BaseWorkouts_RealWorkoutId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "RolesPermissions",
                schema: "WorkoutReservation.Permissions");

            migrationBuilder.DropTable(
                name: "UsersRoles",
                schema: "WorkoutReservation.Permissions");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "WorkoutReservation.Permissions");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "WorkoutReservation.Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseWorkouts",
                table: "BaseWorkouts");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "WorkoutReservation.Permissions",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "BaseWorkouts",
                newName: "Workouts");

            migrationBuilder.RenameIndex(
                name: "IX_BaseWorkouts_WorkoutTypeId",
                table: "Workouts",
                newName: "IX_Workouts_WorkoutTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseWorkouts_InstructorId",
                table: "Workouts",
                newName: "IX_Workouts_InstructorId");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Workouts_RealWorkoutId",
                table: "Reservations",
                column: "RealWorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Instructors_InstructorId",
                table: "Workouts",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_WorkoutTypes_WorkoutTypeId",
                table: "Workouts",
                column: "WorkoutTypeId",
                principalTable: "WorkoutTypes",
                principalColumn: "Id");
        }
    }
}
