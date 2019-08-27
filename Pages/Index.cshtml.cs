using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Models;
using TodoApplication.Data;
using TodoApplication.DataObjects;
using System.IO;

namespace TodoApplication.Pages
{
    public class IndexModel : PageModel
    {
        // Variable initialization

        private readonly TodoApplicationContext db;
        public IndexModel(TodoApplicationContext db) => this.db = db;
        public List<Todo> Todos { get; set; } = new List<Todo>();

        public List<TodoCategory> TodoCats { get; set; } = new List<TodoCategory>();

        // On page initialisation
        public async Task OnGetAsync()
        {
            // Load default todo category and load todo detail list
            this.Todos = await db.Todos.ToListAsync();
            this.TodoCats = await db.TodoCategory.ToListAsync();
        }

        [HttpPost]
        public IActionResult OnPostAddNewCategory()
        {
            var catName = Request.Form["catName"];
            if (String.IsNullOrEmpty(catName))
            {
                return new JsonResult(new ResponseMessage(500, "Invalid inut", null));
            }

            // Check name dupplicate
            foreach (var cat in this.TodoCats)
            {
                if (cat.Name.Equals(catName))
                {
                    return new JsonResult(new ResponseMessage(500, "Existed category", null));
                }
            }
            TodoCategory newCat = new TodoCategory();
            newCat.Name = catName;
            this.db.Add(newCat);
            return new JsonResult(new ResponseMessage(200, "Added successfully", newCat));
        }
    }
}
