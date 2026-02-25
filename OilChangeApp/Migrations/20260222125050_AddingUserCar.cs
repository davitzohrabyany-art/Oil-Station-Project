using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oil_changes_Service_visits_Service_id",
                table: "Oil_changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_visits_Cars_Car_id",
                table: "Service_visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service_visits",
                table: "Service_visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oil_changes",
                table: "Oil_changes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Service_visits",
                newName: "Service_visit");

            migrationBuilder.RenameTable(
                name: "Oil_changes",
                newName: "Oil_change");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Service_visits_Car_id",
                table: "Service_visit",
                newName: "IX_Service_visit_Car_id");

            migrationBuilder.RenameIndex(
                name: "IX_Oil_changes_Service_id",
                table: "Oil_change",
                newName: "IX_Oil_change_Service_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service_visit",
                table: "Service_visit",
                column: "Service_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oil_change",
                table: "Oil_change",
                column: "Oil_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "car_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oil_change_Service_visit_Service_id",
                table: "Oil_change",
                column: "Service_id",
                principalTable: "Service_visit",
                principalColumn: "Service_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_visit_Car_Car_id",
                table: "Service_visit",
                column: "Car_id",
                principalTable: "Car",
                principalColumn: "car_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oil_change_Service_visit_Service_id",
                table: "Oil_change");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_visit_Car_Car_id",
                table: "Service_visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service_visit",
                table: "Service_visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oil_change",
                table: "Oil_change");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Service_visit",
                newName: "Service_visits");

            migrationBuilder.RenameTable(
                name: "Oil_change",
                newName: "Oil_changes");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Service_visit_Car_id",
                table: "Service_visits",
                newName: "IX_Service_visits_Car_id");

            migrationBuilder.RenameIndex(
                name: "IX_Oil_change_Service_id",
                table: "Oil_changes",
                newName: "IX_Oil_changes_Service_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service_visits",
                table: "Service_visits",
                column: "Service_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oil_changes",
                table: "Oil_changes",
                column: "Oil_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "car_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oil_changes_Service_visits_Service_id",
                table: "Oil_changes",
                column: "Service_id",
                principalTable: "Service_visits",
                principalColumn: "Service_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_visits_Cars_Car_id",
                table: "Service_visits",
                column: "Car_id",
                principalTable: "Cars",
                principalColumn: "car_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
