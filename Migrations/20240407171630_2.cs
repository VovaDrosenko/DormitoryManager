using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Dormitory_DormitoryID",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Dormitory_DormitoryID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Room_RoomID",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dormitory",
                table: "Dormitory");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Dormitory",
                newName: "Dormitorys");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoomID",
                table: "Users",
                newName: "IX_Users_RoomID");

            migrationBuilder.RenameIndex(
                name: "IX_User_DormitoryID",
                table: "Users",
                newName: "IX_Users_DormitoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Room_DormitoryID",
                table: "Rooms",
                newName: "IX_Rooms_DormitoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "RoomID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dormitorys",
                table: "Dormitorys",
                column: "DormitoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Dormitorys_DormitoryID",
                table: "Rooms",
                column: "DormitoryID",
                principalTable: "Dormitorys",
                principalColumn: "DormitoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Dormitorys_DormitoryID",
                table: "Users",
                column: "DormitoryID",
                principalTable: "Dormitorys",
                principalColumn: "DormitoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rooms_RoomID",
                table: "Users",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Dormitorys_DormitoryID",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Dormitorys_DormitoryID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dormitorys",
                table: "Dormitorys");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "Dormitorys",
                newName: "Dormitory");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoomID",
                table: "User",
                newName: "IX_User_RoomID");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DormitoryID",
                table: "User",
                newName: "IX_User_DormitoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_DormitoryID",
                table: "Room",
                newName: "IX_Room_DormitoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "RoomID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dormitory",
                table: "Dormitory",
                column: "DormitoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Dormitory_DormitoryID",
                table: "Room",
                column: "DormitoryID",
                principalTable: "Dormitory",
                principalColumn: "DormitoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Dormitory_DormitoryID",
                table: "User",
                column: "DormitoryID",
                principalTable: "Dormitory",
                principalColumn: "DormitoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Room_RoomID",
                table: "User",
                column: "RoomID",
                principalTable: "Room",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
