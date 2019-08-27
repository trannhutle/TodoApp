using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Services;

namespace TodoApplication.ViewComponents
{
    public class TodoListViewComponent:ViewComponent
    {
        private ITodoListServices _iTodoListServices = null;
        public TodoListViewComponent(ITodoListServices todoListServices)
        {
            _iTodoListServices = todoListServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedCatId)
        {
            var todoList = await _iTodoListServices.GetTodoListAsync(selectedCatId);
            return View(todoList);
        }
    }
}
