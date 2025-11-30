//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace Online_Medical.Migrations
//{
//    /// <inheritdoc />
//    public partial class FinalClinicIntegerSetup : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_DoctorClinics_Doctors_DoctorId1",
//                table: "DoctorClinics");

//            migrationBuilder.DropForeignKey(
//                name: "FK_Reviews_Doctors_DoctorId1",
//                table: "Reviews");

//            migrationBuilder.DropIndex(
//                name: "IX_Reviews_DoctorId1",
//                table: "Reviews");

//            migrationBuilder.DropIndex(
//                name: "IX_DoctorClinics_DoctorId1",
//                table: "DoctorClinics");

//            migrationBuilder.DropColumn(
//                name: "DoctorId1",
//                table: "Reviews");

//            migrationBuilder.DropColumn(
//                name: "DoctorId1",
//                table: "DoctorClinics");

//            migrationBuilder.AlterColumn<string>(
//                name: "DoctorId",
//                table: "Reviews",
//                type: "nvarchar(450)",
//                nullable: false,
//                oldClrType: typeof(int),
//                oldType: "int");

//            migrationBuilder.AlterColumn<string>(
//                name: "DoctorId",
//                table: "DoctorClinics",
//                type: "nvarchar(450)",
//                nullable: false,
//                oldClrType: typeof(int),
//                oldType: "int");

//            migrationBuilder.CreateIndex(
//                name: "IX_Reviews_DoctorId",
//                table: "Reviews",
//                column: "DoctorId");

//            migrationBuilder.AddForeignKey(
//                name: "FK_DoctorClinics_Doctors_DoctorId",
//                table: "DoctorClinics",
//                column: "DoctorId",
//                principalTable: "Doctors",
//                principalColumn: "Id",
//                onDelete: ReferentialAction.Cascade);

//            migrationBuilder.AddForeignKey(
//                name: "FK_Reviews_Doctors_DoctorId",
//                table: "Reviews",
//                column: "DoctorId",
//                principalTable: "Doctors",
//                principalColumn: "Id",
//                onDelete: ReferentialAction.Cascade);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_DoctorClinics_Doctors_DoctorId",
//                table: "DoctorClinics");

//            migrationBuilder.DropForeignKey(
//                name: "FK_Reviews_Doctors_DoctorId",
//                table: "Reviews");

//            migrationBuilder.DropIndex(
//                name: "IX_Reviews_DoctorId",
//                table: "Reviews");

//            migrationBuilder.AlterColumn<int>(
//                name: "DoctorId",
//                table: "Reviews",
//                type: "int",
//                nullable: false,
//                oldClrType: typeof(string),
//                oldType: "nvarchar(450)");

//            migrationBuilder.AddColumn<string>(
//                name: "DoctorId1",
//                table: "Reviews",
//                type: "nvarchar(450)",
//                nullable: false,
//                defaultValue: "");

//            migrationBuilder.AlterColumn<int>(
//                name: "DoctorId",
//                table: "DoctorClinics",
//                type: "int",
//                nullable: false,
//                oldClrType: typeof(string),
//                oldType: "nvarchar(450)");

//            migrationBuilder.AddColumn<string>(
//                name: "DoctorId1",
//                table: "DoctorClinics",
//                type: "nvarchar(450)",
//                nullable: true);

//            migrationBuilder.CreateIndex(
//                name: "IX_Reviews_DoctorId1",
//                table: "Reviews",
//                column: "DoctorId1");

//            migrationBuilder.CreateIndex(
//                name: "IX_DoctorClinics_DoctorId1",
//                table: "DoctorClinics",
//                column: "DoctorId1");

//            migrationBuilder.AddForeignKey(
//                name: "FK_DoctorClinics_Doctors_DoctorId1",
//                table: "DoctorClinics",
//                column: "DoctorId1",
//                principalTable: "Doctors",
//                principalColumn: "Id");

