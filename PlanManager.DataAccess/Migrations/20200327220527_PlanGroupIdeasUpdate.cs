using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanManager.DataAccess.Migrations
{
    public partial class PlanGroupIdeasUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanGroupIdeas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(maxLength: 200, nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanGroupIdeas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanGroupIdeas_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanGroupIdeas_PlanGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "PlanGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupIdeas_CreatorId",
                table: "PlanGroupIdeas",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupIdeas_GroupId",
                table: "PlanGroupIdeas",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanGroupIdeas");
        }
    }
}
