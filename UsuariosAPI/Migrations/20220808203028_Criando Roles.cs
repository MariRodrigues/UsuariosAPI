using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class CriandoRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99999, "cfa0206c-6b02-4f35-b2fc-15a89c450649", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "7f930656-2449-4686-aba6-11ceebbf7a36", "regular", "REGULAR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CPF", "ConcurrencyStamp", "DataCriacao", "DataModificacao", "DataNascimento", "Email", "EmailConfirmed", "EnderecoId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { 99999, 0, "123456789", "866fb7e9-09e3-4ad3-9e0b-c01410755899", new DateTime(2022, 8, 8, 17, 30, 28, 628, DateTimeKind.Local).AddTicks(4594), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 8, 17, 30, 28, 629, DateTimeKind.Local).AddTicks(7154), "admin@admin.com", false, 1, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAELZ18wXGpgYOWPfRjmc6FUCTZKrIRuODbs1wu/g2SQUq5K6VzQoVQojY1kwjuc3RtA==", null, false, "7d61286f-c06b-4671-95ba-7d251ee8b6dc", true, false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 99999, 99999 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 99999, 99999 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999);
        }
    }
}
