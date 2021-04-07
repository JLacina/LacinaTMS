using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LacinaTmsApi.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace TmsApi.Tests
{
    public static class DbContextMocker
    {
        public static TaskDbContext GetTaskDbContext(string dbName)
        {
            var mockConfig = new Mock<IConfiguration>();
            //Create options for DbContext instance 
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: dbName).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            //Create instance of DbContext
            var dbContext = new TaskDbContext(options);

            //Add entities in memory 
            dbContext.Seed();

            return dbContext;
        }
    }
}

