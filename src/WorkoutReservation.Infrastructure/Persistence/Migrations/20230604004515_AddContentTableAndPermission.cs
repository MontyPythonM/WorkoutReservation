using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class AddContentTableAndPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Content",
                schema: "WorkoutReservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation",
                table: "Content",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Type", "Value" },
                values: new object[] { new Guid("66247d3d-7f51-4ee3-a25e-560abd516003"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, "HomePageHtml", "The home page is empty." });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 36, "CreateHomePageContent" });

            migrationBuilder.InsertData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                columns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                values: new object[] { 36, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content",
                schema: "WorkoutReservation");

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "RolesPermissions",
                keyColumns: new[] { "ApplicationPermissionId", "ApplicationRoleId" },
                keyValues: new object[] { 36, 1 });

            migrationBuilder.DeleteData(
                schema: "WorkoutReservation.Permissions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 36);
        }
    }
}
