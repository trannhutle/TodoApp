using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Models;

namespace TodoApplication.Pages
{
    public class IndexModel : PageModel
    {  
        // Variable initialization

        private readonly TodoApplicationContext db;
        public IndexModel(TodoApplicationContext db) => this.db = db;
        public List<Todo> Todos { get; set; } = new List<Todo>();

        public async Task OnGetAsync()
        {
            Todos = await db.Todos.ToListAsync();
        }
    }
}
