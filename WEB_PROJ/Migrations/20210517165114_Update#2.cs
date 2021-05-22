using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_PROJ.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "twitter",
                table: "users",
                newName: "instagram");

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facebook",
                table: "users",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "users");

            migrationBuilder.DropColumn(
                name: "facebook",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "instagram",
                table: "users",
                newName: "twitter");
        }
    }
}
