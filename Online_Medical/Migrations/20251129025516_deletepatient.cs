using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Medical.Migrations
{
    /// <inheritdoc />
    public partial class deletepatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Patients_PatientId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PatientId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PatientId",
                table: "Reviews",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Patients_PatientId",
                table: "Reviews",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Patients_PatientId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PatientId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PatientId1",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PatientId1",
                table: "Reviews",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Patients_PatientId1",
                table: "Reviews",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
