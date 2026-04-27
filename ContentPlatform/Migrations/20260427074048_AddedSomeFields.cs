using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Contents");
        }
    }
}
