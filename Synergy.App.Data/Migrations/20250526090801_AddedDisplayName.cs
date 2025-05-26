using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDisplayName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Template");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Column");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Template",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Template");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Template",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Table",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Column",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
