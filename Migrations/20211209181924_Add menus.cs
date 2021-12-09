using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iNOBStudios.Migrations
{
    public partial class Addmenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Posts",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "List",
                table: "Posts",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 191, nullable: false),
                    JSON = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentMenuName = table.Column<string>(nullable: false),
                    ParentMenuItemId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 191, nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 1023, nullable: true),
                    PostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuItems_ParentMenuItemId",
                        column: x => x.ParentMenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_ParentMenuName",
                        column: x => x.ParentMenuName,
                        principalTable: "Menus",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItems_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostVersions_PostedDate",
                table: "PostVersions",
                column: "PostedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AddedTime",
                table: "Posts",
                column: "AddedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Alias",
                table: "Posts",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_FirstPublished",
                table: "Posts",
                column: "FirstPublished");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_List",
                table: "Posts",
                column: "List");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Published",
                table: "Posts",
                column: "Published");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalFiles_PostedTime",
                table: "ExternalFiles",
                column: "PostedTime");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentMenuItemId",
                table: "MenuItems",
                column: "ParentMenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentMenuName",
                table: "MenuItems",
                column: "ParentMenuName");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_PostId",
                table: "MenuItems",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_Priority",
                table: "MenuItems",
                column: "Priority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_PostVersions_PostedDate",
                table: "PostVersions");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AddedTime",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Alias",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_FirstPublished",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_List",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Published",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_ExternalFiles_PostedTime",
                table: "ExternalFiles");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "List",
                table: "Posts");
        }
    }
}
