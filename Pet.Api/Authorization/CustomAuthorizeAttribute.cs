using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Pet.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // If "azp" claim do not exists - reject
            var auth0AppIdClaim = user.Claims.FirstOrDefault(c => c.Type == "azp");
            if (auth0AppIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // If JWT produced through unauthorized Auth0 app - reject
            var services = context.HttpContext.RequestServices;
            var auth0Settings = services.GetService<AuthSettings>();
            if (auth0Settings.AllowedAppId != auth0AppIdClaim.Value)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }
    }
}
