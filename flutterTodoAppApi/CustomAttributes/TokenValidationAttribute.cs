using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using flutterTodoAppApi.Data.Contexts;
using flutterTodoAppApi.Services;
using flutterTodoAppApi.Data.Models;

namespace flutterTodoAppApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ValidateTokenAttribute : TypeFilterAttribute
    {
        public ValidateTokenAttribute() : base(typeof(ValidateTokenFilter))
        {
        }
    }

    public class ValidateTokenFilter(TodoAppContext context, JwtService jwt) : IAuthorizationFilter
    {
        private readonly TodoAppContext _context = context;
        private readonly JwtService _jwt = jwt;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claimExist = int.TryParse(_jwt.GetTokenClaim(AppCustomClaims.ConnectionId), out int connectionId);
            if (!claimExist) context.Result = new UnauthorizedResult();

            var connection = _context.Connections.FirstOrDefault(connection => connection.Id == connectionId);
            if(connection == null) context.Result = new UnauthorizedResult();
        }
    }
}
