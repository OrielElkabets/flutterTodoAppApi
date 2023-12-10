using flutterTodoAppApi.Data.DTO.CheckList;
using flutterTodoAppApi.Data.DTO.Todo;

namespace flutterTodoAppApi.Data.DTO
{
    public class UserDataResDTO
    {
        public required IEnumerable<TodoDTO> Todos { get; set; }
        public required IEnumerable<CheckListDTO> CheckLists { get; set; }
    }
}
