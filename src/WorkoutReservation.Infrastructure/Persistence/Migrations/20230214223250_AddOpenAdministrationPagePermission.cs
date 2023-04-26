using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddOpenAdministrationPagePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 31, "OpenAdministrationPage" });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 31, 1 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 31, 2 });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 31, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 31, 1 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 31, 2 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 31, 3 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 31);
        }
    }
}
