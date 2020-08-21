using Microsoft.EntityFrameworkCore.Migrations;

namespace iNOBStudios.Migrations
{
    public partial class Addingpublishedtagtopost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Posts");
        }
    }
}
