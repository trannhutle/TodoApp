using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Services;
using TodoApplication.ViewModels;

namespace TodoApplication.ViewComponents
{
    public class TodoListViewComponent : ViewComponent
    {
        private ITodoListServices _todoListServices = null;
        private ITodoCatServices _todoCatServices = null;

        public TodoListViewComponent(ITodoListServices todoListServices, ITodoCatServices todoCatServices)
        {
            _todoListServices = todoListServices;
            _todoCatServices = todoCatServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedCatId)
        {
            var todoList = await _todoListServices.GetTodoListAsync(selectedCatId);
            var todoCat = _todoCatServices.FindById(selectedCatId);
            var todoCatName = "";
            if (todoCat != null)
            {
                todoCatName = todoCat.Name;
            }
            return View(new TodoListViewModel { TodoCatId = selectedCatId, TodoCatName = todoCatName, TodoList = todoList });

        }
    }
}
