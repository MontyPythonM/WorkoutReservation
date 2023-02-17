using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddCanSeeAdministrativeContentPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 32, "CanSeeAdministrativeContent" });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 32, 1 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 32, 2 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 32, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 32, 1 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 32, 2 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 32, 3 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 32);
        }
    }
}
