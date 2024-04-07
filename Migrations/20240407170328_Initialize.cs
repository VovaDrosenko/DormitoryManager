using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Dormitory",
                columns: new[] { "DormitoryID", "DormitoryNumber" },
                values: new object[] { 1, 123 });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
