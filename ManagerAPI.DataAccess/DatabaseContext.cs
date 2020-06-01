using ManagerAPI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagerAPI.DataAccess
{
    public class DatabaseContext : IdentityDbContext
    {
        public DbSet<Gender> Genders { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<WebsiteRole> AppRoles { get; set; }
        public DbSet<NotificationSystem> NotificationSystems { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // PM
        public DbSet<PlanType> PlanTypes { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanGroup> PlanGroups { get; set; }
        public DbSet<PlanGroupIdea> PlanGroupIdeas { get; set; }
        public DbSet<PlanGroupChatMessage> PlanGroupChatMessages { get; set; }
        public DbSet<MarkType> MarkTypes { get; set; }
        public DbSet<PlanGroupPlan> PlanGroupPlans { get; set; }
        public DbSet<PlanGroupPlanComment> PlanGroupPlanComments { get; set; }
        public DbSet<GroupRole> GroupRoles { get; set; }
        public DbSet<UserPlanGroup> UserPlanGroupsSwitch { get; set; }

        // EM
        public DbSet<MasterEvent> MasterEvents { get; set; }
        public DbSet<DSportEvent> DSportEvents { get; set; }
        public DbSet<DGtEvent> DGtEvents { get; set; }
        public DbSet<UserEvent> UserEventsSwitch { get; set; }
        public DbSet<EventRole> EventRoles { get; set; }
        public DbSet<UserEventRole> UserEventRolesSwitch { get; set; }
        public DbSet<EventAction> EventActions { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Gender table settings
            builder.Entity<Gender>()
                .HasData(new Gender {Id = 1, Name = "Male"});
            builder.Entity<Gender>()
                .HasData(new Gender {Id = 2, Name = "Female"});
            builder.Entity<Gender>()
                .HasData(new Gender {Id = 3, Name = "Other"});

            // User table settings
            builder.Entity<User>()
                .Property(x => x.LastLogin)
                .HasDefaultValueSql("getdate()");
            builder.Entity<User>()
                .Property(x => x.RegistrationDate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<User>()
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Entity<User>()
                .HasOne(x => x.Gender)
                .WithMany(x => x.Users)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasData(new User
                {
                    UserName = "karcagtamas", Email = "karcagtamas@outlook.com",
                    Id = "44045506-66fd-4af8-9d59-133c47d1787c", EmailConfirmed = true,
                    IsActive = true, FullName = "Karcag Tamas", NormalizedEmail = "KARCAGTAMAS@OUTLOOK.COM",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEG9SljY4ow/I7990YZ15dSGvCesg0bad3pQSWi4ekt0RT8J5JuL3lQmNJCnxo2lGIA==",
                    NormalizedUserName = "KARCAGTAMAS"
                });
            builder.Entity<User>()
                .HasData(new User
                {
                    UserName = "aaronkaa", Email = "aron.klenovszky@gmail.com",
                    Id = "f8237fac-c6dc-47b0-8f71-b72f93368b02", EmailConfirmed = true,
                    IsActive = true, FullName = "Klenovszky Áron", NormalizedEmail = "ARON.KLENOVSZKY@GMAIL.COM",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEL9QeDNFqEAq8WDl2/fXBSc02Tzxxnek963ILEw1L3aQsFysXXG4L3KvFYIVg/LpLA==",
                    NormalizedUserName = "AARONKAA"
                });
            builder.Entity<User>()
                .HasData(new User
                {
                    UserName = "root", Email = "root@karcags.hu", Id = "cd5e5069-59c8-4163-95c5-776fab95e51a",
                    EmailConfirmed = true,
                    IsActive = true, FullName = "Root", NormalizedEmail = "ROOT@KARCAGS.HU",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEHdK+ODabrjejNLGhod4ftL37G5zT97p2g0Ck5dH9MchA2B/JFDiwb9kk9soZBPF5Q==",
                    NormalizedUserName = "ROOT"
                });
            builder.Entity<User>()
                .HasData(new User
                {
                    UserName = "barni363hun", Email = "barni.pbs@gmail.com",
                    Id = "fa2edf69-5fc8-a163-9fc5-726f3b94e51b", EmailConfirmed = true,
                    IsActive = true, FullName = "Root", NormalizedEmail = "BARNI.PBS@GMAIL.COM",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEL9QeDNFqEAq8WDl2/fXBSc02Tzxxnek963ILEw1L3aQsFysXXG4L3KvFYIVg/LpLA==",
                    NormalizedUserName = "BARNI363HUN"
                });

            // Website table settings
            builder.Entity<WebsiteRole>()
                .HasData(new WebsiteRole
                {
                    Id = "2f76c2fc-bbca-41ff-86ed-5ef43d41d8f9", AccessLevel = 0, Name = "Visitor",
                    NormalizedName = "VISITOR"
                });
            builder.Entity<WebsiteRole>()
                .HasData(new WebsiteRole
                {
                    Id = "776474d7-8d01-4809-963e-c721f39dbb45", AccessLevel = 1, Name = "Normal",
                    NormalizedName = "NORMAL"
                });
            builder.Entity<WebsiteRole>()
                .HasData(new WebsiteRole
                {
                    Id = "5e0a9192-793f-4c85-a0b1-3198295bf409", AccessLevel = 2, Name = "Moderator",
                    NormalizedName = "MODERATOR"
                });
            builder.Entity<WebsiteRole>()
                .HasData(new WebsiteRole
                {
                    Id = "936e42dc-5d3f-4355-bc3a-304a4fe4f518", AccessLevel = 3, Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });
            builder.Entity<WebsiteRole>()
                .HasData(new WebsiteRole
                {
                    Id = "fa5deb78-59c2-4faa-83dc-6c3369eedf20", AccessLevel = 4, Name = "Root", NormalizedName = "ROOT"
                });

            // Notification system table settings
            builder.Entity<NotificationSystem>()
                .HasData(new NotificationSystem {Id = 1, Name = "System", ShortName = "Sys"});
            builder.Entity<NotificationSystem>()
                .HasData(new NotificationSystem {Id = 2, Name = "Event Manager", ShortName = "EM"});
            builder.Entity<NotificationSystem>()
                .HasData(new NotificationSystem {Id = 3, Name = "Plan Manager", ShortName = "PM"});
            builder.Entity<NotificationSystem>()
                .HasData(new NotificationSystem {Id = 4, Name = "Movie Corner", ShortName = "MC"});
            builder.Entity<NotificationSystem>()
                .HasData(new NotificationSystem {Id = 5, Name = "Work Manager", ShortName = "WM"});

            // Notification type table settings
            builder.Entity<NotificationType>()
                .HasOne(x => x.System)
                .WithMany(x => x.Types)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 1, Title = "Login", ImportanceLevel = 2, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 2, Title = "Registration", ImportanceLevel = 3, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 3, Title = "Logout", ImportanceLevel = 1, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 4, Title = "My Profile Updated", ImportanceLevel = 3, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 5, Title = "Message Arrived", ImportanceLevel = 1, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 6, Title = "ToDo Added", ImportanceLevel = 2, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 7, Title = "ToDo Deleted", ImportanceLevel = 2, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 8, Title = "ToDo Updated", ImportanceLevel = 1, SystemId = 1});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 9, Title = "Event Created", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 10, Title = "Event Disabled", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 11, Title = "Event Published", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 12, Title = "Event Locked", ImportanceLevel = 1, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 13, Title = "Event Updated", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 14, Title = "Event Message Arrived", ImportanceLevel = 1, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 15, Title = "Event Member Invited", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 16, Title = "Invitation Accepted", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 17, Title = "Invitation Declined", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 18, Title = "Invited To An Event", ImportanceLevel = 1, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 19, Title = "Accept Event Invitation", ImportanceLevel = 1, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 20, Title = "Decline Event Invitation", ImportanceLevel = 1, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 21, Title = "Event Member Removed", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 22, Title = "Removed From An Event", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 23, Title = "Event Evolved To Sport Event", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 24, Title = "Event Evolved To GT Event", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 25, Title = "Event Date Changed", ImportanceLevel = 1, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 26, Title = "Event Role Added", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 27, Title = "Event Role Updated", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 28, Title = "Event Role Deleted", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 29, Title = "Event Role Added To A User", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 30, Title = "Role Added In An Event", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 31, Title = "Event Role Removed From A User", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 32, Title = "Role Removed In An Event", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType {Id = 33, Title = "Event ToDo Added", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 34, Title = "Event ToDo Deleted", ImportanceLevel = 2, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 35, Title = "Event ToDo Updated", ImportanceLevel = 1, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 36, Title = "Event PayOut Added", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 37, Title = "Event PayOut Deleted", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                    {Id = 38, Title = "Event PayOut Updated", ImportanceLevel = 3, SystemId = 2});
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                { Id = 39, Title = "Password Changed", ImportanceLevel = 3, SystemId = 1 });
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                { Id = 40, Title = "Profile Image Changed", ImportanceLevel = 1, SystemId = 1 });
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                { Id = 41, Title = "Username Changed", ImportanceLevel = 2, SystemId = 1 });
            builder.Entity<NotificationType>()
                .HasData(new NotificationType
                { Id = 42, Title = "Profile Disabled", ImportanceLevel = 3, SystemId = 1 });

            // Notification table settings
            builder.Entity<Notification>()
                .HasOne(x => x.Type)
                .WithMany(x => x.Notifications)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Notification>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Notifications)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Notification>()
                .Property(x => x.Archived)
                .HasDefaultValue(false);
            builder.Entity<Notification>()
                .Property(x => x.IsRead)
                .HasDefaultValue(false);
            builder.Entity<Notification>()
                .Property(x => x.SentDate)
                .HasDefaultValueSql("getdate()");


            // Plan type table settings
            builder.Entity<PlanType>()
                .HasData(new PlanType {Id = 1, Name = "Plan"});
            builder.Entity<PlanType>()
                .HasData(new PlanType {Id = 2, Name = "Future Idea"});
            builder.Entity<PlanType>()
                .HasData(new PlanType {Id = 3, Name = "Nice To Have"});
            builder.Entity<PlanType>()
                .HasData(new PlanType {Id = 4, Name = "Learning"});
            builder.Entity<PlanType>()
                .HasData(new PlanType {Id = 5, Name = "Decision"});
            builder.Entity<PlanType>()
                .HasData(new PlanType {Id = 6, Name = "Event"});

            // Plan table settings
            builder.Entity<Plan>()
                .Property(x => x.Creation)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Plan>()
                .Property(x => x.EndTime)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Plan>()
                .Property(x => x.LastUpdate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Plan>()
                .Property(x => x.StartTime)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Plan>()
                .Property(x => x.IsPublic)
                .HasDefaultValue(false);
            builder.Entity<Plan>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Plans)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Plan>()
                .HasOne(x => x.PlanType)
                .WithMany(x => x.Plans)
                .OnDelete(DeleteBehavior.Restrict);

            // Plan group table settings
            builder.Entity<PlanGroup>()
                .Property(x => x.LastUpdate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<PlanGroup>()
                .Property(x => x.Creation)
                .HasDefaultValueSql("getdate()");
            builder.Entity<PlanGroup>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedPlanGroups)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroup>()
                .HasOne(x => x.LastUpdater)
                .WithMany(x => x.LastUpdatedPlanGroups)
                .OnDelete(DeleteBehavior.SetNull);

            // Plan group idea table settings
            builder.Entity<PlanGroupIdea>()
                .Property(x => x.Creation)
                .HasDefaultValueSql("getdate()");
            builder.Entity<PlanGroupIdea>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedPlanGroupIdeas)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroupIdea>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Ideas)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Plan group message table settings
            builder.Entity<PlanGroupChatMessage>()
                .Property(x => x.Sent)
                .HasDefaultValueSql("getdate()");
            builder.Entity<PlanGroupChatMessage>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.SentPlanGroupChatMessages)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroupChatMessage>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Messages)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Mark type table settings
            builder.Entity<MarkType>()
                .HasData(new MarkType {Id = 1, Title = "Responsible"});
            builder.Entity<MarkType>()
                .HasData(new MarkType {Id = 2, Title = "Owner"});
            builder.Entity<MarkType>()
                .HasData(new MarkType {Id = 3, Title = "Modifier"});
            builder.Entity<MarkType>()
                .HasData(new MarkType {Id = 4, Title = "Leader"});

            // Plan group plan table settings
            builder.Entity<PlanGroupPlan>()
                .Property(x => x.Creation)
                .HasDefaultValueSql("getdate()");
            builder.Entity<PlanGroupPlan>()
                .Property(x => x.LastUpdate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<PlanGroupPlan>()
                .Property(x => x.IsPublic)
                .HasDefaultValue(false);
            builder.Entity<PlanGroupPlan>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.CreatedPlanGroupPlans)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroupPlan>()
                .HasOne(x => x.LastUpdater)
                .WithMany(x => x.LastUpdatedPlanGroupPlans)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<PlanGroupPlan>()
                .HasOne(x => x.PlanType)
                .WithMany(x => x.GroupPlans)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroupPlan>()
                .HasOne(x => x.MarkedUser)
                .WithMany(x => x.MarkedOnGroupPlans)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroupPlan>()
                .HasOne(x => x.MarkType)
                .WithMany(x => x.MarkedPlans)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroupPlan>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Plans)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Plan group plan comment table settings
            builder.Entity<PlanGroupPlanComment>()
                .Property(x => x.Creation)
                .HasDefaultValueSql("getdate()");
            builder.Entity<PlanGroupPlanComment>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.CreatedPlanGroupPlanComment)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PlanGroupPlanComment>()
                .HasOne(x => x.Plan)
                .WithMany(x => x.Comments)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Group role table settings
            builder.Entity<GroupRole>()
                .HasData(new GroupRole {Id = 1, Title = "Visitor", AccessLevel = 0});
            builder.Entity<GroupRole>()
                .HasData(new GroupRole {Id = 2, Title = "Normal", AccessLevel = 1});
            builder.Entity<GroupRole>()
                .HasData(new GroupRole {Id = 3, Title = "Editor", AccessLevel = 2});
            builder.Entity<GroupRole>()
                .HasData(new GroupRole {Id = 4, Title = "Moderator", AccessLevel = 3});
            builder.Entity<GroupRole>()
                .HasData(new GroupRole {Id = 5, Title = "Administrator", AccessLevel = 4});
            builder.Entity<GroupRole>()
                .HasData(new GroupRole {Id = 6, Title = "Owner", AccessLevel = 5});

            // User - Plan group switch table settings
            builder.Entity<UserPlanGroup>()
                .HasKey(x => new {x.UserId, x.GroupId});
            builder.Entity<UserPlanGroup>()
                .Property(x => x.Connection)
                .HasDefaultValueSql("getdate()");
            builder.Entity<UserPlanGroup>()
                .HasOne(x => x.User)
                .WithMany(x => x.Groups)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserPlanGroup>()
                .HasOne(x => x.Group)
                .WithMany(x => x.Users)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserPlanGroup>()
                .HasOne(x => x.Role)
                .WithMany(x => x.GroupMembers)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserPlanGroup>()
                .HasOne(x => x.AddedBy)
                .WithMany(x => x.AddedUsersToGroups)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Master Events
            builder.Entity<MasterEvent>()
                .Property(x => x.CreationDate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<MasterEvent>()
                .Property(x => x.LastUpdate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<MasterEvent>()
                .Property(x => x.IsDisabled)
                .HasDefaultValue(false);
            builder.Entity<MasterEvent>()
                .Property(x => x.IsPublic)
                .HasDefaultValue(true);
            builder.Entity<MasterEvent>()
                .Property(x => x.IsLocked)
                .HasDefaultValue(false);
            builder.Entity<MasterEvent>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedMasterEvents)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<MasterEvent>()
                .HasOne(x => x.LastUpdater)
                .WithMany(x => x.UpdatedMasterEvents)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // D Sport Events
            builder.Entity<DSportEvent>()
                .HasOne(x => x.Event)
                .WithOne(x => x.SportEvent)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // D Gt Events
            builder.Entity<DGtEvent>()
                .HasOne(x => x.Event)
                .WithOne(x => x.GtEvent)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // User - Event Switch
            builder.Entity<UserEvent>()
                .HasKey(x => new {x.UserId, x.EventId});
            builder.Entity<UserEvent>()
                .Property(x => x.ConnectionDate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<UserEvent>()
                .HasOne(x => x.User)
                .WithMany(x => x.Events)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserEvent>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Users)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserEvent>()
                .HasOne(x => x.AddedBy)
                .WithMany(x => x.AddedUsersToEvents)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Event Roles table settings
            builder.Entity<EventRole>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Roles)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // User - Event Roles Switch table settings
            builder.Entity<UserEventRole>()
                .HasKey(x => new {x.UserId, x.RoleId});
            builder.Entity<UserEventRole>()
                .Property(x => x.OwnershipDate)
                .HasDefaultValueSql("getdate()");
            builder.Entity<UserEventRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.EventRoles)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserEventRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserEventRole>()
                .HasOne(x => x.AddedBy)
                .WithMany(x => x.AddedRolesToEvent)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Event Actions table settings
            builder.Entity<EventAction>()
                .HasOne(x => x.Event)
                .WithMany(x => x.Actions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<EventAction>()
                .HasOne(x => x.User)
                .WithMany(x => x.CausedEventActions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}