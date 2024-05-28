using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.DataAccess.Migrations
{
    public partial class AddOnCallsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnCalls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(nullable: false),
                    STime = table.Column<DateTime>(nullable: false),
                    ETime = table.Column<DateTime>(nullable: false),
                    NurseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnCalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnCalls_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnCalls_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnCalls_NurseId",
                table: "OnCalls",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_OnCalls_RoomId",
                table: "OnCalls",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnCalls");
        }
    }
}
