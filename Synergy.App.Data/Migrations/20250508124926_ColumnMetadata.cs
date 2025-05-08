using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDefaultDisplayColumn = table.Column<bool>(type: "boolean", nullable: false),
                    LabelName = table.Column<string>(type: "text", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: false),
                    IsNullable = table.Column<bool>(type: "boolean", nullable: false),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    UdfUIType = table.Column<int>(type: "integer", nullable: false),
                    IsForeignKey = table.Column<bool>(type: "boolean", nullable: false),
                    IsVirtualColumn = table.Column<bool>(type: "boolean", nullable: false),
                    IsVirtualForeignKey = table.Column<bool>(type: "boolean", nullable: false),
                    IsPrimaryKey = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemColumn = table.Column<bool>(type: "boolean", nullable: false),
                    IsUniqueColumn = table.Column<bool>(type: "boolean", nullable: false),
                    IsLogColumn = table.Column<bool>(type: "boolean", nullable: false),
                    IsMultiValueColumn = table.Column<bool>(type: "boolean", nullable: false),
                    IsUdfColumn = table.Column<bool>(type: "boolean", nullable: false),
                    IsHiddenColumn = table.Column<bool>(type: "boolean", nullable: false),
                    HideForeignKeyTableColumns = table.Column<bool>(type: "boolean", nullable: false),
                    IsReferenceColumn = table.Column<bool>(type: "boolean", nullable: false),
                    ForeignKeyTableId = table.Column<Guid>(type: "uuid", nullable: false),
                    ForeignKeyTableName = table.Column<string>(type: "text", nullable: false),
                    ForeignKeyTableAliasName = table.Column<string>(type: "text", nullable: false),
                    ForeignKeyTableSchemaName = table.Column<string>(type: "text", nullable: false),
                    ForeignKeyColumnId = table.Column<Guid>(type: "uuid", nullable: false),
                    ForeignKeyColumnName = table.Column<string>(type: "text", nullable: false),
                    ForeignKeyDisplayColumnLabelName = table.Column<string>(type: "text", nullable: false),
                    ForeignKeyDisplayColumnAlias = table.Column<string>(type: "text", nullable: false),
                    ForeignKeyDisplayColumnDataType = table.Column<int>(type: "integer", nullable: false),
                    ForeignKeyConstraintName = table.Column<string>(type: "text", nullable: false),
                    TableMetadataId = table.Column<Guid>(type: "uuid", nullable: false),
                    EditableBy = table.Column<string[]>(type: "text[]", nullable: false),
                    ViewableBy = table.Column<string[]>(type: "text[]", nullable: false),
                    EditableContext = table.Column<string[]>(type: "text[]", nullable: false),
                    ViewableContext = table.Column<string[]>(type: "text[]", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnMetadata_ColumnMetadata_ForeignKeyColumnId",
                        column: x => x.ForeignKeyColumnId,
                        principalTable: "ColumnMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnMetadata_TableMetadata_ForeignKeyTableId",
                        column: x => x.ForeignKeyTableId,
                        principalTable: "TableMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnMetadata_TableMetadata_TableMetadataId",
                        column: x => x.TableMetadataId,
                        principalTable: "TableMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnMetadata_ForeignKeyColumnId",
                table: "ColumnMetadata",
                column: "ForeignKeyColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnMetadata_ForeignKeyTableId",
                table: "ColumnMetadata",
                column: "ForeignKeyTableId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnMetadata_TableMetadataId",
                table: "ColumnMetadata",
                column: "TableMetadataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColumnMetadata");
        }
    }
}
