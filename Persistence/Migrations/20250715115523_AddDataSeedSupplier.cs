using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSeedSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "suppliers",
                columns: new[] { "Id", "Address", "IsDeleted", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "one@example.com", false, "Supplier One", "01000000000" },
                    { 2, "two@example.com", false, "Supplier Two", "01111111111" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "suppliers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "suppliers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
