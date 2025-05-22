using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableModelToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_TableModel_TableId",
                table: "Template");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableModel",
                table: "TableModel");

            migrationBuilder.RenameTable(
                name: "TableModel",
                newName: "Table");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table",
                table: "Table",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Table_TableId",
                table: "Template",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_Table_TableId",
                table: "Template");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table",
                table: "Table");

            migrationBuilder.RenameTable(
                name: "Table",
                newName: "TableModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableModel",
                table: "TableModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_TableModel_TableId",
                table: "Template",
                column: "TableId",
                principalTable: "TableModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
