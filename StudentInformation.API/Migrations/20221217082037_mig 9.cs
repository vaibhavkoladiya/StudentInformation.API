using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentInformation.API.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Did = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    departmentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dphonenumber = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Did);
                });

            migrationBuilder.CreateTable(
                name: "allStudents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Did = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allStudents", x => x.id);
                    table.ForeignKey(
                        name: "FK_allStudents_departments_Did",
                        column: x => x.Did,
                        principalTable: "departments",
                        principalColumn: "Did",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "professors",
                columns: table => new
                {
                    Pid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coreSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Did = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professors", x => x.Pid);
                    table.ForeignKey(
                        name: "FK_professors_departments_Did",
                        column: x => x.Did,
                        principalTable: "departments",
                        principalColumn: "Did",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_allStudents_Did",
                table: "allStudents",
                column: "Did");

            migrationBuilder.CreateIndex(
                name: "IX_professors_Did",
                table: "professors",
                column: "Did");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "allStudents");

            migrationBuilder.DropTable(
                name: "professors");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
