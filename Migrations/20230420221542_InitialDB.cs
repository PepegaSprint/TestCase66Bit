using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestCase66Bit.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    lastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    gender = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    birthDate = table.Column<DateTime>(type: "date", nullable: false),
                    team = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballPlayers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FootballPlayers_firstName_lastName",
                table: "FootballPlayers",
                columns: new[] { "firstName", "lastName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballPlayers");
        }
    }
}
