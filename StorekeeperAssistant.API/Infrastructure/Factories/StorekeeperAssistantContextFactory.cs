using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StorekeeperAssistant.Infrastructure;

namespace StorekeeperAssistant.API.Infrastructure.Factories
{
    public class StorekeeperAssistantContextFactory : IDesignTimeDbContextFactory<StorekeeperAssistantContext>
    {
        public StorekeeperAssistantContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StorekeeperAssistantContext>();

            optionsBuilder.UseNpgsql(config.GetConnectionString("StorekeeperAssistantContext"), o => o.MigrationsAssembly("StorekeeperAssistant.API"));

            return new StorekeeperAssistantContext(optionsBuilder.Options);
        }
    }
}
