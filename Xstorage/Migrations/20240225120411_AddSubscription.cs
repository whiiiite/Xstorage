using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e4ae993-66f4-46d7-b1d6-5a323e4c86e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "baa55bca-16d2-43fb-8570-a8804da756ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db02c4ac-518c-4165-9543-32b4d481b130");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea9cfc32-ec19-4232-9e26-6019ffea6094");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "57efc3b4-5214-4792-8015-f9d6f230fb2a", null, "Administrator", "ADMINISTRATOR" },
                    { "6d09f4a9-c87b-4485-bd18-046cd9eb35db", null, "Donator", "DONATOR" },
                    { "6e0bc7af-c1f1-41cf-b8f4-6ce926772528", null, "Owner", "OWNER" },
                    { "c9189f28-8f8b-4ce6-a33b-0aaf9ec485b2", null, "Simple", "SIMPLE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57efc3b4-5214-4792-8015-f9d6f230fb2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d09f4a9-c87b-4485-bd18-046cd9eb35db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e0bc7af-c1f1-41cf-b8f4-6ce926772528");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9189f28-8f8b-4ce6-a33b-0aaf9ec485b2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e4ae993-66f4-46d7-b1d6-5a323e4c86e3", null, "Administrator", "ADMINISTRATOR" },
                    { "baa55bca-16d2-43fb-8570-a8804da756ea", null, "Simple", "SIMPLE" },
                    { "db02c4ac-518c-4165-9543-32b4d481b130", null, "Donator", "DONATOR" },
                    { "ea9cfc32-ec19-4232-9e26-6019ffea6094", null, "Owner", "OWNER" }
                });
        }
    }
}
