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

        /// <summary>
        /// Add new todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public Todo AddNewTodo(Todo todo)
        {
            db.Todo.Add(todo);
            db.SaveChanges();
            return todo;
        }

        /// <summary>
        /// Delete todo by Id
        /// </summary>
        /// <param name="todoId"></param>
        public void DeleteTodo(int todoId)
        {
            var todo = db.Todo.Find((long)todoId);
            if (todo != null)
            {
                db.Todo.Remove(todo);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Remove all todos of one todo category
        /// </summary>
        /// <param name="selectedCatId"></param>
        public void DeleteTodoByCatId(int selectedCatId)
        {
            var todoList = GetTodoList(selectedCatId);
            if (todoList != null)
            {
                foreach (var todo in todoList)
                {
                    db.Todo.Remove(todo);
                }
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Change complete status of todo
        /// </summary>
        /// <param name="todoId"></param>
        /// <param name="compete"></param>
        public void UpdateTodo(int todoId, bool compete)
        {
            var todo = db.Todo.Find((long)todoId);
            if (todo != null)
            {
                todo.Complete = compete;
                todo.UpdateDate = DateTime.Now.Ticks;
                db.Todo.Update(todo);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Get list todos of todo category .
        /// </summary>
        /// <param name="selectedCatId"></param>
        /// <returns></returns>
        public List<Todo> GetTodoList(int selectedCatId)
        {
            var todoList = this.db.Todo.Where(x => x.CatID == selectedCatId).ToList();
            return todoList;
        }
        /// <summary>
        /// Get list todos of todo category asynchronously .
        /// </summary>
        /// <param name="selectedCatId"></param>
        /// <returns></returns>
        public async Task<List<Todo>> GetTodoListAsync(int selectedCatId)
        {
            var todoList = await this.db.Todo.Where(x => x.CatID == selectedCatId).ToListAsync();
            return todoList;
        }

    }
}
