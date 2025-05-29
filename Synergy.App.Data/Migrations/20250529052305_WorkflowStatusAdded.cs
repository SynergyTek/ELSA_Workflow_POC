using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkflowStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Workflow",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Workflow");
        }
    }
}
