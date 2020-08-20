using Microsoft.EntityFrameworkCore.Migrations;

namespace iNOBStudios.Migrations
{
    public partial class AddingTexttoowntable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "PostVersions");

            migrationBuilder.CreateTable(
                name: "RawText",
                columns: table => new
                {
                    PostVersionId = table.Column<int>(maxLength: 191, nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawText", x => x.PostVersionId);
                    table.ForeignKey(
                        name: "FK_RawText_PostVersions_PostVersionId",
                        column: x => x.PostVersionId,
                        principalTable: "PostVersions",
                        principalColumn: "PostVersionId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawText");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "PostVersions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
