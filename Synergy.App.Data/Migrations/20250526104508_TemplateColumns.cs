using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemplateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Template");

            migrationBuilder.RenameColumn(
                name: "TemplateCode",
                table: "Template",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Template",
                newName: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Template",
                newName: "TemplateCode");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Template",
                newName: "DisplayName");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Template",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }
    }
}
