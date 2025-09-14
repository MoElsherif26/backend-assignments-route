using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore01.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_Dep_Id",
                table: "Students",
                column: "Dep_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stud_Course_Course_ID",
                table: "Stud_Course",
                column: "Course_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Dept_ID",
                table: "Instructors",
                column: "Dept_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Inst_Course_ID",
                table: "Course_Inst",
                column: "Course_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Top_ID",
                table: "Course",
                column: "Top_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Topics_Top_ID",
                table: "Course",
                column: "Top_ID",
                principalTable: "Topics",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Inst_Course_Course_ID",
                table: "Course_Inst",
                column: "Course_ID",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Inst_Instructors_Inst_ID",
                table: "Course_Inst",
                column: "Inst_ID",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Department_Dept_ID",
                table: "Instructors",
                column: "Dept_ID",
                principalTable: "Department",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stud_Course_Course_Course_ID",
                table: "Stud_Course",
                column: "Course_ID",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stud_Course_Students_Stud_ID",
                table: "Stud_Course",
                column: "Stud_ID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Department_Dep_Id",
                table: "Students",
                column: "Dep_Id",
                principalTable: "Department",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Topics_Top_ID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Inst_Course_Course_ID",
                table: "Course_Inst");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Inst_Instructors_Inst_ID",
                table: "Course_Inst");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Department_Dept_ID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Stud_Course_Course_Course_ID",
                table: "Stud_Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Stud_Course_Students_Stud_ID",
                table: "Stud_Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Department_Dep_Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Dep_Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Stud_Course_Course_ID",
                table: "Stud_Course");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_Dept_ID",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Course_Inst_Course_ID",
                table: "Course_Inst");

            migrationBuilder.DropIndex(
                name: "IX_Course_Top_ID",
                table: "Course");
        }
    }
}
