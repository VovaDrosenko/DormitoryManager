using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomID1",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoomID1",
                table: "Users",
                column: "RoomID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_RoomID1",
                table: "Users",
                column: "RoomID1",
                principalTable: "Rooms",
                principalColumn: "RoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomID1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoomID1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoomID1",
                table: "Users");
        }
    }
}
