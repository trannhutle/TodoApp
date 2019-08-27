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
using TodoApplication.Services;

namespace TodoApplication.Pages
{
    public class IndexModel : PageModel
    {
        private ITodoCatServices _todoCatServices;
        private ITodoListServices _todoListServices;

        public IndexModel(ITodoCatServices todoCatServices, ITodoListServices todoListServices)
        {
            _todoCatServices = todoCatServices;
            _todoListServices = todoListServices;
        }

        public List<TodoCategory> TodoCats { get; set; } = new List<TodoCategory>();

        public int ID { get; set; } = 0;

        // On page initialisation
        public async Task OnGetAsync(int id)
        {
            //var id = Request.Query["id"];
            this.ID = id;
            this.TodoCats = await _todoCatServices.GetTodoCategoryListAsync();
        }

        [HttpPost]
        public IActionResult OnPostAddNewCategory()
        {
            var catName = Request.Form["catName"];
            if (String.IsNullOrEmpty(catName))
            {
                return new JsonResult(new ResponseMessage(500, "Invalid inut", null));
            }
            this.TodoCats = _todoCatServices.GetTodoCategoryList();
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
            _todoCatServices.AddNewTodoCategory(newCat);
            return new JsonResult(new ResponseMessage(200, "Added successfully", newCat));
        }

        [HttpPost]
        public IActionResult OnPostAddNewTodo()
        {
            //var catId = Int32.Parse(Request.Form["catId"]);
            var catId = 2;
            var todoTitle = Request.Form["todoTitle"];
            var newTodo = new Todo
            {
                Title = todoTitle,
                CatID = catId,
                AssignmentDate = DateTime.Now.Ticks,
                CreateDate = DateTime.Now.Ticks,
                Content = "temp"
            };
            _todoListServices.AddNewTodo(newTodo);

            return new JsonResult(new ResponseMessage(200, "Added successfully", newTodo));
        }
    }
}
