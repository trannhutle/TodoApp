using Microsoft.EntityFrameworkCore;
using TodoApplication.Data.Configurations;

namespace TodoApplication.Data
{
    public class TodoApplicationContext : DbContext
    {
        public DbSet<Models.Todo> Todos { get; set; }
        public DbSet<Models.TodoCategory> TodoCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=TodoApp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
            modelBuilder.Seed();
        }
    }   
}