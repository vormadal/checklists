using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChecklistType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CopiedFromId",
                table: "ChecklistItem",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Checklist",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Checklist",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_CopiedFromId",
                table: "ChecklistItem",
                column: "CopiedFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_TemplateId",
                table: "Checklist",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklist_Checklist_TemplateId",
                table: "Checklist",
                column: "TemplateId",
                principalTable: "Checklist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_ChecklistItem_CopiedFromId",
                table: "ChecklistItem",
                column: "CopiedFromId",
                principalTable: "ChecklistItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklist_Checklist_TemplateId",
                table: "Checklist");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_ChecklistItem_CopiedFromId",
                table: "ChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItem_CopiedFromId",
                table: "ChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_Checklist_TemplateId",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "CopiedFromId",
                table: "ChecklistItem");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Checklist");
        }
    }
}
