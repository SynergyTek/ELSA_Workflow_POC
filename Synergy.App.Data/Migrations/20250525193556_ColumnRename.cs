using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Workflow",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Workflow",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Template",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Template",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Table",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Table",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Column",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Column",
                newName: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_CreatedById",
                table: "Workflow",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_UpdatedById",
                table: "Workflow",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Template_CreatedById",
                table: "Template",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Template_UpdatedById",
                table: "Template",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Table_CreatedById",
                table: "Table",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Table_UpdatedById",
                table: "Table",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Column_CreatedById",
                table: "Column",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Column_UpdatedById",
                table: "Column",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Column_User_CreatedById",
                table: "Column",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Column_User_UpdatedById",
                table: "Column",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_User_CreatedById",
                table: "Table",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_User_UpdatedById",
                table: "Table",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Template_User_CreatedById",
                table: "Template",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Template_User_UpdatedById",
                table: "Template",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_CreatedById",
                table: "Workflow",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_UpdatedById",
                table: "Workflow",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Column_User_CreatedById",
                table: "Column");

            migrationBuilder.DropForeignKey(
                name: "FK_Column_User_UpdatedById",
                table: "Column");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_User_CreatedById",
                table: "Table");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_User_UpdatedById",
                table: "Table");

            migrationBuilder.DropForeignKey(
                name: "FK_Template_User_CreatedById",
                table: "Template");

            migrationBuilder.DropForeignKey(
                name: "FK_Template_User_UpdatedById",
                table: "Template");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_CreatedById",
                table: "Workflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_UpdatedById",
                table: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_Workflow_CreatedById",
                table: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_Workflow_UpdatedById",
                table: "Workflow");

            migrationBuilder.DropIndex(
                name: "IX_Template_CreatedById",
                table: "Template");

            migrationBuilder.DropIndex(
                name: "IX_Template_UpdatedById",
                table: "Template");

            migrationBuilder.DropIndex(
                name: "IX_Table_CreatedById",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Table_UpdatedById",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Column_CreatedById",
                table: "Column");

            migrationBuilder.DropIndex(
                name: "IX_Column_UpdatedById",
                table: "Column");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Workflow",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Workflow",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Template",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Template",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Table",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Table",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Column",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Column",
                newName: "CreatedBy");
        }
    }
}
