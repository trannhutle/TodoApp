using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Models;

namespace TodoApplication.ViewModels
{
    public class TodoListViewModel
    {
        public List<Todo> TodoList { get; set; }
        public int TodoCatId { get; set; }
        public int RemainingTasks { get; set; }
        public string TodoCatName { get; set; }
    }
}
