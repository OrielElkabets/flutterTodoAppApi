using AutoMapper;
using AutoMapper.Configuration.Conventions;
using flutterTodoAppApi.Data.Contexts;
using flutterTodoAppApi.Data.DTO;
using flutterTodoAppApi.Data.DTO.CheckList;
using flutterTodoAppApi.Data.DTO.Todo;
using flutterTodoAppApi.Data.Entities;
using flutterTodoAppApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace flutterTodoAppApi.Services
{
    public class UserService(TodoAppContext context, IMapper mapper, JwtService jwt)
    {
        private readonly TodoAppContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly JwtService _jwt = jwt;
        private int UserId => int.Parse(_jwt.GetTokenClaim(AppCustomClaims.UserId)!);

        public async Task<UserDataResDTO> GetUserAppData()
        {
            UserEO user = await _context.Users
                .AsNoTracking()
                .Include(user => user.Todos)
                .Include(user => user.CheckLists)
                .ThenInclude(list => list.CheckListItems)
                .FirstAsync(user => user.Id == UserId);

            return new UserDataResDTO
            {
                Todos = user.Todos.Select(_mapper.Map<TodoDTO>),
                CheckLists = user.CheckLists.Select(_mapper.Map<CheckListDTO>)
            };
        }

        public async Task<UserDataResDTO> SyncNewLocalData(SyncNewLocalDataReq data)
        {
            UserEO user = await _context.Users
                .Include(user => user.Todos)
                .Include(user => user.CheckLists)
                .ThenInclude(list => list.CheckListItems)
                .FirstAsync(user => user.Id == UserId);


            foreach (var todo in data.Todos)
            {
                user.Todos.Add(_mapper.Map<TodoEO>(todo));
            }


            foreach (var checkList in data.CheckLists)
            {
                user.CheckLists.Add(_mapper.Map<CheckListEO>(checkList));
            }

            await _context.SaveChangesAsync();

            return new UserDataResDTO
            {
                Todos = user.Todos.Select(_mapper.Map<TodoDTO>),
                CheckLists = user.CheckLists.Select(_mapper.Map<CheckListDTO>)
            };
        }

        public async Task<TodoDTO> AddTodo(NewTodoDTO newTodo)
        {
            var todo = _mapper.Map<TodoEO>(newTodo);
            todo.UserId = UserId;

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoDTO>(todo);
        }

        public async Task<bool> UpdateTodo(TodoDTO todo)
        {
            var todoEO = await _context.Todos.FindAsync(todo.Id);
            if (todoEO == null || todoEO.UserId != UserId) return false;

            todoEO.Title = todo.Title;
            todoEO.Body = todo.Body;
            todoEO.LastModified = todo.LastModified;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTodo(int todoId)
        {
            var todoEO = await _context.Todos.FindAsync(todoId);
            if (todoEO == null || todoEO.UserId != UserId) return false;

            _context.Remove(todoEO);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CheckListDTO> AddCheckList(CheckListDTO newCheckList)
        {
            var checkListEO = _mapper.Map<CheckListEO>(newCheckList);
            checkListEO.UserId = UserId;

            _context.CheckLists.Add(checkListEO);
            await _context.SaveChangesAsync();

            return _mapper.Map<CheckListDTO>(checkListEO);
        }

        public async Task<bool> UpdateCheckList(CheckListDTO checkList)
        {
            var checkListEO = await _context.CheckLists.Include(list => list.CheckListItems).FirstOrDefaultAsync(list => list.Id == checkList.Id);
            if (checkListEO == null || checkListEO.UserId != UserId) return false;

            _mapper.Map(checkList, checkListEO);
            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> ToggleCheckListItem(int checkListItemId)
        {
            var checkListItemEO = await _context.CheckListsItems.Include(item => item.CheckList).FirstOrDefaultAsync(item => item.Id == checkListItemId);
            if (checkListItemEO == null || checkListItemEO.CheckList.UserId != UserId) return false;

            checkListItemEO.Checked = !checkListItemEO.Checked;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCheckList(int checkListId)
        {
            var checkListEO = await _context.CheckLists.FindAsync(checkListId);
            if (checkListEO == null || checkListEO.UserId != UserId) return false;

            _context.Remove(checkListEO);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
