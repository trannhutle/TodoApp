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
using Microsoft.Extensions.Logging;
using TodoApplication.ViewModels;

namespace TodoApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITodoCatServices _todoCatServices;
        private readonly ITodoListServices _todoListServices;
        private readonly ILogger _logger;

        public IndexModel(ITodoCatServices todoCatServices, ITodoListServices todoListServices,
            ILogger<IndexModel> logger)
        {
            _todoCatServices = todoCatServices;
            _todoListServices = todoListServices;
            _logger = logger;
        }
        public List<TodoCategory> TodoCats { get; set; } = new List<TodoCategory>();

        public int ID { get; set; } = 0;

        /// <summary>
        /// Handle load index page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var catIdStr = Request.Query["id"];
            var id = ParseTodoCatId(catIdStr);

            this.TodoCats = await _todoCatServices.GetTodoCategoryListAsync();
            // The the first item on the list
            if (id == 0 && this.TodoCats.Count > 0)
            {
                this.ID = this.TodoCats[1].ID;
            }
            else if (id > 0)
            {
                var curCat = _todoCatServices.FindById(id);
                if (curCat == null)
                {
                    return RedirectToPage("./Error");
                }
                this.ID = curCat.ID;
            }
            else if (this.TodoCats.Count == 0)
            {
                return RedirectToPage("./Error");
            }
            return Page();
        }

        /// <summary>
        /// Handle  3 delete methods: remove done todos, remove all todos of todo category and remove the todo category
        /// </summary>
        /// <param name="todoList"></param>
        /// <param name="removeDone"></param>
        /// <param name="removeAllTodoList"></param>
        /// <param name="removeList"></param>
        /// <param name="todoCatId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult OnPostDeleteTodo(TodoViewModel todoList, string removeDone,
            string removeAllTodoList, string removeList, int todoCatId)

        {
            if (!string.IsNullOrEmpty(removeDone))
            {
                if (todoList != null && todoList.SelectedTodos != null)
                {
                    RemoveDoneTodos(todoList.SelectedTodos);
                }
            }
            else if (!string.IsNullOrEmpty(removeAllTodoList))
            {
                _todoListServices.DeleteTodoByCatId(todoCatId);
            }
            else if (!string.IsNullOrEmpty(removeList))
            {
                _todoListServices.DeleteTodoByCatId(todoCatId);
                _todoCatServices.DeleteById(todoCatId);
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index", new { id = todoList.TodoCatId });
        }

        /// <summary>
        /// Get all todos of todo category. It requires catId from query param
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGetTodoList()
        {
            var catIdStr = Request.Query["catId"];
            var id = ParseTodoCatId(catIdStr);
            var todoList = _todoListServices.GetTodoList(id);
            // The the first item on the list
            return new JsonResult(new ResponseMessage(200, "Get todo list successfully", todoList));
        }

        /// <summary>
        /// Handle post method from client for add new todo category. It requires catName in formdata 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Handle post method from client for set "complete" status of the todo from client.
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult OnPostUpdateTodo([FromForm] UpdateTodoViewModel todo)
        {
            if (todo != null)
            {
                var todoId = todo.ID;
                var complete = todo.Complete;
                _todoListServices.UpdateTodo(todoId, complete);
                return new JsonResult(new ResponseMessage(200, "Update successfully", todo));
            }
            return new JsonResult(new ResponseMessage(500, "Update failed", todo));
        }

        /// <summary>
        /// Handle AddNewDo from client. This handler requires 2 params: catId and todoName
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult OnPostAddNewTodo()
        {
            var catId = Request.Form["catId"];
            var todoName = Request.Form["todoName"];
            // Valid input 
            if (!IsAddNewTodoValid(catId, todoName))
            {
                return new JsonResult(new ResponseMessage(500, "Input Error", null));
            }
            var newTodo = new Todo
            {
                Name = todoName,
                CatID = Int32.Parse(catId),
                UpdateDate = DateTime.Now.Ticks,
                CreateDate = DateTime.Now.Ticks,
            };
            _todoListServices.AddNewTodo(newTodo);
            return new JsonResult(new ResponseMessage(200, "Added successfully", newTodo));
        }

        /// <summary>
        /// This function is used for remove the list of todo
        /// </summary>
        /// <param name="todoIdList"></param>
        private void RemoveDoneTodos(List<int> todoIdList)
        {
            foreach (var item in todoIdList)
            {
                _todoListServices.DeleteTodo(item);
            }
        }
        /// <summary>
        /// This function is used for parse todoCat from string to int
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        private int ParseTodoCatId(string catId)
        {
            var id = 0;
            try
            {
                id = Int32.Parse(catId);
            }
            catch (Exception e)
            {
                _logger.LogError("Parse Error");
            }
            return id;
        }

        /// <summary>
        /// Validate the information when adding new todo
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsAddNewTodoValid(string catId, string name)
        {
            try
            {
                var id = Int32.Parse(catId);
            }
            catch (Exception e)
            {
                _logger.LogError("Parse Todo list id Error");
                return false;
            }
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            return true;
        }
    }
}
