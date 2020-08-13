using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamThree.Migrations
{
    public partial class TwoNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorLevel",
                table: "Passions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorLevel",
                table: "Passions",
                nullable: true);
        }
    }
}
