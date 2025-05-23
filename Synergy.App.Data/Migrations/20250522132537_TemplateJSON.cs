using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemplateJSON : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Json",
                table: "Template");
            migrationBuilder.AddColumn<string>(
                name: "Json",
                table: "Template",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Json",
                table: "Template");

            migrationBuilder.AddColumn<string>(
                name: "Json",
                table: "Template",
                type: "text",
                nullable: false,
                defaultValue: "{}");
        }
    }
}
