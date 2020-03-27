using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanManager.DataAccess.Entities;

namespace PlanManager.DataAccess
{
    public class DatabaseContext : IdentityDbContext
    {
        public DbSet<User> AppUsers { get; set; }
        public DbSet<WebsiteRole> AppRoles { get; set; }
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

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
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
            
            // User - Plan group switch table settings
            builder.Entity<UserPlanGroup>()
                .HasKey(x => new { x.UserId, x.GroupId });
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
        }
    }
}