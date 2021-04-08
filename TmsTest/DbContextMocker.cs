using System;
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
            // var mockConfiguration = new Mock<IConfiguration>();
            // mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings:testDB")));
            // mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);
            //
            // var mockConfSection = new Mock<IConfigurationSection>();
            // mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "testDB")]).Returns("mock value");

            //using: https://nightbaker.github.io/moq.net/,/.net/core,/tdd/2019/03/01/moq/
            // var mockConfSection = new Mock<IConfigurationSection>();
            // mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "default")]).Returns("mock value");
            //
            // var mockConfiguration = new Mock<IConfiguration>();
            // mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(_mockConfSection.Object);
            //
            // var _configuration = new Mock<IConfiguration>();
            // _configuration.SetupGet(x => x[It.Is<string>(s => s == "ConnectionStrings:default")]).Returns("mock value");


            // var mockConfiguration = new Mock<IConfiguration>();
            //  mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfiguration.Object);
             // var mockConfiguration = new Mock<IConfiguration>();
             // mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
             //     .Returns(mockConfSection.Object);
             //
             // var mockConfSection = new Mock<IConfigurationSection>();
             // mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "testDB")]).Returns("mock value");
             //
             // var mockConfiguration = new Mock<IConfiguration>();
             // mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
             //     .Returns(mockConfSection.Object);
            

             // Console.WriteLine(mockConfiguration.Object.GetConnectionString("testDB")); // prints "mock value"


            /*Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
            configuration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(new Mock<IConfigurationSection>().Object)*/;


            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("config.json")
                .Build();


            //Create options for DbContext instance 
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: dbName).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            //Create instance of DbContext
            var dbContext = new TaskDbContext(options, config);

            //Add entities in memory 
            dbContext.Seed();

            return dbContext;
        }
    }
}

