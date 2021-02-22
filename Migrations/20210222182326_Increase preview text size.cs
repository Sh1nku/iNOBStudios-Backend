using Microsoft.EntityFrameworkCore.Migrations;

namespace iNOBStudios.Migrations
{
    public partial class Increasepreviewtextsize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PreviewText",
                table: "PostVersions",
                maxLength: 2047,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(511) CHARACTER SET utf8mb4",
                oldMaxLength: 511,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PreviewText",
                table: "PostVersions",
                type: "varchar(511) CHARACTER SET utf8mb4",
                maxLength: 511,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2047,
                oldNullable: true);
        }
    }
}
