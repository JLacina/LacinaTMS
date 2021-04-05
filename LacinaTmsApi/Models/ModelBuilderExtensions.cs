using System;
using Microsoft.EntityFrameworkCore;
namespace LacinaTmsApi.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            MainTask mainTaskOne = new MainTask
            {
                Id = 1,
                Name = "Code Entity Framework task",
                Description = "Code the EF part of the project",
                StartDate = DateTime.Now,
                FinishDate = DateTime.Today,
                State = State.InProgress
            };

            modelBuilder.Entity<MainTask>().HasData(
                mainTaskOne
            );

            // //this is not working with EF - issue with FK
            // modelBuilder.Entity<SubTask>().HasData(
            //     new SubTask
            //     {
            //         Id = 1, Name = "Code First Sub Task", Description = "Development Work", StartDate = DateTime.Now,
            //         FinishDate = DateTime.Today, State = State.InProgress, MainTaskId = mainTaskOne.Id, ParentMainTask = mainTaskOne
            //     }
            // );

        }
    }
}
