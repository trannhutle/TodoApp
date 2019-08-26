using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication.Models
{
    public class Todo
    {
        public long ID { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string Content { get; set;}

        public long AssignmentDate { get; set; }

        public long CreateDate { get; set; }
    }
}
