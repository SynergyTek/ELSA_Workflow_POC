using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class TemplateCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TemplateCode",
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
                name: "TemplateCode",
                table: "Template");
        }
    }
}
