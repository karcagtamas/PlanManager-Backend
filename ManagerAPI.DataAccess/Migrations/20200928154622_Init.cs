using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ManagerAPI.DataAccess.Migrations
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
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
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
                name: "WorkingDayTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    DayIsActive = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDayTypes", x => x.Id);
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
                    Group = table.Column<string>(maxLength: 40, nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    ProfileImageTitle = table.Column<string>(nullable: true),
                    ProfileImageData = table.Column<byte[]>(nullable: true),
                    Country = table.Column<string>(maxLength: 120, nullable: true),
                    GenderId = table.Column<int>(nullable: true),
                    City = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Author = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Publish = table.Column<DateTime>(nullable: true),
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
                name: "FriendRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(nullable: false),
                    DestinationId = table.Column<string>(nullable: false),
                    SentDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Message = table.Column<string>(maxLength: 120, nullable: false),
                    Response = table.Column<bool>(nullable: true),
                    ResponseDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequests_AspNetUsers_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendRequests_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(400)", nullable: false),
                    SenderId = table.Column<string>(nullable: false),
                    ReceiverId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 999, nullable: true),
                    ReleaseYear = table.Column<int>(nullable: true),
                    Length = table.Column<int>(nullable: true),
                    Director = table.Column<string>(maxLength: 60, nullable: true),
                    TrailerUrl = table.Column<string>(maxLength: 200, nullable: true),
                    ImageTitle = table.Column<string>(maxLength: 100, nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
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
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(maxLength: 512, nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdaterId = table.Column<string>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_LastUpdaterId",
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
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 999, nullable: true),
                    StartYear = table.Column<int>(nullable: true),
                    EndYear = table.Column<int>(nullable: true),
                    TrailerUrl = table.Column<string>(maxLength: 200, nullable: true),
                    ImageTitle = table.Column<string>(maxLength: 100, nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
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
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsSolved = table.Column<bool>(nullable: false, defaultValue: false),
                    OwnerId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingDays_WorkingDayTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "WorkingDayTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkingDays_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    SentDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
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
                name: "Friends",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    FriendId = table.Column<string>(nullable: false),
                    ConnectionDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    RequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => new { x.UserId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_FriendRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "FriendRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    LastUpdate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Comment = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieComments_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "UserMovieSwitch",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false, defaultValue: false),
                    IsAdded = table.Column<bool>(nullable: false),
                    SeenOn = table.Column<DateTime>(nullable: true),
                    AddedOn = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Rate = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMovieSwitch", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserMovieSwitch_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMovieSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Description = table.Column<string>(maxLength: 512, nullable: true),
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

            migrationBuilder.CreateTable(
                name: "UserSeriesSwitch",
                columns: table => new
                {
                    SeriesId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    IsAdded = table.Column<bool>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Rate = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSeriesSwitch", x => new { x.UserId, x.SeriesId });
                    table.ForeignKey(
                        name: "FK_UserSeriesSwitch_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSeriesSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkingFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Length = table.Column<decimal>(nullable: false),
                    WorkingDayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingFields_WorkingDays_WorkingDayId",
                        column: x => x.WorkingDayId,
                        principalTable: "WorkingDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CsomorPersonTables",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
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
                    Id = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    SeasonId = table.Column<int>(nullable: false),
                    LastUpdaterId = table.Column<string>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_AspNetUsers_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEpisodeSwitch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "AccessLevel" },
                values: new object[,]
                {
                    { "fa5deb78-59c2-4faa-83dc-6c3369eedf20", "5fa9966b-92bb-498c-a892-6a83d90eb630", "WebsiteRole", "Root", "ROOT", 4 },
                    { "936e4ddc-5d3f-5466-af3a-3b4a4424d518", "4209642a-8daf-468b-b400-40cc0f5c1fac", "WebsiteRole", "Status Library Moderator", "STATUS LIBRARY MODERATOR", 3 },
                    { "936e4ddc-5d3f-4355-af3a-304a4fe4f518", "8c591ae9-c29a-4c3e-91eb-76c3ad3cb35f", "WebsiteRole", "Status Library User", "STATUS LIBRARY USER", 1 },
                    { "936e42dc-5d3f-4355-bc3a-304a4fe4f518", "491a02c2-9844-4f7a-8cfe-c0e422ee9991", "WebsiteRole", "Administrator", "ADMINISTRATOR", 3 },
                    { "5e0a9192-793f-4c85-a0b1-3198295bf409", "77c7e1fc-b7b6-4cd7-8244-b18eb3a13f60", "WebsiteRole", "Moderator", "MODERATOR", 2 },
                    { "776474d7-8d01-4809-963e-c721f39dbb45", "0d1a3e75-7a9c-4d93-b6ba-d18b74a12117", "WebsiteRole", "Normal", "NORMAL", 1 },
                    { "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9", "ac92ea20-75bd-40bd-b1b3-2c7721f5f24f", "WebsiteRole", "Visitor", "VISITOR", 0 },
                    { "936d4dfc-5536-4d5f-af2a-304d4fe4f518", "cabaad41-bdf8-4464-ba7a-dff9cfb4dd13", "WebsiteRole", "Status Library Administrator", "STATUS LIBRARY ADMINISTRATOR", 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Allergy", "BirthDay", "City", "Country", "FullName", "GenderId", "Group", "IsActive", "ProfileImageData", "ProfileImageTitle", "SecondaryEmail", "TShirtSize" },
                values: new object[,]
                {
                    { "fa2edf69-5fc8-a163-9fc5-726f3b94e51b", 0, "6eca96cd-81f1-44e3-bb16-41ce06b14792", "User", "barni.pbs@gmail.com", true, false, null, "BARNI.PBS@GMAIL.COM", "BARNI363HUN", "AQAAAAEAACcQAAAAEL9QeDNFqEAq8WDl2/fXBSc02Tzxxnek963ILEw1L3aQsFysXXG4L3KvFYIVg/LpLA==", null, false, "d7d39c56-cff0-4f78-88fb-50906ffde5de", false, "barni363hun", null, null, null, null, "Root", null, null, true, null, null, null, null },
                    { "cd5e5069-59c8-4163-95c5-776fab95e51a", 0, "5b3afc34-1754-4c65-8f68-c3920c89350f", "User", "root@karcags.hu", true, false, null, "ROOT@KARCAGS.HU", "ROOT", "AQAAAAEAACcQAAAAEHdK+ODabrjejNLGhod4ftL37G5zT97p2g0Ck5dH9MchA2B/JFDiwb9kk9soZBPF5Q==", null, false, "586abc6c-4a1e-4567-91f8-3ec6cb818cc3", false, "root", null, null, null, null, "Root", null, null, true, null, null, null, null },
                    { "f8237fac-c6dc-47b0-8f71-b72f93368b02", 0, "f1202b71-fd6c-4c2b-89b3-5101c7b5d4e6", "User", "aron.klenovszky@gmail.com", true, false, null, "ARON.KLENOVSZKY@GMAIL.COM", "AARONKAA", "AQAAAAEAACcQAAAAEL9QeDNFqEAq8WDl2/fXBSc02Tzxxnek963ILEw1L3aQsFysXXG4L3KvFYIVg/LpLA==", null, false, "41c3d685-b28b-4de7-a57e-444247fd1658", false, "aaronkaa", null, null, null, null, "Klenovszky Áron", null, null, true, null, null, null, null },
                    { "44045506-66fd-4af8-9d59-133c47d1787c", 0, "8f9d1437-c8d5-42a5-8382-c258556bc740", "User", "karcagtamas@outlook.com", true, false, null, "KARCAGTAMAS@OUTLOOK.COM", "KARCAGTAMAS", "AQAAAAEAACcQAAAAEG9SljY4ow/I7990YZ15dSGvCesg0bad3pQSWi4ekt0RT8J5JuL3lQmNJCnxo2lGIA==", null, false, "76a53ee0-fdc2-499e-8e27-f36ea0246c54", false, "karcagtamas", null, null, null, null, "Karcag Tamas", null, null, true, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 3, "Other" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "GroupRoles",
                columns: new[] { "Id", "AccessLevel", "Title" },
                values: new object[,]
                {
                    { 6, 5, "Owner" },
                    { 1, 0, "Visitor" },
                    { 5, 4, "Administrator" },
                    { 3, 2, "Editor" },
                    { 4, 3, "Moderator" },
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
                table: "MovieCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Drama" },
                    { 2, "Action" },
                    { 3, "Romantic" },
                    { 4, "Sci-fi" }
                });

            migrationBuilder.InsertData(
                table: "NotificationSystems",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "System", "Sys" },
                    { 2, "Event Manager", "EM" },
                    { 3, "Plan Manager", "PM" },
                    { 4, "Status Library", "SL" },
                    { 5, "Work Manager", "WM" }
                });

            migrationBuilder.InsertData(
                table: "PlanTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, "Event" },
                    { 5, "Decision" },
                    { 4, "Learning" },
                    { 3, "Nice To Have" },
                    { 2, "Future Idea" },
                    { 1, "Plan" }
                });

            migrationBuilder.InsertData(
                table: "SeriesCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Action" },
                    { 1, "Drama" },
                    { 4, "Sci-fi" },
                    { 3, "Romantic" }
                });

            migrationBuilder.InsertData(
                table: "WorkingDayTypes",
                columns: new[] { "Id", "DayIsActive", "Title" },
                values: new object[] { 1, true, "Work Day" });

            migrationBuilder.InsertData(
                table: "WorkingDayTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 2, "University" },
                    { 3, "Empty Day" },
                    { 4, "Holiday" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "ImportanceLevel", "SystemId", "Title" },
                values: new object[,]
                {
                    { 1, 2, 1, "Login" },
                    { 82, 1, 4, "Season Deleted" },
                    { 81, 1, 4, "Season Added" },
                    { 80, 1, 4, "My Series List Updated" },
                    { 79, 1, 4, "Series Updated" },
                    { 78, 1, 4, "Series Deleted" },
                    { 77, 1, 4, "Series Added" },
                    { 76, 1, 4, "My Movie List Updated" },
                    { 75, 1, 4, "Movie Seen Status Updated" },
                    { 74, 1, 4, "My Book List Updated" },
                    { 73, 1, 4, "Book Read Status Updated" },
                    { 72, 1, 4, "Book Updated" },
                    { 71, 1, 4, "Book Deleted" },
                    { 70, 1, 4, "Book Added" },
                    { 69, 1, 4, "Movie Updated" },
                    { 68, 1, 4, "Movie Deleted" },
                    { 67, 1, 4, "Movie Added" },
                    { 38, 3, 2, "Event PayOut Updated" },
                    { 37, 3, 2, "Event PayOut Deleted" },
                    { 36, 3, 2, "Event PayOut Added" },
                    { 35, 1, 2, "Event ToDo Updated" },
                    { 34, 2, 2, "Event ToDo Deleted" },
                    { 83, 1, 4, "Season Updated" },
                    { 84, 1, 4, "Episode Added" },
                    { 85, 1, 4, "Episode Deleted" },
                    { 86, 1, 4, "Episode Updated" },
                    { 58, 2, 5, "Working Day Type Added" },
                    { 57, 1, 5, "Working Day Updated" },
                    { 56, 1, 5, "Working Day Deleted" },
                    { 55, 1, 5, "Working Day Added" },
                    { 54, 1, 5, "Working Field Updated" },
                    { 53, 1, 5, "Working Field Deleted" },
                    { 52, 1, 5, "Working Field Added" },
                    { 101, 1, 4, "Series Comment Updated" },
                    { 100, 2, 4, "Series Comment Deleted" },
                    { 99, 2, 4, "Series Comment Added" },
                    { 33, 2, 2, "Event ToDo Added" },
                    { 98, 2, 4, "Series Category Updated" },
                    { 96, 2, 4, "Series Category Added" },
                    { 95, 1, 4, "Movie Comment Updated" },
                    { 94, 2, 4, "Movie Comment Deleted" },
                    { 93, 2, 4, "Movie Comment Added" },
                    { 92, 2, 4, "Movie Category Updated" },
                    { 91, 2, 4, "Movie Category Deleted" },
                    { 90, 2, 4, "Movie Category Added" },
                    { 89, 1, 4, "Season Seen Status Updated" },
                    { 88, 1, 4, "Series Seen Status Updated" },
                    { 87, 1, 4, "Episode Seen Status Updated" },
                    { 97, 2, 4, "Series Category Deleted" },
                    { 59, 3, 5, "Working Day Type Deleted" },
                    { 32, 2, 2, "Role Removed In An Event" },
                    { 30, 2, 2, "Role Added In An Event" },
                    { 61, 1, 1, "Message Added" },
                    { 51, 3, 1, "News Deleted" },
                    { 50, 1, 1, "News Updated" },
                    { 49, 2, 1, "News Added" },
                    { 48, 3, 1, "Friend Removed" },
                    { 47, 2, 1, "You Has a new Friend" },
                    { 46, 2, 1, "Friend Request Declined" },
                    { 45, 2, 1, "Friend Request Accepted" },
                    { 44, 1, 1, "Friend Request Sent" },
                    { 43, 2, 1, "Friend Request Received" },
                    { 42, 3, 1, "Profile Disabled" },
                    { 41, 2, 1, "Username Changed" },
                    { 40, 1, 1, "Profile Image Changed" },
                    { 39, 3, 1, "Password Changed" },
                    { 8, 1, 1, "ToDo Updated" },
                    { 7, 2, 1, "ToDo Deleted" },
                    { 6, 2, 1, "ToDo Added" },
                    { 5, 1, 1, "Message Arrived" },
                    { 4, 3, 1, "My Profile Updated" },
                    { 3, 1, 1, "Logout" },
                    { 2, 3, 1, "Registration" },
                    { 62, 1, 1, "Message Deleted" },
                    { 63, 1, 1, "Message Updated" },
                    { 64, 2, 1, "Gender Added" },
                    { 65, 3, 1, "Gender Deleted" },
                    { 29, 2, 2, "Event Role Added To A User" },
                    { 28, 3, 2, "Event Role Deleted" },
                    { 27, 2, 2, "Event Role Updated" },
                    { 26, 2, 2, "Event Role Added" },
                    { 25, 1, 2, "Event Date Changed" },
                    { 24, 2, 2, "Event Evolved To GT Event" },
                    { 23, 2, 2, "Event Evolved To Sport Event" },
                    { 22, 3, 2, "Removed From An Event" },
                    { 21, 3, 2, "Event Member Removed" },
                    { 20, 1, 2, "Decline Event Invitation" },
                    { 31, 2, 2, "Event Role Removed From A User" },
                    { 19, 1, 2, "Accept Event Invitation" },
                    { 17, 2, 2, "Invitation Declined" },
                    { 16, 2, 2, "Invitation Accepted" },
                    { 15, 2, 2, "Event Member Invited" },
                    { 14, 1, 2, "Event Message Arrived" },
                    { 13, 2, 2, "Event Updated" },
                    { 12, 1, 2, "Event Locked" },
                    { 11, 2, 2, "Event Published" },
                    { 10, 3, 2, "Event Disabled" },
                    { 9, 3, 2, "Event Created" },
                    { 66, 2, 1, "Gender Updated" },
                    { 18, 1, 2, "Invited To An Event" },
                    { 60, 2, 5, "Working Day Type Updated" }
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
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers",
                column: "GenderId");

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
                name: "IX_Books_CreatorId",
                table: "Books",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LastUpdaterId",
                table: "Books",
                column: "LastUpdaterId");

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
                name: "IX_Episodes_LastUpdaterId",
                table: "Episodes",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_DestinationId",
                table: "FriendRequests",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_SenderId",
                table: "FriendRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendId",
                table: "Friends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_RequestId",
                table: "Friends",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_IgnoredWorks_WorkId",
                table: "IgnoredWorks",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_MovieId",
                table: "MovieComments",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_UserId",
                table: "MovieComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieMovieCategorySwitch_CategoryId",
                table: "MovieMovieCategorySwitch",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CreatorId",
                table: "Movies",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_LastUpdaterId",
                table: "Movies",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CreatorId",
                table: "News",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_News_LastUpdaterId",
                table: "News",
                column: "LastUpdaterId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SharedCsomors_CsomorId",
                table: "SharedCsomors",
                column: "CsomorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserSeriesSwitch_SeriesId",
                table: "UserSeriesSwitch",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_TypeId",
                table: "WorkingDays",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_UserId",
                table: "WorkingDays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingFields_WorkingDayId",
                table: "WorkingFields",
                column: "WorkingDayId");
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
                name: "CsomorPersonTables");

            migrationBuilder.DropTable(
                name: "CsomorWorkTables");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "IgnoredWorks");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MovieComments");

            migrationBuilder.DropTable(
                name: "MovieMovieCategorySwitch");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PlanGroupChatMessages");

            migrationBuilder.DropTable(
                name: "PlanGroupIdeas");

            migrationBuilder.DropTable(
                name: "PlanGroupPlanComments");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "SeriesComments");

            migrationBuilder.DropTable(
                name: "SeriesSeriesCategoriesSwitch");

            migrationBuilder.DropTable(
                name: "SharedCsomors");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "UserBookSwitch");

            migrationBuilder.DropTable(
                name: "UserEpisodeSwitch");

            migrationBuilder.DropTable(
                name: "UserMovieSwitch");

            migrationBuilder.DropTable(
                name: "UserPlanGroupsSwitch");

            migrationBuilder.DropTable(
                name: "UserSeriesSwitch");

            migrationBuilder.DropTable(
                name: "WorkingFields");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "CsomorPersons");

            migrationBuilder.DropTable(
                name: "CsomorWorks");

            migrationBuilder.DropTable(
                name: "MovieCategories");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "PlanGroupPlans");

            migrationBuilder.DropTable(
                name: "SeriesCategories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "GroupRoles");

            migrationBuilder.DropTable(
                name: "WorkingDays");

            migrationBuilder.DropTable(
                name: "Csomors");

            migrationBuilder.DropTable(
                name: "NotificationSystems");

            migrationBuilder.DropTable(
                name: "PlanGroups");

            migrationBuilder.DropTable(
                name: "MarkTypes");

            migrationBuilder.DropTable(
                name: "PlanTypes");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "WorkingDayTypes");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
