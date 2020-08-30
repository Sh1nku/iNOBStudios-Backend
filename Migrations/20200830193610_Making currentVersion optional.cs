using Microsoft.EntityFrameworkCore.Migrations;

namespace iNOBStudios.Migrations
{
    public partial class MakingcurrentVersionoptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostVersions_Posts_CurrentVersionId",
                table: "PostVersions");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentVersionId",
                table: "PostVersions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PostVersions_Posts_CurrentVersionId",
                table: "PostVersions",
                column: "CurrentVersionId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostVersions_Posts_CurrentVersionId",
                table: "PostVersions");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentVersionId",
                table: "PostVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVersions_Posts_CurrentVersionId",
                table: "PostVersions",
                column: "CurrentVersionId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
