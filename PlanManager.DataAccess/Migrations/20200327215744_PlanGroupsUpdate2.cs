using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanManager.DataAccess.Migrations
{
    public partial class PlanGroupsUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanGroups_AspNetUsers_LastUpdaterId",
                table: "PlanGroups");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdaterId",
                table: "PlanGroups",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanGroups_AspNetUsers_LastUpdaterId",
                table: "PlanGroups",
                column: "LastUpdaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanGroups_AspNetUsers_LastUpdaterId",
                table: "PlanGroups");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdaterId",
                table: "PlanGroups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanGroups_AspNetUsers_LastUpdaterId",
                table: "PlanGroups",
                column: "LastUpdaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
