using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace flutterTodoAppApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequiresClaimAttribute(string claimName, string claimValue) : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _claimName = claimName;
        private readonly string _claimValue = claimValue;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasClaim(_claimName, _claimValue))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
