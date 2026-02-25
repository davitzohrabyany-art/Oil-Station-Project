using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilChangeApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTypeOfOilLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Oil_location",
                table: "Oil_change",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Oil_location",
                table: "Oil_change",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
