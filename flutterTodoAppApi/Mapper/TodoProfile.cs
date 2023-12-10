using AutoMapper;
using flutterTodoAppApi.Data.DTO.Todo;
using flutterTodoAppApi.Data.Entities;

namespace flutterTodoAppApi.Mapper
{
    public class TodoProfile: Profile
    {
        public TodoProfile() { 
            CreateMap<TodoEO, NewTodoDTO>().ReverseMap();
            CreateMap<TodoEO, TodoDTO>().ReverseMap();
        }
    }
}
