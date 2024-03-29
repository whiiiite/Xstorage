using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class AddPathToStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Storage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Storage");
        }
    }
}
