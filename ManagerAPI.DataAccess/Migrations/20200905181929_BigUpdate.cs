using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class BigUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DGtEvents");

            migrationBuilder.DropTable(
                name: "DSportEvents");

            migrationBuilder.DropTable(
                name: "EventActions");

            migrationBuilder.DropTable(
                name: "UserEventRolesSwitch");

            migrationBuilder.DropTable(
                name: "UserEventsSwitch");

            migrationBuilder.DropTable(
                name: "EventRoles");

            migrationBuilder.DropTable(
                name: "MasterEvents");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "2f104c41-4b53-489f-92c0-f8094a6fa361");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "e12462f7-1824-4230-a327-5da559ca3282");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "948a109a-7fb1-4219-aef7-6eaf9cc27050");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936d4dfc-5536-4d5f-af2a-304d4fe4f518",
                column: "ConcurrencyStamp",
                value: "0ab7d8e9-9710-428b-b803-3f09df87adc5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "1608505e-4d51-4bad-9cf1-9ab2bbeac07c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-4355-af3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "3d4a49bb-aff4-402d-85a3-19367d1027ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-5466-af3a-3b4a4424d518",
                column: "ConcurrencyStamp",
                value: "74c1608d-e74b-4a16-bfbc-f1ac44e88dd3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "8ae73143-598c-44df-9cbd-906c0af8aeaa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "20744a3c-04af-4139-9df1-c5663f517c23", "683dabc6-e93a-49bf-8f8b-8ade33c5bba9", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "d005e05a-55fd-4bd4-b912-3a276974ec75", "4d745d31-a203-4b82-ad84-546b820fa693", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "202674cf-3a2c-4fb6-8126-9568d9229db8", "bbb23d9a-da28-4c29-8c49-6e8391ccff4b", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "b6f14862-2ae7-445b-ae3b-47c35e7f2a72", "3bc4810b-9303-4e8e-b42e-049baf3a5161", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LastUpdaterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterEvents_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterEvents_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DGtEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Greeny = table.Column<int>(type: "int", nullable: true),
                    GreenyCost = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    TShirtColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DGtEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DGtEvents_MasterEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "MasterEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DSportEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    FixTeamCost = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    FixTeamDeposit = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    HelperLimit = table.Column<int>(type: "int", nullable: true),
                    Helpers = table.Column<int>(type: "int", nullable: true),
                    Injured = table.Column<int>(type: "int", nullable: true),
                    MatchJudges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerCost = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    PlayerDeposit = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    PlayerLimit = table.Column<int>(type: "int", nullable: true),
                    Players = table.Column<int>(type: "int", nullable: true),
                    TeamLimit = table.Column<int>(type: "int", nullable: true),
                    VisitorCost = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    VisitorLimit = table.Column<int>(type: "int", nullable: true),
                    Visitors = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSportEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DSportEvents_MasterEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "MasterEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventActions_MasterEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "MasterEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventActions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRoles_MasterEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "MasterEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEventsSwitch",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEventsSwitch", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_UserEventsSwitch_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEventsSwitch_MasterEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "MasterEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEventsSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEventRolesSwitch",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AddedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnershipDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEventRolesSwitch", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserEventRolesSwitch_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEventRolesSwitch_EventRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EventRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEventRolesSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                keyValue: "936d4dfc-5536-4d5f-af2a-304d4fe4f518",
                column: "ConcurrencyStamp",
                value: "1649c6d9-452c-4b60-934c-4615a11f46fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "2bfd9695-8781-4b2b-bb62-0f9a805d4a02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-4355-af3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "2d4f30e7-2b3d-4f7c-9e32-613af7f42dd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-5466-af3a-3b4a4424d518",
                column: "ConcurrencyStamp",
                value: "947e400d-e610-48f3-bde3-c0b5e0b6496c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "d01c650c-0846-48af-9ade-e19a790ad888");

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

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_DGtEvents_EventId",
                table: "DGtEvents",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DSportEvents_EventId",
                table: "DSportEvents",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventActions_EventId",
                table: "EventActions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventActions_UserId",
                table: "EventActions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRoles_EventId",
                table: "EventRoles",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterEvents_CreatorId",
                table: "MasterEvents",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterEvents_LastUpdaterId",
                table: "MasterEvents",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEventRolesSwitch_AddedById",
                table: "UserEventRolesSwitch",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserEventRolesSwitch_RoleId",
                table: "UserEventRolesSwitch",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEventsSwitch_AddedById",
                table: "UserEventsSwitch",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserEventsSwitch_EventId",
                table: "UserEventsSwitch",
                column: "EventId");
        }
    }
}
