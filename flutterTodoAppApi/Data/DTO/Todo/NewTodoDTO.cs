namespace flutterTodoAppApi.Data.DTO.Todo
{
    public class NewTodoDTO
    {
        public required DateTime CreationDate { get; set; }
        public required DateTime LastModified { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}
