using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    car_num = table.Column<int>(type: "int", nullable: false),
                    car_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    oil_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.car_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Telegram_id = table.Column<long>(type: "bigint", nullable: false),
                    Phone_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Service_visits",
                columns: table => new
                {
                    Service_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Car_id = table.Column<int>(type: "int", nullable: false),
                    Visit_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_visits", x => x.Service_id);
                    table.ForeignKey(
                        name: "FK_Service_visits_Cars_Car_id",
                        column: x => x.Car_id,
                        principalTable: "Cars",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Oil_changes",
                columns: table => new
                {
                    Oil_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Service_id = table.Column<long>(type: "bigint", nullable: false),
                    Oil_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Oil_liters = table.Column<long>(type: "bigint", nullable: false),
                    Next_change_km = table.Column<long>(type: "bigint", nullable: false),
                    Oil_location = table.Column<long>(type: "bigint", nullable: false),
                    Next_change_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oil_changes", x => x.Oil_id);
                    table.ForeignKey(
                        name: "FK_Oil_changes_Service_visits_Service_id",
                        column: x => x.Service_id,
                        principalTable: "Service_visits",
                        principalColumn: "Service_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Oil_changes_Service_id",
                table: "Oil_changes",
                column: "Service_id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_visits_Car_id",
                table: "Service_visits",
                column: "Car_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oil_changes");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Service_visits");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
