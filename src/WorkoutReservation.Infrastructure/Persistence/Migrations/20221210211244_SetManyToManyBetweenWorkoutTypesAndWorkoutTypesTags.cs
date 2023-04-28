using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservation.Infrastructure.Migrations
{
    public partial class SetManyToManyBetweenWorkoutTypesAndWorkoutTypesTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[WorkoutTypeTags]");
            
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTypeTags_WorkoutTypes_WorkoutTypeId",
                table: "WorkoutTypeTags");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutTypeTags_WorkoutTypeId",
                table: "WorkoutTypeTags");

            migrationBuilder.DropColumn(
                name: "WorkoutTypeId",
                table: "WorkoutTypeTags");

            migrationBuilder.CreateTable(
                name: "WorkoutTypeWorkoutTypeTag",
                columns: table => new
                {
                    WorkoutTypeTagsId = table.Column<int>(type: "int", nullable: false),
                    WorkoutTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTypeWorkoutTypeTag", x => new { x.WorkoutTypeTagsId, x.WorkoutTypesId });
                    table.ForeignKey(
                        name: "FK_WorkoutTypeWorkoutTypeTag_WorkoutTypes_WorkoutTypesId",
                        column: x => x.WorkoutTypesId,
                        principalTable: "WorkoutTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutTypeWorkoutTypeTag_WorkoutTypeTags_WorkoutTypeTagsId",
                        column: x => x.WorkoutTypeTagsId,
                        principalTable: "WorkoutTypeTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTypeWorkoutTypeTag_WorkoutTypesId",
                table: "WorkoutTypeWorkoutTypeTag",
                column: "WorkoutTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutTypeWorkoutTypeTag");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutTypeId",
                table: "WorkoutTypeTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTypeTags_WorkoutTypeId",
                table: "WorkoutTypeTags",
                column: "WorkoutTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTypeTags_WorkoutTypes_WorkoutTypeId",
                table: "WorkoutTypeTags",
                column: "WorkoutTypeId",
                principalTable: "WorkoutTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
