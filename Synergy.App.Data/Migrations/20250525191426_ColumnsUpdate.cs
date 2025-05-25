using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Table");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Workflow",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Workflow",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Workflow",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Template",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Template",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Template",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Table",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Table",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Table",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Column",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Column",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Column",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Schema",
                table: "Table",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "TemplateId",
                table: "Table",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Table_TemplateId",
                table: "Table",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Template_TemplateId",
                table: "Table",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_Template_TemplateId",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Table_TemplateId",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Table");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Workflow",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Workflow",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Workflow",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Template",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Template",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Template",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Table",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Table",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Table",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Column",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Column",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Column",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "Schema",
                table: "Table",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(16)",
                oldMaxLength: 16);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Table",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Table",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Table",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
