using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class WorktItemSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "To Do" },
                    { 2, "Doing" },
                    { 3, "Done" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
