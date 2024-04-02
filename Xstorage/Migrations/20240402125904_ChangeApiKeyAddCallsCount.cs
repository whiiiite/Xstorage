using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xstorage.Migrations
{
    /// <inheritdoc />
    public partial class ChangeApiKeyAddCallsCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CallsCount",
                table: "ApiKey",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallsCount",
                table: "ApiKey");
        }
    }
}
