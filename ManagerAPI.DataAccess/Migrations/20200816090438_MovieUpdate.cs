using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerAPI.DataAccess.Migrations
{
    public partial class MovieUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Movies",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                table: "Movies",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieMovieCategorySwitch",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieMovieCategorySwitch", x => new { x.MovieId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_MovieMovieCategorySwitch_MovieCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MovieCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieMovieCategorySwitch_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9",
                column: "ConcurrencyStamp",
                value: "80c4c6ec-1b2a-4a7e-8393-5094b37e3952");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e0a9192-793f-4c85-a0b1-3198295bf409",
                column: "ConcurrencyStamp",
                value: "d96f44a8-22b4-4eb2-9276-f92cb8c56474");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "776474d7-8d01-4809-963e-c721f39dbb45",
                column: "ConcurrencyStamp",
                value: "fce12f9b-bd3c-4287-a0b9-f42e54e9a6bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "936e42dc-5d3f-4355-bc3a-304a4fe4f518",
                column: "ConcurrencyStamp",
                value: "0ee08dc2-e44a-4de5-8f91-7d2316c371c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa5deb78-59c2-4faa-83dc-6c3369eedf20",
                column: "ConcurrencyStamp",
                value: "611242ac-4b16-4ca4-a1e7-3902c5d4f7fd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44045506-66fd-4af8-9d59-133c47d1787c",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "3ee6e720-516b-4b01-bf03-d9fecbbddb85", "cddbd593-c2bd-4f2f-9a39-9578f8c833d6", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd5e5069-59c8-4163-95c5-776fab95e51a",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "6325f43e-f531-45e4-bdcf-60b4ee5dee92", "9bc06d38-e22c-4360-b4ee-d2313e8f0738", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8237fac-c6dc-47b0-8f71-b72f93368b02",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "5678493d-194a-4b21-82c2-a7c8eabc3492", "58739a95-a96d-4fe7-a7c9-b57ec5d349e6", true });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa2edf69-5fc8-a163-9fc5-726f3b94e51b",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "IsActive" },
                values: new object[] { "c2e8a5f2-f4ed-42fd-8286-a90c404d8b17", "a2e223d2-691f-4f0e-995a-e46778719aaa", true });

            migrationBuilder.InsertData(
                table: "MovieCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Drama" },
                    { 2, "Action" },
                    { 3, "Romantic" },
                    { 4, "Sci-fi" }
                });

            migrationBuilder.UpdateData(
                table: "NotificationSystems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Status Library", "SL" });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieMovieCategorySwitch_CategoryId",
                table: "MovieMovieCategorySwitch",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieMovieCategorySwitch");

            migrationBuilder.DropTable(
                name: "MovieCategories");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                table: "NotificationSystems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "ShortName" },
                values: new object[] { "Movie Corner", "MC" });

            migrationBuilder.UpdateData(
                table: "WorkingDayTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayIsActive",
                value: true);
        }
    }
}
