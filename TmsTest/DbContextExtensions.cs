using System;
using LacinaTmsApi.Data;
using LacinaTmsApi.Data.Migrations;
using LacinaTmsApi.Models;

namespace TmsApi.Tests
{
    public static class DbContextExtensions
    {
        public static void Seed(this TaskDbContext dbContext)
        {
            // Add entities for DbContext instance
            dbContext.MainTasks.Add(new MainTask
            {
                Id = 1,
                Name = "Testing main Name 2",
                Description = "Testing Description main 1",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today,
                State = null
            });

            /*dbContext.MainTasks.Add(new MainTask
            {
                Id = 2,
                Name = "Testing main Name 2",
                Description = "Testing Description main 2",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today,
                State = null
            });

            dbContext.MainTasks.Add(new MainTask
            {
                Id = 3,
                Name = "Testing main Name 3",
                Description = "Testing Description main 3",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today,
                State = null
            });

            dbContext.SubTasks.Add(new SubTask
            {
                Id = 1,
                Name = "Testing subTask Name 1",
                Description = "Testing subTask Description 1",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today,
                State = State.Planned,
                MainTaskId = 1
            });*/

            dbContext.SaveChanges();
        }

    }
}
