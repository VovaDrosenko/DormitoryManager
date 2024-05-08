using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class addToUserDormFac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Faculties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Dormitories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DormId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacultyId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_AppUserId",
                table: "Faculties",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dormitories_AppUserId",
                table: "Dormitories",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dormitories_AspNetUsers_AppUserId",
                table: "Dormitories",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_AspNetUsers_AppUserId",
                table: "Faculties",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dormitories_AspNetUsers_AppUserId",
                table: "Dormitories");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_AspNetUsers_AppUserId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_AppUserId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Dormitories_AppUserId",
                table: "Dormitories");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Dormitories");

            migrationBuilder.DropColumn(
                name: "DormId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "AspNetUsers");
        }
    }
}
