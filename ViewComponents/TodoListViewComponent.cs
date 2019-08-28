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
            var remainingTasks = 0;

            if (todoCat != null)
            {
                todoCatName = todoCat.Name;
            }
            if (todoList != null & todoList.Count > 0)
            {
                remainingTasks = todoList.Count; ;
                foreach (var item in todoList)
                {
                    if (item.Complete)
                    {
                        remainingTasks--;
                    }
                }
            }
            return View(new TodoListViewModel { TodoCatId = selectedCatId, TodoCatName = todoCatName, TodoList = todoList, RemainingTasks = remainingTasks });
        }
    }
}
