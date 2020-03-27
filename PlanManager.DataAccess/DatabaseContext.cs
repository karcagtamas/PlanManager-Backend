using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanManager.DataAccess
{
    public class DatabaseContext : IdentityDbContext
    {
        /*
        public DbSet<User> AppUsers { get; set; }
        public DbSet<WebsiteRole> AppRoles { get; set; }
        */

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}