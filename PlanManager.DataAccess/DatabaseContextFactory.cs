using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PlanManager.DataAccess
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
        {
            public DatabaseContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                optionsBuilder.UseSqlServer("Server=localhost;Database=PlanManagerDb;User Id=SA;password=Tomi42457;Trusted_Connection=False;MultipleActiveResultSets=true;");

                return new DatabaseContext(optionsBuilder.Options);
            }
        }
}