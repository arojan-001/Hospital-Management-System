using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.DataAccess.Migrations
{
    public partial class AddRoomToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNo = table.Column<int>(nullable: false),
                    RNo = table.Column<int>(nullable: false),
                    RType = table.Column<string>(nullable: false),
                    Availability = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
