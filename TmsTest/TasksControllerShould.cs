using System.Linq;
using System.Threading.Tasks;
using LacinaTmsApi.Services;
using LacinaTmsApi.Models;
using LacinaTmsApi.Controllers;
using LacinaTmsApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;

namespace TmsApi.Tests
{
    public class TasksControllerShould
    {
        [Fact]
        public async Task TestGetMainTask()
        {
            //Arrange
            //https://github.com/ardalis/TestingLogging/blob/master/TestingLogging.UnitTests/SomeOtherServiceDoSomething.cs
            var mockLogger = new Mock<ILogger<TaskRepository>>();
            var mockRepository = new Mock<ITaskRepository>();

            var dbContext = DbContextMocker.GetTaskDbContext("TestingDB");
            var taskRepository = new TaskRepository(dbContext, mockLogger.Object);

            //Act
            //var response = await taskRepository.GetAllMainTasks(null);

            //Assert
            //Assert.True(response.Any());


        }
    }
}
