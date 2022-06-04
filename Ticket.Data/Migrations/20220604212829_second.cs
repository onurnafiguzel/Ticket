using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Gender", "Name", "ProfilePath", "TmdbId" },
                values: new object[] { 1, 1, "Feyyaz Yiğit", "abc", 5 });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "enessolak.dev", "Enes Solak" },
                    { 2, "ongguzel@gmail.com", "Onur Güzel" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "BackdropPath", "Description", "Director", "Duration", "ImdbId", "NowPlaying", "OriginalLanguage", "OriginalTitle", "PosterPath", "Rating", "ReleaseDate", "Slug", "Status", "Title", "TrailerUrl" },
                values: new object[,]
                {
                    { 1, "backdroppath", "A drea film.", "Cristopher Nolan", 148, "imdbId", false, "English", "123", "posterpath", 8.8000000000000007, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "status", "Inception", null },
                    { 2, "backdroppath", "Turkish philosophy movie", "Nuri Bilge Ceylan", 188, "imdbId", false, "Turkish", "123", "posterpath", 8.0999999999999996, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "status", "Ahlat Ağacı", null }
                });
        }
    }
}
