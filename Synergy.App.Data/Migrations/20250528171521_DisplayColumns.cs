using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class DisplayColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_AssignedByUserId",
                table: "Workflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_AssignedToUserId",
                table: "Workflow");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_AssignedByUserId",
                table: "Workflow",
                column: "AssignedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_AssignedToUserId",
                table: "Workflow",
                column: "AssignedToUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_AssignedByUserId",
                table: "Workflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_User_AssignedToUserId",
                table: "Workflow");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_AssignedByUserId",
                table: "Workflow",
                column: "AssignedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_User_AssignedToUserId",
                table: "Workflow",
                column: "AssignedToUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
