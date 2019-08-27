using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Data;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public class TodoCatService : ITodoCatServices
    {
        private readonly TodoApplicationContext db;
        public TodoCatService(TodoApplicationContext db) => this.db = db;

        public TodoCategory AddNewTodoCategory(TodoCategory todoCategory)
        {
            db.TodoCategory.Add(todoCategory);
            db.SaveChanges();
            return todoCategory;
        }

        public List<TodoCategory> GetTodoCategoryList()
        {
            var todoCatList = db.TodoCategory.ToList();
            return todoCatList;
        }

        public async Task<List<TodoCategory>> GetTodoCategoryListAsync()
        {
            var todoCatList = await db.TodoCategory.ToListAsync();
            return todoCatList;
        }


    }
}
