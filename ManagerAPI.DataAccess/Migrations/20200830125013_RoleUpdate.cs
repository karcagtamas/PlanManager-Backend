using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class RoleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "f57778cf-9123-4bbd-9aad-e64f43f61b32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "cf406b10-a1ba-4030-83f3-c2caae81ec93");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "5e2e013c-bced-45ef-a348-863547eeb44e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "2bfd9695-8781-4b2b-bb62-0f9a805d4a02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "d01c650c-0846-48af-9ade-e19a790ad888");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "AccessLevel" },
                values: new object[,]
                {
                    { "936e4ddc-5d3f-4355-af3a-304a4fe4f518", "2d4f30e7-2b3d-4f7c-9e32-613af7f42dd6", "WebsiteRole", "Status Library User", "STATUS LIBRARY USER", 1 },
                    { "936e4ddc-5d3f-5466-af3a-3b4a4424d518", "947e400d-e610-48f3-bde3-c0b5e0b6496c", "WebsiteRole", "Status Library Moderator", "STATUS LIBRARY MODERATOR", 3 },
                    { "936d4dfc-5536-4d5f-af2a-304d4fe4f518", "1649c6d9-452c-4b60-934c-4615a11f46fe", "WebsiteRole", "Status Library Administrator", "STATUS LIBRARY ADMINISTRATOR", 3 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "7272cddf-0272-4c0d-bd5a-683a4f353337", "9d212a32-a4b6-4121-b979-3504d8eb90eb", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "1cd61e69-1bab-453d-8754-bad625dfde6a", "02c83542-9091-4fd4-aa3f-5934991dd535", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "e4ebf071-c3ca-473c-969b-ca13d4409bdf", "f5ddb80e-1041-475a-bee8-c5cc3cab94c5", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "ce382ba0-e99b-403a-b0c3-2396a8f8fc31", "956ef7f8-3f85-4226-b3a9-77eb0b1ae00a", true });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "ImportanceLevel", "SystemId", "Title" },
                values: new object[,]
                {
                    { 96, 2, 4, "Series Category Added" },
                    { 100, 2, 4, "Series Comment Deleted" },
                    { 99, 2, 4, "Series Comment Added" },
                    { 98, 2, 4, "Series Category Updated" },
                    { 97, 2, 4, "Series Category Deleted" },
                    { 101, 1, 4, "Series Comment Updated" }
                });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936d4dfc-5536-4d5f-af2a-304d4fe4f518");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-4355-af3a-304a4fe4f518");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-5466-af3a-3b4a4424d518");

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "8a07f633-6072-435a-9441-4224fd5c8ad6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "f4153925-1430-4e6b-9fd7-d66609ae0a2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "86e74139-ce10-4308-aac9-1c95fd609aa7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "56b7699c-9e27-4696-a014-45e5c4c3b9c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "91e9adf5-90be-4949-b2d7-dce76df05641");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "907405c6-ac42-42e4-a4cc-e725e2c54728", "ac30a3b3-6cdc-457e-ad2e-907436e46460", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "a81fb72c-f78d-48fd-bd7a-0a46a8c56903", "d6a827de-1c6e-49f6-a679-179ff31239c0", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "a0f1bedb-ed75-48f1-8294-69a2f39f61be", "afb62518-e278-426f-8c1d-79bbb20611f6", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "a0d77cd7-b9c9-49b2-b6a4-8b0ded6c52d1", "f4971c6a-2185-4660-b6b2-237756d9b92e", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }
    }
}
