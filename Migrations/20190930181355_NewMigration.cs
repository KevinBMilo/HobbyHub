using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamThree.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CreatorLevel",
                table: "Passions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProLevel",
                table: "Fans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorLevel",
                table: "Passions");

            migrationBuilder.DropColumn(
                name: "ProLevel",
                table: "Fans");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16);
        }
    }
}
