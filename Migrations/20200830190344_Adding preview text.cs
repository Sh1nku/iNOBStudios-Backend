using Microsoft.EntityFrameworkCore.Migrations;

namespace iNOBStudios.Migrations
{
    public partial class Addingpreviewtext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewText",
                table: "PostVersions",
                maxLength: 511,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewText",
                table: "PostVersions");
        }
    }
}
