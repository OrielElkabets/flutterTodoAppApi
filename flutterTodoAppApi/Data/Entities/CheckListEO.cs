using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flutterTodoAppApi.Data.Entities
{
    [Table("check_list")]
    public class CheckListEO
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModified { get; set; }
        public required string Title { get; set; }


        // Navigation Properties
        public virtual UserEO User { get; set; }
        public virtual ICollection<CheckListItemEO> CheckListItems { get; set; }
    }
}
