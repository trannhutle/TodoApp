using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication.ViewModels
{
    public class TodoViewModel
    {
        public List<int> SelectedTodos { get; set; }
        public int TodoCatId { get; set; }
        public TodoViewModel()
        {
        }
        public TodoViewModel(List<int> selectedTodos, int todoCatId)
        {
            SelectedTodos = selectedTodos;
            TodoCatId = todoCatId;
        }
    }
}
