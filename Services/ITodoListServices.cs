using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public interface ITodoListServices
    {
        Task<List<Todo>> GetTodoListAsync(int selectedCatId);
        List<Todo> GetTodoList(int selectedCatId);
        Todo AddNewTodo(Todo todoCategory);
        void DeleteTodo(int todoId);
        void UpdateTodo(int todoId, bool complete);
        void DeleteTodoByCatId(int selectedCatId);
    }
}
