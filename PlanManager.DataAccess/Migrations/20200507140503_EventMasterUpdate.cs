using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanManager.DataAccess.Migrations
{
    public partial class EventMasterUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "MasterEvents");

            migrationBuilder.DropColumn(
                name: "EndMinute",
                table: "MasterEvents");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "MasterEvents");

            migrationBuilder.DropColumn(
                name: "StartMinute",
                table: "MasterEvents");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "34177531-df31-4a57-8d4f-5c9e9b351251");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "5e3b6542-5d0f-4f4d-b407-c3d6c431d282");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "965b9f93-2b95-4b41-a613-10a835a399dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "a433d53a-9376-4781-b1a5-a7d526e9f5e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "954de741-3f14-4ea3-964b-52d8dd9e3287");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "1bd2a50e-0f44-4aa8-b700-7fc5101b3abc", "435db4de-e568-47c1-a787-23a4b52ee6d0", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "c1fe2aca-c1df-47aa-acc7-50123ca195cb", "15c99573-eb70-460a-aef6-ec4d938c8d5b", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "4a764b65-a9f5-4bff-8b82-777a3c55974a", "e1a86432-100c-493f-9644-62eda58fa06e", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "0f7282c1-324e-4bfa-a1ec-0905e85dfeb7", "941c2e83-9ace-4c39-a4fd-ab3af040850f", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "MasterEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndMinute",
                table: "MasterEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "MasterEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartMinute",
                table: "MasterEvents",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "e0341698-e788-4711-9d75-0eaf13389fe2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "87e96172-fc77-44c0-bc0f-36f76ff60312");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "12bd67bb-3755-4a19-a63c-c95793295edd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "8736edfe-75c8-4d79-a35a-068b28bf7199");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "9af0502b-bbfc-49a0-be66-b25b74b781e4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "f36cc2ff-1949-4347-bbac-b72bf03fcf0d", "af9fd038-dfb8-4871-8e8d-b42390e0aeaf", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "d292b9b4-639a-4fd6-a8d8-ef8995273b82", "8b0c7454-e351-424d-8c33-15ded4b658ce", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "78b590c1-9602-4116-ab18-88f16abeb4ff", "4ae11205-07ea-464c-938b-bc369db27f26", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "24c48aa9-cbe6-4a39-bcd0-ae44ba0958ab", "d7e6b28d-1ac1-405a-aef6-786d4c3ca329", true });
        }
    }
}
