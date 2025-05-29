using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkflowStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Workflow");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Workflow",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
