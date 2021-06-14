using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnologyPractice.Migrations
{
    public partial class FixDatabaseContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountLetters",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountLetters",
                table: "Contacts");
        }
    }
}
