using Microsoft.EntityFrameworkCore;
using TodoApplication.Data.Configurations;

namespace TodoApplication.Models
{
    public class TodoApplicationContext : DbContext
    {
        public DbSet<TodoApplication.Models.Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=TodoApp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
        }
    }   
}