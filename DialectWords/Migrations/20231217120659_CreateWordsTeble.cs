using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DialectWords.Migrations
{
    /// <inheritdoc />
    public partial class CreateWordsTeble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdabiyTil = table.Column<string>(type: "TEXT", nullable: false),
                    Transliteratsiya = table.Column<string>(type: "TEXT", nullable: false),
                    Transkripsiya = table.Column<string>(type: "TEXT", nullable: false),
                    Turkum = table.Column<string>(type: "TEXT", nullable: false),
                    ShevaIzohi = table.Column<string>(type: "TEXT", nullable: false),
                    Misollar = table.Column<string>(type: "TEXT", nullable: false),
                    Sinonim = table.Column<string>(type: "TEXT", nullable: false),
                    Omonim = table.Column<string>(type: "TEXT", nullable: false),
                    Antonim = table.Column<string>(type: "TEXT", nullable: false),
                    OzlashganQatlam = table.Column<string>(type: "TEXT", nullable: false),
                    RusTilida = table.Column<string>(type: "TEXT", nullable: false),
                    IngilizTilida = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "Words");
        }
    }
}
