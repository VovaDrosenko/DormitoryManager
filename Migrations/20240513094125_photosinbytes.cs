using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class photosinbytes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Students",
                newName: "PhotoInByte");

            migrationBuilder.RenameColumn(
                name: "ApplicationScan",
                table: "Students",
                newName: "ApplicationScanInByte");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoInByte",
                table: "Students",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "ApplicationScanInByte",
                table: "Students",
                newName: "ApplicationScan");
        }
    }
}
