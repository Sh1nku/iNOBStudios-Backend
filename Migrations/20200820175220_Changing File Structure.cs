using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iNOBStudios.Migrations
{
    public partial class ChangingFileStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RawFile",
                table: "ExternalFiles");

            migrationBuilder.CreateTable(
                name: "RawFiles",
                columns: table => new
                {
                    FileName = table.Column<string>(maxLength: 191, nullable: false),
                    Data = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFiles", x => x.FileName);
                    table.ForeignKey(
                        name: "FK_RawFiles_ExternalFiles_FileName",
                        column: x => x.FileName,
                        principalTable: "ExternalFiles",
                        principalColumn: "FileName",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawFiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "RawFile",
                table: "ExternalFiles",
                type: "longblob",
                nullable: true);
        }
    }
}
