using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.DataAccess.Migrations
{
    public partial class AddPerscribtionToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perscribtions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    MedicationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perscribtions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perscribtions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perscribtions_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perscribtions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perscribtions_DoctorId",
                table: "Perscribtions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Perscribtions_MedicationId",
                table: "Perscribtions",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Perscribtions_PatientId",
                table: "Perscribtions",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perscribtions");
        }
    }
}
