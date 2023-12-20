using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialectWords.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdabiyTil = table.Column<string>(type: "TEXT", nullable: true),
                    Transliteratsiya = table.Column<string>(type: "TEXT", nullable: true),
                    Transkripsiya = table.Column<string>(type: "TEXT", nullable: true),
                    Turkum = table.Column<string>(type: "TEXT", nullable: true),
                    ShevaIzohi = table.Column<string>(type: "TEXT", nullable: true),
                    Misollar = table.Column<string>(type: "TEXT", nullable: true),
                    Sinonim = table.Column<string>(type: "TEXT", nullable: true),
                    Omonim = table.Column<string>(type: "TEXT", nullable: true),
                    Antonim = table.Column<string>(type: "TEXT", nullable: true),
                    OzlashganQatlam = table.Column<string>(type: "TEXT", nullable: true),
                    RusTilida = table.Column<string>(type: "TEXT", nullable: true),
                    IngilizTilida = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
