using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class Template : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<string>(type: "text", nullable: false),
                    IconFileId = table.Column<string>(type: "text", nullable: false),
                    AllowedPortalIds = table.Column<string[]>(type: "text[]", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TemplateCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableMetadataId = table.Column<Guid>(type: "uuid", nullable: false),
                    UdfTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    UdfTableMetadataId = table.Column<Guid>(type: "uuid", nullable: false),
                    Json = table.Column<string>(type: "text", nullable: false),
                    OtherAttachmentId = table.Column<string>(type: "text", nullable: false),
                    GroupCode = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Template_TableMetadata_TableMetadataId",
                        column: x => x.TableMetadataId,
                        principalTable: "TableMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_TableMetadata_UdfTableMetadataId",
                        column: x => x.UdfTableMetadataId,
                        principalTable: "TableMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_TemplateCategory_TemplateCategoryId",
                        column: x => x.TemplateCategoryId,
                        principalTable: "TemplateCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_Template_UdfTemplateId",
                        column: x => x.UdfTemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Template_TableMetadataId",
                table: "Template",
                column: "TableMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_TemplateCategoryId",
                table: "Template",
                column: "TemplateCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_UdfTableMetadataId",
                table: "Template",
                column: "UdfTableMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_UdfTemplateId",
                table: "Template",
                column: "UdfTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "TemplateCategory");
        }
    }
}
