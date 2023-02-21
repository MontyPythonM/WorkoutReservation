using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddGetReservationDetailsPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 33, "GetOwnReservationDetails" });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 34, "GetSomeoneReservationDetails" });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[,]
                {
                    { 33, 1 },
                    { 34, 1 },
                    { 34, 2 },
                    { 33, 3 },
                    { 34, 3 },
                    { 33, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 33, 1 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 34, 1 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 34, 2 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 33, 3 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 34, 3 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 33, 4 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 34);
        }
    }
}
