using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public interface ITodoCatServices
    {
        Task<List<TodoCategory>> GetTodoCategoryListAsync();
        List<TodoCategory> GetTodoCategoryList();
        TodoCategory AddNewTodoCategory(TodoCategory todoCategory);
        TodoCategory FindById(int id);
        void DeleteById(int id);
    }
}