//            migrationBuilder.AddForeignKey(
//                name: "FK_Reviews_Doctors_DoctorId1",
//                table: "Reviews",
//                column: "DoctorId1",
//                principalTable: "Doctors",
//                principalColumn: "Id",
//                onDelete: ReferentialAction.Cascade);
//        }
//    }
//}
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Medical.Migrations
{
    // تأكدي أن اسم الكلاس الجزئي (partial class) مطابق لاسم الملف لديك
    public partial class FinalClinicIntegerSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 🛑 1. حذف المفتاح الأساسي المركب الحالي (لتحرير عمود DoctorId)
            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorClinics",
                table: "DoctorClinics");

            // ... (باقي كود حذف المفاتيح الخارجية والأعمدة المتضاربة) ...
            migrationBuilder.DropForeignKey(name: "FK_DoctorClinics_Doctors_DoctorId1", table: "DoctorClinics");
            migrationBuilder.DropForeignKey(name: "FK_Reviews_Doctors_DoctorId1", table: "Reviews");
            migrationBuilder.DropIndex(name: "IX_Reviews_DoctorId1", table: "Reviews");
            migrationBuilder.DropIndex(name: "IX_DoctorClinics_DoctorId1", table: "DoctorClinics");
            migrationBuilder.DropColumn(name: "DoctorId1", table: "Reviews");
            migrationBuilder.DropColumn(name: "DoctorId1", table: "DoctorClinics");

            // 🛑 2. تعديل نوع عمود DoctorId في جدول Reviews (من int إلى string)
            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // 🛑 3. تعديل نوع عمود DoctorId في جدول DoctorClinics (من int إلى string)
            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "DoctorClinics",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // 🛑 4. إعادة إنشاء المفتاح الأساسي المركب باستخدام النوع الجديد (nvarchar/string)
            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorClinics",
                table: "DoctorClinics",
                columns: new[] { "DoctorId", "ClinicId" });

            // 5. إعادة إنشاء الـIndexes والمفاتيح الخارجية
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DoctorId",
                table: "Reviews",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinics_Doctors_DoctorId",
                table: "DoctorClinics",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // هذا الجزء فقط لعكس العملية في حال احتجتِ لذلك
            // 🛑 1. حذف المفتاح الأساسي المركب الحالي
            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorClinics",
                table: "DoctorClinics");

            migrationBuilder.DropForeignKey(name: "FK_DoctorClinics_Doctors_DoctorId", table: "DoctorClinics");
            migrationBuilder.DropForeignKey(name: "FK_Reviews_Doctors_DoctorId", table: "Reviews");
            migrationBuilder.DropIndex(name: "IX_Reviews_DoctorId", table: "Reviews");

            // 🛑 2. تعديل نوع عمود DoctorId في DoctorClinics للعودة إلى int
            migrationBuilder.AlterColumn<int>(name: "DoctorId", table: "DoctorClinics", type: "int", nullable: false, oldClrType: typeof(string), oldType: "nvarchar(450)");

            // 🛑 3. تعديل نوع عمود DoctorId في Reviews للعودة إلى int
            migrationBuilder.AlterColumn<int>(name: "DoctorId", table: "Reviews", type: "int", nullable: false, oldClrType: typeof(string), oldType: "nvarchar(450)");

            // 4. إعادة إنشاء المفتاح المركب القديم
            migrationBuilder.AddPrimaryKey(name: "PK_DoctorClinics", table: "DoctorClinics", columns: new[] { "DoctorId", "ClinicId" });

            // 5. إعادة الأعمدة والمفاتيح القديمة
            migrationBuilder.AddColumn<string>(name: "DoctorId1", table: "Reviews", type: "nvarchar(450)", nullable: false, defaultValue: "");
            migrationBuilder.AddColumn<string>(name: "DoctorId1", table: "DoctorClinics", type: "nvarchar(450)", nullable: true);

            migrationBuilder.CreateIndex(name: "IX_Reviews_DoctorId1", table: "Reviews", column: "DoctorId1");
            migrationBuilder.CreateIndex(name: "IX_DoctorClinics_DoctorId1", table: "DoctorClinics", column: "DoctorId1");

            migrationBuilder.AddForeignKey(name: "FK_DoctorClinics_Doctors_DoctorId1", table: "DoctorClinics", column: "DoctorId1", principalTable: "Doctors", principalColumn: "Id");
            migrationBuilder.AddForeignKey(name: "FK_Reviews_Doctors_DoctorId1", table: "Reviews", column: "DoctorId1", principalTable: "Doctors", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
        }
    }
}