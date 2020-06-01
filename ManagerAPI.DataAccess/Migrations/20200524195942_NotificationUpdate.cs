using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class NotificationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationSystems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ShortName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    ImportanceLevel = table.Column<int>(nullable: false),
                    SystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTypes_NotificationSystems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "NotificationSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(maxLength: 256, nullable: false),
                    SentDate = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<string>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false, defaultValue: false),
                    Archived = table.Column<bool>(nullable: false, defaultValue: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "fd0bcc81-b150-4024-9aaf-bb2f76fcc413");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "c6c3f271-d2fa-4aec-9518-5ce609df57ac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "d4325be6-234c-4840-9f3c-f754280c56d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "c0ed15ff-ec19-4b27-88ad-5722a7f136e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "29e54655-0207-4261-b2b2-0ccc2e7c1285");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "49088773-cf31-46fa-96bb-bc463f77a98a", "a4c51be8-f020-48d2-9ac0-fb3f517982f8", null, true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "ee654dd4-f4d4-46c2-b0eb-52bc64f05cac", "552c8e7d-da35-47f3-a709-d64e4d7670c9", null, true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "5ce928d3-80df-405f-bc62-37e07c8a1ebd", "023b4be8-7ccc-4c68-9281-3151182365f5", null, true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "c435044a-5fe7-4dd4-88cf-f41f9950513e", "89cab713-c296-4427-a070-f9ec7573b17e", null, true });

            migrationBuilder.InsertData(
                table: "NotificationSystems",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "System", "Sys" },
                    { 2, "Event Manager", "EM" },
                    { 3, "Plan Manager", "PM" },
                    { 4, "Movie Corner", "MC" },
                    { 5, "Work Manager", "WM" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "ImportanceLevel", "SystemId", "Title" },
                values: new object[,]
                {
                    { 1, 2, 1, "Login" },
                    { 22, 3, 2, "Removed From An Event" },
                    { 23, 2, 2, "Event Evolved To Sport Event" },
                    { 24, 2, 2, "Event Evolved To GT Event" },
                    { 25, 1, 2, "Event Date Changed" },
                    { 26, 2, 2, "Event Role Added" },
                    { 27, 2, 2, "Event Role Updated" },
                    { 28, 3, 2, "Event Role Deleted" },
                    { 29, 2, 2, "Event Role Added To A User" },
                    { 30, 2, 2, "Role Added In An Event" },
                    { 31, 2, 2, "Event Role Removed From A User" },
                    { 32, 2, 2, "Role Removed In An Event" },
                    { 33, 2, 2, "Event ToDo Added" },
                    { 34, 2, 2, "Event ToDo Deleted" },
                    { 35, 1, 2, "Event ToDo Updated" },
                    { 36, 3, 2, "Event PayOut Added" },
                    { 21, 3, 2, "Event Member Removed" },
                    { 20, 1, 2, "Decline Event Invitation" },
                    { 19, 1, 2, "Accept Event Invitation" },
                    { 18, 1, 2, "Invited To An Event" },
                    { 2, 3, 1, "Registration" },
                    { 3, 1, 1, "Logout" },
                    { 4, 3, 1, "My Profile Updated" },
                    { 5, 1, 1, "Message Arrived" },
                    { 6, 2, 1, "ToDo Added" },
                    { 7, 2, 1, "ToDo Deleted" },
                    { 8, 1, 1, "ToDo Updated" },
                    { 37, 3, 2, "Event PayOut Deleted" },
                    { 9, 3, 2, "Event Created" },
                    { 11, 2, 2, "Event Published" },
                    { 12, 1, 2, "Event Locked" },
                    { 13, 2, 2, "Event Updated" },
                    { 14, 1, 2, "Event Message Arrived" },
                    { 15, 2, 2, "Event Member Invited" },
                    { 16, 2, 2, "Invitation Accepted" },
                    { 17, 2, 2, "Invitation Declined" },
                    { 10, 3, 2, "Event Disabled" },
                    { 38, 3, 2, "Event PayOut Updated" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OwnerId",
                table: "Notifications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TypeId",
                table: "Notifications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTypes_SystemId",
                table: "NotificationTypes",
                column: "SystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "NotificationSystems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "0ed42660-de47-4716-9772-4713ab8ee045");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "8bbe4d8e-d4da-45f6-879a-2820a0595a1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "556ad3e2-1b87-4b86-a4b3-aff4026f5ac3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "2d6b1694-c67c-4e86-adbe-b0b71f91596f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "98cdd0dc-94f8-4037-9fed-ab7bf17e312f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "b871abb4-ed81-4741-96be-c36ec31845de", "69ebc969-d130-4641-b026-d8e82e5d586a", 0, true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "e1103253-28cd-4652-93c3-9af429b5b21b", "cce5cee6-c976-483a-82c6-b0ed46a377ca", 0, true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "50f6cc5e-1079-4b7f-aab0-a57cf2d84ce5", "58fe9863-ff36-4e6e-9934-f373b37f7b81", 0, true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "GenderId", "IsActive" },
                values: new object[] { "aa861ede-3bce-4330-a3d4-ba16dd2e415b", "2058103b-da33-40ef-b70b-327112f03c68", 0, true });
        }
    }
}
