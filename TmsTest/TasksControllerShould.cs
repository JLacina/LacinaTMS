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
        [Fact]
        public void TestGetMainTask()
        {
            //Arrange
            //https://github.com/ardalis/TestingLogging/blob/master/TestingLogging.UnitTests/SomeOtherServiceDoSomething.cs

            /*var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings:testDB")));

            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("TestingDB1");

            using (var context = new TaskDbContext((DbContextOptions<TaskDbContext>) builder.Options,
                mockConfiguration.Object))
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
                Debug.WriteLine("$Before Save {mainTask.Id} ");
                context.SaveChanges();
                Debug.WriteLine("$Before Save {mainTask.Id} ");
                Assert.True(mainTask.Id > -1);
                Assert.Equal(EntityState.Added, context.Entry(mainTask).State);
            }*/

            var dbContext = DbContextMocker.GetTaskDbContext("TestingDB");


            //Act
            //var response = await taskRepository.GetAllMainTasks(null);

            //Assert
            //Assert.True(response.Any());

        }

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

    }
}
