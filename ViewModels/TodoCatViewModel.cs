using System.Collections.Generic;
using TodoApplication.Models;

namespace TodoApplication.ViewModels
{
    public class TodoCatViewModel
    {
        public List<TodoCategory> TodoCatList { get; set; }
        public int SelectedCatId { get; set; }
    }
}
