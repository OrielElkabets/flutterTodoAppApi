using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flutterTodoAppApi.Data.Entities
{
    [Table("user_settings")]
    public class UserSettingEO
    {
        [Key]
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? ThemeId { get; set; }
        public required string DisplayName { get; set; }


        // Navigation Properties
        public virtual UserEO User { get; set; }
        public virtual ThemeEO Theme { get; set; }
    }
}
