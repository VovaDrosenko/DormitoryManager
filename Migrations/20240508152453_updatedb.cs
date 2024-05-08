using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_AspNetUsers_AppUserId",
                table: "Faculties");

            migrationBuilder.DropTable(
                name: "DormitoryFaculty");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_AppUserId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Faculties");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DormitoryId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId2",
                table: "FacultyWorkers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Dormitories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_AppUserId",
                table: "Students",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DormitoryId",
                table: "Students",
                column: "DormitoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkers_FacultyId2",
                table: "FacultyWorkers",
                column: "FacultyId2");

            migrationBuilder.CreateIndex(
                name: "IX_Dormitories_FacultyId",
                table: "Dormitories",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dormitories_Faculties_FacultyId",
                table: "Dormitories",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyWorkers_Students_FacultyId2",
                table: "FacultyWorkers",
                column: "FacultyId2",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_AppUserId",
                table: "Students",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Dormitories_DormitoryId",
                table: "Students",
                column: "DormitoryId",
                principalTable: "Dormitories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dormitories_Faculties_FacultyId",
                table: "Dormitories");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyWorkers_Students_FacultyId2",
                table: "FacultyWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_AppUserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Dormitories_DormitoryId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AppUserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DormitoryId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_FacultyWorkers_FacultyId2",
                table: "FacultyWorkers");

            migrationBuilder.DropIndex(
                name: "IX_Dormitories_FacultyId",
                table: "Dormitories");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DormitoryId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyId2",
                table: "FacultyWorkers");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Dormitories");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Faculties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DormitoryFaculty",
                columns: table => new
                {
                    DormsId = table.Column<int>(type: "int", nullable: false),
                    FacultiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DormitoryFaculty", x => new { x.DormsId, x.FacultiesId });
                    table.ForeignKey(
                        name: "FK_DormitoryFaculty_Dormitories_DormsId",
                        column: x => x.DormsId,
                        principalTable: "Dormitories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DormitoryFaculty_Faculties_FacultiesId",
                        column: x => x.FacultiesId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_AppUserId",
                table: "Faculties",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DormitoryFaculty_FacultiesId",
                table: "DormitoryFaculty",
                column: "FacultiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_AspNetUsers_AppUserId",
                table: "Faculties",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
