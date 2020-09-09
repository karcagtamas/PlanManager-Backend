using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class CSMUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Csomors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 120, nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    OwnerId = table.Column<string>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    LastUpdaterId = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    Finish = table.Column<DateTime>(nullable: false),
                    MaxWorkHour = table.Column<int>(nullable: false),
                    MinRestHour = table.Column<int>(nullable: false),
                    IsShared = table.Column<bool>(nullable: false, defaultValue: false),
                    IsPublic = table.Column<bool>(nullable: false, defaultValue: false),
                    HasGeneratedCsomor = table.Column<bool>(nullable: false, defaultValue: false),
                    LastGeneration = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Csomors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Csomors_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Csomors_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CsomorPersons",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    CsomorId = table.Column<int>(nullable: false),
                    PlusWorkCounter = table.Column<int>(nullable: false, defaultValue: 0),
                    IsIgnored = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsomorPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CsomorPersons_Csomors_CsomorId",
                        column: x => x.CsomorId,
                        principalTable: "Csomors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CsomorWorks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    CsomorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsomorWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CsomorWorks_Csomors_CsomorId",
                        column: x => x.CsomorId,
                        principalTable: "Csomors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SharedCsomors",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    CsomorId = table.Column<int>(nullable: false),
                    SharedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    HasWriteAccess = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedCsomors", x => new { x.UserId, x.CsomorId });
                    table.ForeignKey(
                        name: "FK_SharedCsomors_Csomors_CsomorId",
                        column: x => x.CsomorId,
                        principalTable: "Csomors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SharedCsomors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CsomorPersonTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<string>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false, defaultValue: false),
                    WorkId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsomorPersonTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CsomorPersonTables_CsomorPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "CsomorPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CsomorPersonTables_CsomorWorks_WorkId",
                        column: x => x.WorkId,
                        principalTable: "CsomorWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CsomorWorkTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkId = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    PersonId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsomorWorkTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CsomorWorkTables_CsomorPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "CsomorPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CsomorWorkTables_CsomorWorks_WorkId",
                        column: x => x.WorkId,
                        principalTable: "CsomorWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IgnoredWorks",
                columns: table => new
                {
                    PersonId = table.Column<string>(nullable: false),
                    WorkId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoredWorks", x => new { x.PersonId, x.WorkId });
                    table.ForeignKey(
                        name: "FK_IgnoredWorks_CsomorPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "CsomorPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IgnoredWorks_CsomorWorks_WorkId",
                        column: x => x.WorkId,
                        principalTable: "CsomorWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "ff12a29f-9e6d-4e77-9f98-d34197bce2d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "89359d82-1502-453c-815f-f85675875dc6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "22087df2-3052-44af-8405-915a59ba3467");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936d4dfc-5536-4d5f-af2a-304d4fe4f518",
                column: "ConcurrencyStamp",
                value: "dc832198-5768-4fa9-8c64-ded9317c9ba8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "901467c2-d611-452b-9464-eef991dbf8f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-4355-af3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "869529b1-1bb7-4bbf-b9be-1b27fbb3d954");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e4ddc-5d3f-5466-af3a-3b4a4424d518",
                column: "ConcurrencyStamp",
                value: "6f9e8c15-eed4-4547-af15-0944db57f9d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "3ca1f05a-d40e-4ab6-ba34-fa328c4c3cf3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "b790710a-a2f1-4548-99f5-2b82d6f55a0f", "85624367-e115-4127-9c14-866fea1c685f", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "19820ebf-c572-4f4a-b5bc-257b7bb66a0a", "c4bc1f98-d810-476f-ba99-2e798c7ec3e8", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "94dbf3dc-162d-4c6d-a1e9-71f6fce1de90", "05e1de8d-5e7e-4108-96da-83ba1f749d69", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "7b769d8f-2c7c-4b9e-8e12-ceb6ca652e6e", "1fe24392-0cc6-4fcb-b8f4-f0924f4e074d", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_CsomorPersons_CsomorId",
                table: "CsomorPersons",
                column: "CsomorId");

            migrationBuilder.CreateIndex(
                name: "IX_CsomorPersonTables_PersonId",
                table: "CsomorPersonTables",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CsomorPersonTables_WorkId",
                table: "CsomorPersonTables",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Csomors_LastUpdaterId",
                table: "Csomors",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Csomors_OwnerId",
                table: "Csomors",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CsomorWorks_CsomorId",
                table: "CsomorWorks",
                column: "CsomorId");

            migrationBuilder.CreateIndex(
                name: "IX_CsomorWorkTables_PersonId",
                table: "CsomorWorkTables",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CsomorWorkTables_WorkId",
                table: "CsomorWorkTables",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredWorks_WorkId",
                table: "IgnoredWorks",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedCsomors_CsomorId",
                table: "SharedCsomors",
                column: "CsomorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CsomorPersonTables");

            migrationBuilder.DropTable(
                name: "CsomorWorkTables");

            migrationBuilder.DropTable(
                name: "IgnoredWorks");

            migrationBuilder.DropTable(
                name: "SharedCsomors");

            migrationBuilder.DropTable(
                name: "CsomorPersons");

            migrationBuilder.DropTable(
                name: "CsomorWorks");

            migrationBuilder.DropTable(
                name: "Csomors");

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
    }
}
