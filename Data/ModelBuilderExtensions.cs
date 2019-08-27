using Microsoft.EntityFrameworkCore;
using TodoApplication.Models;

namespace TodoApplication.Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoCategory>().HasData(
                new TodoCategory { ID = 1, Name = "Work"},
                new TodoCategory { ID = 2, Name = "Home"},
                new TodoCategory { ID = 3, Name = "Personal"}
                );
            return modelBuilder;
        }
    }
}