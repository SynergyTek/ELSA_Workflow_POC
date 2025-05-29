using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Column_Column_ForeignKeyColumnId",
                table: "Column");

            migrationBuilder.DropForeignKey(
                name: "FK_Column_Table_ForeignKeyTableId",
                table: "Column");

            migrationBuilder.DropIndex(
                name: "IX_Column_ForeignKeyColumnId",
                table: "Column");

            migrationBuilder.DropIndex(
                name: "IX_Column_ForeignKeyTableId",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "EditableBy",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "EditableContext",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyColumnId",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyColumnName",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyConstraintName",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyDisplayColumnAlias",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyDisplayColumnDataType",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyDisplayColumnLabelName",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyTableAliasName",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyTableId",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyTableName",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ForeignKeyTableSchemaName",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "HideForeignKeyTableColumns",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "IsDefaultDisplayColumn",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "IsHiddenColumn",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "IsLogColumn",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "IsMultiValueColumn",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "IsReferenceColumn",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "IsUdfColumn",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "IsVirtualColumn",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "LabelName",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "UdfUIType",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ViewableBy",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "ViewableContext",
                table: "Column");

            migrationBuilder.RenameColumn(
                name: "IsVirtualForeignKey",
                table: "Column",
                newName: "IsVisible");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVisible",
                table: "Column",
                newName: "IsVirtualForeignKey");

            migrationBuilder.AddColumn<string[]>(
                name: "EditableBy",
                table: "Column",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "EditableContext",
                table: "Column",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<Guid>(
                name: "ForeignKeyColumnId",
                table: "Column",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ForeignKeyColumnName",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForeignKeyConstraintName",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForeignKeyDisplayColumnAlias",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ForeignKeyDisplayColumnDataType",
                table: "Column",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ForeignKeyDisplayColumnLabelName",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForeignKeyTableAliasName",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ForeignKeyTableId",
                table: "Column",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ForeignKeyTableName",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForeignKeyTableSchemaName",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HideForeignKeyTableColumns",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultDisplayColumn",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHiddenColumn",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLogColumn",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMultiValueColumn",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReferenceColumn",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUdfColumn",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVirtualColumn",
                table: "Column",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LabelName",
                table: "Column",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UdfUIType",
                table: "Column",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string[]>(
                name: "ViewableBy",
                table: "Column",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "ViewableContext",
                table: "Column",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Column_ForeignKeyColumnId",
                table: "Column",
                column: "ForeignKeyColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_Column_ForeignKeyTableId",
                table: "Column",
                column: "ForeignKeyTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Column_Column_ForeignKeyColumnId",
                table: "Column",
                column: "ForeignKeyColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Column_Table_ForeignKeyTableId",
                table: "Column",
                column: "ForeignKeyTableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
