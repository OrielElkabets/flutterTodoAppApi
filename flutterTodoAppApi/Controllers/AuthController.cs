using flutterTodoAppApi.Data.Contexts;
using flutterTodoAppApi.Data.DTO;
using flutterTodoAppApi.Data.Entities;
using flutterTodoAppApi.Data.Models;
using flutterTodoAppApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace flutterTodoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(TodoAppContext context, JwtService jwt) : ControllerBase
    {
        private readonly TodoAppContext _context = context;
        private readonly JwtService _jwt = jwt;

        [HttpPost("log-in")]
        async public Task<ActionResult<AppResData<string>>> LogIn([FromBody] LoginRequestDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == request.UserName);
            if (user == null)
            {
                return BadRequest(new AppResData<string>
                {
                    StatusCode = AppStatusCodes.UserNotFound,
                    Message = $"User with the name {request.UserName} not exist!"
                });
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest(new AppResData<string>
                {
                    StatusCode = AppStatusCodes.IncorrectPassword,
                    Message = $"Incorrect password!"
                });
            }

            var tokenExpirationDate = DateTime.Now.AddDays(7);

            var connection = new ConnectionEO { 
                User = user,
                Expiers = tokenExpirationDate,
                ConnectionName = "unnamed"
            };

            await _context.Connections.AddAsync(connection);
            await _context.SaveChangesAsync();

            return Ok(new AppResData<string>
            {
                StatusCode = AppStatusCodes.Success,
                Message = $"Authentication completed successfully",
                Data = _jwt.GenerateToken(tokenExpirationDate,
                    new Claim(AppCustomClaims.UserId, user.Id.ToString()),
                    new Claim(AppCustomClaims.ConnectionId, connection.Id.ToString()))
            });
        }
    }
}
