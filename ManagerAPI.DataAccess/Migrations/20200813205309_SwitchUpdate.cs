using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class SwitchUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookSwitch_Books_BookId",
                table: "UserBookSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookSwitch_AspNetUsers_UserId",
                table: "UserBookSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovieSwitch_Movies_MovieId",
                table: "UserMovieSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovieSwitch_AspNetUsers_UserId",
                table: "UserMovieSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSeriesSwitch_Series_SeriesId",
                table: "UserSeriesSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSeriesSwitch_AspNetUsers_UserId",
                table: "UserSeriesSwitch");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "0db2c86f-1c0c-49bd-b627-037d60e10948");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "126a3e74-fd34-427c-98c7-e092cec6e855");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "5606a0d4-78fd-4f02-834b-83898b29f5af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "5e684fc4-609c-4336-9c0c-d039bf4a19af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "a7c8b507-ad71-4fa3-af99-3ada08230f93");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "7fdfcf2f-0c02-4190-824b-7343b639c114", "dcf71087-3aeb-4daf-9f85-479fed6b32f3", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "fa805f22-dc00-41a2-9de0-7dfb28ba9201", "3ace5120-ceb9-4761-95e0-e626c353d11c", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "2435014a-992e-4e00-a61e-1ce2b37dab8c", "293314ec-7136-45fc-9762-0c068bb1d051", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "d3980906-44e6-41ac-8264-5a9eec9689ed", "dc5882b9-07f1-4af9-a4d0-5cb5076f8d50", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookSwitch_Books_BookId",
                table: "UserBookSwitch",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookSwitch_AspNetUsers_UserId",
                table: "UserBookSwitch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovieSwitch_Movies_MovieId",
                table: "UserMovieSwitch",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovieSwitch_AspNetUsers_UserId",
                table: "UserMovieSwitch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSeriesSwitch_Series_SeriesId",
                table: "UserSeriesSwitch",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSeriesSwitch_AspNetUsers_UserId",
                table: "UserSeriesSwitch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookSwitch_Books_BookId",
                table: "UserBookSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookSwitch_AspNetUsers_UserId",
                table: "UserBookSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovieSwitch_Movies_MovieId",
                table: "UserMovieSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovieSwitch_AspNetUsers_UserId",
                table: "UserMovieSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSeriesSwitch_Series_SeriesId",
                table: "UserSeriesSwitch");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSeriesSwitch_AspNetUsers_UserId",
                table: "UserSeriesSwitch");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "b54f74a2-db66-4f2f-bb74-e5ffbc5f5d4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "d73461a3-5575-4eeb-89bd-6d2f719b0764");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "30282300-baa8-40b4-8f15-45d94c0204c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "e91c6348-e7a5-4bb1-8ce2-5ea9fb515b2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "ff287b60-e124-4389-9257-241bdeb1d776");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "97495a46-337d-4d22-9a49-4ce91ab2c506", "8650b24f-25bb-496a-8b7a-56fda4adf745", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "de6e228d-5ac0-4131-9619-5659fd03f822", "652a6b62-bd08-4d3b-a954-ffa926eff76e", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "3556a519-18fe-40ab-92e8-00aa7c5d219e", "d7ffc5dc-de1d-4321-8024-58190ac57522", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "911d61d8-7e1b-4877-a7be-3d0fd0204843", "ea03ba34-a3ee-4c3f-9b18-170abad73965", true });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookSwitch_Books_BookId",
                table: "UserBookSwitch",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookSwitch_AspNetUsers_UserId",
                table: "UserBookSwitch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovieSwitch_Movies_MovieId",
                table: "UserMovieSwitch",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovieSwitch_AspNetUsers_UserId",
                table: "UserMovieSwitch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSeriesSwitch_Series_SeriesId",
                table: "UserSeriesSwitch",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSeriesSwitch_AspNetUsers_UserId",
                table: "UserSeriesSwitch",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
