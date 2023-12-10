using flutterTodoAppApi.CustomAttributes;
using flutterTodoAppApi.Data.DTO;
using flutterTodoAppApi.Data.DTO.CheckList;
using flutterTodoAppApi.Data.DTO.Todo;
using flutterTodoAppApi.Data.Models;
using flutterTodoAppApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace flutterTodoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ValidateToken]
    public class UsersController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;


        [HttpGet("get/data")]
        public async Task<ActionResult<AppResData<UserDataResDTO>>> GetUserAppData()
        {
            return Ok(new AppResData<UserDataResDTO>
            {
                StatusCode = AppStatusCodes.Success,
                Message = "Receiving user data has been successfully completed",
                Data = await _userService.GetUserAppData()
            });
        }

        [HttpPost("sync-new-local-data")]
        public async Task<ActionResult<AppResData<UserDataResDTO>>> SyncNewLocalData([FromBody] SyncNewLocalDataReq req)
        {
            return Ok(new AppResData<UserDataResDTO>
            {
                StatusCode = AppStatusCodes.Success,
                Message = "Receiving user data has been successfully completed",
                Data = await _userService.SyncNewLocalData(req)
            });
        }

        [HttpPost("add/todo")]
        public async Task<ActionResult<AppResData<TodoDTO>>> AddTodo([FromBody] NewTodoDTO todo)
        {
            return Ok(new AppResData<TodoDTO>
            {
                StatusCode = AppStatusCodes.Success,
                Message = "todo added successfuly",
                Data = await _userService.AddTodo(todo)
            });
        }

        [HttpPut("update/todo")]
        public async Task<ActionResult<AppRes>> UpdateTodo([FromBody] TodoDTO todo)
        {
            if (await _userService.UpdateTodo(todo))
            {
                return Ok(new AppRes
                {
                    StatusCode = AppStatusCodes.Success,
                    Message = "todo updated successfuly",
                });
            }
            else
            {
                return NotFound(new AppRes
                {
                    StatusCode = AppStatusCodes.ItemNotFound,
                    Message = "todo not found",
                });
            }
        }

        [HttpDelete("delete/todo/{todoId}")]
        public async Task<ActionResult<AppRes>> DeleteTodo(int todoId)
        {
            if (await _userService.DeleteTodo(todoId))
            {
                return Ok(new AppRes
                {
                    StatusCode = AppStatusCodes.Success,
                    Message = "todo deleted successfuly",
                });
            }
            else
            {
                return NotFound(new AppRes
                {
                    StatusCode = AppStatusCodes.ItemNotFound,
                    Message = "todo not found",
                });
            }
        }

        [HttpPost("add/checklist")]
        public async Task<ActionResult<AppResData<CheckListDTO>>> AddCheckList([FromBody] CheckListDTO checkList)
        {
            return Ok(new AppResData<CheckListDTO>
            {
                StatusCode = AppStatusCodes.Success,
                Message = "checkList added successfuly",
                Data = await _userService.AddCheckList(checkList)
            });
        }

        [HttpPut("update/checklist")]
        public async Task<ActionResult<AppRes>> UpdateCheckList([FromBody] CheckListDTO checkList)
        {
            if (await _userService.UpdateCheckList(checkList))
            {
                return Ok(new AppRes
                {
                    StatusCode = AppStatusCodes.Success,
                    Message = "checkList updated successfuly",
                });
            }
            else
            {
                return NotFound(new AppRes
                {
                    StatusCode = AppStatusCodes.ItemNotFound,
                    Message = "checkList not found",
                });
            }
        }
        
        [HttpPut("update/toggle-checklist-item/{checkListItemId}")]
        public async Task<ActionResult<AppRes>> ToggleCheckListItem(int checkListItemId)
        {
            if (await _userService.ToggleCheckListItem(checkListItemId))
            {
                return Ok(new AppRes
                {
                    StatusCode = AppStatusCodes.Success,
                    Message = "checklist item updated successfuly",
                });
            }
            else
            {
                return NotFound(new AppRes
                {
                    StatusCode = AppStatusCodes.ItemNotFound,
                    Message = "checkList item not found",
                });
            }
        }

        [HttpDelete("delete/checklist/{checkListId}")]
        public async Task<ActionResult<AppRes>> DeleteCheckList(int checkListId)
        {
            if (await _userService.DeleteCheckList(checkListId))
            {
                return Ok(new AppRes
                {
                    StatusCode = AppStatusCodes.Success,
                    Message = "checklist deleted successfuly",
                });
            }
            else
            {
                return NotFound(new AppRes
                {
                    StatusCode = AppStatusCodes.ItemNotFound,
                    Message = "checklist not found",
                });
            }
        }
    }
}
