using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.DataAccess.Migrations
{
    public partial class AddUndergoesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Undergoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    NurseId = table.Column<int>(nullable: false),
                    UsageId = table.Column<int>(nullable: true),
                    ProcedureId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Undergoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Undergoes_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Undergoes_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Undergoes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Undergoes_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Undergoes_Usage_UsageId",
                        column: x => x.UsageId,
                        principalTable: "Usage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Undergoes_DoctorId",
                table: "Undergoes",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Undergoes_NurseId",
                table: "Undergoes",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_Undergoes_PatientId",
                table: "Undergoes",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Undergoes_ProcedureId",
                table: "Undergoes",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Undergoes_UsageId",
                table: "Undergoes",
                column: "UsageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Undergoes");
        }
    }
}
