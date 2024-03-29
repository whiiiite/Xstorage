using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUserFoldersAddToUserFolderPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFolders");

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

            migrationBuilder.AddColumn<string>(
                name: "FolderPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FolderPath",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserFolders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFolders_AspNetUsers_UserId",
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
                    { "54da9eea-41ac-4e68-9a5c-ad50284fd93c", null, "Owner", "OWNER" },
                    { "74ef9400-7848-45ce-8ee7-fa7894afd216", null, "Administrator", "ADMINISTRATOR" },
                    { "79f1ea7c-b73a-4308-acde-d9fa8d24705c", null, "Donator", "DONATOR" },
                    { "93aa8b3a-50a6-4c2d-9093-aeccf5dd8c93", null, "Simple", "SIMPLE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFolders_UserId",
                table: "UserFolders",
                column: "UserId");
        }
    }
}
