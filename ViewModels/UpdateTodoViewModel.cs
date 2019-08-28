using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication.ViewModels
{
    public class UpdateTodoViewModel
    {
        public int ID { get; set; }
        public bool Complete { get; set; }

        public UpdateTodoViewModel(int id, bool complete)
        {
            this.ID = id;
            this.Complete = complete;
        }
        public UpdateTodoViewModel()
        {
        }
    }
}
