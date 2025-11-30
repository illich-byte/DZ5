using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tblCountries",
                columns: new[] { "Id", "Code", "DateCreated", "Image", "IsDeleted", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "UA", new DateTime(2025, 11, 26, 21, 52, 57, 845, DateTimeKind.Utc).AddTicks(1501), "https://flagcdn.com/w320/ua.png", false, "Україна", "ukraine" },
                    { 2, "PL", new DateTime(2025, 11, 26, 21, 52, 57, 848, DateTimeKind.Utc).AddTicks(6548), "https://flagcdn.com/w320/pl.png", false, "Польща", "poland" },
                    { 3, "DE", new DateTime(2025, 11, 26, 21, 52, 57, 848, DateTimeKind.Utc).AddTicks(6594), "https://flagcdn.com/w320/de.png", false, "Німеччина", "germany" },
                    { 4, "CZ", new DateTime(2025, 11, 26, 21, 52, 57, 848, DateTimeKind.Utc).AddTicks(6600), "https://flagcdn.com/w320/cz.png", false, "Чехія", "czech-republic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblCountries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tblCountries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tblCountries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tblCountries",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
