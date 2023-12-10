using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flutterTodoAppApi.Data.Entities
{
    [Table("themes")]
    public class ThemeEO
    {
        [Key]
        public int Id { get; set; }
        public required string Color1 { get; set; }
        public required string Color2 { get; set; }
        public required string Color3 { get; set; }
        public int CreatedBy { get; set; }

        // Navigation Properties
        public virtual UserSettingEO Setting { get; set; }
    }
}
