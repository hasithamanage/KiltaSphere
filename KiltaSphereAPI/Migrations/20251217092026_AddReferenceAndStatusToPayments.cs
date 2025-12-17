using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiltaSphereAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddReferenceAndStatusToPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payments",
                newName: "PaymentStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1,
                column: "JoiningDate",
                value: new DateTime(2020, 12, 17, 11, 20, 25, 446, DateTimeKind.Local).AddTicks(7387));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2,
                column: "JoiningDate",
                value: new DateTime(2024, 12, 17, 11, 20, 25, 446, DateTimeKind.Local).AddTicks(7396));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 3,
                column: "JoiningDate",
                value: new DateTime(2025, 9, 17, 11, 20, 25, 446, DateTimeKind.Local).AddTicks(7401));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Payments",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1,
                column: "JoiningDate",
                value: new DateTime(2020, 12, 1, 11, 14, 5, 244, DateTimeKind.Local).AddTicks(9174));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2,
                column: "JoiningDate",
                value: new DateTime(2024, 12, 1, 11, 14, 5, 244, DateTimeKind.Local).AddTicks(9184));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 3,
                column: "JoiningDate",
                value: new DateTime(2025, 9, 1, 11, 14, 5, 244, DateTimeKind.Local).AddTicks(9188));
        }
    }
}
