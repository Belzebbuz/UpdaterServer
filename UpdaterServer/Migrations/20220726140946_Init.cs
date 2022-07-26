using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpdaterServer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsWinService = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExeFile = table.Column<string>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseAssemblies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PatchNote = table.Column<string>(type: "TEXT", nullable: true),
                    ReleaseAssemblyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseAssemblies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseAssemblies_Projects_ReleaseAssemblyId",
                        column: x => x.ReleaseAssemblyId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ea7bad5-b551-4cc8-a440-d76398ceeb18", "13146fa8-acef-4d07-aa38-04457583c02a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3623d53c-c317-40a1-8c45-178bd8e12677", "64014581-7d47-40f0-ba6f-66a9bd254c02", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70f0e057-bbc9-44ec-bf3b-32043d6b2f80", "676590ba-f18c-4571-9479-bd31562f1e5e", "Dev", "DEV" });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseAssemblies_ReleaseAssemblyId",
                table: "ReleaseAssemblies",
                column: "ReleaseAssemblyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "ReleaseAssemblies");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
