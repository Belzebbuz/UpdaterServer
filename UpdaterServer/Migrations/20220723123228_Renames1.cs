using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpdaterServer.Migrations
{
    public partial class Renames1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseAssemblies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PatchNote = table.Column<string>(type: "TEXT", nullable: true),
                    ReleaseAssemblyId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseAssemblies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseAssemblies_Apps_ReleaseAssemblyId",
                        column: x => x.ReleaseAssemblyId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseAssemblies_ReleaseAssemblyId",
                table: "ReleaseAssemblies",
                column: "ReleaseAssemblyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReleaseAssemblies");

            migrationBuilder.DropTable(
                name: "Apps");
        }
    }
}
