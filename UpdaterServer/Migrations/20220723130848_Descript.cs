using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpdaterServer.Migrations
{
    public partial class Descript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Apps",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Apps");
        }
    }
}
