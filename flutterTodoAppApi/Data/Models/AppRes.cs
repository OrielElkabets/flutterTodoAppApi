namespace flutterTodoAppApi.Data.Models
{
    public class AppRes
    {
        public AppStatusCodes StatusCode { get; set; }
        public string? Message { get; set; }
    }   
    public class AppResData<T> : AppRes
    {
        public T? Data { get; set; }
    }

    public enum AppStatusCodes
    {
        Success = 100,
        Error = 200,
        UserAlredyExists = 201,     // SignInError
        UserNotFound = 202,
        IncorrectPassword = 203,
        ItemNotFound = 204,
    }
}
