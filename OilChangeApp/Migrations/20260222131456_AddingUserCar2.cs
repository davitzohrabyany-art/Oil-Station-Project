using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserCar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "car_id",
                table: "Car",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "User_Car",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Car_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Car", x => new { x.User_id, x.Car_id });
                    table.ForeignKey(
                        name: "FK_User_Car_Car_Car_id",
                        column: x => x.Car_id,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Car_User_User_id",
                        column: x => x.User_id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_User_Car_Car_id",
                table: "User_Car",
                column: "Car_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Car");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Car",
                newName: "car_id");
        }
    }
}
