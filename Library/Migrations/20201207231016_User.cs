using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_AspNetUsers_ApplicationUserId",
                table: "Patrons");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Patrons",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_ApplicationUserId",
                table: "Patrons",
                newName: "IX_Patrons_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_AspNetUsers_UserId",
                table: "Patrons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_AspNetUsers_UserId",
                table: "Patrons");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Patrons",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_UserId",
                table: "Patrons",
                newName: "IX_Patrons_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_AspNetUsers_ApplicationUserId",
                table: "Patrons",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
