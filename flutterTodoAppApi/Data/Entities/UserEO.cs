using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flutterTodoAppApi.Data.Entities
{
    [Table("users")]
    public class UserEO
    {
        [Key]
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }


        // Navigation Propeties
        public virtual UserSettingEO Setting { get; set; }
        public virtual ICollection<ConnectionEO> Connections { get; set; }
        public virtual ICollection<TodoEO> Todos { get; set; }
        public virtual ICollection<CheckListEO> CheckLists { get; set; }
    }
}
