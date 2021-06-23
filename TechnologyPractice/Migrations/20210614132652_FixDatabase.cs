using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TechnologyPractice.Migrations
{
    public partial class FixDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Contacts_ContactId1",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Hobbies_HobbyId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ContactId1",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_HobbyId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "HobbyId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hobbies",
                newName: "HobbyId");

            migrationBuilder.CreateTable(
                name: "ContactHobby",
                columns: table => new
                {
                    ContactsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HobbiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactHobby", x => new { x.ContactsId, x.HobbiesId });
                    table.ForeignKey(
                        name: "FK_ContactHobby_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactHobby_Hobbies_HobbiesId",
                        column: x => x.HobbiesId,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactHobby_HobbiesId",
                table: "ContactHobby",
                column: "HobbiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactHobby");

            migrationBuilder.RenameColumn(
                name: "HobbyId",
                table: "Hobbies",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId1",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HobbyId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactId1",
                table: "Contacts",
                column: "ContactId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_HobbyId",
                table: "Contacts",
                column: "HobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Contacts_ContactId1",
                table: "Contacts",
                column: "ContactId1",
                principalTable: "Contacts",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Hobbies_HobbyId",
                table: "Contacts",
                column: "HobbyId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
