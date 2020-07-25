using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class WorkingManagerDayUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "WorkingDays");

            migrationBuilder.DropColumn(
                name: "EndMin",
                table: "WorkingDays");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "WorkingDays");

            migrationBuilder.DropColumn(
                name: "StartMin",
                table: "WorkingDays");

            migrationBuilder.AlterColumn<decimal>(
                name: "Length",
                table: "WorkingFields",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "0bd23f08-6141-49f6-b9b6-8a630c38e7ac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "168e567c-d7b5-4122-b7d2-ec351f8fc8ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "517271d4-0fc1-4e91-83fe-7e1ba2e2883d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "68b5ce87-e347-48b3-b04e-922df4ca43bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "6cc146f2-3ed7-49ca-9876-7756d9a074bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "177b1725-a393-46e5-a2a7-a186ea022bdc", "09fd1c1d-6548-4541-92d4-a88ed0c1a0e8", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "78a6e6b0-62e8-4300-8cff-3ef128750411", "f7b6094a-9973-47c5-aa0b-d4192a3c86c4", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "6b6839d9-7385-41e0-8bd2-ceb65b835b09", "185ec711-4d7d-440b-9f08-e2e799f71ba8", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "e2b156ae-e1dd-4647-a2a2-875b70e120e2", "4dd07869-d4d3-4bbd-9cc5-f91b4cb916d7", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "WorkingFields",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "WorkingDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndMin",
                table: "WorkingDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "WorkingDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartMin",
                table: "WorkingDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "52560d5e-f482-4bcc-9995-f222bc445bd9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "37788415-b6fc-46aa-a65c-e828df7d3d77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "6bb33b58-c47e-44f7-a031-4b6006e1e94a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "3d03784d-feb2-4cfc-85cd-c898bc9dfa2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "699cfdbf-5d62-4d98-bf6b-e8b1e344c5d9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "54255da3-91c7-47a8-b83f-1d88fcb40173", "f24b18bd-fbb2-454b-95ea-7cf6125b418b", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "73876d9e-06ca-4b80-869b-d7f414bef8c9", "c311abab-6565-47aa-a5b1-47e2751a0038", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "2e394b2d-6a13-4c8e-b8dc-34b32bf9095f", "eb4938bd-73d5-4ce0-9e38-acd46e20d9d0", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "5a8334a3-b1bc-4a39-9d8c-51153abfc16a", "244d3846-1202-4920-b7f1-19fb54c43873", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }
    }
}
