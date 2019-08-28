using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Services;
using TodoApplication.ViewModels;

namespace TodoApplication.ViewComponents
{
    public class TodoCategoriesViewComponent : ViewComponent
    {
        private ITodoCatServices _todoCatServices;

        public TodoCategoriesViewComponent(ITodoCatServices todoCatServices)
        {
            _todoCatServices = todoCatServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedCatId)
        {
            var todoCatList = await _todoCatServices.GetTodoCategoryListAsync();
            var todoCatViewModel = new TodoCatViewModel
            {
                SelectedCatId = selectedCatId,
                TodoCatList = todoCatList
            };
            return View(todoCatViewModel);
        }
    }
}
