using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WEB_PROJ.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    password = table.Column<string>(type: "character(64)", fixedLength: true, maxLength: 64, nullable: false),
                    password_salt = table.Column<string>(type: "character(20)", fixedLength: true, maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character(25)", fixedLength: true, maxLength: 25, nullable: false),
                    is_verificated = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    status_id = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "1"),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    region_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    date_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    first_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    last_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    patronymic_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    skype = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    telegram = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    vk = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    twitter = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    youtube = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    purse_perfect_money_usd = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "users_status_id_fkey",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_status_id",
                table: "users",
                column: "status_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
