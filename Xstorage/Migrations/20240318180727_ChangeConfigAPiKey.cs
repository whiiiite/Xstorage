using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfigAPiKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiKey",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKey_Key",
                table: "ApiKey",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApiKey_Key",
                table: "ApiKey");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiKey",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
