using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dormitories",
                columns: new[] { "DormitoryID", "DormitoryNumber" },
                values: new object[] { 1, 7 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomID", "DormitoryID", "NumberOfBeds", "RoomNumber" },
                values: new object[] { 1, 1, 1, 548 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "RoomID", "RoomID1", "UserFirstName", "UserLastName", "UserMiddleName" },
                values: new object[] { 1, 1, null, "Volodymyr", "Drosenko", "Igorovich" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dormitories",
                keyColumn: "DormitoryID",
                keyValue: 1);
        }
    }
}
