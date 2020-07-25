using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class MCUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Author = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Publish = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    LastUpdaterId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 999, nullable: true),
                    Year = table.Column<int>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    LastUpdaterId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StartYear = table.Column<int>(nullable: true),
                    EndYear = table.Column<int>(nullable: true),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreatorId = table.Column<string>(nullable: false),
                    LastUpdaterId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBookSwitch",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Read = table.Column<bool>(nullable: false, defaultValue: false),
                    ReadOn = table.Column<DateTime>(nullable: true),
                    AddOn = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookSwitch", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_UserBookSwitch_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBookSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMovieSwitch",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Seen = table.Column<bool>(nullable: false, defaultValue: false),
                    SeenOn = table.Column<DateTime>(nullable: true),
                    AddOn = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMovieSwitch", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserMovieSwitch_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMovieSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    SeriesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSeriesSwitch",
                columns: table => new
                {
                    SeriesId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    AddOn = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSeriesSwitch", x => new { x.UserId, x.SeriesId });
                    table.ForeignKey(
                        name: "FK_UserSeriesSwitch_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSeriesSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SeasonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEpisodeSwitch",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    EpisodeId = table.Column<int>(nullable: false),
                    Seen = table.Column<bool>(nullable: false, defaultValue: false),
                    SeenOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEpisodeSwitch", x => new { x.UserId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_UserEpisodeSwitch_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEpisodeSwitch_AspNetUsers_UserId",
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
                value: "e4e74e41-8042-4c33-bc3e-44d6a250c020");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "b8cfdaa8-db89-4304-8a71-5baa19cb66e0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "9ffff46f-fd86-406c-b691-fc893746b36c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "45c02fe5-19c9-4d11-b807-8d5a565f831f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "c2bbfe30-3089-4c02-9d13-ceb6af72ee46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "bd2e2fbf-4611-491a-9fe5-b80b304b51dc", "6980522e-0f1e-4527-abf0-97435227e5be", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "c45c6287-8c47-4175-aab9-229f5c7426dd", "a7a37e58-4d93-4d44-a4f2-0bc51ea0bc4d", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "9774bcf4-f258-495a-85e0-9d18aeb7d7ec", "353ecfed-4239-45d6-989c-e04ce950c128", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "a74ce1f0-d309-435b-b149-6f5983940837", "12ddfdc2-7c07-4d0d-9010-2db09583529a", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatorId",
                table: "Books",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LastUpdaterId",
                table: "Books",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CreatorId",
                table: "Movies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_LastUpdaterId",
                table: "Movies",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SeriesId",
                table: "Seasons",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_CreatorId",
                table: "Series",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_LastUpdaterId",
                table: "Series",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookSwitch_BookId",
                table: "UserBookSwitch",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEpisodeSwitch_EpisodeId",
                table: "UserEpisodeSwitch",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovieSwitch_MovieId",
                table: "UserMovieSwitch",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSeriesSwitch_SeriesId",
                table: "UserSeriesSwitch",
                column: "SeriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBookSwitch");

            migrationBuilder.DropTable(
                name: "UserEpisodeSwitch");

            migrationBuilder.DropTable(
                name: "UserMovieSwitch");

            migrationBuilder.DropTable(
                name: "UserSeriesSwitch");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Series");

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

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }
    }
}
