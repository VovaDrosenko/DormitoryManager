using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManager.Migrations
{
    /// <inheritdoc />
    public partial class updateIdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DormitoryComendants");

            migrationBuilder.DropTable(
                name: "DormitoryFaculty");

            migrationBuilder.DropTable(
                name: "FacultyWorkers");

            migrationBuilder.DropTable(
                name: "StudentRooms");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Dormitories");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Dormitories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DormNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floors = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dormitories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dormitories_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacultyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculties_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DormitoryComendants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    DormId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DormId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DormitoryComendants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DormitoryComendants_Dormitories_DormId1",
                        column: x => x.DormId1,
                        principalTable: "Dormitories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DormitoryComendants_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DormId = table.Column<int>(type: "int", nullable: false),
                    NumberOfRoom = table.Column<int>(type: "int", nullable: true),
                    NumberOfBeds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidentsGender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Dormitories_DormId",
                        column: x => x.DormId,
                        principalTable: "Dormitories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "FacultyWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    FacultyId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyWorkers_Faculties_FacultyId1",
                        column: x => x.FacultyId1,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FacultyWorkers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentMiddlename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settlement = table.Column<bool>(type: "bit", nullable: false),
                    Course = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    DormId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DateBegin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentRooms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_Dormitories_AppUserId",
                table: "Dormitories",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DormitoryComendants_DormId1",
                table: "DormitoryComendants",
                column: "DormId1");

            migrationBuilder.CreateIndex(
                name: "IX_DormitoryComendants_WorkerId",
                table: "DormitoryComendants",
                column: "WorkerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DormitoryFaculty_FacultiesId",
                table: "DormitoryFaculty",
                column: "FacultiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_AppUserId",
                table: "Faculties",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkers_FacultyId1",
                table: "FacultyWorkers",
                column: "FacultyId1");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyWorkers_WorkerId",
                table: "FacultyWorkers",
                column: "WorkerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DormId",
                table: "Rooms",
                column: "DormId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRooms_RoomId",
                table: "StudentRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRooms_StudentId",
                table: "StudentRooms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "DormitoryComendants");

            migrationBuilder.DropTable(
                name: "DormitoryFaculty");

            migrationBuilder.DropTable(
                name: "FacultyWorkers");

            migrationBuilder.DropTable(
                name: "StudentRooms");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Dormitories");

            migrationBuilder.DropTable(
                name: "Faculties");

        }
    }
}
