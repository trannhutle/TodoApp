using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApplication.Models
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public int CatID { get; set; }

        public string Title { get; set; }

        public string Content { get; set;}

        public long AssignmentDate { get; set; }

        public long CreateDate { get; set; }
    }
}
