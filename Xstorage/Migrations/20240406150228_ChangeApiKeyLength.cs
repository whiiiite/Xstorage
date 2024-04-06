using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class ChangeApiKeyLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiKey",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ApiKey",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132);
        }
    }
}
