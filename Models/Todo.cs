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

        public string Name { get; set; }

        public bool Complete { get; set; } = false;

        public long UpdateDate { get; set; }

        public long CreateDate { get; set; }
    }
}
