using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpdaterServer.Migrations
{
    public partial class TableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseAssemblies_Apps_ReleaseAssemblyId",
                table: "ReleaseAssemblies");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsWinService = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExeFile = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseAssemblies_Projects_ReleaseAssemblyId",
                table: "ReleaseAssemblies",
                column: "ReleaseAssemblyId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReleaseAssemblies_Projects_ReleaseAssemblyId",
                table: "ReleaseAssemblies");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ExeFile = table.Column<string>(type: "TEXT", nullable: false),
                    IsWinService = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ReleaseAssemblies_Apps_ReleaseAssemblyId",
                table: "ReleaseAssemblies",
                column: "ReleaseAssemblyId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
