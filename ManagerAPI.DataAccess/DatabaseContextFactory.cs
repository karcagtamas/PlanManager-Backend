using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ManagerAPI.DataAccess
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        { 
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ManagerAPI.Backend"))
                .AddJsonFile("appsettings.json")
                .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>(); 
            optionsBuilder.UseSqlServer(config.GetConnectionString("ManagerDb"));
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}