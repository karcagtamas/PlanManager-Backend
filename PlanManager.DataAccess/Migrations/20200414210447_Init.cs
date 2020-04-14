using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanManager.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    AccessLevel = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: true, defaultValue: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    SecondaryEmail = table.Column<string>(nullable: true),
                    TShirtSize = table.Column<string>(maxLength: 6, nullable: true),
                    Allergy = table.Column<string>(nullable: true),
                    Group = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    AccessLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsLocked = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDisabled = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatorId = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdaterId = table.Column<string>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    StartHour = table.Column<int>(nullable: true),
                    EndHour = table.Column<int>(nullable: true),
                    StartMinute = table.Column<int>(nullable: true),
                    EndMinute = table.Column<int>(nullable: true)
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
                name: "PlanGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdaterId = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanGroups_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanGroups_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    OwnerId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    StartTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EndTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    PriorityLevel = table.Column<int>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false, defaultValue: false),
                    PlanTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plans_PlanTypes_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalTable: "PlanTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DGtEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    TShirtColor = table.Column<string>(nullable: false),
                    Greeny = table.Column<int>(nullable: true),
                    GreenyCost = table.Column<decimal>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    Injured = table.Column<int>(nullable: true),
                    Visitors = table.Column<int>(nullable: true),
                    VisitorLimit = table.Column<int>(nullable: true),
                    VisitorCost = table.Column<decimal>(nullable: true),
                    Players = table.Column<int>(nullable: true),
                    PlayerLimit = table.Column<int>(nullable: true),
                    PlayerCost = table.Column<decimal>(nullable: true),
                    PlayerDeposit = table.Column<decimal>(nullable: true),
                    Helpers = table.Column<int>(nullable: true),
                    HelperLimit = table.Column<int>(nullable: true),
                    FixTeamDeposit = table.Column<decimal>(nullable: true),
                    FixTeamCost = table.Column<decimal>(nullable: true),
                    TeamLimit = table.Column<int>(nullable: true),
                    MatchJudges = table.Column<string>(nullable: true),
                    Doctors = table.Column<string>(nullable: true)
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
                name: "EventRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 120, nullable: false),
                    AccessLevel = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    ConnectionDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    AddedById = table.Column<string>(nullable: false)
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
                name: "PlanGroupChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: false),
                    SenderId = table.Column<string>(nullable: false),
                    Sent = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanGroupChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanGroupChatMessages_PlanGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "PlanGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanGroupChatMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "PlanGroupPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    OwnerId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdaterId = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    PriorityLevel = table.Column<int>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false, defaultValue: false),
                    PlanTypeId = table.Column<int>(nullable: true),
                    MarkedUserId = table.Column<string>(nullable: false),
                    MarkTypeId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanGroupPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlans_PlanGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "PlanGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlans_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlans_MarkTypes_MarkTypeId",
                        column: x => x.MarkTypeId,
                        principalTable: "MarkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlans_AspNetUsers_MarkedUserId",
                        column: x => x.MarkedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlans_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlans_PlanTypes_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalTable: "PlanTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPlanGroupsSwitch",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    Connection = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    AddedById = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlanGroupsSwitch", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserPlanGroupsSwitch_AspNetUsers_AddedById",
                        column: x => x.AddedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPlanGroupsSwitch_PlanGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "PlanGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlanGroupsSwitch_GroupRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "GroupRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPlanGroupsSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEventRolesSwitch",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    OwnershipDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    AddedById = table.Column<string>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PlanGroupPlanComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(maxLength: 256, nullable: false),
                    SenderId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    PlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanGroupPlanComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlanComments_PlanGroupPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "PlanGroupPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanGroupPlanComments_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "AccessLevel" },
                values: new object[,]
                {
                    { "fa5deb78-59c2-4faa-83dc-6c3369eedf20", "e382b838-5178-4c53-8009-bacc0ea6d7ff", "WebsiteRole", "Root", "ROOT", 4 },
                    { "5e0a9192-793f-4c85-a0b1-3198295bf409", "2e9c901a-9240-40c7-8980-61512571fb81", "WebsiteRole", "Moderator", "MODERATOR", 2 },
                    { "776474d7-8d01-4809-963e-c721f39dbb45", "e787c556-128a-42b2-871e-b7ac0e9482ea", "WebsiteRole", "Normal", "NORMAL", 1 },
                    { "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9", "518d6845-67bf-4c11-b788-cd814eda6296", "WebsiteRole", "Visitor", "VISITOR", 0 },
                    { "936e42dc-5d3f-4355-bc3a-304a4fe4f518", "6f62872c-f680-4e86-8033-8e92f641c2fb", "WebsiteRole", "Administrator", "ADMINISTRATOR", 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Allergy", "FullName", "Group", "IsActive", "SecondaryEmail", "TShirtSize" },
                values: new object[,]
                {
                    { "fa2edf69-5fc8-a163-9fc5-726f3b94e51b", 0, "c3e0777d-3ccd-4f94-8fa9-1f5070bc5a0a", "User", "barni.pbs@gmail.com", true, false, null, "BARNI.PBS@GMAIL.COM", "BARNI363HUN", "AQAAAAEAACcQAAAAEL9QeDNFqEAq8WDl2/fXBSc02Tzxxnek963ILEw1L3aQsFysXXG4L3KvFYIVg/LpLA==", null, false, "3cbbf4d5-52bb-4beb-84c5-cd7cd8af4087", false, "barni363hun", null, "Root", null, true, null, null },
                    { "cd5e5069-59c8-4163-95c5-776fab95e51a", 0, "ce01bcdd-32bd-4e34-bcf9-e1c94210d844", "User", "root@karcags.hu", true, false, null, "ROOT@KARCAGS.HU", "ROOT", "AQAAAAEAACcQAAAAEHdK+ODabrjejNLGhod4ftL37G5zT97p2g0Ck5dH9MchA2B/JFDiwb9kk9soZBPF5Q==", null, false, "6327b3a7-642c-40e1-b0f2-04be0e8227ea", false, "root", null, "Root", null, true, null, null },
                    { "f8237fac-c6dc-47b0-8f71-b72f93368b02", 0, "8c2a2365-32a7-42a0-b924-580bc96ec310", "User", "aron.klenovszky@gmail.com", true, false, null, "ARON.KLENOVSZKY@GMAIL.COM", "AARONKAA", "AQAAAAEAACcQAAAAEL9QeDNFqEAq8WDl2/fXBSc02Tzxxnek963ILEw1L3aQsFysXXG4L3KvFYIVg/LpLA==", null, false, "0b833dbb-eae3-4b7c-bdd6-9a5e70d1e89c", false, "aaronkaa", null, "Klenovszky Áron", null, true, null, null },
                    { "44045506-66fd-4af8-9d59-133c47d1787c", 0, "1597aa99-2268-49e3-84fe-6aef094818b6", "User", "karcagtamas@outlook.com", true, false, null, "KARCAGTAMAS@OUTLOOK.COM", "KARCAGTAMAS", "AQAAAAEAACcQAAAAEG9SljY4ow/I7990YZ15dSGvCesg0bad3pQSWi4ekt0RT8J5JuL3lQmNJCnxo2lGIA==", null, false, "4acf51e9-3987-4440-b041-c55cbd766c80", false, "karcagtamas", null, "Karcag Tamas", null, true, null, null }
                });

            migrationBuilder.InsertData(
                table: "GroupRoles",
                columns: new[] { "Id", "AccessLevel", "Title" },
                values: new object[,]
                {
                    { 1, 0, "Visitor" },
                    { 6, 5, "Owner" },
                    { 5, 4, "Administrator" },
                    { 4, 3, "Moderator" },
                    { 3, 2, "Editor" },
                    { 2, 1, "Normal" }
                });

            migrationBuilder.InsertData(
                table: "MarkTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 3, "Modifier" },
                    { 2, "Owner" },
                    { 1, "Responsible" },
                    { 4, "Leader" }
                });

            migrationBuilder.InsertData(
                table: "PlanTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Plan" },
                    { 2, "Future Idea" },
                    { 4, "Learning" },
                    { 5, "Decision" },
                    { 6, "Event" },
                    { 3, "Nice To Have" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_EventRoles_EventId",
                table: "EventRoles",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterEvents_CreatorId",
                table: "MasterEvents",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterEvents_LastUpdaterId",
                table: "MasterEvents",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupChatMessages_GroupId",
                table: "PlanGroupChatMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupChatMessages_SenderId",
                table: "PlanGroupChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupIdeas_CreatorId",
                table: "PlanGroupIdeas",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupIdeas_GroupId",
                table: "PlanGroupIdeas",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlanComments_PlanId",
                table: "PlanGroupPlanComments",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlanComments_SenderId",
                table: "PlanGroupPlanComments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlans_GroupId",
                table: "PlanGroupPlans",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlans_LastUpdaterId",
                table: "PlanGroupPlans",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlans_MarkTypeId",
                table: "PlanGroupPlans",
                column: "MarkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlans_MarkedUserId",
                table: "PlanGroupPlans",
                column: "MarkedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlans_OwnerId",
                table: "PlanGroupPlans",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroupPlans_PlanTypeId",
                table: "PlanGroupPlans",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroups_CreatorId",
                table: "PlanGroups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanGroups_LastUpdaterId",
                table: "PlanGroups",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_OwnerId",
                table: "Plans",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlanTypeId",
                table: "Plans",
                column: "PlanTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserPlanGroupsSwitch_AddedById",
                table: "UserPlanGroupsSwitch",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlanGroupsSwitch_GroupId",
                table: "UserPlanGroupsSwitch",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlanGroupsSwitch_RoleId",
                table: "UserPlanGroupsSwitch",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DGtEvents");

            migrationBuilder.DropTable(
                name: "DSportEvents");

            migrationBuilder.DropTable(
                name: "PlanGroupChatMessages");

            migrationBuilder.DropTable(
                name: "PlanGroupIdeas");

            migrationBuilder.DropTable(
                name: "PlanGroupPlanComments");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "UserEventRolesSwitch");

            migrationBuilder.DropTable(
                name: "UserEventsSwitch");

            migrationBuilder.DropTable(
                name: "UserPlanGroupsSwitch");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PlanGroupPlans");

            migrationBuilder.DropTable(
                name: "EventRoles");

            migrationBuilder.DropTable(
                name: "GroupRoles");

            migrationBuilder.DropTable(
                name: "PlanGroups");

            migrationBuilder.DropTable(
                name: "MarkTypes");

            migrationBuilder.DropTable(
                name: "PlanTypes");

            migrationBuilder.DropTable(
                name: "MasterEvents");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
