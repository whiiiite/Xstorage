using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFoldersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFolders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    { "c6866eb4-dc28-4384-bbae-169fd7a4c32d", null, "Administrator", "ADMINISTRATOR" },
                    { "cdee16ff-9c77-4e23-842c-8a3d6a49d657", null, "Simple", "SIMPLE" },
                    { "fcabc775-8cff-48d8-8ea9-8502fb423098", null, "Owner", "OWNER" },
                    { "fd03c209-f2c9-4663-8757-9ad2e31dcd42", null, "Donator", "DONATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFolders_UserId",
                table: "UserFolders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFolders");

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
        }
    }
}
