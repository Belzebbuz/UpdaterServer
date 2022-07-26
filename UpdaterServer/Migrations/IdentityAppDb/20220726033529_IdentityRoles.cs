using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpdaterServer.Migrations.IdentityAppDb
{
    public partial class IdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ae8ec36-966b-43a4-80de-513eb7d4fa16", "50221505-5d58-4525-818f-b79d40c634e1", "Dev", "DEV" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae40501a-c0ab-4a28-8bc5-ed7808d518f6", "cdacee48-e6c4-4bfc-87c2-229e67f9241c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e21a522a-fd36-4b0b-a269-64df068aa17a", "df0aea80-3d6d-47eb-aed3-bf9a59174c2c", "Viewer", "VIEWER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ae8ec36-966b-43a4-80de-513eb7d4fa16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae40501a-c0ab-4a28-8bc5-ed7808d518f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e21a522a-fd36-4b0b-a269-64df068aa17a");
        }
    }
}
