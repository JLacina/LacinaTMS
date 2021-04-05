using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LacinaTmsApi.Models;


namespace LacinaTmsApi.Data.Migrations
{
    public class TaskDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public TaskDbContext(DbContextOptions<TaskDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }
        public DbSet<MainTask> MainTasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubTask>().HasIndex(s => new { s.Id });
            modelBuilder.Entity<MainTask>().HasIndex(s => new { s.Id });
            modelBuilder.Entity<MainTask>().HasMany(m => m.SubTasks);
            modelBuilder.Entity<SubTask>().HasOne(u => u.ParentMainTask);

            modelBuilder.Seed();
        }
    }
}
