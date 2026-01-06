using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiltaSphereAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId");
                });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1,
                column: "JoiningDate",
                value: new DateTime(2021, 1, 3, 22, 55, 22, 984, DateTimeKind.Local).AddTicks(2747));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2,
                column: "JoiningDate",
                value: new DateTime(2025, 1, 3, 22, 55, 22, 984, DateTimeKind.Local).AddTicks(2756));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 3,
                column: "JoiningDate",
                value: new DateTime(2025, 10, 3, 22, 55, 22, 984, DateTimeKind.Local).AddTicks(2761));

            migrationBuilder.CreateIndex(
                name: "IX_Users_MemberId",
                table: "Users",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

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
    }
}
