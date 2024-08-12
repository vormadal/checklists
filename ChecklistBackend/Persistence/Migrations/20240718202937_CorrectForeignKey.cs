using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CorrectForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Checklist_Id",
                table: "ChecklistItem");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChecklistItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_ChecklistId",
                table: "ChecklistItem",
                column: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Checklist_ChecklistId",
                table: "ChecklistItem",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItem_Checklist_ChecklistId",
                table: "ChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItem_ChecklistId",
                table: "ChecklistItem");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChecklistItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItem_Checklist_Id",
                table: "ChecklistItem",
                column: "Id",
                principalTable: "Checklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
