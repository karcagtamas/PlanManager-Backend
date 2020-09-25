using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class UserMovieFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "UserMovieSwitch",
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "12763e1f-9d29-4896-b051-3ac4f3ca5b35");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "91261a8f-da6a-45f6-bc4f-db5707249159");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "0ece7a83-9735-46f4-a974-a0708bf45592");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "2a578123-08b6-49c5-8030-d3517412c685");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "29aa2a06-1095-43f8-9edb-5739345eb98f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "046fb8c2-da37-4b6e-88d8-c59e30acf4e0", "a54d8c4f-9cf6-4a2d-a806-f7182a0b904c", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "1be385c7-4b78-403c-ac56-ceaf4f5ee2f0", "48befa4c-bc09-4073-bbf7-d4777ed32a43", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "f61682ef-5213-4fd3-ad16-a896f069ee2d", "7dc910e5-e02c-46f2-9a8a-f90ad5052b9b", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "895f60ef-fd51-46b8-b4bf-5c627b0c85bb", "469f3e17-fbcb-43a5-aefb-f242f5fe19bb", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "UserMovieSwitch",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "53cab9cd-8dab-4bc6-820a-d5d1fa6bb6a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "1e8db51d-ec1c-40df-9e2d-a64ed123ebde");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "5f0d85ab-f445-48b3-a31e-6a3a58b63859");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "0fb0b3e8-378f-4da0-ba10-89e3837ac309");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "35a11dfb-f9b9-48b4-88bd-c0bc0050a669");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "44b49fcf-557d-4ec7-a322-79516ad0cd75", "6533390b-e65d-4f21-b57e-d72bc31437da", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "97c24019-3b95-4c34-838a-50fd96396e16", "9f404928-def3-440c-8d57-bc23717ba9ee", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "88643b53-d19d-40cd-9a35-4d975ebc4292", "dd97a1f4-75d1-4999-8f58-3612e8448139", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "001e2da9-c085-4d48-b75c-f386d57dfb9f", "b7f61eb9-8b75-45eb-8f67-c09d9fef3318", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }
    }
}
