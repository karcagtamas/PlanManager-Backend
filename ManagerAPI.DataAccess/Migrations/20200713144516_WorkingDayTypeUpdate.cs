using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class WorkingDayTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "037b4369-ee62-4140-b981-b794cc4d3ae9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "ea50c030-1653-4a8c-a6db-f447767730bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "2d548222-8f08-4194-bdf5-970ae75d64ee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "e03b83ad-0b0d-450c-a47d-05a011f34b55");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "6722ca90-5fa9-4baa-9e95-6d9c3a275d40");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "d31f4d8a-3886-4043-b617-b05dfc3dc84e", "0576f598-bc2d-4c85-9d2e-698bf67a168a", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "bb819c01-bb8d-42ed-af50-dfaf4b27f68f", "d2c91443-cc8a-4df0-b7d5-9351f09ba952", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "113fee95-0103-48f6-8e28-92fdb8c38169", "3560bb7d-d86a-4b30-90ee-0170e8c90990", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "dc2fdc97-800c-49cc-b1d9-39128465d711", "e6684ada-aa5a-47f1-8f15-47769caec2b9", true });

            migrationBuilder.InsertData(
                table: "WorkingDayTypes",
                columns: new[] { "Id", "DayIsActive", "Title" },
                values: new object[] { 1, true, "Work Day" });

            migrationBuilder.InsertData(
                table: "WorkingDayTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 2, "University" },
                    { 3, "Empty Day" },
                    { 4, "Holiday" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "911f98b0-85af-4072-ad18-4c59a5568475");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "95ca0630-1bbc-4191-8c23-f6aab70ac6c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "7495f2f1-f8b6-4d6c-848b-405eee0a7174");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "bf1c6eeb-9f61-414f-b51a-3341b00514b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "020f81b8-99a6-42f4-9b0d-fd4eaef3a7f7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "efa9d7d2-caee-4f82-829c-26042e2cd98b", "38ac2db8-4587-4c5b-8c38-58f56ec5fc9d", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "5d80fc78-67d6-429a-8a58-9a9b007d3e2b", "37f7ce0f-64cf-4d17-bcc9-c9aa5c0d4ce7", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "0ec593f2-dc2f-4858-bda6-731a50ee0a76", "609c521c-cad6-48fa-915f-b741c7008aa9", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "61bd41ca-6826-4699-a2af-d2fb6bccd808", "1cf46b43-ec68-4bad-be1b-d5d70484cce5", true });
        }
    }
}
