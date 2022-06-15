using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Data.Migrations
{
    public partial class addNewFieldsToActorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Actors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Actors",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImdbId",
                table: "Actors",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                table: "Actors",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "god" });

            migrationBuilder.CreateIndex(
                name: "IX_Theathers_PlaceId",
                table: "Theathers",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theathers_Places_PlaceId",
                table: "Theathers",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theathers_Places_PlaceId",
                table: "Theathers");

            migrationBuilder.DropIndex(
                name: "IX_Theathers_PlaceId",
                table: "Theathers");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "Actors");
        }
    }
}
