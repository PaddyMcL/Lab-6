using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class MvcData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DepartmentName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LecturerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CourseId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "ID", "DepartmentName", "LecturerName", "Name" },
                values: new object[,]
                {
                    { 1, "Computer Science", "Dr. Murphy", "Advanced Programming" },
                    { 2, "Computer Science", "Prof. O'Brien", "Data Structures" },
                    { 3, "Computer Science", "Dr. Kelly", "Web Technologies" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "Age", "EmailAddress", "Name" },
                values: new object[,]
                {
                    { 1, 21, "alice.j@atu.ie", "Alice Johnson" },
                    { 2, 23, "bob.w@atu.ie", "Bob Wilson" },
                    { 3, 20, "charlie.b@atu.ie", "Charlie Brown" },
                    { 4, 22, "diana.p@atu.ie", "Diana Prince" }
                });

            migrationBuilder.InsertData(
                table: "CourseStudent",
                columns: new[] { "CourseId", "StudentId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 2 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentId",
                table: "CourseStudent",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
