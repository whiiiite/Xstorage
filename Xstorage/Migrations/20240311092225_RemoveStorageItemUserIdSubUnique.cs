using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStorageItemUserIdSubUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorageItems");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions");

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
                    { "16e64755-b4bf-4d06-8c51-1497e1ca723d", null, "Simple", "SIMPLE" },
                    { "4c8709cd-c0a3-4a7b-b869-c2588a2bd3e9", null, "Owner", "OWNER" },
                    { "610e6d78-c37d-4784-8587-8d2480157ca8", null, "Donator", "DONATOR" },
                    { "bf32b439-43e4-48fe-a86e-1b84f7c352ed", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions");

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

            migrationBuilder.CreateTable(
                name: "StorageItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StorageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageItems_Storage_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storage",
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

            migrationBuilder.CreateIndex(
                name: "IX_StorageItems_StorageId",
                table: "StorageItems",
                column: "StorageId");
        }
    }
}
