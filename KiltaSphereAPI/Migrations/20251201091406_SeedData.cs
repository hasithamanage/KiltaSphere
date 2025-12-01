using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KiltaSphereAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "Email", "FirstName", "JoiningDate", "LastName", "MembershipStatus", "NationalIdentificationNumber" },
                values: new object[,]
                {
                    { 1, "aino.m@example.fi", "Aino", new DateTime(2020, 12, 1, 11, 14, 5, 244, DateTimeKind.Local).AddTicks(9174), "Mäkinen", "Active", null },
                    { 2, "elias.v@example.fi", "Elias", new DateTime(2024, 12, 1, 11, 14, 5, 244, DateTimeKind.Local).AddTicks(9184), "Virtanen", "Pending", null },
                    { 3, "sofi.k@example.fi", "Sofi", new DateTime(2025, 9, 1, 11, 14, 5, 244, DateTimeKind.Local).AddTicks(9188), "Korhonen", "Active", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 3);
        }
    }
}
