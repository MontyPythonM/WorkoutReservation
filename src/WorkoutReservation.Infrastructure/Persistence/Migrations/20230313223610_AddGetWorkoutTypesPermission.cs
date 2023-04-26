using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddGetWorkoutTypesPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 14, 3 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 35, "GetWorkoutTypes" });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 35, 1 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 35, 2 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 35, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 35, 1 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 35, 2 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 35, 3 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 12, 3 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 14, 3 });
        }
    }
}
