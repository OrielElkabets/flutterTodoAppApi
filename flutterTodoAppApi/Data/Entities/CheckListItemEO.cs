using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flutterTodoAppApi.Data.Entities
{
    [Table("check_list_item")]
    public class CheckListItemEO
    {
        [Key]
        public int Id { get; set; }
        public int CheckListId { get; set; }
        public required string Text { get; set; }
        public bool Checked { get; set; }

        // Navigation Properties
        public virtual CheckListEO CheckList { get; set; }
    }
}
