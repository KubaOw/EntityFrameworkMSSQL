using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class WorkItemTagsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTag_Tags_TagId",
                table: "WorkItemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTag_WorkItems_WorkItemID",
                table: "WorkItemTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkItemTag",
                table: "WorkItemTag");

            migrationBuilder.RenameTable(
                name: "WorkItemTag",
                newName: "WorkItemTags");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItemTag_WorkItemID",
                table: "WorkItemTags",
                newName: "IX_WorkItemTags_WorkItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkItemTags",
                table: "WorkItemTags",
                columns: new[] { "TagId", "WorkItemID" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTags_Tags_TagId",
                table: "WorkItemTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTags_WorkItems_WorkItemID",
                table: "WorkItemTags",
                column: "WorkItemID",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTags_Tags_TagId",
                table: "WorkItemTags");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemTags_WorkItems_WorkItemID",
                table: "WorkItemTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkItemTags",
                table: "WorkItemTags");

            migrationBuilder.RenameTable(
                name: "WorkItemTags",
                newName: "WorkItemTag");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItemTags_WorkItemID",
                table: "WorkItemTag",
                newName: "IX_WorkItemTag_WorkItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkItemTag",
                table: "WorkItemTag",
                columns: new[] { "TagId", "WorkItemID" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTag_Tags_TagId",
                table: "WorkItemTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemTag_WorkItems_WorkItemID",
                table: "WorkItemTag",
                column: "WorkItemID",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
