using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppliedById",
                table: "Leave",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TableMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: false),
                    Schema = table.Column<string>(type: "text", nullable: false),
                    CreateTable = table.Column<bool>(type: "boolean", nullable: false),
                    Query = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableMetadata", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leave_AppliedById",
                table: "Leave",
                column: "AppliedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Leave_AspNetUsers_AppliedById",
                table: "Leave",
                column: "AppliedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leave_AspNetUsers_AppliedById",
                table: "Leave");

            migrationBuilder.DropTable(
                name: "TableMetadata");

            migrationBuilder.DropIndex(
                name: "IX_Leave_AppliedById",
                table: "Leave");

            migrationBuilder.DropColumn(
                name: "AppliedById",
                table: "Leave");
        }
    }
}
