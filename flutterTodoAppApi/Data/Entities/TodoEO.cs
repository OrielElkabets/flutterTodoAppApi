using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flutterTodoAppApi.Data.Entities
{
    [Table("todos")]
    public class TodoEO
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }


        // Navigation Properties
        public virtual UserEO User { get; set; }
    }
}
