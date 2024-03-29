using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class ConfigStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16e64755-b4bf-4d06-8c51-1497e1ca723d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c8709cd-c0a3-4a7b-b869-c2588a2bd3e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "610e6d78-c37d-4784-8587-8d2480157ca8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf32b439-43e4-48fe-a86e-1b84f7c352ed");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Storage",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d33018f-5e2f-4ab7-bd10-ef03fbebd4f1", null, "Owner", "OWNER" },
                    { "55aad754-dfee-4da8-ac0d-bbe6bef32f30", null, "Donator", "DONATOR" },
                    { "911b1f2d-8759-496b-bf72-154b5795f8bd", null, "Administrator", "ADMINISTRATOR" },
                    { "b85a3fe7-ff54-42fe-a5b6-b20c496ef50d", null, "Simple", "SIMPLE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d33018f-5e2f-4ab7-bd10-ef03fbebd4f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55aad754-dfee-4da8-ac0d-bbe6bef32f30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "911b1f2d-8759-496b-bf72-154b5795f8bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b85a3fe7-ff54-42fe-a5b6-b20c496ef50d");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Storage",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16e64755-b4bf-4d06-8c51-1497e1ca723d", null, "Simple", "SIMPLE" },
                    { "4c8709cd-c0a3-4a7b-b869-c2588a2bd3e9", null, "Owner", "OWNER" },
                    { "610e6d78-c37d-4784-8587-8d2480157ca8", null, "Donator", "DONATOR" },
                    { "bf32b439-43e4-48fe-a86e-1b84f7c352ed", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
