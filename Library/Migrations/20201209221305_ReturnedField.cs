using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class ReturnedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "PatronCopies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Returned",
                table: "PatronCopies");
        }
    }
}
