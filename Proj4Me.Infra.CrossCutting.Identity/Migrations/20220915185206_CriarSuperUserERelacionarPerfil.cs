using Microsoft.EntityFrameworkCore.Migrations;

namespace Proj4Me.Infra.CrossCutting.Identity.Migrations
{
    public partial class CriarSuperUserERelacionarPerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a6bb24a-b3bd-4161-ad31-a7a71e38277e", "aa7be45a-378b-4130-8bfa-4230f78b610e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7cd4747-f6d8-4d23-8e74-31da46e22c82", "d0993d6c-cfc3-4014-be55-024693470b14", "Analista", "COACH" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e232968a-c6c2-44c2-a0e8-b8c61641a34f", 0, "0c996b45-d298-4dd6-8edd-7f6a436c3552", "superuser@simply.com", false, false, null, "superuser@simply.com", "superuser@simply.com", "AQAAAAEAACcQAAAAEF/cSsj4LXN6gQa7tpPmUDFzke9SWfDdTvfGhMLUmfMX3ZVvHfRswDbpI3SEe8jasQ==", null, false, "ab3a1450-4f2d-42af-8570-299a319eed7d", false, "superuser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9a6bb24a-b3bd-4161-ad31-a7a71e38277e", "e232968a-c6c2-44c2-a0e8-b8c61641a34f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7cd4747-f6d8-4d23-8e74-31da46e22c82");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9a6bb24a-b3bd-4161-ad31-a7a71e38277e", "e232968a-c6c2-44c2-a0e8-b8c61641a34f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a6bb24a-b3bd-4161-ad31-a7a71e38277e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e232968a-c6c2-44c2-a0e8-b8c61641a34f");
        }
    }
}
