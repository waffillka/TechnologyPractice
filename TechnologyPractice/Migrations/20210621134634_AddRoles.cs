using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnologyPractice.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6d2c6ff-1719-4912-9f23-80b478f3a670", "aebf6f84-d695-4387-9bb3-dda6d8e39e42", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d36b503e-cad9-48af-8ec7-1b5c91791404", "dbb420f7-b1e3-490b-b768-b54037f3c5d1", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d36b503e-cad9-48af-8ec7-1b5c91791404");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6d2c6ff-1719-4912-9f23-80b478f3a670");
        }
    }
}
