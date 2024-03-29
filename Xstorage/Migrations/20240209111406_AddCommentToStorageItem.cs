using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentToStorageItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a2c725e-d8e8-45d1-99d1-6df140bb3702");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d4a51f5-c322-46fd-a854-4b2455167417");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5de05b81-83b6-4443-af08-713a9ee8a4b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "620596b9-3868-4f89-a126-a3cffab1337f");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "StorageItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "StorageItems");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a2c725e-d8e8-45d1-99d1-6df140bb3702", null, "Donator", "DONATOR" },
                    { "3d4a51f5-c322-46fd-a854-4b2455167417", null, "Administrator", "ADMINISTRATOR" },
                    { "5de05b81-83b6-4443-af08-713a9ee8a4b4", null, "Owner", "OWNER" },
                    { "620596b9-3868-4f89-a126-a3cffab1337f", null, "Simple", "SIMPLE" }
                });
        }
    }
}
