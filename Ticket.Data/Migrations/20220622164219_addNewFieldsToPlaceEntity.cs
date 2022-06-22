using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Data.Migrations
{
    public partial class addNewFieldsToPlaceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Places",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Places",
                type: "decimal(8,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Places",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Places");
        }
    }
}
