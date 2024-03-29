using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserFolderField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6866eb4-dc28-4384-bbae-169fd7a4c32d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdee16ff-9c77-4e23-842c-8a3d6a49d657");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcabc775-8cff-48d8-8ea9-8502fb423098");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd03c209-f2c9-4663-8757-9ad2e31dcd42");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserFolders",
                newName: "Path");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "54da9eea-41ac-4e68-9a5c-ad50284fd93c", null, "Owner", "OWNER" },
                    { "74ef9400-7848-45ce-8ee7-fa7894afd216", null, "Administrator", "ADMINISTRATOR" },
                    { "79f1ea7c-b73a-4308-acde-d9fa8d24705c", null, "Donator", "DONATOR" },
                    { "93aa8b3a-50a6-4c2d-9093-aeccf5dd8c93", null, "Simple", "SIMPLE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54da9eea-41ac-4e68-9a5c-ad50284fd93c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74ef9400-7848-45ce-8ee7-fa7894afd216");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79f1ea7c-b73a-4308-acde-d9fa8d24705c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93aa8b3a-50a6-4c2d-9093-aeccf5dd8c93");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "UserFolders",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c6866eb4-dc28-4384-bbae-169fd7a4c32d", null, "Administrator", "ADMINISTRATOR" },
                    { "cdee16ff-9c77-4e23-842c-8a3d6a49d657", null, "Simple", "SIMPLE" },
                    { "fcabc775-8cff-48d8-8ea9-8502fb423098", null, "Owner", "OWNER" },
                    { "fd03c209-f2c9-4663-8757-9ad2e31dcd42", null, "Donator", "DONATOR" }
                });
        }
    }
}
