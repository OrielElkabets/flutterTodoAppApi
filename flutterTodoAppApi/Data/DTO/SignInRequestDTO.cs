namespace flutterTodoAppApi.Data.DTO
{
    public class SignInRequestDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string DisplayName { get; set; }
    }
}
