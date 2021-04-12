using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LacinaTmsApi.Services;
using LacinaTmsApi.Models;
using LacinaTmsApi.Controllers;
using LacinaTmsApi.Data.Migrations;
using LacinaTmsApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;

namespace TmsApi.Tests
{
    public class TasksControllerShould
    {
        #region Tests
        
        [Fact]
        public void TestGetMainTask()
        {
            //Arrange (set-up builder and seed data) 
            var builder = new DbContextOptionsBuilder<TaskDbContext>();
            builder.UseInMemoryDatabase("TestingDB1");
            using (var context = new TaskDbContext(builder.Options))
            {
                var mainTask = new MainTask
                {
                    Name = "Testing main Name 2",
                    Description = "Testing Description main 1",
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Today
                };

                context.MainTasks.Add(mainTask);
                context.SaveChanges();
                Assert.True(mainTask.Id > -1);
                //Assert.Equal(EntityState.Added, context.Entry(mainTask).State); //left for record but can't be used due to state being one the MainTask "object" property
            }
        }


        [Fact]
        public void TestGetMainTaskGetById()
        {
            //Arrange (set-up builder and seed data) 
            var builder = new DbContextOptionsBuilder<TaskDbContext>();
            builder.UseInMemoryDatabase("TestingDB1");
            using (var context = new TaskDbContext(builder.Options))
            {
                var mainTask = new MainTask
                {
                    Name = "Testing main Name 2",
                    Description = "Testing Description main 1",
                    StartDate = DateTime.Now,
                    FinishDate = DateTime.Today,
                    State = null
                };
                context.MainTasks.Add(mainTask);
                context.SaveChanges();
                Assert.True(mainTask.Id > -1);
            }
        }

        [Fact]
        public void CanDisableTracking()
        {
            //Arrange (set up builder & seed data)
            var builder = new DbContextOptionsBuilder<TaskDbContext>();
            builder.UseInMemoryDatabase("UnTrackedMainTask")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            SeedWithOneMainTask(builder.Options);
            //Act (call the method)
            using (var context = new TaskDbContext(builder.Options))
            {
                context.MainTasks.ToList();
                //Assert (check the results)
                Assert.Equal(0, context.ChangeTracker.Entries().Count());
            }
        }

        #endregion


        #region Helper methods

        private int SeedWithOneMainTask(DbContextOptions<TaskDbContext> options)
        {
            using (var seedContext = new TaskDbContext(options))
            {
                var testingMainTask = new MainTask();
                seedContext.Add(testingMainTask);
                seedContext.SaveChanges();
                return testingMainTask.Id;
            }
        }

        #endregion


        #region To be removed 

        /*
           //Arrange
            //https://github.com/ardalis/TestingLogging/blob/master/TestingLogging.UnitTests/SomeOtherServiceDoSomething.cs
            /*var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings:testDB")));
         */

        /*[Fact]
        public async void GetOrderById_ScenarioReturnsCorrectData_ReturnsTrue()
        {
            // Arrange
            OrderDTO order = new OrderDTO();
            // Mocking the ASP.NET IConfiguration for getting the connection string from appsettings.json
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "testDB")]).Returns("mock value");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings:testDB"))).Returns(mockConfSection.Object);

            IDataAccess dataAccess = new SqlDatabase(mockConfiguration.Object);
            IRepository repository = new repository(dataAccess, connectionStringData);
            var connectionStringData = new ConnectionStringData
            {
                SqlConnectionLocation = "testDatabase"
            };

            // Act
            int id = await repository.CreateOrder(order);

            // Assert
            Assert.Equal(1, id);
        }*/

        /* build this test:
         [TestMethod]public void CanDisableTracking() { //Arrange (set up builder & seed data)   var builder = new DbContextOptionsBuilder<SamuraiContext>();   builder.UseInMemoryDatabase("UnTrackedSamurai")     .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);    SeedWithOneSamurai(builder.Options);   //Act (call the method)   using (var context = new SamuraiContext(builder.Options))   {     context.Samurais.ToList();     //Assert (check the results)     Assert.AreEqual(0, context.ChangeTracker.Entries().Count());   }
    [TestClass]public class ControllerIntegrationTests {
        private readonly WebApplicationFactory < SamuraiAPI.Startup > _factory;
        public ControllerIntegrationTests() {
            _factory = new WebApplicationFactory < SamuraiAPI.Startup > ();
        }
        [TestMethod]public async Task GetEndpointReturnsSuccessAndSomeDataFromTheDatabse() { // Arrange     var client = _factory.CreateClient();     // Act     var response = await client.GetAsync("/api/SamuraisSoc");     response.EnsureSuccessStatusCode(); // Status Code 200-299     var responseString = await response.Content.ReadAsStringAsync();     var responseObjectList = JsonConvert.DeserializeObject<List<Samurai>>(responseString);     // Assert     Assert.AreNotEqual(0, responseObjectList.Count);   } }
        */

        #endregion
    }
}
