using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class ModificandotipodacolunaCEP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CEP",
                table: "Enderecos",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "30a8ce35-fc2e-4849-aaad-9b7262c6dbbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "691b1a8c-1794-4140-8f92-77e9baccb39d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "DataCriacao", "DataNascimento", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89006f79-3a40-4d4c-902b-4918ca3dde46", new DateTime(2022, 8, 8, 17, 44, 5, 411, DateTimeKind.Local).AddTicks(6815), new DateTime(2022, 8, 8, 17, 44, 5, 412, DateTimeKind.Local).AddTicks(9049), "AQAAAAEAACcQAAAAEC+f1n/9y6Qnvxnvqef88KF4sXFRYOGtGyxwK38AxIvYINOWdWjZcQ/RY3Avu2EI4g==", "deaf48af-c9b2-40b4-8500-6c905111fcb8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CEP",
                table: "Enderecos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "7f930656-2449-4686-aba6-11ceebbf7a36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "cfa0206c-6b02-4f35-b2fc-15a89c450649");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "DataCriacao", "DataNascimento", "PasswordHash", "SecurityStamp" },
                values: new object[] { "866fb7e9-09e3-4ad3-9e0b-c01410755899", new DateTime(2022, 8, 8, 17, 30, 28, 628, DateTimeKind.Local).AddTicks(4594), new DateTime(2022, 8, 8, 17, 30, 28, 629, DateTimeKind.Local).AddTicks(7154), "AQAAAAEAACcQAAAAELZ18wXGpgYOWPfRjmc6FUCTZKrIRuODbs1wu/g2SQUq5K6VzQoVQojY1kwjuc3RtA==", "7d61286f-c06b-4671-95ba-7d251ee8b6dc" });
        }
    }
}
