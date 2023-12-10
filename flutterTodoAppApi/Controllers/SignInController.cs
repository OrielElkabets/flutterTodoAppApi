using flutterTodoAppApi.Data.Contexts;
using flutterTodoAppApi.Data.DTO;
using flutterTodoAppApi.Data.Entities;
using flutterTodoAppApi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace flutterTodoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController(TodoAppContext context) : ControllerBase
    {
        private readonly TodoAppContext _context = context;

        [HttpPost]
        async public Task<ActionResult<AppRes>> SignIn([FromBody] SignInRequestDTO userData)
        {
            if(await _context.Users.FirstOrDefaultAsync(user => user.UserName == userData.UserName) is not null)
            {
                return BadRequest(new AppRes {
                    StatusCode = AppStatusCodes.UserAlredyExists,
                    Message = $"User with the UserName {userData.UserName} alredy exist!"
                });
            }

            var newUser = new UserEO
            {
                UserName = userData.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(userData.Password),
                Setting = new UserSettingEO
                {
                    DisplayName = userData.DisplayName
                },
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new AppRes
            {
                StatusCode = AppStatusCodes.Success,
                Message = $"User created succesfuly!"
            });
        } 
    }
}
