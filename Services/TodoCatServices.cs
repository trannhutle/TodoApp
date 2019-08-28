using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Data;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public class TodoCatServices : ITodoCatServices
    {
        private readonly TodoApplicationContext db;
        public TodoCatServices(TodoApplicationContext db) => this.db = db;
        /// <summary>
        /// Add new todo category
        /// </summary>
        /// <param name="todoCategory"></param>
        /// <returns></returns>
        public TodoCategory AddNewTodoCategory(TodoCategory todoCategory)
        {
            db.TodoCategory.Add(todoCategory);
            db.SaveChanges();
            return todoCategory;
        }

        /// <summary>
        /// Delete todo category by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            var cat = FindById(id);
            if (cat != null)
            {
                db.TodoCategory.Remove(cat);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Find todo category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TodoCategory FindById(int id)
        {
            return db.TodoCategory.Find(id);
        }
        

        /// <summary>
        /// Get all categories from db
        /// </summary>
        /// <returns></returns>
        public List<TodoCategory> GetTodoCategoryList()
        {
            var todoCatList = db.TodoCategory.ToList();
            return todoCatList;
        }

        /// <summary>
        /// Get all categories from db asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoCategory>> GetTodoCategoryListAsync()
        {
            var todoCatList = await db.TodoCategory.ToListAsync();
            return todoCatList;
        }


    }
}
