using flutterTodoAppApi.Data.DTO.CheckList;
using flutterTodoAppApi.Data.DTO.Todo;

namespace flutterTodoAppApi.Data.DTO
{
    public class SyncNewLocalDataReq
    {
        public required IEnumerable<NewTodoDTO> Todos { get; set; }
        public required IEnumerable<CheckListDTO> CheckLists { get; set; }
    }
}
