using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flutterTodoAppApi.Data.Entities
{
    [Table("connections")]
    public class ConnectionEO
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Expiers { get; set; }
        public string? ConnectionName { get; set; }

        // Navigation Properties
        public virtual UserEO User { get; set; }
    }
}
