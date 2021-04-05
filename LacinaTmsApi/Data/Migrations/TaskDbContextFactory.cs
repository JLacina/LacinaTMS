using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LacinaTmsApi.Data.Migrations
{
    public class TaskDbContextFactory : IDesignTimeDbContextFactory<TaskDbContext>
    {
        public TaskDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new TaskDbContext(new DbContextOptionsBuilder<TaskDbContext>().Options, config);
        }
    }
}
