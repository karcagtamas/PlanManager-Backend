using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class SeriesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddOn",
                table: "UserSeriesSwitch");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "UserSeriesSwitch",
                nullable: true,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdded",
                table: "UserSeriesSwitch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "UserSeriesSwitch",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Series",
                maxLength: 999,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Series",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Series",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                table: "Series",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "MovieComments",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Episodes",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Episodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Episodes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Episodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdaterId",
                table: "Episodes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Episodes",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SeriesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeriesComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Comment = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesComments_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeriesSeriesCategoriesSwitch",
                columns: table => new
                {
                    SeriesId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesSeriesCategoriesSwitch", x => new { x.SeriesId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_SeriesSeriesCategoriesSwitch_SeriesCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "SeriesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeriesSeriesCategoriesSwitch_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "SeriesCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Drama" },
                    { 2, "Action" },
                    { 3, "Romantic" },
                    { 4, "Sci-fi" }
                });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_LastUpdaterId",
                table: "Episodes",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesComments_SeriesId",
                table: "SeriesComments",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesComments_UserId",
                table: "SeriesComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesSeriesCategoriesSwitch_CategoryId",
                table: "SeriesSeriesCategoriesSwitch",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_AspNetUsers_LastUpdaterId",
                table: "Episodes",
                column: "LastUpdaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_AspNetUsers_LastUpdaterId",
                table: "Episodes");

            migrationBuilder.DropTable(
                name: "SeriesComments");

            migrationBuilder.DropTable(
                name: "SeriesSeriesCategoriesSwitch");

            migrationBuilder.DropTable(
                name: "SeriesCategories");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_LastUpdaterId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "UserSeriesSwitch");

            migrationBuilder.DropColumn(
                name: "IsAdded",
                table: "UserSeriesSwitch");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "UserSeriesSwitch");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "LastUpdaterId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Episodes");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddOn",
                table: "UserSeriesSwitch",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Series",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 999,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "MovieComments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Episodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

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
    }
}
