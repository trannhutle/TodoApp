using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Data;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public class TodoListServices : ITodoListServices
    {
        private readonly TodoApplicationContext db;

        public TodoListServices(TodoApplicationContext db)
        {
            this.db = db;
        }

        public Todo AddNewTodo(Todo todo)
        {
            db.Todos.Add(todo);
            db.SaveChanges();
            return todo;
        }

        public List<Todo> GetTodoList(int selectedCatId)
        {
            var todoList = this.db.Todos.Where(x => x.CatID == selectedCatId).ToList();
            return todoList;
        }

        public async Task<List<Todo>> GetTodoListAsync(int selectedCatId)
        {
            var todoList = await this.db.Todos.Where(x => x.CatID == selectedCatId).ToListAsync();
            return todoList;
        }

    }
}
